using ATEC.Core.DataModel;
using DataModel.GenericRepository;

namespace ATEC.Core.DataModel.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Properties
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<SysUser> UserRepository { get; }
        GenericRepository<Token> TokenRepository { get; } 
        #endregion
        
        #region Public methods
        /// <summary>
        /// Save method.
        /// </summary>
        void Save(); 
        #endregion
    }
}