using System;
using System.Diagnostics;
using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Scene.Src.View;

namespace Scene.View
{
    public class WelcomePopup : PopupBase
    {
        public event Action<string> UserClickedStartGame = delegate { };

        private readonly BitmapTextNode _tittle;
        private readonly ButtonNode _centralButton;
        private readonly MultilineBitmapTextNode _inputNameTxt;
        private readonly ButtonNode _backButton;

        private string _userName;

        public WelcomePopup(Game game) : base(game, "welcome.bip", "sceneWelcomePopup4x3.object")
        {
            _tittle = View.FindById<BitmapTextNode>("TitleTxt");
            _centralButton = View.FindById<ButtonNode>("NewGameBtn");
            _inputNameTxt = View.FindById<MultilineBitmapTextNode>("inputFieldTxt");
            _backButton = View.FindById<ButtonNode>("backBtn");

            ShowPopup("showPopup");

            _backButton.Hidden = true;
            _tittle.TextLineRenderer.Text = "Borcuha";
            _centralButton.Clicked += OnCentralClicked;
        }

        private void NameValidation(string name)
        {
            var nameLenght = name.Length;
            if (nameLenght <= 3)
            {
            //    WrongNameEntered();
            }
            else
            {
                UserClickedStartGame(_userName);
            }
        }

        private void OnCentralClicked()
        {
            _userName = _inputNameTxt.TextRenderer.Text;
            NameValidation(_userName);
        }

        public override void Dispose()
        {
            base.Dispose();
            _centralButton.Clicked -= OnCentralClicked;
        }

    }
}