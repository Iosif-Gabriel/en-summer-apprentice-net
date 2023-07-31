using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.Controllers;
using TicketManagement.Models.Dto;
using TicketManagement.Models;
using TicketManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Repositories;

namespace TicketManagement.TestUnit
{
    [TestClass]   
    public class OrderContollerTest
    {
        Mock<IOrderRepository> _orderRepositoryMoq;
        Mock<ITicketCategoryRepository> _ticketRepoMoq;
        List<OrderU> _moqList;
        Mock<IMapper> _mapperMoq;
        List<OrderDto> _dtoMoq;
        Mock<ILogger<OrderController>> _loggerMoq;
        private OrderController _controller;
        Mock<IUserRepository> _userRepositoryMoq;

        [TestInitialize]
        public void Setup()
        {
            _orderRepositoryMoq = new Mock<IOrderRepository>();
            _mapperMoq = new Mock<IMapper>();
            _ticketRepoMoq = new Mock<ITicketCategoryRepository>();
            _userRepositoryMoq=new Mock<IUserRepository>();
            _loggerMoq = new Mock<ILogger<OrderController>>();
            _controller = new OrderController(_orderRepositoryMoq.Object, _mapperMoq.Object, _ticketRepoMoq.Object,_loggerMoq.Object,_userRepositoryMoq.Object);
           
            }



        [TestMethod]
        public void GetAll_ReturnsListOfOrders()
        {
            // Arrange
            var orders = new List<OrderU>
            {
                new OrderU { IdOrder = 1, NumberOfTickets = 3, TotalPrice = 150 },
                new OrderU { IdOrder = 2, NumberOfTickets = 2, TotalPrice = 100 }
            };



            _orderRepositoryMoq.Setup(repo => repo.GetAll()).Returns(orders);
            _mapperMoq.Setup(mapper => mapper.Map<List<OrderDto>>(orders)).Returns(new List<OrderDto>
            {
                new OrderDto { IdOrder = 1, NumberOfTickets = 3, TotalPrice = 150 },
                new OrderDto { IdOrder = 2, NumberOfTickets = 2, TotalPrice = 100 }
            });



            var controller = _controller;


            // Act
            var result = controller.GetAll();



            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<OrderDto>));
            var dtoOrders = okResult.Value as List<OrderDto>;
            Assert.AreEqual(2, dtoOrders.Count);
            Assert.AreEqual(1, dtoOrders[0].IdOrder);
            Assert.AreEqual(2, dtoOrders[1].IdOrder);
        }



        [TestMethod]
        public async Task GetById_ExistingOrderId_ReturnsOrderDto()
        {
            // Arrange
            var orderId = 1;
            var order = new OrderU { IdOrder = orderId, NumberOfTickets = 2, TotalPrice = 100 };
            _orderRepositoryMoq.Setup(repo => repo.GetById(orderId)).ReturnsAsync(order);
            _mapperMoq.Setup(mapper => mapper.Map<OrderDto>(order)).Returns(new OrderDto { IdOrder = orderId, NumberOfTickets = 2, TotalPrice = 100 });



            var controller = _controller;


            // Act
            var result = await controller.GetById(orderId);



            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(OrderDto));
            var orderDto = okResult.Value as OrderDto;
            Assert.AreEqual(orderId, orderDto.IdOrder);
            Assert.AreEqual(2, orderDto.NumberOfTickets);
            Assert.AreEqual(100, orderDto.TotalPrice);
        }



        [TestMethod]
        public async Task GetById_NonExistentOrderId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _orderRepositoryMoq.Setup(repo => repo.GetById(nonExistentOrderId)).ReturnsAsync((OrderU)null);



            var controller = _controller;


            // Act
            var result = await controller.GetById(nonExistentOrderId);



            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }





        [TestMethod]
        public async Task Patch_NonExistentOrderId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistentOrderId = 999;
            var orderPatchDto = new OrderPatchDto { IdOrderPatch = nonExistentOrderId, NumberOfTicketsPatch = 3, IdTicketCategoryPatch = 1 };
            _orderRepositoryMoq.Setup(repo => repo.GetById(nonExistentOrderId)).ReturnsAsync((OrderU)null);



            var controller = _controller;

            // Act
            var result = await controller.Patch(orderPatchDto);



            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }




        [TestMethod]
        public async Task Delete_ExistingOrderId_ReturnsNoContentResult()
        {
            // Arrange
            var orderId = 1;
            var orderEntity = new OrderU { IdOrder = orderId, NumberOfTickets = 2, TotalPrice = 100 };
            _orderRepositoryMoq.Setup(repo => repo.GetById(orderId)).ReturnsAsync(orderEntity);



            var controller = _controller;

            // Act
            var result = await controller.Delete(orderId);



            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var noContentResult = result as NoContentResult;
            Assert.AreEqual(204, noContentResult.StatusCode);
        }



        [TestMethod]
        public async Task Delete_NonExistentOrderId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _orderRepositoryMoq.Setup(repo => repo.GetById(nonExistentOrderId)).ReturnsAsync((OrderU)null);



            var controller = _controller;
            // Act
            var result = await controller.Delete(nonExistentOrderId);



            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}

