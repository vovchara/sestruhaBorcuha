using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Controller
{
  public class ControllerBase
    {
        protected readonly Game _game;
        protected readonly RenderStatesNode _rootScene;

        protected ControllerBase()
        {
            var rootStorage = RootStorage.getInstance();
            _game = rootStorage.Game;
            _rootScene = rootStorage.RootScene;
        }
    }
}
