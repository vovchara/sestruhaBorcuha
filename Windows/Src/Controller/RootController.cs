using Monosyne;
using Monosyne.Scene.V3;
using Scene.View;

namespace Scene.Controller
{
    public class RootController
    {
        private readonly Game _game;
        private readonly RenderStatesNode _root;

        private WelcomePopup _welcomePopup;

        public RootController(Game game, RenderStatesNode rootNode)
        {
            _game = game;
            _root = rootNode;
        }

        public void Start()
        {
            _welcomePopup = new WelcomePopup(_game);
            var view = _welcomePopup.View;

            _root.AddChild(view);
        }

        public void Dispose()
        {
            _welcomePopup?.Dispose();
        }
    }
}