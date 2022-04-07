using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmaManagment.Models;
using PharmaManagment.Models.Dtos;
using PharmaManagment.Repository;
using PharmaManagment.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PharmaManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPharmaRepository _repository;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly CalculateTotalPrice _calculateTotalPrice;

        public OrderController(IPharmaRepository repository, ILogger<OrderController> logger, IMapper mapper,CalculateTotalPrice calculateTotalPrice)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _calculateTotalPrice = calculateTotalPrice;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDetailDto>),StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrderDetailDto>>> GetAllOrder()
        {
            var orderItems=await _repository.GetAllOrders();            
            if (orderItems == null)
                return Ok(new List<OrderDetailDto>());
            foreach (var item in orderItems)
            {
                item.Customer = await _repository.GetCustomerById(item.CustomerId);
                item.Product = await _repository.GetProductsById(item.ProductId);
            }
            return Ok(_mapper.Map<List<OrderDetailDto>>(orderItems));
        }

        [HttpGet("{Id}",Name ="GetOrderById")]
        [ProducesResponseType(typeof(OrderDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<OrderDetailDto>>> GetOrderById(int Id)
        {
            var orderItem = await _repository.GetOrderById(Id);
            if (orderItem == null)
                return NotFound();
            orderItem.Customer = await _repository.GetCustomerById(orderItem.CustomerId);
            orderItem.Product = await _repository.GetProductsById(orderItem.ProductId);
            return Ok(_mapper.Map<OrderDetailDto>(orderItem));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDetailDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailDto>> Checkout(OrderDetailsCreateDto orderDto)
        {
            var order=_mapper.Map<OrderDetails>(orderDto);
            order = await _calculateTotalPrice.GetTotalPrice(order);
            
            var orderItem = await _repository.CreateOrder(order);
            
            if (orderItem == null)
                return BadRequest();
            
            orderItem.Customer = await _repository.GetCustomerById(orderItem.CustomerId);
            orderItem.Product = await _repository.GetProductsById(orderItem.ProductId);
            return CreatedAtAction(nameof(GetOrderById), new { Id = orderItem.CustomerId }, _mapper.Map<OrderDetailDto>(orderItem));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateOrder(OrderDetailsUpdateDto orderDto)
        {
            var order = _mapper.Map<OrderDetails>(orderDto);
            order = await _calculateTotalPrice.GetTotalPrice(order);
            var result = await _repository.UpdateOrder(order);
            if (!result )
                return BadRequest();
            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteOrder(int Id)
        {
            await _repository.DeleteOrder(Id);
            return Ok();
        }
    }
}
