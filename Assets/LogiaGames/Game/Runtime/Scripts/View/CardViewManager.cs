using System;
using System.Collections.Generic;
using UnityEngine;
using LogiaGames.Game.Runtime.Model;

namespace LogiaGames.Game.Runtime.View
{
    public interface ICardViewManager
    {
        void Initalize(List<CardModel> cardModels, Action<int> onCardChosen);
        ICardView GetCardViewAt(int cardListingId);
    }
    public class CardViewManager : MonoBehaviour, ICardViewManager
    {
        [SerializeField] private List<Sprite> _cardSprites;
        [SerializeField] private List<CardView> _cardViews;

        public void Initalize(List<CardModel> cardModels, Action<int> onCardChosen)
        {
            int cardViewsLength = _cardViews.Count;
            for(int cardIndex = 0; cardIndex < cardViewsLength; cardIndex++)
            {
                CardView cardView = _cardViews[cardIndex];
                Sprite cardSprite = _cardSprites[cardModels[cardIndex].TypeID];
                cardView.InitializeView(cardSprite, cardIndex, onCardChosen);
            }
        }

        public ICardView GetCardViewAt(int cardListingId)
        {
            return _cardViews[cardListingId];
        }
    }
}