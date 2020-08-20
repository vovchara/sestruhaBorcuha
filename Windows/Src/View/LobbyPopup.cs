using Monosyne;
using Monosyne.Content;
using Monosyne.Events;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Monosyne.Utils;
using Scene.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.View
{
    public class LobbyPopup : IDisposable
    {
        public event Action backBtnClicked = delegate { };

        private readonly PackageContentManager _packageContentManager; 
        public AbstractNode View { get; }

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

         public LobbyPopup(Game game, string userData)
         {
            _packageContentManager = new PackageContentManager(game, game.Platform.FileSystem.AssetStorage.CreateBinaryPackage("lobby.bip", true));
            View = _packageContentManager.Load<AbstractNode>("sceneLobbyPopup.object");

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

            View.PostToStateMachine(new ParamEvent<string>("showLobbyPopup"));

            _nameTxt.TextLineRenderer.Text = userData;
            _backBtn.Clicked += backBtn_isClicked;
        }

        private void backBtn_isClicked()
        {
            backBtnClicked();
        }

        public void Dispose()
        {
            _packageContentManager.Dispose();
        }
    }
}
