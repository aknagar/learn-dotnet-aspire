param serviceBusNamespaceName string
param topicName string
param location string = resourceGroup().location

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-06-01-preview' existing = {
  name: serviceBusNamespaceName
}

resource serviceBusTopic 'Microsoft.ServiceBus/namespaces/topics@2021-06-01-preview' = {
  name: '${serviceBusNamespaceName}/${topicName}'
  location: location
  properties: {
    enablePartitioning: true
  }
}

output topicId string = serviceBusTopic.id
