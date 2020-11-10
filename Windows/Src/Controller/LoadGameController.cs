using Scene.Model;
using Scene.Src.Model;
using Scene.Src.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Controller
{
  public class LoadGameController : ControllerBase
    {
        public event Action CloseLoadGamePopup = delegate { };
        public event Action<UserModel> ContinueGameForUser = delegate { };
        private LoadGamePopup _loadGamePopup;

        public void Start()
        {
            var savedUsers = GetSavedUsers();
            _loadGamePopup = new LoadGamePopup(_game, savedUsers);
            _loadGamePopup.BackBtnIsClicked += OnBackBtnIsClicked;
            _loadGamePopup.LoadGameForUser += OnContinueBtnIsClicked;
            var view = _loadGamePopup.View;
            _rootScene.AddChild(view);
        }

        private void OnContinueBtnIsClicked(UserModel user)
        {
            ContinueGameForUser(user);
        }

        private LoadGameItemView [] GetSavedUsers()
        {
            var allUsers = UserStorage.getInstance().AllUsers;
            var sortedUsers = allUsers.OrderByDescending(user => user.UserScore).ToArray();
            var result = new List<LoadGameItemView>();
            for (int i = 0; i < sortedUsers.Length; i++)
            {
                var loadItemView = new LoadGameItemView(_game, sortedUsers[i]);
                result.Add(loadItemView);
            }
            return result.ToArray();
        }

        private void OnBackBtnIsClicked()
        {
            CloseLoadGamePopup();
        }

        public void Dispose()
        {
            if (_loadGamePopup != null)
            {
                _loadGamePopup.Dispose();
            }
        }

    }
}
