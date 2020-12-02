using Monosyne.Scene.V3;

namespace Scene.Src.Infra
{
    public class RootSceneContainer
    {
        public RenderStatesNode RootScene { get; }

        public RootSceneContainer(RenderStatesNode rootScene)
        {
            RootScene = rootScene;
        }
    }
}
