using Monosyne;
using Monosyne.Scene.V3;
using Scene.View;

namespace Scene.Controller
{
    public class RootController
    {
        private readonly Game _game;
        private readonly RenderStatesNode _rootScene;

        private WelcomePopup _welcomePopup;

        public RootController(Game game, RenderStatesNode rootSceneNode)
        {
            _game = game;
            _rootScene = rootSceneNode;
        }

        public void Start()
        {
            _welcomePopup = new WelcomePopup(_game);
            var view = _welcomePopup.View;

            _rootScene.AddChild(view);
        }

        public void Dispose()
        {
            _welcomePopup?.Dispose();
// the same           
//            if (_welcomePopup != null)
//            {
//                _welcomePopup.Dispose();
//            }
        }
    }
}