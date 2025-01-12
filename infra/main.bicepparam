using './main.bicep'

param securePassword = az.getSecret('<subscriptionId>', '<resourceGroupName>', '<keyVaultName>', '<secretName>')
