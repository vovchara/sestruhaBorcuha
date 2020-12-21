using System;
using System.Text.RegularExpressions;
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
        private readonly ButtonNode _leaderBoardButton;
        private readonly ButtonNode _loadGameButton;
        private readonly BitmapTextNode _errorTxt;
        private string _userName;

        public WelcomePopup(Game game) : base(game, "welcome.bip", "sceneWelcomePopup4x3.object")
        {
            _tittle = View.FindById<BitmapTextNode>("TitleTxt");
            _startNewGameButton = View.FindById<ButtonNode>("NewGameBtn");
             var newGameBtnText = _startNewGameButton.FindById<BitmapTextNode>("BtnName");
            newGameBtnText.TextLineRenderer.Text = "NEW GAME";
            _inputNameTxt = View.FindById<MultilineBitmapTextNode>("inputFieldTxt");
            _leaderBoardButton = View.FindById<ButtonNode>("leaderBoardBtn");
            _loadGameButton = View.FindById<ButtonNode>("loadGameBtn");
            var leaderBoardBtnText = _leaderBoardButton.FindById<BitmapTextNode>("BtnName");
            var loadGameBtnText = _loadGameButton.FindById<BitmapTextNode>("BtnName");
            _errorTxt = View.FindById<BitmapTextNode>("ErrorTxt");
            leaderBoardBtnText.TextLineRenderer.Text = "LEADERBOARD";
            loadGameBtnText.TextLineRenderer.Text = "LOAD GAME";

            ShowPopup("showPopup");

            _tittle.TransformModel.PositionX = 170;
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
            if(nameLenght <=3)
            {
                _errorTxt.TextLineRenderer.Text = "Name should contain at least 3 symbols!";
                _errorTxt.TransformModel.Color = Color.Red;
            }
            if(!regexItem.IsMatch(name))
            {
                _errorTxt.TextLineRenderer.Text = "Only latin symbols and numbers are supported!";
                _errorTxt.TransformModel.Color = Color.Red;
            }
            if (nameLenght > 3 && regexItem.IsMatch(name))
            {
                UserClickedStartGame(_userName);
            }
        }

        public void ExistedNameEntered()
        {
            _errorTxt.TextLineRenderer.Text = $"User with name {_userName} already exists!";
            _errorTxt.TransformModel.Color = Color.Red;
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