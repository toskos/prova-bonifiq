﻿using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{

    [ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
    {
		private readonly BaseService<Product> _productService;

		private readonly BaseService<Customer> _customerService;

		/// <summary>
		/// Precisamos fazer algumas alterações:
		/// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
		/// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
		/// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
		/// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
		/// 
		/// </summary>
		public Parte2Controller(BaseService<Product> productService, BaseService<Customer> customerService)
		{
            _productService = productService;
			_customerService = customerService;

		}
	
		[HttpGet("products")]
		public ResultRecord<Product> ListProducts(int page)
		{
			return _productService.List(page);
		}

		[HttpGet("customers")]
		public ResultRecord<Customer> ListCustomers(int page)
		{
			return _customerService.List(page);
        }
	}
}
