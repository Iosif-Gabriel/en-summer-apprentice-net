using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;
using TicketManagement.Controllers;
using TicketManagement.Models;
using TicketManagement.Models.Dto;
using TicketManagement.Repositories.Interfaces;

namespace TicketManagement.TestUnit
{
    [TestClass]
    public class EventControllerTest
    {
        Mock<IEventRepository> _eventRepositoryMock;
        List<EventU> _moqList;
        Mock<IMapper> _mapperMoq;
        List<EventDto> _dtoMoq;
        Mock<ILogger<EventController>> _loggerMoq;
        private EventController _controller;

     
        [TestInitialize]
        public void SetupMoqData()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _mapperMoq = new Mock<IMapper>();
            _loggerMoq = new Mock<ILogger<EventController>>();
            _controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _loggerMoq.Object);
            _moqList = new List<EventU>

            {
                new EventU {Idevent = 1,
                    EventName = "Moq name",
                    DescriptionEvent = "Moq description",
                    EndDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    IdEventTypeNavigation = new EventType {IdEventType = 1,EventTypeName="test event type"},
                    IdEventType = 1,
                    IdVenueNavigation = new Venue {IdVenue = 1,VenueCapacity = 12, VenueLocation = "Mock location",VenueType = "mock type"},
                    IdVenue = 1
                }
            };
            _dtoMoq = new List<EventDto>
            {
                new EventDto
                {
                    EndDate = DateTime.Now,
                    EventDescription = "Moq description",
                    EventId = 1,
                    EventName = "Moq name",
                    StartDate = DateTime.Now,
                    Venue = new VenueDto {IdVenue = 1,VenueCapacity = 12, VenueLocation = "Mock location",VenueType = "mock type"}
                }
            };
        }


        [TestMethod]
        public async Task GetAllEventsReturnListOfEvents()
        {


            IReadOnlyList<EventU> moqEvents = _moqList;
            Task<IReadOnlyList<EventU>> moqTask = Task.Run(() => moqEvents);
            _eventRepositoryMock.Setup(moq => moq.GetAll());

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<EventDto>>(It.IsAny<IReadOnlyList<EventU>>())).Returns(_dtoMoq);

            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _loggerMoq.Object);


            var events = controller.GetAll();
            var eventResult = events.Result as OkObjectResult;
            var eventCount = eventResult.Value as IList;



            Assert.AreEqual(_moqList.Count, eventCount.Count);
        }


        [TestMethod]
        public async Task GetEventByIdReturnNotFoundWhenNoRecordFound()
        {
            // Setup mocks
            _eventRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Returns(Task.FromResult<EventU>(null));
            _mapperMoq.Setup(moq => moq.Map<EventDto>(It.IsAny<EventU>())).Returns((EventDto)null);

            // Initialize controller
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _loggerMoq.Object);


            var result = await controller.GetById(1);


            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetEventByIdReturnFirstRecord()
        {

            _eventRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<EventDto>(It.IsAny<EventU>())).Returns(_dtoMoq.First());
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _loggerMoq.Object);

            var result = await controller.GetById(1);
            var eventResult = result.Result as OkObjectResult;
            var eventCount = eventResult.Value as EventDto;



            Assert.IsFalse(string.IsNullOrEmpty(eventCount.EventName));
            Assert.AreEqual(1, eventCount.EventId);
        }

        [TestMethod]
        public async Task GetEventByIDThrowsAnException()
        {

            _eventRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Throws<Exception>();
            _mapperMoq.Setup(moq => moq.Map<EventDto>(It.IsAny<EventU>())).Returns(_dtoMoq.First());
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _loggerMoq.Object);


            await Assert.ThrowsExceptionAsync<Exception>(() => controller.GetById(1));
        }

        [TestMethod]
      
        public async Task Delete_ExistingEvent_DeletesEventAndReturnsNoContent()
        {
            // Arrange
            int eventId = 1;
            var existingEventEntity = new EventU { Idevent = eventId, EventName = "Existing Event", DescriptionEvent = "Event Description" };
            _eventRepositoryMock.Setup(m => m.GetById(eventId)).ReturnsAsync(existingEventEntity);

            // Act
            var result = await _controller.Delete(eventId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _eventRepositoryMock.Verify(m => m.Delete(existingEventEntity), Times.Once);
        }

        [TestMethod]
        public async Task Delete_NonExistingEvent_ReturnsNotFound()
        {
          
            int eventId = 999;
            _eventRepositoryMock.Setup(m => m.GetById(eventId)).ReturnsAsync((EventU)null);

          
            var result = await _controller.Delete(eventId);

           
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            _eventRepositoryMock.Verify(m => m.Delete(It.IsAny<EventU>()), Times.Never);
        }

    

    }
}