param location string = resourceGroup().location
param uniqueSeed string = '${resourceGroup().id}-${deployment().name}'

////////////////////////////////////////////////////////////////////////////////
// Infrastructure
////////////////////////////////////////////////////////////////////////////////

module keyVault 'modules/infra/keyvault.bicep' = {
  name: '${deployment().name}-infra-keyvault'
  params: {
    location: location
    uniqueSeed: uniqueSeed
  }
}
