﻿using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Alan_WarrenDesafio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllersBase<OrdersController>
    {
        private readonly IOrderAppService _orderAppService;

        public OrdersController(
            IOrderAppService orderAppService,
            ILogger<OrdersController> logger)
            : base(logger)
            => _orderAppService = orderAppService ?? throw new ArgumentNullException(nameof(orderAppService));

        [HttpGet("customerId/{id}")]
        public IActionResult GetAll(int customerId)
        {
            return SafeAction(() =>
            {
                var results = _orderAppService.GetAll(customerId);

                return !results.Any()
                    ? NotFound($"Orders not found for Customer Id: {customerId}")
                    : Ok(results);
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return SafeAction(() =>
            {
                var result = _orderAppService.Get(id);

                return result is null
                    ? NotFound($"Order not found for Id: {id}")
                    : Ok(result);
            });
        }
    }
}
