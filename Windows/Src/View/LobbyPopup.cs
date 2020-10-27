using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using System;

namespace Scene.Src.View
{
    public class LobbyPopup : PopupBase
    {
        public event Action backBtnClicked = delegate { };
        public event Action<int> lvlBtnIsClicked = delegate { };

        private readonly WidgetNode _soundBtn;
        private readonly BitmapTextNode _nameTxt;
        private readonly BitmapTextNode _scoresTxt;
        private readonly ButtonNode _backBtn;
        private readonly ButtonNode _lvlOneBtn;
        private readonly ButtonNode _lvlTwoBtn;
        private readonly ButtonNode _lvlThreeBtn;
        private readonly ButtonNode _lvlFourBtn;
        private readonly ButtonNode _lvlFiveBtn;
        private readonly ButtonNode _lvlSixBtn;
        private readonly ButtonNode _lvlSevenBtn;
        private readonly ButtonNode _lvlEightBtn;

         public LobbyPopup(Game game, string userName, long userScore ) : base(game, "lobby.bip", "sceneLobbyPopup.object")
         {

            _soundBtn = View.FindById<WidgetNode>("SoundContainer");
            _nameTxt = View.FindById<BitmapTextNode>("NameTxt");
            _scoresTxt = View.FindById<BitmapTextNode>("ScoresTxt");
            _backBtn = View.FindById<ButtonNode>("backBtn");
            _lvlOneBtn = View.FindById<ButtonNode>("LevelOneBtn");
            _lvlTwoBtn = View.FindById<ButtonNode>("LevelTwoBtn");
            _lvlThreeBtn = View.FindById<ButtonNode>("LevelThreeBtn");
            _lvlFourBtn = View.FindById<ButtonNode>("LevelFourBtn");
            _lvlFiveBtn = View.FindById<ButtonNode>("LevelFiveBtn");
            _lvlSixBtn = View.FindById<ButtonNode>("LevelSixBtn");
            _lvlSevenBtn = View.FindById<ButtonNode>("LevelSevenBtn");
            _lvlEightBtn = View.FindById<ButtonNode>("LevelEightBtn");

            ShowPopup("showLobbyPopup");

            _lvlOneBtn.Hidden = true;
            _lvlTwoBtn.Hidden = true;
            _lvlThreeBtn.Hidden = true;
            _lvlFourBtn.Hidden = true;
            _lvlFiveBtn.Hidden = true;
            _lvlSixBtn.Hidden = true;
            _lvlSevenBtn.Hidden = true;
            _lvlEightBtn.Hidden = true;

            _nameTxt.TextLineRenderer.Text = userName;
            _scoresTxt.TextLineRenderer.Text = userScore.ToString();
            _backBtn.Clicked += () => backBtnClicked();
            _lvlOneBtn.Clicked += () => lvlBtnIsClicked(1);
            _lvlTwoBtn.Clicked += () => lvlBtnIsClicked(2);
            _lvlThreeBtn.Clicked += () => lvlBtnIsClicked(3);
            _lvlFourBtn.Clicked += () => lvlBtnIsClicked(4);
        }
        public void EnableLevel(int lvl)
        {
            switch(lvl)
            {
                case 1:
                    _lvlOneBtn.Hidden = false;
                    break;
                case 2:
                    _lvlTwoBtn.Hidden = false;
                    break;
                case 3:
                    _lvlThreeBtn.Hidden = false;
                    break;
                case 4:
                    _lvlFourBtn.Hidden = false;
                    break;
                case 5:
                    _lvlFiveBtn.Hidden = false;
                    break;
                case 6:
                    _lvlSixBtn.Hidden = false;
                    break;
                case 7:
                    _lvlSevenBtn.Hidden = false;
                    break;
                case 8:
                    _lvlEightBtn.Hidden = false;
                    break;
            }
        }
    }
}