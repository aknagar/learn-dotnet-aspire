using Dapr.Workflow.AsyncApi.Models;

namespace Dapr.Workflow.AsyncApi.Activities
{
    internal sealed class ProcessPaymentActivity : WorkflowActivity<Transaction, object?>
    {
        /// <summary>
        /// Override to implement async (non-blocking) workflow activity logic.
        /// </summary>
        /// <param name="context">Provides access to additional context for the current activity execution.</param>
        /// <param name="input">The deserialized activity input.</param>
        /// <returns>The output of the activity as a task.</returns>
        public override async Task<object?> RunAsync(WorkflowActivityContext context, Transaction input)
        {
            //Confirm payment with processor
            await Task.Delay(TimeSpan.FromSeconds(10));
            return null;
        }
    }
}
