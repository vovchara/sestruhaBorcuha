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
        private int _loadItemsCount;
        private ButtonNode _backButton;
        private WidgetNode _itemContainer;
        private ButtonNode _leftArrow;
        private ButtonNode _rightArrow;
        private List<int> _visibleItemsIds;
        private int _maxItemsInContainer;

        public LoadGamePopup(Game game, LoadGameItemView [] loadGameItems) : base(game, "leaderboard.bip", "sceneBoard.object")
        {
            _loadItems = loadGameItems;
            _loadItemsCount = _loadItems.Length;
            _backButton = View.FindById<ButtonNode>("backBtn");
            _itemContainer = View.FindById<WidgetNode>("item_container");
            _leftArrow = View.FindById<ButtonNode>("leftBtn");
            _rightArrow = View.FindById<ButtonNode>("rightBtn");
            ShowLoadItems(0);
            ShowPopup("showBoardPopup");
            _backButton.Clicked += OnBackButtonClicked;
            _rightArrow.Clicked += NextClicked;
            _leftArrow.Clicked += PreviousClicked;

        }

        private void PreviousClicked()
        {
            _rightArrow.Hidden = false;
            var firstVisibleIndex = _visibleItemsIds.FirstOrDefault();
            if (firstVisibleIndex == 0)
            {
                return;
            }
            ShowLoadItems(firstVisibleIndex - _maxItemsInContainer);
        }

        private void NextClicked()
        {
            _leftArrow.Hidden = false;
            var lastVisibleIndex = _visibleItemsIds.LastOrDefault();
            if (lastVisibleIndex == 0)
            {
                return;
            }
            ShowLoadItems(lastVisibleIndex + 1);
        }

        private void ShowLoadItems (int startIndex)
        {
            if(startIndex == 0)
            {
                _leftArrow.Hidden = true;
            }
            _itemContainer.RemoveAllChildren(Monosyne.Components.ComponentRemoveMode.Remove);
            _visibleItemsIds = new List<int>();
            var positionY = 0;
            for (var i = startIndex; i < _loadItems.Length; i++)
            {
                _itemContainer.AddChild(_loadItems[i].View);
                _loadItems[i].OpenSavedGame += ContinueIsClicked;
                _visibleItemsIds.Add(i);
                _loadItems[i].View.TransformModel.PositionY = positionY;
                var itemHeight = _loadItems[i].View.BoundingRect.Height;
                positionY += itemHeight;
                if(_maxItemsInContainer == 0)
                {
                    _maxItemsInContainer = _itemContainer.BoundingRect.Height / itemHeight;
                }
                if(i == _loadItemsCount-1)
                {
                    _rightArrow.Hidden = true;
                }
                if(_maxItemsInContainer == _visibleItemsIds.Count)
                {
                    break;
                }
            }
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
            _rightArrow.Clicked -= NextClicked;
            _leftArrow.Clicked -= PreviousClicked;
            _itemContainer.RemoveAllChildren(Monosyne.Components.ComponentRemoveMode.Dispose);
            foreach (var oneItemView in _loadItems)
            {
                oneItemView.Dispose();
            }
        }
    }
}
