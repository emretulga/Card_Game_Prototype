using UnityEngine;

namespace LogiaGames.Game.Runtime.View
{
    public interface IUIManagerView
    {
        void ShowLoseScreen();
        void ChangeMovementAmountText(string amountText);
    }

    public class UIManagerView : MonoBehaviour, IUIManagerView
    {
        [SerializeField] private LoseScreenView _loseScreen;
        [SerializeField] private MoveAmountView _movementAmountText;
        public void ShowLoseScreen()
        {
            _loseScreen.Show();
        }

        public void ChangeMovementAmountText(string amountText)
        {
            _movementAmountText.ChangeText(amountText);
        }
    }
}