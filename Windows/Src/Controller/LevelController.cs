using Scene.Src.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Controller
{
   public class LevelController : ControllerBase
    {
        private LevelPopup _levelPopup;
        public void Start()
        {
            _levelPopup = new LevelPopup(_game);

            var view = _levelPopup.View;
            _rootScene.AddChild(view);
        }

        public void Dispose()
        {
            if (_levelPopup != null)
            {
                _levelPopup.Dispose();
            }
        }
    }
}
