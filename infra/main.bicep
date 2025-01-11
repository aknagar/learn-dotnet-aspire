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
/*
module apiKeyVaultAccess './core/security/keyvault-access.bicep' = {
  name: 'api-keyvault-access'
  scope: rg
  params: {
    keyVaultName: keyvault.outputs.name
    principalId: '<put your principal id here>'
  }
}
*/
