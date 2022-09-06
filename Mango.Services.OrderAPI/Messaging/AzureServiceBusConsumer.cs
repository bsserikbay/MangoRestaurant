using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionCheckout;
        private readonly string checkoutMessageTopic;
        private readonly string orderpaymentprocesstopic;
        private readonly string orderUpdatePaymentResultTopic;

        private readonly OrderRepository _orderRepository;

        private ServiceBusProcessor _checkoutProcessor;
        private ServiceBusProcessor _orderUpdatePaymentStatusProcessor;

        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;
        public AzureServiceBusConsumer(
            OrderRepository orderRepository,
            IConfiguration configuration,
            IMessageBus messageBus
            )
        {
            _orderRepository = orderRepository;

            _configuration = configuration;
            _messageBus = messageBus;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionCheckout = _configuration.GetValue<string>("SubscriptionCheckout");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            orderpaymentprocesstopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            orderUpdatePaymentResultTopic = _configuration.GetValue<string>("OrderUpdatePaymentResultTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _checkoutProcessor = client.CreateProcessor(checkoutMessageTopic);
            _orderUpdatePaymentStatusProcessor = client.CreateProcessor(orderUpdatePaymentResultTopic, subscriptionCheckout);
        }
        public async Task Start()
        {
            try
            {
                _checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
                _checkoutProcessor.ProcessErrorAsync += ErrorHandler;
                await _checkoutProcessor.StartProcessingAsync();

                _orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
                _orderUpdatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
                await _orderUpdatePaymentStatusProcessor.StartProcessingAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
        public async Task Stop()
        {
            try
            {
                await _checkoutProcessor.StopProcessingAsync();
                await _checkoutProcessor.DisposeAsync();

                await _orderUpdatePaymentStatusProcessor.StopProcessingAsync();
                await _orderUpdatePaymentStatusProcessor.DisposeAsync();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDto checkoutHeaderDTO = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);


            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDTO.UserId,
                FirstName = checkoutHeaderDTO.FirstName,
                LastName = checkoutHeaderDTO.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDTO.CardNumber,
                CouponCode = checkoutHeaderDTO.CouponCode != null ? checkoutHeaderDTO.CouponCode : "",
                CVV = checkoutHeaderDTO.CVV,
                DiscountTotal = checkoutHeaderDTO.DiscountTotal,
                Email = checkoutHeaderDTO.Email,
                ExpiryMonthYear = checkoutHeaderDTO.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDTO.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDTO.Phone,
                PickupDateTime = checkoutHeaderDTO.PickupDateTime
            };
            foreach (var item in checkoutHeaderDTO.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Count = item.Count
                };
                orderHeader.CartTotalItems += item.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }
            await _orderRepository.AddOrder(orderHeader);

            PaymentRequestMessage paymentRequestMessage = new()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email = orderHeader.Email
            };

            try
            {
                await _messageBus.PublishMessage(paymentRequestMessage, orderpaymentprocesstopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);
            await _orderRepository.UpdateOrderPaymentStatus(paymentResultMessage.OrderId, paymentResultMessage.Status);
            await args.CompleteMessageAsync(args.Message);
        }
    }
}
