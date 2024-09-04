using UnityEngine;
using Zenject;
using LogiaGames.Game.Runtime.Model;
using LogiaGames.Game.Runtime.View;
using LogiaGames.Game.Runtime.Controller;

namespace LogiaGames.Game.Runtime.Injection
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CardViewManager _cardViewManager;
        [SerializeField] private UIManagerView _uiViewManager;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();

            Container.BindInterfacesTo<CardViewManager>().FromInstance(_cardViewManager).AsSingle();
            Container.BindInterfacesTo<CardMainController>().AsSingle();

            Container.BindInterfacesTo<UIManagerView>().FromInstance(_uiViewManager).AsSingle();
            Container.BindInterfacesTo<UIManagerController>().AsSingle();
        }
    }
}
