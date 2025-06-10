using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using Moq;
using msAlerta.Controllers;
using msAlerta.Dto;
using msAlerta.Entity;
using msAlerta.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msAlerta.Tests.Controller
{
    public class AlertaControllerTests
    {
        private readonly Mock<IAlertaService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AlertaController _controller;

        public AlertaControllerTests()
        {
            _mockService = new Mock<IAlertaService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AlertaController(_mockService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfAlertas()
        {
            // Arrange
            var alertas = new List<Alerta>
            {
                new Alerta { Id = 1, Mensagem = "Teste 1" },
                new Alerta { Id = 2, Mensagem = "Teste 2" }
            };

            _mockService.Setup(s => s.GetAllAlertas()).ReturnsAsync(alertas);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Alerta>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAlertAsync_ReturnsOk_WhenAlertaExists()
        {
            var alerta = new Alerta { Id = 1, Mensagem = "Alerta 1" };
            _mockService.Setup(s => s.GetAlertaById(1)).ReturnsAsync(alerta);

            var result = await _controller.GetAlertAsync(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Alerta>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetAlertAsync_ReturnsNotFound_WhenAlertaDoesNotExist()
        {
            _mockService.Setup(s => s.GetAlertaById(1)).ReturnsAsync((Alerta)null!);

            var result = await _controller.GetAlertAsync(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateAlertAsync_ReturnsCreatedResult_WhenValidDto()
        {
            var dto = new AlertaDto
            {
                Id_licenca = 10,
                Data_alerta = DateTime.Now,
                Mensagem = "Novo alerta",
                Enviado = 'E'
            };

            var createdAlerta = new Alerta
            {
                Id = 1,
                Id_licenca = dto.Id_licenca,
                Data_alerta = dto.Data_alerta,
                Mensagem = dto.Mensagem,
                Enviado = dto.Enviado
            };

            _mockService.Setup(s => s.CreateAlert(It.IsAny<Alerta>()))
                        .ReturnsAsync(createdAlerta);

            var result = await _controller.CreateAlertAsync(dto);

            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var returnValue = Assert.IsType<Alerta>(createdResult.Value);
            Assert.Equal("Novo alerta", returnValue.Mensagem);
        }

        [Fact]
        public async Task CreateAlertAsync_ReturnsBadRequest_WhenDtoIsNull()
        {
            var result = await _controller.CreateAlertAsync(null!);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid alert data", badRequest.Value);
        }

        [Fact]
        public async Task UpdateAlertAsync_ReturnsOk_WhenSuccessful()
        {
            var dto = new AlertaDto
            {
                Id_licenca = 5,
                Data_alerta = DateTime.Now,
                Mensagem = "Updated",
                Enviado = 'E'
            };

            var updated = new Alerta
            {
                Id = 2,
                Id_licenca = dto.Id_licenca,
                Data_alerta = dto.Data_alerta,
                Mensagem = dto.Mensagem,
                Enviado = dto.Enviado
            };

            _mockService.Setup(s => s.UpdateAlert(2, It.IsAny<Alerta>()))
                        .ReturnsAsync(updated);

            var result = await _controller.UpdateAlertAsync(2, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Alerta>(okResult.Value);
            Assert.Equal("Updated", returnValue.Mensagem);
        }

        [Fact]
        public async Task UpdateAlertAsync_ReturnsNotFound_WhenAlertNotFound()
        {
            var dto = new AlertaDto
            {
                Id_licenca = 1,
                Data_alerta = DateTime.Now,
                Mensagem = "Missing",
                Enviado = 'E'
            };

            _mockService.Setup(s => s.UpdateAlert(99, It.IsAny<Alerta>()))
                        .ReturnsAsync((Alerta)null!);

            var result = await _controller.UpdateAlertAsync(99, dto);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateAlertAsync_ReturnsBadRequest_WhenDtoIsNull()
        {
            var result = await _controller.UpdateAlertAsync(1, null!);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid alert data", badRequest.Value);
        }

        [Fact]
        public async Task DeleteAlertAsync_ReturnsNoContent_WhenSuccessful()
        {
            _mockService.Setup(s => s.DeleteAlert(1)).ReturnsAsync(true);

            var result = await _controller.DeleteAlertAsync(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAlertAsync_ReturnsNotFound_WhenNotSuccessful()
        {
            _mockService.Setup(s => s.DeleteAlert(1)).ReturnsAsync(false);

            var result = await _controller.DeleteAlertAsync(1);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
