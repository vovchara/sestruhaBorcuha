using System;
using System.Threading.Tasks;
using Monosyne;
using Monosyne.Content;
using Monosyne.Events;
using Monosyne.Fsm;
using Monosyne.Input;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Monosyne.Utils;

namespace Scene.View
{
    public class WelcomePopup : IDisposable
    {
        private readonly PackageContentManager _packageContentManager;
        public AbstractNode View { get; }

        private readonly BitmapTextNode _tittle;
        private readonly ButtonNode _centralButton;
        private readonly MultilineBitmapTextNode _inputNameTxt;
        private readonly ButtonNode _backButton;

        public WelcomePopup(Game game)
        {
            _packageContentManager = new PackageContentManager(game, game.Platform.FileSystem.AssetStorage.CreateBinaryPackage("welcome.bip", true));
            View = _packageContentManager.Load<AbstractNode>("sceneWelcomePopup4x3.object");

            View.PostToStateMachine(new ParamEvent<string>("showPopup"));

            _tittle = View.FindById<BitmapTextNode>("TitleTxt");
            _centralButton = View.FindById<ButtonNode>("NewGameBtn");
            _inputNameTxt = View.FindById<MultilineBitmapTextNode>("inputFieldTxt");
            _backButton = View.FindById<ButtonNode>("backBtn");

            _backButton.Hidden = true;
            _tittle.TextLineRenderer.Text = "Borcuha";
            _centralButton.Clicked += OnCentralClicked;

            //var centralButton = View.FindById<ButtonNode>("WheelBtn");
            //centralButton.Clicked += () => Node_Clicked(centralButton.StateMachine);
        }

        private void OnCentralClicked()
        {
            _centralButton.Hidden = true;
            _tittle.TextLineRenderer.Text = $"Tebe zvaty {_inputNameTxt.TextRenderer.Text}";
        }

//        private async void Node_Clicked(BaseState state)
//        {
//            state.SendEvent("btn_anim");
//            await Task.Delay(5000);
//            state.SendEvent("wheel_stop_in_sector13");
//            await Task.Delay(3000);
//            state.SendEvent("up");
//        }
        
        public void Dispose()
        {
            _packageContentManager.Dispose();
        }
    }
}