using Zenject;
using LogiaGames.Game.Runtime.View;

namespace LogiaGames.Game.Runtime.Controller
{
    public interface IUIManagerController
    {
        void ShowLoseScreen();
        void ChangeMovementAmountText(string amountText);
    }

    public class UIManagerController: IUIManagerController
    {
        [Inject] private readonly IUIManagerView _uiManagerView;

        public void ShowLoseScreen()
        {
            _uiManagerView.ShowLoseScreen();
        }

        public void ChangeMovementAmountText(string amountText)
        {
            _uiManagerView.ChangeMovementAmountText(amountText);
        }
    }
}