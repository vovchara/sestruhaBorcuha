using Scene.Model;
using Scene.Src.Infra;
using Scene.Src.View;
using System;

namespace Scene.Src.Controller
{
    public class LeaderBoardController : ControllerBase
    {
        public event Action CloseLeaderBoard = delegate { };
        private LeaderBoardPopup _leaderBoardPopup;

        public LeaderBoardController(ViewFactory viewFactory, RootSceneContainer sceneContainer, UserStorage userStorage) : base(sceneContainer, viewFactory, userStorage)
        {
        }

        public void Start()
        {
            _leaderBoardPopup = _viewFactory.CreateView<LeaderBoardPopup>();
            var view = _leaderBoardPopup.View;
            _rootScene.AddChild(view);
            _leaderBoardPopup.BackButtonClicked += OnBackButtonClicked;
        }

        private void OnBackButtonClicked()
        {
            CloseLeaderBoard();
        }

        public void Dispose()
        {
            if (_leaderBoardPopup != null)
            {
                _leaderBoardPopup.BackButtonClicked -= OnBackButtonClicked;
                _leaderBoardPopup.Dispose();
            }
        }
    }
}
