using Scene.Model;
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

        public void Start()
        {
           var savedUsers = GetSavedUsers();
            _leaderBoardPopup = new LeaderBoardPopup(_game, savedUsers);
            var view = _leaderBoardPopup.View;
            _rootScene.AddChild(view);
            _leaderBoardPopup.BackButtonClicked += OnBackButtonClicked;

        }

        private ScoreItemView[] GetSavedUsers()
        {
            var allUsers = UserStorage.getInstance().AllUsersSortedByScore();
            var result = new List<ScoreItemView>();
            for (int i = 0; i < allUsers.Length; i++)
            {
                var scoreItemView = new ScoreItemView(_game, allUsers[i], i+1);
                result.Add(scoreItemView);
            }
            return result.ToArray();
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
