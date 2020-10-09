using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Scene.Src.Model;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Monosyne.Events;

namespace Scene.Src.View
{
    public class LevelPopup : PopupBase
    {
        public event Action BackClicked = delegate { };
        public event Action Win = delegate { };
        public event Action Loose = delegate { };
        public event Action TimerEnd = delegate { };
        public event Action OkBtnClicked = delegate { };

        private readonly BitmapTextNode _timerTxt;
        private readonly BitmapTextNode _titletxt;
        private readonly ButtonNode _backBtn;
        private readonly ControlNode _buttonOne;
        private readonly ControlNode _buttonTwo;
        private readonly ControlNode _tooltip;
        private readonly ButtonNode _popupOkButton;
        private readonly BitmapTextNode _popupOkBtnName;
        private readonly BitmapTextNode _popupTitle;

        private Timer _lvlTimer;
        private int _currentTimerSec; //30 //29 .. //1 //0
        private string _lvlId;

        public LevelPopup(Game game, LevelConfigModel config) : base(game, "armwrestling.bip", "sceneArmGame.object")
        {
            _timerTxt = View.FindById<BitmapTextNode>("timerTxt");
            _backBtn = View.FindById<ButtonNode>("backBtn");
            _buttonOne = View.FindById<ControlNode>("button_0");
            _buttonTwo = View.FindById<ControlNode>("button_1");
            _titletxt = View.FindById<BitmapTextNode>("titleTxt");
            _tooltip = View.FindById<ControlNode>("tooltip");
            _popupOkButton = View.FindById<ButtonNode>("okBtn");
            _popupOkBtnName = View.FindById<BitmapTextNode>("BtnName");
            _popupTitle = View.FindById<BitmapTextNode>("gameOverTxt");


            ShowPopup("showArmGame");

            StartTimer(config.LevelTimeSec);
            _lvlId = config.LevelId.ToString();
            _titletxt.TextLineRenderer.Text = $"Level {_lvlId}";
            _backBtn.Clicked += _backBtn_IsClicked;

        }

        private void _backBtn_IsClicked()
        {
            _backBtn.Clicked -= _backBtn_IsClicked;
            BackClicked();
        }

        private void StartTimer(int sec)
        {
            _currentTimerSec = sec;
            _timerTxt.TextLineRenderer.Text = sec.ToString();

            _lvlTimer = new Timer(1000);
            _lvlTimer.Elapsed += OnTimerTick;
            _lvlTimer.Start();
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            _currentTimerSec--;
            _timerTxt.TextLineRenderer.Text = _currentTimerSec.ToString();

            if (_currentTimerSec == 0)
            {
                TimerEnd();
                _lvlTimer.Stop();
            }
        }

        public void ShowTooltip(string text)
        {
            RunState("show", _tooltip);
            _popupOkBtnName.TextLineRenderer.Text = "OK";
            _popupTitle.TextLineRenderer.Text = text;
            _popupOkButton.Clicked += popupOkButton_Clicked;
        }

        private void popupOkButton_Clicked()
        {
            _popupOkButton.Clicked -= popupOkButton_Clicked;
            OkBtnClicked();
        }
    }
}
