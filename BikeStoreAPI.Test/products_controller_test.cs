using AutoFixture;
using BikeStoresAPI.Controllers;
using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BikeStoreAPI.Test
{
    [TestClass]
    public class products_controller_test
    {
        private Mock<Iproducts_repository> _products_repository;
        private Fixture _fixture;
        private productsController _controller;

        public products_controller_test()
        {
            _fixture = new Fixture();
            _products_repository = new Mock<Iproducts_repository>();
        }

        [TestMethod]
        public async Task Get_products_returns_200()
        {
            var products_list = _fixture.CreateMany<products>(5).ToList();
            _products_repository.Setup(repo => repo.GetProducts()).ReturnsAsync(products_list);
            _controller = new productsController(_products_repository.Object);
            var result = await _controller.GetProducts();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task Get_products_throwException_500()
        {
            _products_repository.Setup(repo => repo.GetProducts()).Throws(new Exception());
            _controller = new productsController(_products_repository.Object);
            var result = await _controller.GetProducts();
            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj.StatusCode);
        }

        [TestMethod]
        public async Task Post_products_returns_201()
        {
            //var product = _fixture.Create<products>();
            //_products_repository.Setup(repo => repo.AddProduct(It.IsAny<products>())).ReturnsAsync(product);
            //_controller = new productsController(_products_repository.Object);
            //var result = await _controller.PostProduct(product);
            //var obj = result as ObjectResult;
            //Assert.AreEqual(201, obj.StatusCode);
        }

        [TestMethod]
        public async Task Put_products_returns_200()
        {
            //var product = _fixture.Create<products>();
            //_products_repository.Setup(repo => repo.UpdateProduct(It.IsAny<products>())).ReturnsAsync(product);
            //_controller = new productsController(_products_repository.Object);
            //var result = await _controller.PostProduct(product);
            //var obj = result as ObjectResult;
            //Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task Delete_products_returns_204()
        {
            //_products_repository.Setup(repo => repo.DeleteProduct(It.IsAny<int>())).Returns(true);
            //_controller = new productsController(_products_repository.Object);
            //var result = await _controller.DeleteProduct(It.IsAny<int>());
            //var obj = result as ObjectResult;
            //Assert.AreEqual(204, obj.StatusCode);
        }
    }
}