using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Scene.Src.View;

namespace Scene.View
{
    public class WelcomePopup : PopupBase
    {
        public event Action<string> UserClickedStartGame = delegate { };
        public event Action UserClickedLeaderBoardButton = delegate { };
        public event Action UserClickedLoadGameButton = delegate { };

        private readonly BitmapTextNode _tittle;
        private readonly ButtonNode _startNewGameButton;
        private readonly MultilineBitmapTextNode _inputNameTxt;
        private readonly ButtonNode _backButton;
        private readonly ButtonNode _leaderBoardButton;
        private readonly ButtonNode _loadGameButton;

        private string _userName;

        public WelcomePopup(Game game) : base(game, "welcome.bip", "sceneWelcomePopup4x3.object")
        {
            _tittle = View.FindById<BitmapTextNode>("TitleTxt");
            _startNewGameButton = View.FindById<ButtonNode>("NewGameBtn");
             var newGameBtnText = _startNewGameButton.FindById<BitmapTextNode>("BtnName");
            newGameBtnText.TextLineRenderer.Text = "NEW GAME";
            _inputNameTxt = View.FindById<MultilineBitmapTextNode>("inputFieldTxt");
            _backButton = View.FindById<ButtonNode>("backBtn");
            _leaderBoardButton = View.FindById<ButtonNode>("leaderBoardBtn");
            _loadGameButton = View.FindById<ButtonNode>("loadGameBtn");
            var leaderBoardBtnText = _leaderBoardButton.FindById<BitmapTextNode>("BtnName");
            var loadGameBtnText = _loadGameButton.FindById<BitmapTextNode>("BtnName");
            leaderBoardBtnText.TextLineRenderer.Text = "LEADERBOARD";
            loadGameBtnText.TextLineRenderer.Text = "LOAD GAME";

            ShowPopup("showPopup");

            _backButton.Hidden = true;
            _tittle.TransformModel.PositionX = 200;
            _tittle.TextLineRenderer.Text = "Borcuha";
            _startNewGameButton.Clicked += OnNewGameClicked;
            _leaderBoardButton.Clicked += OnLeaderBoardButtonClicked;
            _loadGameButton.Clicked += OnLoadGameButtonClicked;
        }

        private void OnLoadGameButtonClicked()
        {
            UserClickedLoadGameButton();
        }

        private void OnLeaderBoardButtonClicked()
        {
            UserClickedLeaderBoardButton();
        }

        private void NameValidation(string name)
        {
            var nameLenght = name.Length;
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (nameLenght > 3 && regexItem.IsMatch(name))
            {
                UserClickedStartGame(_userName);
            }
            return;
        }

        private void OnNewGameClicked()
        {
            _userName = _inputNameTxt.TextRenderer.Text;
            NameValidation(_userName);
        }

        public override void Dispose()
        {
            base.Dispose();
            _startNewGameButton.Clicked -= OnNewGameClicked;
            _leaderBoardButton.Clicked -= OnLeaderBoardButtonClicked;
            _loadGameButton.Clicked -= OnLoadGameButtonClicked;
        }

    }
}