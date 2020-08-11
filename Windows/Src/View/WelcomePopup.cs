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
        public event Action<string> StartGameClicked = delegate { };

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
        }

        private void OnCentralClicked()
        {
            var inputedText = _inputNameTxt.TextRenderer.Text;
            if (string.IsNullOrEmpty(inputedText))
            {
                return;
            }

            StartGameClicked(inputedText);
        }
        
        public void Dispose()
        {
            if ((View.Parent as ContainerNode) != null)
            {
                ((ContainerNode) View.Parent).RemoveChild(View);
            }
            View.Dispose();
            _packageContentManager.Dispose();
        }
    }
}