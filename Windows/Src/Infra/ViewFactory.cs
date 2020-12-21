using Autofac;
using Scene.Src.View;

namespace Scene.Src.Infra
{
   public class ViewFactory
    {
        private readonly ILifetimeScope _scope;

        public ViewFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public T CreateView<T>()
    where T : PopupBase
        {
            return _scope.Resolve<T>();
        }
    }
}
