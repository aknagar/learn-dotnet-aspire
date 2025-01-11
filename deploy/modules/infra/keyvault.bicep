param uniqueSeed string
param location string
param vaultName string = 'kv-${uniqueString(uniqueSeed)}'
//param managedIdentityObjectId string

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' = {
  name: vaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: subscription().tenantId    
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: false
    enableSoftDelete: false
  }
}

output vaultName string = keyVault.name
