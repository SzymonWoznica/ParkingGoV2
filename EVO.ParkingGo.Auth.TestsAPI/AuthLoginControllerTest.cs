using ApplicationLayer.CQRS.Commands.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using EVO.ParkingGo.Controllers.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace EVO.ParkingGo.Auth.TestsAPI
{
    public class AuthLoginControllerTest
    {
        [Fact]
        public async Task Login_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange 
            var mediatorMock = new Mock<IMediator>();
            var loginRequest = new LoginRequestDto() { EmailAddress = "login@test.com", Password = "password" };
            var loginResponse = new LoginResponseDto() { 
                IsSuccessful = true, 
                };

            mediatorMock.Setup(m => m.Send(It.IsAny<AuthLoginCommand>(), default)).ReturnsAsync(loginResponse);

            var controller = new LoginController(mediatorMock.Object);

            
            // Act
            var result = await controller.Login(loginRequest);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, (int)okObjectResult.StatusCode);

            
            var actualResponse = okObjectResult.Value as LoginResponseDto;
            Assert.NotNull(actualResponse); 
            Assert.True(actualResponse.IsSuccessful); 

        }
    }
}