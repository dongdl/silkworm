using System.ComponentModel.Composition;
using System.Data.Entity;
using ATEC.Core.DataModel.UnitOfWork;
using ATEC.Core.Resolver;

namespace ATEC.Core.DataModel
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork,UnitOfWork.UnitOfWork>();
        }
    }
}
