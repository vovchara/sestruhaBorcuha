using Monosyne;
using Monosyne.Scene.V3.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.View
{
    public class LeaderBoardPopup : PopupBase
    {
        public event Action BackButtonClicked = delegate { };
        private ButtonNode _backButton;
        private WidgetNode _itemContainer;
        private ButtonNode _leftArrow;
        private ButtonNode _rightArrow;
        private readonly ScoreItemView[] _scoreItemViews;
        private readonly int _scoreItemsCount;
        private List<int> _visibleItemsIds;
        private int _maxItemsInContainer;

        public LeaderBoardPopup(Game game, ScoreItemView[] scoreItemViews ) : base(game, "leaderboard.bip", "sceneBoard.object")
        {
            _scoreItemViews = scoreItemViews;
            _scoreItemsCount = _scoreItemViews.Length;
            _backButton = View.FindById<ButtonNode>("backBtn");
            _itemContainer = View.FindById<WidgetNode>("item_container");
            _leftArrow = View.FindById<ButtonNode>("leftBtn");
            _rightArrow = View.FindById<ButtonNode>("rightBtn");
            ShowScoreItems(0);
            ShowPopup("showBoardPopup");
            _backButton.Clicked += OnBackButtonClicked;
            _rightArrow.Clicked += NextClicked;
            _leftArrow.Clicked += PreviousClicked;
            _leftArrow.Hidden = true;
            _rightArrow.Hidden = true;
        }

        private void PreviousClicked()
        {
            _rightArrow.Hidden = false;
            var firstVisibleIndex = _visibleItemsIds.FirstOrDefault();
            if (firstVisibleIndex == 0)
            {
                return;
            }
            ShowScoreItems(firstVisibleIndex - _maxItemsInContainer);
        }

        private void NextClicked()
        {
            _leftArrow.Hidden = false;
            var lastVisibleIndex = _visibleItemsIds.LastOrDefault();
            if(lastVisibleIndex == 0)
            {
                return;
            }
            ShowScoreItems(lastVisibleIndex + 1);
        }

        private void ShowScoreItems(int startIndex)
        {
            if (startIndex == 0)
            {
                _leftArrow.Hidden = true;
            }
            _itemContainer.RemoveAllChildren(Monosyne.Components.ComponentRemoveMode.Remove);
            _visibleItemsIds = new List<int>();
            var positionY = 0;
            for (var i= startIndex; i<_scoreItemViews.Length;i++)
            {
                _itemContainer.AddChild(_scoreItemViews[i].View);
                _visibleItemsIds.Add(i);
                _scoreItemViews[i].View.TransformModel.PositionY = positionY;
                var itemHeight = _scoreItemViews[i].View.BoundingRect.Height;
                positionY += itemHeight;
                if(_maxItemsInContainer == 0)
                {
                    _maxItemsInContainer = _itemContainer.BoundingRect.Height / itemHeight;
                }

               if(i == _scoreItemsCount-1)
                {
                    _rightArrow.Hidden = true;
                }
                if(_maxItemsInContainer == _visibleItemsIds.Count)
                {
                    break;
                }
            }
        }

        private void OnBackButtonClicked()
        {
            BackButtonClicked();
        }

        public override void Dispose()
        {
            base.Dispose();
            _backButton.Clicked -= OnBackButtonClicked;
            _rightArrow.Clicked -= NextClicked;
            _leftArrow.Clicked -= PreviousClicked;
            _itemContainer.RemoveAllChildren(Monosyne.Components.ComponentRemoveMode.Dispose);
            foreach(var oneItemView in _scoreItemViews)
            {
                oneItemView.Dispose();
            }
        }
    }
}
