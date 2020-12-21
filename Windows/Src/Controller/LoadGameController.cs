using Scene.Model;
using Scene.Src.Infra;
using Scene.Src.Model;
using Scene.Src.View;
using System;

namespace Scene.Src.Controller
{
  public class LoadGameController : ControllerBase
    {
        public event Action CloseLoadGamePopup = delegate { };
        public event Action<UserModel> ContinueGameForUser = delegate { };
        private LoadGamePopup _loadGamePopup;

        public LoadGameController(ViewFactory viewFactory, RootSceneContainer sceneContainer, UserStorage userStorage) : base(sceneContainer, viewFactory, userStorage)
        {
        }

        public void Start()
        {
            _loadGamePopup = _viewFactory.CreateView<LoadGamePopup>();
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
