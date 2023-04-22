using BusinessLogic.Validators;
using DTOs;
using FluentValidation.TestHelper;

namespace Tests.Validators
{
    [TestFixture]
    public class ClientValidatorTests
    {
        private ClientValidator _validator;


        [SetUp]
        public void Setup()
        {
            _validator = new ClientValidator();
        }

        [Test]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            // Arrange
            var client = new ClientDto { Name = "John Doe" };

            // Act
            var result = _validator.TestValidate(client);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            // Arrange
            var client = new ClientDto { Email = "invalidemail" };

            // Act
            var result = _validator.TestValidate(client);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

    }
}
