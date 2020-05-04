using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Forum.Core;
using Forum.Core.Repositories;
using Forum.Persistence;
using Forum.Persistence.Repositories;
using Ninject;
using Ninject.Web.Common;

namespace Forum.DependencyResolution
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IPostRepository>().To<PostRepository>().InRequestScope();
            _kernel.Bind<IReplyRepository>().To<ReplyRepository>().InRequestScope();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
        }
    }
}