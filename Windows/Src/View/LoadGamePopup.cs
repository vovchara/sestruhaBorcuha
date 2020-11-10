using Monosyne;
using Monosyne.Scene.V3.Widgets;
using Scene.Src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.View
{
   public class LoadGamePopup : PopupBase
    {
        public event Action BackBtnIsClicked = delegate { };
        public event Action <UserModel> LoadGameForUser = delegate { };

        private LoadGameItemView[] _loadItems;
        private ButtonNode _backButton;
        private WidgetNode _itemContainer;

        public LoadGamePopup(Game game, LoadGameItemView [] loadGameItems) : base(game, "leaderboard.bip", "sceneBoard.object")
        {
            _loadItems = loadGameItems;
            _backButton = View.FindById<ButtonNode>("backBtn");
            _itemContainer = View.FindById<WidgetNode>("item_container");
            var positionY = 0;
            foreach (var oneItem in _loadItems)
            {
                _itemContainer.AddChild(oneItem.View);
                oneItem.View.TransformModel.PositionY = positionY;
                positionY += oneItem.View.BoundingRect.Height;
                oneItem.OpenSavedGame += ContinueIsClicked;
            }
            ShowPopup("showBoardPopup");
            _backButton.Clicked += OnBackButtonClicked;
            
        }

        private void ContinueIsClicked(UserModel user)
        {
            LoadGameForUser(user);
        }

        private void OnBackButtonClicked()
        {
            BackBtnIsClicked();
        }

        public override void Dispose()
        {
            base.Dispose();
            _backButton.Clicked -= OnBackButtonClicked;
            _itemContainer.RemoveAllChildren(Monosyne.Components.ComponentRemoveMode.Dispose);
            foreach (var oneItemView in _loadItems)
            {
                oneItemView.Dispose();
            }
        }
    }
}
