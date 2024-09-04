using UnityEngine;
using DG.Tweening;

namespace LogiaGames.Game.Runtime.View
{
    public class LoseScreenView : MonoBehaviour
    {
        public void Show()
        {
            transform.localScale = Vector3.zero;
            transform.gameObject.SetActive(true);
            transform.DOScale(Vector3.one, 0.5f);
        }

        public void Hide()
        {
            transform.gameObject.SetActive(false);
        }
    }
}