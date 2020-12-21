using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Scene.Src.Model;
using System;

namespace Scene.Src.View
{
   public class LoadGameItemView : PopupBase
    {
        private UserModel _user;
        private ButtonNode _loadButton;
        private BitmapTextNode _userName;
        private BitmapTextNode _userScore;

        public event Action <UserModel> OpenSavedGame = delegate { };

        public LoadGameItemView(Game game) : base(game, "leaderboard.bip", "sceneSavedGameItem.object")
        {
            _userScore = View.FindById<BitmapTextNode>("userScoreTxt");
            _userName = View.FindById<BitmapTextNode>("userNameTxt");
            _loadButton = View.FindById<ButtonNode>("loadGameBtn");
            var loadbtnTxt = View.FindById<BitmapTextNode>("BtnName");

            loadbtnTxt.TextLineRenderer.Text = "CONTINUE";
            _loadButton.Clicked += OnUserClickedLoadButton;
        }

        public void SetData(UserModel user)
        {
            _user = user;
            _userName.TextLineRenderer.Text = user.UserName;
            _userScore.TextLineRenderer.Text = user.UserScore.ToString();
        }

        private void OnUserClickedLoadButton()
        {
            OpenSavedGame(_user);
        }
        public override void Dispose()
        {
            base.Dispose();
            _loadButton.Clicked -= OnUserClickedLoadButton;
            if (View != null && !View.IsDisposed)
            {
                View.Dispose();
            }
        }
    }
}
