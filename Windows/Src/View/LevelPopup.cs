﻿using Monosyne;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Scene.Src.Model;
using System;

namespace Scene.Src.View
{
    public class LevelPopup : PopupBase
    {
        public event Action BackClicked = delegate { };
        public event Action TimerEnd = delegate { };
        public event Action OkBtnClicked = delegate { };
        public event Action<int> ActionButtonClicked = delegate { };

        private readonly BitmapTextNode _timerTxt;
        private readonly BitmapTextNode _titletxt;
        private readonly ButtonNode _backBtn;
        private readonly ControlNode _buttonContainer;
        private readonly ButtonNode _buttonZero;
        private readonly ButtonNode _buttonOne;
        private readonly ControlNode _tooltip;
        private readonly ButtonNode _popupOkButton;
        private readonly BitmapTextNode _popupOkBtnName;
        private readonly BitmapTextNode _popupTitle;
        private readonly ControlNode _armStates;
        private readonly BitmapTextNode _zeroBtnText;
        private readonly BitmapTextNode _oneBtnText;
        private readonly BitmapTextNode _btnTwoText;
        private readonly BitmapTextNode _btnThreeText;
        private string _lvlId;
        public LevelPopup(Game game) : base(game, "armwrestling.bip", "sceneArmGame.object")
        {
            _timerTxt = View.FindById<BitmapTextNode>("timerTxt");
            _backBtn = View.FindById<ButtonNode>("backBtn");
            _buttonContainer = View.FindById<ControlNode>("ButtonsContainer");
            _buttonZero = View.FindById<ButtonNode>("button_0");
            _buttonOne = View.FindById<ButtonNode>("button_1");
            var buttonTwo = View.FindById<ButtonNode>("button_2");
            var buttonThree = View.FindById<ButtonNode>("button_3");
            _titletxt = View.FindById<BitmapTextNode>("titleTxt");
            _tooltip = View.FindById<ControlNode>("tooltip");
            _popupOkButton = View.FindById<ButtonNode>("okBtn");
            _popupOkBtnName = View.FindById<BitmapTextNode>("OkBtnName");
            _popupTitle = View.FindById<BitmapTextNode>("gameOverTxt");
            _armStates = View.FindById<ControlNode>("arm_states");
            _zeroBtnText = _buttonZero.FindById<BitmapTextNode>("BtnName");
            _oneBtnText = _buttonOne.FindById<BitmapTextNode>("BtnName");
            _btnTwoText = buttonTwo.FindById<BitmapTextNode>("BtnName");
            _btnThreeText = buttonThree.FindById<BitmapTextNode>("BtnName");

            ShowPopup("showArmGame");


            _titletxt.TextLineRenderer.Text = $"Level {_lvlId}";
            _backBtn.Clicked += BackBtn_IsClicked;
            _buttonZero.Clicked += () => ActionButtonClicked(0);
            _buttonOne.Clicked += () => ActionButtonClicked(1);
            buttonTwo.Clicked += () => ActionButtonClicked(2);
            buttonThree.Clicked += () => ActionButtonClicked(3);
        }

        public void ShowCorrectLevelState(LevelConfigModel config)
        {
            _lvlId = config.LevelId.ToString();
            if (config.ButtonsAmount == 2)
            {
                RunState("two_buttons", _buttonContainer);
            }
            else if (config.ButtonsAmount == 3)
            {
                RunState("three_buttons", _buttonContainer);
            }
            else
            {
                RunState("four_buttons", _buttonContainer);
            }
        }

        private void BackBtn_IsClicked()
        {
            _backBtn.Clicked -= BackBtn_IsClicked;
            BackClicked();
        }
        public void ShowCurrentTimer(int sec)
        {
            _timerTxt.TextLineRenderer.Text = sec.ToString();
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

        public void EnableButton0()
        {
            HideTextOnAllButtons();
            _zeroBtnText.TextLineRenderer.Text = "PRESS";
        }
        public void EnableButton1()
        {
            HideTextOnAllButtons();
            _oneBtnText.TextLineRenderer.Text = "PRESS";
        }
        public void EnableButton2()
        {
            HideTextOnAllButtons();
            _btnTwoText.TextLineRenderer.Text = "PRESS";
        }
        public void EnableButton3()
        {
            HideTextOnAllButtons();
            _btnThreeText.TextLineRenderer.Text = "PRESS";
        }

        private void HideTextOnAllButtons()
        {
            _zeroBtnText.TextLineRenderer.Text = "";
            _oneBtnText.TextLineRenderer.Text = "";
            _btnTwoText.TextLineRenderer.Text = "";
            _btnThreeText.TextLineRenderer.Text = "";
        }
        public void ProgressStates(int state)
        {
            switch(state)
            {
                case 1:
                    RunState("plusOne", _armStates);
                    break;
                case 2:
                    RunState("plusTwo", _armStates);
                    break;
                case 3:
                    RunState("plusThree", _armStates);
                    break;
                case 4:
                    RunState("win", _armStates);
                    break;
                case -1:
                    RunState("minusOne", _armStates);
                    break;
                case -2:
                    RunState("minusTwo", _armStates);
                    break;
                case -3:
                    RunState("minusThree", _armStates);
                    break;
                case -4:
                    RunState("lose", _armStates);
                    break;
                case 0:
                    RunState("startState", _armStates);
                    break;
            }
        }
    }
}
