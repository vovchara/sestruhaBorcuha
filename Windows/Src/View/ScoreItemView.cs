using Monosyne;
using Monosyne.Scene.V3;
using Scene.Src.Model;
namespace Scene.Src.View
{
    public class ScoreItemView : PopupBase
    {
        private readonly BitmapTextNode _userScore;
        private readonly BitmapTextNode _userName;
        private readonly BitmapTextNode _userPlace;
        public ScoreItemView(Game game) : base(game, "leaderboard.bip", "sceneScoreBoardItem.object")
        {
            _userScore = View.FindById<BitmapTextNode>("userScoreTxt");
            _userName = View.FindById<BitmapTextNode>("userNameTxt");
            _userPlace = View.FindById<BitmapTextNode>("userPlaceTxt");
        }

        public void SetData(UserModel user, int position)
        {
            _userName.TextLineRenderer.Text = user.UserName;
            _userScore.TextLineRenderer.Text = user.UserScore.ToString();
            _userPlace.TextLineRenderer.Text = position.ToString();
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
