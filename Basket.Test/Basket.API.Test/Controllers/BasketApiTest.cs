using System.Net;
using System.Threading.Tasks;
using Basket.API.Controllers;
using Basket.Core.Constants;
using Basket.Models.Request;
using Basket.Services.Abstract;
using Basket.Test.Basket.API.Test.Controllers.TestData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Dtos;
using Xunit;

namespace Basket.Test.Basket.API.Test.Controllers
{
    public class BasketApiTest
    {

        private readonly Mock<IBasketService> _mockRepo;

        private readonly BasketsController _controller;

        public BasketApiTest()
        {
            _mockRepo = new Mock<IBasketService>();
            _controller = new BasketsController(_mockRepo.Object);
        }

        [Theory]
        [ClassData(typeof(PostAddBasketItem_ValidBasketItem_ReturnResponseTrue))]
        public async Task PostAddBasketItem_ValidBasketItem_ReturnResponseTrue(string userId, BasketItemRequest basketItemRequest)
        {
            Response<bool> responseResult = new Response<bool>
            {
                IsSuccessfull = true,
                StatusCode = (int)HttpStatusCode.Created
            };
            _mockRepo.Setup(x => x.AddBasketItemAsync(It.IsAny<string>(), It.IsAny<BasketItemRequest>())).ReturnsAsync(responseResult);

            var result = await _controller.AddBasketItemAsync(userId, basketItemRequest);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnProduct = Assert.IsAssignableFrom<Response<bool>>(okResult.Value);
            Assert.Equal((int)HttpStatusCode.Created, returnProduct.StatusCode);
            Assert.True(returnProduct.IsSuccessfull);
            _mockRepo.Verify(x => x.AddBasketItemAsync(It.IsAny<string>(), It.IsAny<BasketItemRequest>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(PostAddBasketItem_NotEnoughStock_ReturnBadRequest))]
        public async Task PostAddBasketItem_NotEnoughStock_ReturnBadRequest(string userId, BasketItemRequest basketItemRequest)
        {
            Response<bool> responseResult = new Response<bool>
            {
                IsSuccessfull = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Errors = new() { BasketMessages.NotAddBasketMessage }
            };
            _mockRepo.Setup(x => x.AddBasketItemAsync(It.IsAny<string>(), It.IsAny<BasketItemRequest>())).ReturnsAsync(responseResult);

            var result = await _controller.AddBasketItemAsync(userId, basketItemRequest);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnProduct = Assert.IsAssignableFrom<Response<bool>>(okResult.Value);
            Assert.Equal((int)HttpStatusCode.BadRequest, returnProduct.StatusCode);
            Assert.Equal(responseResult.Errors, returnProduct.Errors);
            Assert.False(returnProduct.IsSuccessfull);
            _mockRepo.Verify(x => x.AddBasketItemAsync(It.IsAny<string>(), It.IsAny<BasketItemRequest>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(PutUpdateBasketItem_ValidUpdateBasketItem_ReturnResponseTrue))]
        public async Task PutUpdateBasketItem_ValidUpdateBasketItem_ReturnResponseTrue(string userId, string productId, int quantity)
        {
            Response<bool> responseResult = new Response<bool>
            {
                IsSuccessfull = true,
                StatusCode = (int)HttpStatusCode.NoContent
            };
            _mockRepo.Setup(x => x.UpdateBasketItemAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(responseResult);

            var result = await _controller.UpdateBasketItemAsync(userId, productId, quantity);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnProduct = Assert.IsAssignableFrom<Response<bool>>(okResult.Value);
            Assert.Equal((int)HttpStatusCode.NoContent, returnProduct.StatusCode);
            Assert.True(returnProduct.IsSuccessfull);
            _mockRepo.Verify(x => x.UpdateBasketItemAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(PutUpdateBasketItem_NotEnoughStock_ReturnBadRequest))]
        public async Task PutUpdateBasketItem_NotEnoughStock_ReturnBadRequest(string userId, string productId, int quantity)
        {
            Response<bool> responseResult = new Response<bool>
            {
                IsSuccessfull = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Errors = new()
                {
                    BasketMessages.NotAddBasketMessage
                }
            };
            _mockRepo.Setup(x => x.UpdateBasketItemAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(responseResult);

            var result = await _controller.UpdateBasketItemAsync(userId, productId, quantity);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnProduct = Assert.IsAssignableFrom<Response<bool>>(okResult.Value);
            Assert.Equal((int)HttpStatusCode.BadRequest, returnProduct.StatusCode);
            Assert.False(returnProduct.IsSuccessfull);
            _mockRepo.Verify(x => x.UpdateBasketItemAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }


    }
}
