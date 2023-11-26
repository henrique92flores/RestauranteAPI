using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Data;
using Restaurante.Data.DTOs;
using Restaurante.Data.DTOs.Enum;
using Restaurante.Data.DTOs.Food;
using Restaurante.Model;
using System.Collections.Generic;

namespace Restaurante.Controllers
{
    [Route("Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private FoodContext _context;

        public OrdersController(FoodContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            if(orderDto.OrderItemDto.Count ==0)
                return NotFound();

            Order order = new Order();
             order.Items = new List<OrderItem>();


            if (orderDto == null || orderDto.OrderItemDto == null) return NotFound();

            foreach (var item in orderDto.OrderItemDto)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.Name = item.Name;
                orderItem.Price = item.Price;
                orderItem.Quantity = item.Quantity;
                order.Items.Add(orderItem);
            }
            order.status = orderDto.status;
            order.NomeCartao = orderDto.NomeCartao;
            order.NumeroCartao = orderDto.NumeroCartao;
            order.PaymentType = orderDto.PaymentType;
            order.Total = orderDto.Total;
            order.RestaurantId = (int)orderDto.RestaurantId;
            order.UserId = (int)orderDto.UserId;
            order.dateTime = DateTime.Now.ToUniversalTime();

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder),
            new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(order => order.Id == id);
            if (order == null) return NotFound();
            OrderDto orderDto = new OrderDto();

            orderDto.OrderItemDto = new List<OrderItemDto>();

            if (order == null || order.Items == null) return NotFound();

            foreach (var item in order.Items)
            {
                OrderItemDto orderItemDto = new OrderItemDto();
                orderItemDto.Id = item.Id;
                orderItemDto.Name = item.Name;
                orderItemDto.Price = item.Price;
                orderItemDto.Quantity = item.Quantity;
                orderDto.OrderItemDto.Add(orderItemDto);
            }
            orderDto.Id = order.Id;
            orderDto.Total = order.Total;
            orderDto.RestaurantId = order.RestaurantId;
            orderDto.UserId = order.UserId;
            orderDto.NumeroCartao = order.NumeroCartao;
            orderDto.NomeCartao = order.NomeCartao;
            orderDto.PaymentType = order.PaymentType;
            orderDto.status = order.status;
            orderDto.dateTime = order.dateTime;
            return Ok(orderDto);
        }

        [HttpGet("/Orders/Rest/{id}")]
        public IActionResult GetRestaurantOrders(int id, [FromQuery] DateTime dataInicial,
                                                         [FromQuery] DateTime dataFinal)
        {
            var orders = _context.Orders.Where(order => order.RestaurantId == id);
            orders = orders.Where(p => p.dateTime < dataFinal && p.dateTime > dataInicial);

            List<OrderDto> ListaOrderDto = new List<OrderDto>();

            if (orders == null) return NotFound();
            if (orders.Count() == 0) return NotFound();

            foreach (var item in orders)
            {
                OrderDto orderDto = new OrderDto();
                orderDto.Id = item.Id;
                orderDto.RestaurantId = item.RestaurantId;
                orderDto.UserId = item.UserId;
                orderDto.Total = item.Total;
                orderDto.NumeroCartao = item.NumeroCartao;
                orderDto.NomeCartao = item.NomeCartao;
                orderDto.PaymentType = item.PaymentType;
                orderDto.status = item.status;
                orderDto.dateTime = item.dateTime;
                ListaOrderDto.Add(orderDto);
            }


            return Ok(ListaOrderDto);
        }
        [HttpGet("/Orders/User/{id}")]
        public IActionResult GetUserOrders(int id, [FromQuery] DateTime dataInicial ,
[FromQuery] DateTime dataFinal)
        {
            var orders = _context.Orders.Where(order => order.UserId == id);
             orders = orders.Where(p => p.dateTime < dataFinal && p.dateTime > dataInicial);


            List<OrderDto> ListaOrderDto = new List<OrderDto>();

            if (orders == null) return NotFound();
            if (orders.Count() == 0) return NotFound();

            foreach (var item in orders)
            {
                OrderDto orderDto = new OrderDto();
                orderDto.Id = item.Id;
                orderDto.RestaurantId = item.RestaurantId;
                orderDto.UserId = item.UserId;
                orderDto.Total = item.Total;
                orderDto.NumeroCartao = item.NumeroCartao;
                orderDto.NomeCartao = item.NomeCartao;
                orderDto.PaymentType = item.PaymentType;
                orderDto.status = item.status;
                orderDto.dateTime = item.dateTime;
                ListaOrderDto.Add(orderDto);
            }

            return Ok(ListaOrderDto);
        }

        [HttpGet()]
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.Items).ToListAsync();
        }



        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id,[FromBody] OrderDto orderDto)
        {
            string erro = string.Empty; 
            var order = _context.Orders.FirstOrDefault(order => order.Id == id);
            if (order == null)
            {
                erro = "Ordem não encontrada";
                return BadRequest(erro);
            }

            if ((StatusOrder)order.status == StatusOrder.Pago)
            {
                erro = "Não se pode alterar uma Ordem que já foi paga";
                return BadRequest(erro);
            }

            if (order == null ||order.Items == null) 
                return NotFound();

            for (int i = 0; i > order.Items.Count; i++)
            {
                order.Items[i].Name = orderDto.OrderItemDto[i].Name;
                order.Items[i].Price = orderDto.OrderItemDto[i].Price;
                order.Items[i].Quantity = orderDto.OrderItemDto[i].Quantity;
            }
            order.status = orderDto.status;
            order.NomeCartao = orderDto.NomeCartao;
            order.NumeroCartao = orderDto.NumeroCartao;
            order.PaymentType = orderDto.PaymentType;
            order.Total = orderDto.Total;
            order.dateTime = DateTime.Now.ToUniversalTime();

            //_context.Orders.Add(order);
            _context.SaveChanges();

            return(Ok());

        }
    }
}
