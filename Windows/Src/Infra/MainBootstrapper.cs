using Autofac;
using Scene.Controller;
using Scene.Model;
using Scene.Src.Controller;
using Scene.Src.Model;
using Scene.Src.View;
using Scene.View;

namespace Scene.Src.Infra
{
    public class MainBootstrapper
    {
        public void RegisterAll(ContainerBuilder builder)
        {
            builder.RegisterType<ControllerFactory>();
            builder.RegisterType<LoadUserSavesController>().ExternallyOwned();
            builder.RegisterType<WelcomeController>().ExternallyOwned();
            builder.RegisterType<LoadGameController>().ExternallyOwned();
            builder.RegisterType<LeaderBoardController>().ExternallyOwned();
            builder.RegisterType<LobbyController>().ExternallyOwned();
            builder.RegisterType<LevelController>().ExternallyOwned();
            builder.RegisterType<ViewFactory>();
            builder.RegisterType<WelcomePopup>().ExternallyOwned();
            builder.RegisterType<LoadGamePopup>().ExternallyOwned();
            builder.RegisterType<LeaderBoardPopup>().ExternallyOwned();
            builder.RegisterType<LobbyPopup>().ExternallyOwned();
            builder.RegisterType<LevelPopup>().ExternallyOwned();
            builder.RegisterType<LevelConfigStorage>().SingleInstance();
            builder.RegisterType<ScoreItemView>().ExternallyOwned();
            builder.RegisterType<LoadGameItemView>().ExternallyOwned();
            builder.RegisterType<UserStorage>().SingleInstance();
        }
    }
}
