using UnityEngine;
using TMPro;

namespace LogiaGames.Game.Runtime.View
{
    public class MoveAmountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void ChangeText(string amountText)
        {
            text.text = amountText;
        }
    }
}