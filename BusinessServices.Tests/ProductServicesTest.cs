#region using namespaces.
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.GenericRepository;
using DataModel.UnitOfWork;
using Moq;
using NUnit.Framework;
using TestsHelper;

#endregion

namespace BusinessServices.Tests
{
    /// <summary>
    /// Product Service Test
    /// </summary>
    public class ProductServicesTest
    {
        #region Variables

        private IProductServices _productService;
        private IUnitOfWork _unitOfWork;
        private List<Product> _products;
        private GenericRepository<Product> _productRepository;
        private WebApiDbEntities _dbEntities;
        #endregion

        #region Test fixture setup

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [TestFixtureSetUp]
        public void Setup()
        {
            _products = SetUpProducts();
        }

        #endregion

        #region Setup

        /// <summary>
        /// Re-initializes test.
        /// </summary>
        [SetUp]
        public void ReInitializeTest()
        {
            _products = SetUpProducts();
            _dbEntities = new Mock<WebApiDbEntities>().Object;
            _productRepository = SetUpProductRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.ProductRepository).Returns(_productRepository);
            _unitOfWork = unitOfWork.Object;
            _productService = new ProductServices(_unitOfWork);
            
        }

        #endregion

        #region Private member methods

        /// <summary>
        /// Setup dummy repository
        /// </summary>
        /// <returns></returns>
        private GenericRepository<Product> SetUpProductRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Product>>(MockBehavior.Default, _dbEntities);

            // Setup mocking behavior
            mockRepo.Setup(p => p.GetAll()).Returns(_products);

            mockRepo.Setup(p => p.GetByID(It.IsAny<int>()))
                .Returns(new Func<int, Product>(
                             id => _products.Find(p => p.Id.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Product>())))
                .Callback(new Action<Product>(newProduct =>
                                                  {
                                                      dynamic maxProductID = _products.Last().Id;
                                                      dynamic nextProductID = maxProductID + 1;
                                                      newProduct.Id = nextProductID;
                                                      _products.Add(newProduct);
                                                  }));

            mockRepo.Setup(p => p.Update(It.IsAny<Product>()))
                .Callback(new Action<Product>(prod =>
                                                  {
                                                      var oldProduct = _products.Find(a => a.Id == prod.Id);
                                                      oldProduct = prod;
                                                  }));

            mockRepo.Setup(p => p.Delete(It.IsAny<Product>()))
                .Callback(new Action<Product>(prod =>
                                                  {
                                                      var productToRemove =
                                                          _products.Find(a => a.Id == prod.Id);

                                                      if (productToRemove != null)
                                                          _products.Remove(productToRemove);
                                                  }));

            // Return mock implementation object
            return mockRepo.Object;
        }

        /// <summary>
        /// Setup dummy products data
        /// </summary>
        /// <returns></returns>
        private static List<Product> SetUpProducts()
        {
            var prodId = new int();
            var products = DataInitializer.GetAllProducts();
            foreach (Product prod in products)
                prod.Id = ++prodId;
            return products;

        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Service should return all the products
        /// </summary>
        [Test]
        public void GetAllProductsTest()
        {
            var products = _productService.GetAllProducts();
            if (products != null)
            {
                var productList =
                    products.Select(
                        productEntity =>
                        new Product {Id = productEntity.Id, Name = productEntity.Name}).
                        ToList();
                var comparer = new ProductComparer();
                CollectionAssert.AreEqual(
                    productList.OrderBy(product => product, comparer),
                    _products.OrderBy(product => product, comparer), comparer);
            }
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [Test]
        public void GetAllProductsTestForNull()
        {
            _products.Clear();
            var products = _productService.GetAllProducts();
            Assert.Null(products);
            SetUpProducts();
        }

        /// <summary>
        /// Service should return product if correct id is supplied
        /// </summary>
        [Test]
        public void GetProductByRightIdTest()
        {
            var mobileProduct = _productService.GetProductById(2);
            if (mobileProduct != null)
            {
                Mapper.CreateMap<ProductEntity, Product>();
                var productModel = Mapper.Map<ProductEntity, Product>(mobileProduct);
                AssertObjects.PropertyValuesAreEquals(productModel,
                                                      _products.Find(a => a.Name.Contains("Mobile")));
            }
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [Test]
        public void GetProductByWrongIdTest()
        {
            var product = _productService.GetProductById(0);
            Assert.Null(product);
        }

        /// <summary>
        /// Add new product test
        /// </summary>
        [Test]
        public void AddNewProductTest()
        {
            var newProduct = new ProductEntity()
                                 {
                                     Name = "Android Phone"
                                 };

            var maxProductIDBeforeAdd = _products.Max(a => a.Id);
            newProduct.Id = maxProductIDBeforeAdd + 1;
            _productService.CreateProduct(newProduct);
            var addedproduct = new Product() {Name = newProduct.Name, Id = newProduct.Id};
            AssertObjects.PropertyValuesAreEquals(addedproduct, _products.Last());
            Assert.That(maxProductIDBeforeAdd + 1, Is.EqualTo(_products.Last().Id));
        }

        /// <summary>
        /// Update product test
        /// </summary>
        [Test]
        public void UpdateProductTest()
        {
            var firstProduct = _products.First();
            firstProduct.Name = "Laptop updated";
            var updatedProduct = new ProductEntity()
                                     {Name = firstProduct.Name, Id = firstProduct.Id};
            _productService.UpdateProduct(firstProduct.Id, updatedProduct);
            Assert.That(firstProduct.Id, Is.EqualTo(1)); // hasn't changed
            Assert.That(firstProduct.Name, Is.EqualTo("Laptop updated")); // Product name changed
        }

        /// <summary>
        /// Delete product test
        /// </summary>
        [Test]
        public void DeleteProductTest()
        {
            int maxID = _products.Max(a => a.Id); // Before removal
            var lastProduct = _products.Last();

            // Remove last Product
            _productService.DeleteProduct(lastProduct.Id);
            Assert.That(maxID, Is.GreaterThan(_products.Max(a => a.Id))); // Max id reduced by 1
        }

        #endregion


        #region Tear Down

        /// <summary>
        /// Tears down each test data
        /// </summary>
        [TearDown]
        public void DisposeTest()
        {
            _productService = null;
            _unitOfWork = null;
            _productRepository = null;
            if (_dbEntities != null)
                _dbEntities.Dispose();
            _products = null;
        }

        #endregion

        #region TestFixture TearDown.

        /// <summary>
        /// TestFixture teardown
        /// </summary>
        [TestFixtureTearDown]
        public void DisposeAllObjects()
        {
            _products = null;
        }

        #endregion
    }
}
