using Autofac;
using Scene.Src.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
