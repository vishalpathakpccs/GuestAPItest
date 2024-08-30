using Application.Commands.AddGuest;
using Application.Commands.AddPhone;
using Application.Queries.GetGuestById;
using Core.Entities;
using Core.Enum;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestAPITest
{
    public class GuestControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<GuestController>> _mockLogger;
        private readonly GuestController _controller;

        public GuestControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<GuestController>>();
            _controller = new GuestController(_mockMediator.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task AddGuest_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var command = new AddGuestCommand
            {
                Title = Title.Mr,
                Firstname = "VISHAL",
                Lastname = "PATHAK",
                BirthDate = DateTime.Now,
                Email = "TEST@TEST.com",
                PhoneNumbers = new List<string> { "1234567890" }
            };

            var validationResult = System.ComponentModel.DataAnnotations.ValidationResult.Success;
            var guest = new Guest { Id = Guid.NewGuid(), Firstname = "John", Lastname = "Doe" };

            _mockMediator.Setup(m => m.Send(command, default))
                .ReturnsAsync((validationResult, guest));

            // Act
            var result = await _controller.AddGuest(command) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(guest);
        }

        [Fact]
        public async Task AddGuest_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var command = new AddGuestCommand
            {
                Title = Title.Mr,
                Firstname = "",
                Lastname = "",
                BirthDate = DateTime.Now,
                Email = "TEST@TEST.com",
                PhoneNumbers = new List<string>()
            };

            var validationResult = new System.ComponentModel.DataAnnotations.ValidationResult("Firstname Firstname is required.PhoneNumbers At least one phone number must be provided.");

            _mockMediator.Setup(m => m.Send(command, default))
                .ReturnsAsync((validationResult, null));

            // Act
            var result = await _controller.AddGuest(command) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Firstname is required., At least one phone number must be provided.");
        }

        [Fact]
        public async Task AddPhone_ShouldReturnNoContent_WhenSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var phoneNumber = "0987654321";
            var command = new AddPhoneCommand { Id = id, PhoneNumber = phoneNumber };

            var validationResult = System.ComponentModel.DataAnnotations.ValidationResult.Success;

            _mockMediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.AddPhone(id, phoneNumber) as NoContentResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task AddPhone_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var id = Guid.NewGuid();
            var phoneNumber = "0987654321";
            var command = new AddPhoneCommand { Id = id, PhoneNumber = phoneNumber };

            var validationResult = new System.ComponentModel.DataAnnotations.ValidationResult( "Phone number is invalid.");

            _mockMediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.AddPhone(id, phoneNumber) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Phone number is invalid.");
        }

        [Fact]
        public async Task GetGuestById_ShouldReturnOk_WhenGuestExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new GetGuestByIdQuery { Id = id };
            var guest = new Guest { Id = id, Firstname = "John", Lastname = "Doe" };

            _mockMediator.Setup(m => m.Send(query, default))
                .ReturnsAsync(guest);

            // Act
            var result = await _controller.GetGuestById(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(guest);
        }

        [Fact]
        public async Task GetGuestById_ShouldReturnNotFound_WhenGuestDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new GetGuestByIdQuery { Id = id };

            _mockMediator.Setup(m => m.Send(query, default))
                .ReturnsAsync((Guest)null);

            // Act
            var result = await _controller.GetGuestById(id) as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetAllGuests_ShouldReturnOk_WhenGuestsExist()
        {
            // Arrange
            var query = new GetAllGuestsQuery();
            var guests = new List<Guest>
            {
                new Guest { Id = Guid.NewGuid(), Firstname = "John", Lastname = "Doe" }
            };

            _mockMediator.Setup(m => m.Send(query, default))
                .ReturnsAsync(guests);

            // Act
            var result = await _controller.GetAllGuests() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(guests);
        }
    }
}
