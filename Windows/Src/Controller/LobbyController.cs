using System;
using Monosyne;
using Monosyne.Scene.V3;
using Scene.Storage;
using Scene.View;

namespace Scene.Controller
{
    public class LobbyController : IDisposable
    {
        private readonly Game _game;
        private readonly RenderStatesNode _rootScene;
        private LobbyPopup _lobbyPopup;

        public LobbyController()
        {
            _game = RootStorage.getInstance().Game;
            _rootScene = RootStorage.getInstance().RootScene;
        }

        public void Start()
        {
            var userModel = UserStorage.getInstance().User;
            if (userModel == null)
            {
                return;
            }

            _lobbyPopup = new LobbyPopup(_game, userModel);
            
            var view = _lobbyPopup.View;
            _rootScene.AddChild(view);
        }

        public void Dispose()
        {
            _lobbyPopup?.Dispose();
        }
    }
}