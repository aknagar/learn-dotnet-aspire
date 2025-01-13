
using Dapr.Workflow.AsyncApi.Activities;
using Dapr.Workflow.AsyncApi.Models;

namespace Dapr.Workflow.AsyncApi.Workflows
{
    internal sealed class DemoWorkflow : Workflow<Transaction, bool>
    {
        /// <summary>
        /// Override to implement workflow logic.
        /// </summary>
        /// <param name="context">The workflow context.</param>
        /// <param name="input">The deserialized workflow input.</param>
        /// <returns>The output of the workflow as a task.</returns>
        public override async Task<bool> RunAsync(WorkflowContext context, Transaction input)
        {
            try
            {
                //Submit the transaction to the payment processor
                context.SetCustomStatus("Processing payment...");
                await context.CallActivityAsync(nameof(ProcessPaymentActivity), input);


                //Send the transaction details to the warehouse
                context.SetCustomStatus("Contacting warehouse...");
                await context.CallActivityAsync(nameof(NotifyWarehouseActivity), input);

                context.SetCustomStatus("Success!");
                return true;
            }
            catch
            {
                //If anything goes wrong, return false
                context.SetCustomStatus("Something went wrong");
                return false;
            }
        }
    }
}
