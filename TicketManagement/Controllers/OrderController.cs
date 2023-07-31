using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Models;
using TicketManagement.Models.Dto;
using TicketManagement.Repositories.Interfaces;

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
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRepository ticketCategoryRepository, ILogger<OrderController> logger, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
            _logger = logger;
            _userRepository = userRepository;   
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


        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderPostDto orderPostDto, int id)
        {


            var orderEntity = _mapper.Map<OrderU>(orderPostDto);
            orderEntity.IdUser = id;
            orderEntity.OrderedAt = DateTime.Now;
            orderEntity.IdTicketCategory = orderPostDto.IdTicketCategory;

            var ticketById = await _ticketCategoryRepository.GetById(orderPostDto.IdTicketCategory);
            double price = (double)(ticketById.Price * orderPostDto.NumberOfTickets);
            orderEntity.TotalPrice = price;

            _orderRepository.Add(orderEntity);

          
            return Ok(orderEntity);
        }
    }
}
