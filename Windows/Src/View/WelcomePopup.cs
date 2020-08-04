using System;
using System.Threading.Tasks;
using Monosyne;
using Monosyne.Content;
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

        public WelcomePopup(Game game)
        {
            _packageContentManager = new PackageContentManager(game, game.Platform.FileSystem.AssetStorage.CreateBinaryPackage("packagev3.bip", true));
            View = _packageContentManager.Load<AbstractNode>("scene3x2.object");
            
            var centralButton = View.FindById<ButtonNode>("WheelBtn");
            centralButton.HoverPointerType = PointerType.Hand;
            centralButton.Clicked += () => Node_Clicked(centralButton.StateMachine);
        }
        
        private async void Node_Clicked(BaseState state)
        {
            state.SendEvent("btn_anim");
            await Task.Delay(5000);
            state.SendEvent("wheel_stop_in_sector13");
            await Task.Delay(3000);
            state.SendEvent("up");
        }
        
        public void Dispose()
        {
            _packageContentManager.Dispose();
        }
    }
}