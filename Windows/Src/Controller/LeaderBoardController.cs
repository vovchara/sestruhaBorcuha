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
            var allUsers = UserStorage.getInstance().AllUsers;
            var sortedUsers = allUsers.OrderByDescending(user => user.UserScore).ToArray();
            var result = new List<ScoreItemView>();
            for (int i = 0; i < sortedUsers.Length; i++)
            {
                var scoreItemView = new ScoreItemView(_game, sortedUsers[i], i+1);
                result.Add(scoreItemView);
            }
            return result.ToArray();
        }

        private void OnBackButtonClicked()
        {
            Dispose();
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
