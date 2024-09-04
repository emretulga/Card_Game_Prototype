using UnityEngine;
using Zenject;
using LogiaGames.Game.Runtime.Model;
using LogiaGames.Game.Runtime.View;
using LogiaGames.Game.Runtime.Controller;
using NSubstitute;

namespace LogiaGames.Game.Test.UnitTest.Injection
{
    public class GameCoreTestInstaller : Installer<GameCoreTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();

            Container.Bind<ICardViewManager>().FromInstance(Substitute.For<ICardViewManager>()).AsSingle();
            Container.BindInterfacesTo<CardMainController>().AsSingle();

            Container.Bind<IUIManagerView>().FromInstance(Substitute.For<IUIManagerView>()).AsSingle();
            Container.BindInterfacesTo<UIManagerController>().AsSingle();
        }
    }
}
