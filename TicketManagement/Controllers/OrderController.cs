using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Models.Dto;
using TicketManagement.Repositories;

namespace TicketManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly ILogger _logger;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRepository ticketCategoryRepository, ILogger<EventController> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = _mapper.Map<List<OrderDto>>(orders);

            return Ok(dtoOrders);
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(@order);

            return Ok(orderDto);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.IdOrderPatch);
            
            if (orderEntity == null)
            {
                return NotFound();
            }
            var ticketByid = await _ticketCategoryRepository.GetById(orderPatch.IdTicketCategoryPatch);
            double price = (double)(ticketByid.Price * orderEntity.NumberOfTickets);
            orderEntity.TotalPrice = price;
            Console.WriteLine(price);
            _mapper.Map(orderPatch, orderEntity);
            _orderRepository.Update(orderEntity);
            
            return Ok(orderEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _orderRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(eventEntity);
            return NoContent();
        }
    }
}
