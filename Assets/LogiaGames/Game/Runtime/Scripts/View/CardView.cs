using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;

namespace LogiaGames.Game.Runtime.View
{
    public interface ICardView
    {

        public int ListingId{get;}
        public Task HideCard();
        public Task RevealCard();
        public Task RemoveCard();
    }
    public class CardView : MonoBehaviour, ICardView
    {
        [SerializeField] private Transform _cardViewTransform;
        [SerializeField] private SpriteRenderer _cardBackSpriteRenderer, _cardFrontSpriteRenderer;

        public int ListingId {get; private set;}

        private Action<int> _onCardChosen;

        public async Task HideCard()
        {
            DOTween.Kill(_cardBackSpriteRenderer.transform);
            DOTween.Kill(_cardFrontSpriteRenderer.transform);

            _cardFrontSpriteRenderer.transform.localScale = Vector3.one;
            _cardBackSpriteRenderer.transform.localScale = new Vector3(0f, 1f, 1f);


            var hidingSequence = DOTween.Sequence();
            hidingSequence.Append(_cardFrontSpriteRenderer.transform.DOScaleX(0f, 0.2f));
            hidingSequence.Append(_cardBackSpriteRenderer.transform.DOScaleX(1f, 0.2f));

            await hidingSequence.AsyncWaitForCompletion();
        }
        public async Task RevealCard()
        {
            DOTween.Kill(_cardBackSpriteRenderer.transform);
            DOTween.Kill(_cardFrontSpriteRenderer.transform);

            _cardBackSpriteRenderer.transform.localScale = Vector3.one;
            _cardFrontSpriteRenderer.transform.localScale = new Vector3(0f, 1f, 1f);
            
            var revealingSequence = DOTween.Sequence();
            revealingSequence.Append(_cardBackSpriteRenderer.transform.DOScaleX(0f, 0.2f));
            revealingSequence.Append(_cardFrontSpriteRenderer.transform.DOScaleX(1f, 0.2f));

            await revealingSequence.AsyncWaitForCompletion();

            await Task.Delay(600);
        }

        public async Task RemoveCard()
        {
            await _cardViewTransform.transform.DOScale(Vector3.zero, 0.5f).AsyncWaitForCompletion();
        }

        public void InitializeView(Sprite cardSprite, int listingID, Action<int> onCardChosen)
        {
            _cardFrontSpriteRenderer.sprite = cardSprite;
            ListingId = listingID;
            _onCardChosen = onCardChosen;
        }

        public void OnMouseDown()
        {
            _onCardChosen?.Invoke(ListingId);
        }
    }
}