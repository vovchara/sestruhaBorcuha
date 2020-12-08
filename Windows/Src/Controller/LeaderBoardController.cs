using Scene.Model;
using Scene.Src.Infra;
using Scene.Src.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Controller
{
    public class LeaderBoardController : ControllerBase
    {
        public event Action CloseLeaderBoard = delegate { };
        private LeaderBoardPopup _leaderBoardPopup;

        public void Start(ViewFactory viewFactory)
        {
            //  var savedUsers = GetSavedUsers();
            //  _leaderBoardPopup = new LeaderBoardPopup(_game, savedUsers);
            _leaderBoardPopup = viewFactory.CreateView<LeaderBoardPopup>();
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
