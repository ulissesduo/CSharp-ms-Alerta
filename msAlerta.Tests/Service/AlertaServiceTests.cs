using Moq;
using msAlerta.Entity;
using msAlerta.Repository;
using msAlerta.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msAlerta.Tests.Service
{
    public class AlertaServiceTests
    {
        [Fact]
        public async Task GetAllAlertas_ReturnsAllAlerts()
        {
            // Arrange
            var mockRepo = new Mock<IAlertaRepository>();
            mockRepo.Setup(repo => repo.ListaAlertas())
                    .ReturnsAsync(new List<Alerta>
                    {
                        new Alerta { Id = 1, Mensagem = "Test 1" },
                        new Alerta { Id = 2, Mensagem = "Test 2" }
                    });

            var service = new AlertaService(mockRepo.Object);

            // Act
            var result = await service.GetAllAlertas();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAlertas_ReturnAlertaById() 
        {
            var mockRepo = new Mock<IAlertaRepository>();
            var id = 99;
            mockRepo.Setup(repo => repo.AlertaById(id)).ReturnsAsync(new Alerta { Id = id });
            var service = new AlertaService(mockRepo.Object);
            var result = await service.GetAlertaById(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);


        }

        [Fact]
        public async Task GetAlertaById_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            var mockRepo = new Mock<IAlertaRepository>();
            int id = 1;

           
            mockRepo.Setup(repo => repo.AlertaById(id))
                    .ThrowsAsync(new Exception("Database failure"));

            var service = new AlertaService(mockRepo.Object);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetAlertaById(id));
        }

        [Fact]
        public async Task CreateAlert_ShouldReturnCreatedAlert() 
        {
            var mockRepo = new Mock<IAlertaRepository>();
            var newAlerta = new Alerta { Id = 1, Data_alerta = DateTime.Now, Id_licenca = 1, Enviado = 'S', Mensagem = "mymessage" };
            mockRepo.Setup(repo => repo.CreateAlerta(newAlerta)).ReturnsAsync(newAlerta);//check difference between return and not return
            var service = new AlertaService(mockRepo.Object);
            var result = await service.CreateAlert(newAlerta);
            Assert.NotNull(result);
            Assert.Equal("mymessage", result.Mensagem);
            Assert.Equal(1, result.Id);


        }

        [Fact]
        public async Task UpdateAlert_ShouldReturnUpdatedAlert() 
        {
            var mockRepo = new Mock<IAlertaRepository>();
            var id = 1;
            var updatedAlert = new Alerta { Id = id, Mensagem = "updated"};
            mockRepo.Setup(repo => repo.AlertaById(id)).ReturnsAsync(new Alerta { Id = id });

            mockRepo.Setup(repo => repo.UpdateAlerta(id, updatedAlert)).ReturnsAsync(updatedAlert);
            var service = new AlertaService(mockRepo.Object);
            var result = await service.UpdateAlert(id,updatedAlert);
            Assert.NotNull(result);
            Assert.Equal("updated", result.Mensagem);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeleteAlert_ShouldDeleteSuccessfully()
        {
            // Arrange
            var mockRepo = new Mock<IAlertaRepository>();
            var id = 1;

            mockRepo.Setup(repo => repo.DeleteAlerta(id)).ReturnsAsync(true);

            var service = new AlertaService(mockRepo.Object);

            // Act
            var result = await service.DeleteAlert(id);

            // Assert
            Assert.True(result);



        }



    }
}
