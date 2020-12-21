using Scene.Model;
using Scene.Src.Infra;
using Scene.Src.Model;
using Scene.Src.View;
using System;
using System.Diagnostics;
using System.Timers;

namespace Scene.Src.Controller
{
   public class LevelController : ControllerBase
    {
        public event Action OpenLobbyScreen = delegate { };

        private readonly LevelConfigStorage _levelConfigStorage;
        private LevelPopup _levelPopup;
        private Timer _btnTimer;
        private LevelConfigModel _levelConfig;
        private int _correctButtonId;
        private int _progressId = 0;
        private int _currentTimerSec;
        private Timer _lvlTimer;
        private const int WinProgressId = 4;
        private const int LooseProgressId = -4;

        public LevelController(ViewFactory viewFactory, RootSceneContainer sceneContainer, LevelConfigStorage levelConfigStorage, UserStorage userStorage) : base(sceneContainer, viewFactory, userStorage)
        {
            _levelConfigStorage = levelConfigStorage;
        }

        public void Start(int levelNumber)
        {
             _levelConfig = _levelConfigStorage.GetConfig(levelNumber);
            if (_levelConfig == null)
            {
                Debug.WriteLine($"Can not popup for start level:{levelNumber}");
                OpenLobbyScreen();
                return;
            }
            _levelPopup = _viewFactory.CreateView<LevelPopup>();
            _levelPopup.ShowCorrectLevelState(_levelConfig);
            _levelPopup.BackClicked += OnBackClicked;
            _levelPopup.TimerEnd += OnTimeEnd;
            _levelPopup.ActionButtonClicked += ActionButtonIsClickedHandler;
            _levelPopup.OkBtnClicked += OkBtnClicked;

            StartLvlTimer();

            _btnTimer = new Timer(_levelConfig.ButtonsActiveTimeSec * 1000);
            _btnTimer.AutoReset = false;
            _btnTimer.Elapsed += OnBtnTimerTik;

            ActivateButton();

            var view = _levelPopup.View;
            _rootScene.AddChild(view);
        }

        private void StartLvlTimer()
        {
            _currentTimerSec = _levelConfig.LevelTimeSec;
            _levelPopup.ShowCurrentTimer(_currentTimerSec);
            _lvlTimer = new Timer(1000);
            _lvlTimer.Elapsed += OnTimerTick;
            _lvlTimer.Start();
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)  
        {
            if (_currentTimerSec == 0)
            {
                OnTimeEnd();
                _lvlTimer.Stop();
            }
            else
            {
                _currentTimerSec--;
            }
            _levelPopup.ShowCurrentTimer(_currentTimerSec);
        }

        private void ActionButtonIsClickedHandler(int btnId)
        {
            _btnTimer.Stop();
            if (btnId == _correctButtonId)
            {
                _progressId++;
            }
            else
            {
                _progressId--;
            }
            UpdateState();
        }

        private void UpdateState()
        {
            _levelPopup.ProgressStates(_progressId);
            if (_progressId == WinProgressId)
            {
                OnWin();
            }
            else if (_progressId == LooseProgressId)
            {
                OnLoose();
            }
            else
            {
                ActivateButton();
            }
        }

        private void ActivateButton()
        {
            var random = new Random();
            _correctButtonId = random.Next(_levelConfig.ButtonsAmount);
            switch(_correctButtonId)
            {
                case 0:
                    _levelPopup.EnableButton0();
                    break;
                case 1:
                    _levelPopup.EnableButton1();
                    break;
                case 2:
                    _levelPopup.EnableButton2();
                    break;
                case 3:
                    _levelPopup.EnableButton3();
                    break;
            }
            _btnTimer.Start();
        }

        private void OnBtnTimerTik(object sender, ElapsedEventArgs e)
        {
           _progressId --;
            UpdateState();
        }

        private void OnLoose()
        {
            var userData = _userStorage.MyUserModel;
            if (userData.UserScore > 0)
            {
                userData.UserScore--;
            }
            _lvlTimer.Stop();
            _levelPopup.ShowTooltip("YOU LOSE");
        }

        private void OnWin()
        {
            var allUsersScores = _userStorage.AllUsersSortedByScore();
            var higestCurrentScore = allUsersScores[0].UserScore;
            _userStorage.MyUserModel.UserScore++;
            var myUserScore = _userStorage.MyUserModel.UserScore;
            _lvlTimer.Stop();
            if (higestCurrentScore < myUserScore)
            {
                _levelPopup.ShowTooltip("WOW! NEW RECORD!");
            }
            else
            {
                _levelPopup.ShowTooltip("YOU WIN");
            }
        }

        private void OnTimeEnd()
        {
            _lvlTimer.Stop();
            _levelPopup.ShowTooltip("TIME IS UP");
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
            if (_btnTimer != null)
            {
                _btnTimer.Elapsed -= OnBtnTimerTik;
                _btnTimer.Stop();
            }
            if(_lvlTimer != null)
            {
                _lvlTimer.Elapsed -= OnTimerTick;
                _lvlTimer.Stop();
            }
            if (_levelPopup != null)
            {
                _levelPopup.BackClicked -= OnBackClicked;
                _levelPopup.OkBtnClicked -= OkBtnClicked;
                _levelPopup.ActionButtonClicked -= ActionButtonIsClickedHandler;
                _levelPopup.TimerEnd -= OnTimeEnd;
                _levelPopup.Dispose();
            }
        }
    }
}
