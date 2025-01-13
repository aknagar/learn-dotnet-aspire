namespace Dapr.Workflow.AsyncApi.Models
{
    internal sealed record Transaction(decimal Value)
    {
        public Guid CustomerId { get; init; } = Guid.NewGuid();
    }
}
