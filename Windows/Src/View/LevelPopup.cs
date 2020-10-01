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

namespace Scene.Src.View
{
    public class LevelPopup : PopupBase
    {
        public event Action BackClicked = delegate { };
        public event Action Win = delegate { };
        public event Action Loose = delegate { };
        public event Action TimerEnd = delegate { };

        private readonly BitmapTextNode _timerTxt;
        private readonly ButtonNode _backBtn;
        private readonly ControlNode _buttonOne;
        private readonly ControlNode _buttonTwo;

        private Timer _lvlTimer;
        private int _currentTimerSec; //30 //29 .. //1 //0

        public LevelPopup(Game game, LevelConfigModel config) : base(game, "armwrestling.bip", "sceneArmGame.object")
        {
            _timerTxt = View.FindById<BitmapTextNode>("timerTxt");
            _backBtn = View.FindById<ButtonNode>("backBtn");
            _buttonOne = View.FindById<ControlNode>("button_0");
            _buttonTwo = View.FindById<ControlNode>("button_1");

            ShowPopup("showArmGame");

            StartTimer(config.LevelTimeSec);
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
    }
}
