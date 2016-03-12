using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using DataModel;
using ATEC.Core.BusinessEntities;
using ATEC.Core.DataModel;
using ATEC.Core.DataModel.UnitOfWork;

namespace ATEC.Core.BusinessServices
{
    /// <summary>
    /// Offers services for product specific CRUD operations
    /// </summary>
    public class ProductServices:IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches product details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductEntity GetProductById(int productId)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product != null)
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productModel = Mapper.Map<Product, ProductEntity>(product);
                return productModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
                Mapper.CreateMap<Product, ProductEntity>();
                var productsModel = Mapper.Map<List<Product>, List<ProductEntity>>(products);
                return productsModel;
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        public int CreateProduct(ProductEntity productEntity)
        {
            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    Name = productEntity.ProductName
                };
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
                return product.Id;
            }
        }

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        public bool UpdateProduct(int productId, ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        product.Name = productEntity.ProductName;
                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProduct(int productId)
        {
            var success = false;
            if (productId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {

                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
