using 'main.bicep'

param userPrincipalId = readEnvironmentVariable('AZURE_PRINCIPAL_ID', 'defaultPrincipalId')
param environmentName = readEnvironmentVariable('AZURE_ENV_NAME', 'local')
param location = readEnvironmentVariable('AZURE_LOCATION', 'westindia')

