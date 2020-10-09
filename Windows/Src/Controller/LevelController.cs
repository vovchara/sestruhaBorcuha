using Scene.Src.Model;
using Scene.Src.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Controller
{
   public class LevelController : ControllerBase
    {
        public event Action OpenLobbyScreen = delegate { };
        private readonly int _levelToOpen;
        private LevelPopup _levelPopup;

        public LevelController(int levelNumber)
        {
            _levelToOpen = levelNumber;
        }

        public void Start()
        {
            var levelConfig = LevelConfigStorage.getInstance().GetConfig(_levelToOpen);
            if (levelConfig == null)
            {
                Debug.WriteLine($"Can not popup for start level:{_levelToOpen}");
                //back to lobby
                return;
            }

            _levelPopup = new LevelPopup(_game, levelConfig);
            _levelPopup.BackClicked += OnBackClicked;
            _levelPopup.TimerEnd += OnTimeEnd;
            _levelPopup.Win += OnWin;
            _levelPopup.Loose += OnLoose;

            var view = _levelPopup.View;
            _rootScene.AddChild(view);
        }

        private void OnLoose()
        {
        }

        private void OnWin()
        {
        }

        private void OnTimeEnd()
        {
            _levelPopup.ShowTooltip("TIME IS UP");
            _levelPopup.OkBtnClicked += OkBtnClicked;
            Debug.WriteLine("LEVEL TIME END!");
        }

        private void OkBtnClicked()
        {
            OpenLobbyScreen();
        }

        private void OnBackClicked()
        {
            OpenLobbyScreen();
        }

        public void Dispose()
        {
            if (_levelPopup != null)
            {
                _levelPopup.BackClicked -= OnBackClicked;
                _levelPopup.OkBtnClicked -= OkBtnClicked;
                _levelPopup.TimerEnd -= OnTimeEnd;
                _levelPopup.Win -= OnWin;
                _levelPopup.Loose -= OnLoose;
                _levelPopup.Dispose();
            }
        }
    }
}
