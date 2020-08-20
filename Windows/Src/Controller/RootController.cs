using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;

namespace Scene.Controller
{
    public class RootController
    {
        private WelcomeController _welcomeController;
        private LobbyController _lobbyController;

        public RootController(Game game, RenderStatesNode rootSceneNode)
        {
            var rootStorage = RootStorage.getInstance();
            rootStorage.Game = game;
            rootStorage.RootScene = rootSceneNode;
        }

        public void Start()
        {
            _welcomeController = new WelcomeController();
            _welcomeController.StartNewGame += StartNewGameHandler;
            _welcomeController.Start();
        }

        private void StartNewGameHandler()
        {
            DisposeWelcomeIfNeeded();
            
            _lobbyController = new LobbyController();
            _lobbyController.Start();
            _lobbyController.OpenWelcomePopup += _lobbyController_OpenWelcomePopup;
        }

        private void _lobbyController_OpenWelcomePopup()
        {
            _lobbyController.Dispose();
            Start();
        }

        public void Dispose()
        {
            DisposeWelcomeIfNeeded();
            DisposeLobbyIfNeeded();
        }

        private void DisposeLobbyIfNeeded()
        {
            if (_lobbyController != null)
            {
                _lobbyController.OpenWelcomePopup -= _lobbyController_OpenWelcomePopup;
                _lobbyController.Dispose();
            }
        }

        private void DisposeWelcomeIfNeeded()
        {
            if (_welcomeController != null)
            {
                _welcomeController.StartNewGame -= StartNewGameHandler;
                _welcomeController.Dispose();
                _welcomeController = null;
            }
        }
    }
}