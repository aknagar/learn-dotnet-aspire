using Azure.Messaging.ServiceBus;
using DataEntities;
using DotNet.Testcontainers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopLite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ServiceBusSender _serviceBusSender;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="queueClient">Client to send messages to queue with</param>
        public OrdersController(ServiceBusClient serviceBusClient)
        {
            // Guard.NotNull(queueClient, nameof(queueClient));

            _serviceBusSender = serviceBusClient.CreateSender("orders");

        }

        /// <summary>
        ///     Create Order
        /// </summary>
        [HttpPost(Name = "Order_Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody, Required] Order order)
        {
            var rawOrder = JsonConvert.SerializeObject(order);
            var orderMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(rawOrder));
            await _serviceBusSender.SendMessageAsync(orderMessage);

            return Accepted();
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return "Hello !";
        }
    }
}
