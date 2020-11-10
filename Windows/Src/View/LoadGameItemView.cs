using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Scene.Src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.View
{
   public class LoadGameItemView : PopupBase
    {
        private UserModel _user;

        public event Action <UserModel> OpenSavedGame = delegate { };

        public LoadGameItemView(Game game, UserModel user) : base(game, "leaderboard.bip", "sceneSavedGameItem.object")
        {
            var userScore = View.FindById<BitmapTextNode>("userScoreTxt");
            var userName = View.FindById<BitmapTextNode>("userNameTxt");
            var loadButton = View.FindById<ButtonNode>("loadGameBtn");
            var loadbtnTxt = View.FindById<BitmapTextNode>("BtnName");

            _user = user;
            userName.TextLineRenderer.Text = user.UserName;
            userScore.TextLineRenderer.Text = user.UserScore.ToString();
            loadbtnTxt.TextLineRenderer.Text = "CONTINUE";
            loadButton.Clicked += OnUserClickedLoadButton;
        }

        private void OnUserClickedLoadButton()
        {
            OpenSavedGame(_user);
        }
    }
}
