using Monosyne;
using Monosyne.Content;
using Monosyne.Events;
using Monosyne.Scene.V3;
using Monosyne.Utils;
using System.Diagnostics;

namespace Scene.Src.View
{
    public class PopupBase
    {
        protected readonly PackageContentManager _packageContentManager;
        private readonly string _resName;
        private readonly string _sceneName;
        public AbstractNode View { get; protected set; }

        protected PopupBase(Game game, string resName, string sceneName)
        {
            _resName = resName;
            _sceneName = sceneName;
            Debug.WriteLine($"Starting popup Res: {resName} Scene: {sceneName}");
            _packageContentManager = new PackageContentManager(game, game.Platform.FileSystem.AssetStorage.CreateBinaryPackage(resName, true));
            View = _packageContentManager.Load<AbstractNode>(sceneName);
        }

        protected virtual void ShowPopup(string showState)
        {
            View.PostToStateMachine(new ParamEvent<string>(showState));
        }
        public void Dispose()
        {
            Debug.WriteLine($"Killing Popup Res: {_resName} Scene: {_sceneName}");
            _packageContentManager.Dispose();
        }
    }
}
