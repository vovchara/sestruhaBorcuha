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
        private readonly int _levelToOpen = 1; //todo
        private LevelPopup _levelPopup;

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
            Debug.WriteLine("LEVEL TIME END!");
        }

        private void OnBackClicked()
        {
        }

        public void Dispose()
        {
            if (_levelPopup != null)
            {
                _levelPopup.BackClicked -= OnBackClicked;
                _levelPopup.TimerEnd -= OnTimeEnd;
                _levelPopup.Win -= OnWin;
                _levelPopup.Loose -= OnLoose;
                _levelPopup.Dispose();
            }
        }
    }
}
