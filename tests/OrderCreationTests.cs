﻿using Bogus;
using eShopLite.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    public class OrderCreationTests
    {
        
        [Fact]
        public async Task OrderGeneration()
        {
            Console.WriteLine("Let's queue some orders, how many do you want?");

            var requestedAmount = DetermineOrderAmount();
            await QueueOrders(requestedAmount);

            Console.WriteLine("That's it, see you later!");
        }

        private static async Task QueueOrders(int requestedAmount)
        {
            /*
            var serviceBusClient = new ServiceBusClient(ConnectionString);
            var serviceBusSender = serviceBusClient.CreateSender(QueueName);

            for (int currentOrderAmount = 0; currentOrderAmount < requestedAmount; currentOrderAmount++)
            {
                var order = GenerateOrder();
                var rawOrder = JsonConvert.SerializeObject(order);
                var orderMessage = new ServiceBusMessage(rawOrder);

                Console.WriteLine($"Queuing order {order.Id} - A {order.ArticleNumber} for {order.Customer.FirstName} {order.Customer.LastName}");
                await serviceBusSender.SendMessageAsync(orderMessage);
            }
            */
        }

        private static Order GenerateOrder()
        {
            /*
            var customerGenerator = new Faker<Customer>()
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName());

            var orderGenerator = new Faker<Order>()
                .RuleFor(u => u.Name, () => customerGenerator)
                .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                .RuleFor(u => u.Amount, f => f.Random.Int())
                .RuleFor(u => u.ArticleNumber, f => f.Commerce.Product());

            return orderGenerator.Generate();
            */
            return new Order();
        }

        private static int DetermineOrderAmount()
        {
            var rawAmount = Console.ReadLine();
            if (int.TryParse(rawAmount, out int amount))
            {
                return amount;
            }

            Console.WriteLine("That's not a valid amount, let's try that again");
            return DetermineOrderAmount();
        }
    }
}
