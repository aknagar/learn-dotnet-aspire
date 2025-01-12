targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the environment that can be used as part of naming resource convention')
param environmentName string

@minLength(1)
@description('Primary location for all resources')
param location string

param appName string = 'aspire'
param uniqueSeed string = uniqueString(environmentName, appName)

param userPrincipalId string

var abbrs = loadJsonContent('./abbreviations.json')
//var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))

var resourceToken = '${environmentName}-${uniqueSeed}'



// Tags that should be applied to all resources.
// 
// Note that 'azd-service-name' tags should be applied separately to service host resources.
// Example usage:
//   tags: union(tags, { 'azd-service-name': <service name in azure.yaml> })
var tags = {
  'azd-env-name': environmentName
}

resource rg 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: 'rg-${environmentName}'
  location: location
  tags: tags
}

////////////////////////////////////////////////////////////////////////////////
// Infrastructure
////////////////////////////////////////////////////////////////////////////////

// Create a user assigned identity
module userManagedIdentity './core//identity/user-assigned-identity.bicep' = {
  name: 'identity'
  scope: rg
  params: {
    name: 'umi-${environmentName}-${uniqueSeed}'
  }
}

// Create a key vault
module keyvault './core/security/keyvault.bicep' = {
  name: 'keyvault'
  params: {
    name: 'kv-${environmentName}-${uniqueSeed}'
    location: location
    tags: tags    
  }
  scope: rg
}

// Give the API access to KeyVault

module apiKeyVaultAccess './core/security/keyvault-access.bicep' = {
  name: 'api-keyvault-access'
  scope: rg
  params: {
    keyVaultName: keyvault.outputs.name
    principalId: userPrincipalId //userManagedIdentity.outputs.principalId
  }
}


module serviceBusResources './core/servicebus/servicebus.bicep' = {
  name: 'sb-resources'
  scope: rg
  params: {
    location: location
    tags: tags
    resourceToken: resourceToken
    skuName: 'Standard'
  }
}

// Get Service bus access
module serviceBusAccess './core/servicebus/servicebus-access.bicep' = {
  name: 'sb-access'
  scope: rg
  params: {
    location: location
    serviceBusName: serviceBusResources.outputs.serviceBusName
    managedIdentityName: '${abbrs.managedIdentityUserAssignedIdentities}${resourceToken}'
  }
}

// Create Service bus topic
module serviceBusTopic './core/servicebus/servicebus-topic.bicep' = {
  name: 'createServiceBusTopic'
  scope: rg
  params: {
    serviceBusNamespaceName: serviceBusResources.outputs.serviceBusName
    topicName: 'notifications'
  }
}
