using Monosyne;
using Monosyne.Components;
using Monosyne.Content;
using Monosyne.Dev;
using Monosyne.Events;
using Monosyne.Graphics;
using Monosyne.Graphics.States;
using Monosyne.Input;
using Monosyne.Platform;
using Monosyne.Scene.V3;
using Monosyne.Scene.V3.Widgets;
using Monosyne.Threading;
using Scene.Controller;

namespace Scene
{
	public class TestGame: GameV3
	{
	    public readonly RectangleF CoordinateSystem = new RectangleF(0, 0, 960, 640);

	    private readonly RenderStatesNode _rootNode;
        private readonly GesturesTranslator _touchTranslator;
        private RasterizerContext _rasterContext;
        private RootController _rootController;

	    private readonly EventPropagator _eventPropagator;

        public TestGame(IPlatform platform)
			: base(platform)
        {
		    PackageTypeReaderManager.DefaultManager.AddReader(new PackageSceneV2Reader());

            WidgetNode.DefaultHoverPointerType = PointerType.Arrow;

            GameView.Fullscreen = true;
            GameView.KeepScreenOn = true;
            GameView.DisplayModeChange += GameView_DisplayModeChange;

            _rootNode = new RenderStatesNode(this);

            _touchTranslator = new GesturesTranslator(Platform.TouchPanel,
                                                       CoordinateSystem,
                                                       ViewPort.Empty);
            _touchTranslator.Translated += TouchTranslatorTranslated1;
		    _touchTranslator.GesturesGroup.Pinch.Retain();

            _eventPropagator = new EventPropagator(_rootNode);

            Platform.Keyboard.KeyUp += Keyboard_KeyUp;

            GameView_DisplayModeChange(GameView.CurrentDisplayMode);
		}

        private void TouchTranslatorTranslated1(Event obj)
        {
            _eventPropagator.SendEvent(obj);
        }

        private void Keyboard_KeyUp(KeyEvent obj)
        {
            if (obj.KeyCode == Keys.Back)
            {
                obj.Accept();
                Terminate();
                Log.Debug("Back key pressed");
            }
        }

        private void GameView_DisplayModeChange(DisplayMode mode)
        {
            RenderSupport.DefaultProjectionState = new ProjectionState(ProjectionDescription.Orthogonal(0, 0, mode.Width, mode.Height));

            _rasterContext = new RasterizerContext(RenderSupport.RasterizerStatesManager.ScissorDisableViewPortEnable);
            _rasterContext.ViewPort = ViewPort.CreateViewPort(0, 0, mode.Width, mode.Height, CoordinateSystem, AspectMode.LetterBox);

            ProjectionState projectionState = new ProjectionState(ProjectionDescription.Orthogonal(CoordinateSystem));

            _touchTranslator.ViewPort = _rasterContext.ViewPort;
            _rootNode.RenderStates.ProjectionState = projectionState;
            _rootNode.RenderStates.RasterizerContext = _rasterContext;
        }

        protected override async void OnInitialize()
        {
	        await SwitchContext.To(GameThreadEnvironment);
	        
	        AddFpsPanel();
	        
            _rootNode.Initialize();
            AddComponent(_rootNode);
            
            _rootController = new RootController(this, _rootNode);
            _rootController.Start();
        }

        private void AddFpsPanel()
        {
	        var fpsPanel = new PerformanceMeterComponent(this, ContentManager, "int.fnt");
	        fpsPanel.DrawOrder = 255;
	        AddComponent(fpsPanel);
        }

        protected override void OnRenderFrame(RenderContext renderContext)
        {
            renderContext.DeviceContext.Clear(ClearPlane.All, Color.Black, 0, 0);
            base.OnRenderFrame(renderContext);
        }

	    protected override void Dispose(bool disposing)
	    {
	        base.Dispose(disposing);
	        if (disposing)
	        {
                _touchTranslator.Dispose();
                _rootController?.Dispose();
	        }
	    }
	}
}
