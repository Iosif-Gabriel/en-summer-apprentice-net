using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketManagement.Models.Dto;
using TicketManagement.Repositories.Interfaces;

namespace TicketManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public EventController(IEventRepository eventRepository, IMapper mapper, ILogger<EventController> logger)
        {
            _eventRepositoryMock = eventRepository;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepositoryMock.GetAll();

            var dtoEvents = _mapper.Map<List<EventDto>>(events);

            return Ok(dtoEvents);
        }


        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {

            var @event = await _eventRepositoryMock.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            var eventDto = _mapper.Map<EventDto>(@event);

            return Ok(eventDto);
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            var eventEntity = await _eventRepositoryMock.GetById(eventPatch.EventId);
            if (eventEntity == null)
            {
                return NotFound();
            }
            if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.EventName = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.DescriptionEvent = eventPatch.EventDescription;
            _eventRepositoryMock.Update(eventEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _eventRepositoryMock.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepositoryMock.Delete(eventEntity);
            return NoContent();
        }


     

    }

}