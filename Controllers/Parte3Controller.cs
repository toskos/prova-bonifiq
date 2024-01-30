﻿using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// </summary>
    [ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
        private readonly OrderService _orderService;
        public Parte3Controller(OrderService orderService)
        {
            _orderService = orderService;

        }

        [HttpGet("orders-paypal")]
		public async Task<Order> PlaceOrderPaypal(decimal paymentValue, int customerId)
		{
			return await _orderService.PayOrderPaypal(paymentValue, customerId);
        }

        [HttpGet("orders-pix")]
        public async Task<Order> PlaceOrderPix(decimal paymentValue, int customerId)
        {
            return await _orderService.PayOrderPix(paymentValue, customerId);
        }

        [HttpGet("orders-creditcard")]
        public async Task<Order> PlaceOrderCreditCard(decimal paymentValue, int customerId)
        {
            return await _orderService.PayOrderCreditCard(paymentValue, customerId);
        }
    }
}
