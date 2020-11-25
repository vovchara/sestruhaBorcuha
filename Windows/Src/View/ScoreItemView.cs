using Monosyne;
using Monosyne.Scene.V3;
using Scene.Src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.View
{
    public class ScoreItemView : PopupBase
    {
        public ScoreItemView(Game game, UserModel user, int position) : base(game, "leaderboard.bip", "sceneScoreBoardItem.object")
        {
            var userScore = View.FindById<BitmapTextNode>("userScoreTxt");
            var userName = View.FindById<BitmapTextNode>("userNameTxt");
            var userPlace = View.FindById<BitmapTextNode>("userPlaceTxt");

            userName.TextLineRenderer.Text = user.UserName;
            userScore.TextLineRenderer.Text = user.UserScore.ToString();
            userPlace.TextLineRenderer.Text = position.ToString();
        }
        public override void Dispose()
        {
            base.Dispose();
            if (View != null && !View.IsDisposed)
            {
                View.Dispose();
            }
        }

    }
}
