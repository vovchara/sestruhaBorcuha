using Scene.Model;
using Scene.Src.Infra;
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

        public void Start(ViewFactory viewFactory)
        {
            _loadGamePopup = viewFactory.CreateView<LoadGamePopup>();
         //   var savedUsers = GetSavedUsers();
          //  _loadGamePopup = new LoadGamePopup(_game, savedUsers);
            _loadGamePopup.BackBtnIsClicked += OnBackBtnIsClicked;
            _loadGamePopup.LoadGameForUser += OnContinueBtnIsClicked;
            var view = _loadGamePopup.View;
            _rootScene.AddChild(view);
        }

        private void OnContinueBtnIsClicked(UserModel user)
        {
            ContinueGameForUser(user);
        }

        private void OnBackBtnIsClicked()
        {
            CloseLoadGamePopup();
        }

        public void Dispose()
        {
            if (_loadGamePopup != null)
            {
                _loadGamePopup.BackBtnIsClicked -= OnBackBtnIsClicked;
                _loadGamePopup.LoadGameForUser -= OnContinueBtnIsClicked;
                _loadGamePopup.Dispose();
            }
        }

    }
}
