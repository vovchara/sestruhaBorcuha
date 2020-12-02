using Autofac;
using Scene.Src.Controller;

namespace Scene.Src.Infra
{
    public class ControllerFactory
    {
        private readonly ILifetimeScope _scope;

        public ControllerFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public T CreateController<T>()
            where T : ControllerBase
        {
            return _scope.Resolve<T>();
        }

        //public WelcomeController CreateControllerW()
        //{
        //    return _scope.Resolve<WelcomeController>();
        //}
    }
}
