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
        private readonly ScoreItemView[] _scoreItemViews;

        public LeaderBoardPopup(Game game, ScoreItemView[] scoreItemViews ) : base(game, "leaderboard.bip", "sceneBoard.object")
        {
            _scoreItemViews = scoreItemViews;
            _backButton = View.FindById<ButtonNode>("backBtn");
            _itemContainer = View.FindById<WidgetNode>("item_container");
            var positionY = 0;
            foreach(var oneItem in _scoreItemViews)
            {
                _itemContainer.AddChild(oneItem.View);
                oneItem.View.TransformModel.PositionY = positionY;
                positionY += oneItem.View.BoundingRect.Height;
            }
            ShowPopup("showBoardPopup");
            _backButton.Clicked += OnBackButtonClicked;
        }

        private void OnBackButtonClicked()
        {
            BackButtonClicked();
        }

        public override void Dispose()
        {
            base.Dispose();
            _backButton.Clicked -= OnBackButtonClicked;
            _itemContainer.RemoveAllChildren(Monosyne.Components.ComponentRemoveMode.Dispose);
            foreach(var oneItemView in _scoreItemViews)
            {
                oneItemView.Dispose();
            }
        }
    }
}
