using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.View
{
    public class LevelPopup : PopupBase
    {
        private readonly BitmapTextNode _timerTxt;
        private readonly ButtonNode _backBtn;
        private readonly ControlNode _buttonOne;
        private readonly ControlNode _buttonTwo;
        public LevelPopup(Game game) : base(game, "armwrestling.bip", "sceneArmGame.object")
        {
            _timerTxt = View.FindById<BitmapTextNode>("timerTxt");
            _backBtn = View.FindById<ButtonNode>("backBtn");
            _buttonOne = View.FindById<ControlNode>("button_0");
            _buttonTwo = View.FindById<ControlNode>("button_1");

            ShowPopup("showArmGame");
        }
    }
}
