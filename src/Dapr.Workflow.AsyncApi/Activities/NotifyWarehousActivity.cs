using System.Transactions;

namespace Dapr.Workflow.AsyncApi.Activities
{
    internal sealed class NotifyWarehouseActivity : WorkflowActivity<Transaction, object?>
    {
        /// <summary>
        /// Override to implement async (non-blocking) workflow activity logic.
        /// </summary>
        /// <param name="context">Provides access to additional context for the current activity execution.</param>
        /// <param name="input">The deserialized activity input.</param>
        /// <returns>The output of the activity as a task.</returns>
        public override async Task<object?> RunAsync(WorkflowActivityContext context, Transaction input)
        {
            //Contact the warehouse to ship the product
            await Task.Delay(TimeSpan.FromSeconds(8));
            return null;
        }
    }
}
