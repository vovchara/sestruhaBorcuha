using Monosyne;
using Monosyne.Scene.V3;

namespace Scene.Model
{
    public class RootStorage
    {
        private static RootStorage instance;

        private RootStorage()
        { }
 
        public static RootStorage getInstance()
        {
            if (instance == null)
            {
                instance = new RootStorage();
            }

            return instance;
        }
        
        public Game Game { get; set; }
        public RenderStatesNode RootScene { get; set; }
    }
}