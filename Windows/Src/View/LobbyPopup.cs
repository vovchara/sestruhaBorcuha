using System;
using Monosyne;
using Monosyne.Content;
using Monosyne.Events;
using Monosyne.Scene.V3;
using Monosyne.Utils;
using Scene.Model;

namespace Scene.View
{
    public class LobbyPopup : IDisposable
    {
        public AbstractNode View { get; }

        private readonly PackageContentManager _packageContentManager;
        private readonly BitmapTextNode _nameTxt;

        public LobbyPopup(Game game, UserModel userModel)
        {
            _packageContentManager = new PackageContentManager(game, game.Platform.FileSystem.AssetStorage.CreateBinaryPackage("lobby.bip", true));
            View = _packageContentManager.Load<AbstractNode>("sceneLobbyPopup.object");

            View.PostToStateMachine(new ParamEvent<string>("showLobbyPopup"));
            
            _nameTxt = View.FindById<BitmapTextNode>("NameTxt");

            _nameTxt.TextLineRenderer.Text = userModel.Name;
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