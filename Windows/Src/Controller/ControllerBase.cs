using Monosyne.Scene.V3;
using Scene.Model;
using Scene.Src.Infra;

namespace Scene.Src.Controller
{
  public class ControllerBase
    {
        protected readonly RenderStatesNode _rootScene;
        protected readonly ViewFactory _viewFactory;
        protected readonly UserStorage _userStorage;

        protected ControllerBase(RootSceneContainer sceneContainer, ViewFactory viewFactory, UserStorage userStorage)
        {
            _rootScene = sceneContainer.RootScene;
            _viewFactory = viewFactory;
            _userStorage = userStorage;
        }
    }
}
