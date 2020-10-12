using Scene.Src.Model;
using Scene.Src.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Scene.Src.Controller
{
   public class LevelController : ControllerBase
    {
        public event Action OpenLobbyScreen = delegate { };
        private readonly int _levelToOpen;
        private LevelPopup _levelPopup;
    //    private Timer _btnTimer;
        private LevelConfigModel _levelConfig;
        private int _correctButtonId;
        private int _progressId = 0;

        public LevelController(int levelNumber)
        {
            _levelToOpen = levelNumber;
        }

        public void Start()
        {
             _levelConfig = LevelConfigStorage.getInstance().GetConfig(_levelToOpen);
            if (_levelConfig == null)
            {
                Debug.WriteLine($"Can not popup for start level:{_levelToOpen}");
                //back to lobby
                return;
            }

            _levelPopup = new LevelPopup(_game, _levelConfig);
            _levelPopup.BackClicked += OnBackClicked;
            _levelPopup.TimerEnd += OnTimeEnd;
            _levelPopup.Win += OnWin;
            _levelPopup.Loose += OnLoose;
            _levelPopup.ActionButtonClicked += ActionButtonIsClickedHandler;
            InitializeActionButtons();

            var view = _levelPopup.View;
            _rootScene.AddChild(view);
        }

        private void ActionButtonIsClickedHandler(int btnId)
        {
            if (btnId == _correctButtonId)
            {
                _progressId++;
            }
            else
            {
                _progressId--;
            }
            _levelPopup.ActionButtonClicked -= ActionButtonIsClickedHandler;
            InitializeActionButtons();
            _levelPopup.ProgressStates(_progressId, _correctButtonId);
            _levelPopup.ActionButtonClicked += ActionButtonIsClickedHandler;
            //визвати метод з вюшки
        }

        private void InitializeActionButtons()
        {
            ActivateButton();
          //  _btnTimer = new Timer(_levelConfig.ButtonsActiveTimeSec*1000);
          //  _btnTimer.Elapsed += OnBtnTimerTik;
          //  _btnTimer.Start();
        }

        private void ActivateButton()
        {
            var random = new Random();
            _correctButtonId = random.Next(2);
            if (_correctButtonId == 0)
            {
                _levelPopup.EnableButton0();
            }
            else
            {
                _levelPopup.EnableButton1();
            }
        }

    //    private void OnBtnTimerTik(object sender, ElapsedEventArgs e)
    //    {

    //    }

        private void OnLoose()
        {
            _levelPopup.ShowTooltip("YOU LOSE");
            _levelPopup.OkBtnClicked += OkBtnClicked;
        }

        private void OnWin()
        {
            _levelPopup.ShowTooltip("YOU WIN");
            _levelPopup.OkBtnClicked += OkBtnClicked;
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
                _levelPopup.ActionButtonClicked -= ActionButtonIsClickedHandler;
                _levelPopup.TimerEnd -= OnTimeEnd;
                _levelPopup.Win -= OnWin;
                _levelPopup.Loose -= OnLoose;
                _levelPopup.Dispose();
            }
        }
    }
}
