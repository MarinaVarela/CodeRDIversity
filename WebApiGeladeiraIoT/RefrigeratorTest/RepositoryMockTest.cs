using ApiRefrigerator.Models;
using Application.Services;
using Domain.Interfaces;
using Moq;

namespace TestRefrigerator
{
    public class RepositoryMockTest
    {
        private readonly Mock<IRefrigeratorRepository> _repositoryMock;
        private readonly RefrigeratorService _service;

        public RepositoryMockTest()
        {
            _repositoryMock = new Mock<IRefrigeratorRepository>();
            _service = new RefrigeratorService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnItems_WhenItemsExist()
        {
            // Arrange
            var items = new List<Refrigerator>
            {
                new() { Id = 1, Floor = 1, Container = 2, Position = 3, Name = "Item 1" },
                new() { Id = 2, Floor = 2, Container = 1, Position = 2, Name = "Item 2" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Item 1", result.First().Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnItem_WhenItemExists()
        {
            // Arrange
            var item = new Refrigerator { Id = 1, Floor = 1, Container = 2, Position = 3, Name = "Item 1" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(item.Id)).ReturnsAsync(item);

            // Act
            var result = await _service.GetByIdAsync(item.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(item.Id, result.Id);
            Assert.Equal(item.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenItemDoesNotExist()
        {
            // Arrange
            int nonExistentItemId = 99;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentItemId)).ReturnsAsync((Refrigerator?)null);

            // Act
            var result = await _service.GetByIdAsync(nonExistentItemId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task InsertItemAsync_ShouldThrowException_WhenItemAlreadyExists()
        {
            // Arrange
            var newItem = new Refrigerator { Id = 1, Floor = 1, Container = 1, Position = 1, Name = "Item" };
            _repositoryMock.Setup(repo => repo.GetByNameAsync(newItem.Name)).ReturnsAsync(newItem);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _service.InsertItemAsync(newItem));
        }

        [Fact]
        public async Task InsertItemAsync_ShouldAddItem_WhenItemIsNew()
        {
            // Arrange
            var newItem = new Refrigerator { Id = 1, Floor = 1, Container = 1, Position = 1, Name = "Item" };
            _repositoryMock.Setup(repo => repo.GetByNameAsync(newItem.Name)).ReturnsAsync((Refrigerator?)null);
            _repositoryMock.Setup(repo => repo.InsertItemAsync(newItem)).ReturnsAsync(newItem);

            // Act
            var result = await _service.InsertItemAsync(newItem);

            // Assert
            Assert.Equal(newItem, result);
        }

        [Fact]
        public async Task InsertItemsAsync_ShouldReturnItems_WhenSuccess()
        {
            // Arrange
            var items = new List<Refrigerator>
        {
            new() { Id = 1, Name = "Item 1", Floor = 1, Container = 1, Position = 1 },
            new() { Id = 2, Name = "Item 2", Floor = 2, Container = 2, Position = 2 }
        };

            _repositoryMock.Setup(repo => repo.InsertItemsAsync(It.IsAny<IEnumerable<Refrigerator>>()))
                .ReturnsAsync(items);

            // Act
            var result = await _service.InsertItemsAsync(items);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _repositoryMock.Verify(repo => repo.InsertItemsAsync(It.IsAny<IEnumerable<Refrigerator>>()), Times.Once);
        }

        [Fact]
        public async Task InsertItemsAsync_ShouldThrowException_WhenFailure()
        {
            // Arrange
            var items = new List<Refrigerator>
        {
            new() { Id = 1, Name = "Item 1", Floor = 1, Container = 1, Position = 1 },
            new() { Id = 2, Name = "Item 2", Floor = 2, Container = 2, Position = 2 }
        };

            _repositoryMock.Setup(repo => repo.InsertItemsAsync(It.IsAny<IEnumerable<Refrigerator>>()))
                .ReturnsAsync((IEnumerable<Refrigerator>?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _service.InsertItemsAsync(items));
            _repositoryMock.Verify(repo => repo.InsertItemsAsync(It.IsAny<IEnumerable<Refrigerator>>()), Times.Once);
        }

        [Fact]
        public async Task InsertItemAsync_ShouldThrowException_WhenInsertFails()
        {
            // Arrange
            var newItem = new Refrigerator { Id = 1, Floor = 1, Container = 1, Position = 1, Name = "New Item" };

            _repositoryMock.Setup(repo => repo.GetByNameAsync(newItem.Name)).ReturnsAsync((Refrigerator?)null);

            _repositoryMock.Setup(repo => repo.InsertItemAsync(newItem)).ReturnsAsync((Refrigerator?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _service.InsertItemAsync(newItem));
            Assert.Equal("Failed to add item.", exception.Message);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldThrowException_WhenItemDoesNotExist()
        {
            // Arrange
            var updateItem = new Refrigerator { Id = 1, Name = "Item" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(updateItem.Id)).ReturnsAsync((Refrigerator?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _service.UpdateItemAsync(updateItem));
        }

        [Fact]
        public async Task UpdateItemAsync_Should_Return_UpdatedItem_When_Successful()
        {
            // Arrange
            var itemToUpdate = new Refrigerator { Id = 1, Name = "Item", Floor = 2, Container = 2, Position = 3 };
            var existingItem = new Refrigerator { Id = 1, Name = "Item", Floor = 1, Container = 1, Position = 2 };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(itemToUpdate.Id))
                .ReturnsAsync(existingItem);
            _repositoryMock.Setup(repo => repo.UpdateItemAsync(itemToUpdate))
                .ReturnsAsync(itemToUpdate);

            // Act
            var result = await _service.UpdateItemAsync(itemToUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itemToUpdate.Name, result.Name);
            Assert.Equal(itemToUpdate.Floor, result.Floor);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(itemToUpdate.Id), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateItemAsync(itemToUpdate), Times.Once);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldThrowException_WhenUpdateFails()
        {
            // Arrange
            var existingItem = new Refrigerator { Id = 1, Floor = 1, Container = 1, Position = 1, Name = "Existing Item" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(existingItem.Id)).ReturnsAsync(existingItem);

            _repositoryMock.Setup(repo => repo.UpdateItemAsync(existingItem)).ReturnsAsync((Refrigerator?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _service.UpdateItemAsync(existingItem));
            Assert.Equal("Failed to update item.", exception.Message);
        }

        [Fact]
        public async Task RemoveItemAsync_ShouldThrowKeyNotFoundException_WhenItemDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.RemoveItemAsync(1)).ReturnsAsync((Refrigerator?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.RemoveItemAsync(1));
        }

        [Fact]
        public async Task RemoveItemAsync_ShouldRemoveItem_WhenItemExists()
        {
            // Arrange
            var item = new Refrigerator { Id = 1, Name = "Item" };
            _repositoryMock.Setup(repo => repo.RemoveItemAsync(item.Id)).ReturnsAsync(item);

            // Act
            var result = await _service.RemoveItemAsync(item.Id);

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public async Task RemoveAllAsync_ShouldReturnItemCount_WhenSuccess()
        {
            // Arrange
            var items = new List<Refrigerator>
            {
                new() { Id = 1, Name = "Item 1", Floor = 1, Container = 1, Position = 1 },
                new() { Id = 2, Name = "Item 2", Floor = 2, Container = 2, Position = 2 }
            };

            _repositoryMock.Setup(repo => repo.RemoveAllAsync()).ReturnsAsync(items.Count);

            // Act
            var result = await _service.RemoveAllAsync();

            // Assert
            Assert.Equal(2, result);
            _repositoryMock.Verify(repo => repo.RemoveAllAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveAllAsync_ShouldThrowException_WhenNoItemsToRemove()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.RemoveAllAsync()).ReturnsAsync(0);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _service.RemoveAllAsync());
            _repositoryMock.Verify(repo => repo.RemoveAllAsync(), Times.Once);
        }
    }
}
