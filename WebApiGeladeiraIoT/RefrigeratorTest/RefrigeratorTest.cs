using ApiRefrigerator.Models;
using Application.Services;
using Domain.Interfaces;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace TestRefrigerator
{
    public class RefrigeratorTest
    {
        [Fact]
        public void Refrigerator_Floor_ShouldFailValidation_WhenOutOfRange()
        {
            // Arrange
            var refrigerator = new Refrigerator
            {
                Id = 1,
                Floor = 4, // range (1-3)
                Container = 2,
                Position = 2,
                Name = "Item Test"
            };

            var context = new ValidationContext(refrigerator, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(refrigerator, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "The floor must be a value between 1 and 3.");
        }

        [Fact]
        public void Refrigerator_Name_ShouldFailValidation_WhenNull()
        {
            // Arrange
            var refrigerator = new Refrigerator
            {
                Id = 1,
                Floor = 2,
                Container = 2,
                Position = 2,
                Name = null!
            };

            var context = new ValidationContext(refrigerator, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(refrigerator, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "The name is mandatory.");
        }

        [Fact]
        public void Refrigerator_Name_ShouldFailValidation_WhenExceedsMaxLength()
        {
            // Arrange
            var refrigerator = new Refrigerator
            {
                Id = 1,
                Floor = 2,
                Container = 2,
                Position = 2,
                Name = new string('A', 101)
            };

            var context = new ValidationContext(refrigerator, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(refrigerator, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "The name must have a maximum of 100 characters.");
        }

        [Fact]
        public void Refrigerator_Container_ShouldFailValidation_WhenOutOfRange()
        {
            // Arrange
            var refrigerator = new Refrigerator
            {
                Id = 1,
                Floor = 1,
                Container = 5, // range (1-3)
                Position = 2,
                Name = "Item Test"
            };

            var context = new ValidationContext(refrigerator, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(refrigerator, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "The container must be a value between 1 and 3.");
        }

        [Fact]
        public void Refrigerator_Position_ShouldFailValidation_WhenOutOfRange()
        {
            // Arrange
            var refrigerator = new Refrigerator
            {
                Id = 1,
                Floor = 2,
                Container = 1,
                Position = 5, // range (1-4)
                Name = "Item Test"
            };

            var context = new ValidationContext(refrigerator, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(refrigerator, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "The position must be a value between 1 and 4.");
        }
    }
}

