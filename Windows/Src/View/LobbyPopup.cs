using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using System;

namespace Scene.Src.View
{
    public class LobbyPopup : PopupBase
    {
        public event Action backBtnClicked = delegate { };
        public event Action lvlOneBtnClicked = delegate { };

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

         public LobbyPopup(Game game, string userData) : base(game, "lobby.bip", "sceneLobbyPopup.object")
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

            _nameTxt.TextLineRenderer.Text = userData;
            _backBtn.Clicked += backBtn_isClicked;
            _lvlOneBtn.Clicked += lvlOneBtn_isClicked;
        }

        private void lvlOneBtn_isClicked()
        {
            lvlOneBtnClicked();
        }

        //incorrect
        //protected override void ShowPopup(string showState)
        //{
        //    View.PostToStateMachine(null);
        //}

        //correct
        //protected override void ShowPopup(string showState)
        //{
        //   base.ShowPopup(showState);
        //   View.PostToStateMachine(null);
        //}

        private void backBtn_isClicked()
        {
            backBtnClicked();
        }
    }
}
