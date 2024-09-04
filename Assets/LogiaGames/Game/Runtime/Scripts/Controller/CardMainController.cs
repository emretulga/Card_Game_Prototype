using System;
using System.Threading.Tasks;
using Zenject;
using LogiaGames.Game.Runtime.Model;
using LogiaGames.Game.Runtime.View;

namespace LogiaGames.Game.Runtime.Controller
{
    public interface ICardMainController
    {
        void InitializeCards(bool shuffleCards = true);
        Task RevealACard(CardModel cardModel);
        Task HideACard(CardModel cardModel);
        Task RemoveACard(CardModel cardModel);

        Action<CardModel> OnCardSelected { get; set; }
    }

    public class CardMainController : ICardMainController
    {
        [Inject] private readonly ICardViewManager _cardViewManager;
        [Inject] private readonly GameModel _gameModel;

        public const int CardsLength = 12;

        public Action<CardModel> OnCardSelected { get; set; }

        public void InitializeCards(bool shuffleCards = true)
        {
            _gameModel.Cards = new();
            
            for(int cardIndex = 0; cardIndex < CardsLength / 2; cardIndex++)
            {
                AddNewCardPairToList(cardIndex);
            }

            if(shuffleCards) ShuffleCards();

            _cardViewManager.Initalize(_gameModel.Cards, OnCardChosen);
        }

        private void OnCardChosen(int cardListingId)
        {
            CardModel card = _gameModel.Cards[cardListingId];

            if(card.IsRevealing) return;
            if(card.IsRemoved) return;

            OnCardSelected?.Invoke(card);
        }

        private void AddNewCardPairToList(int cardId)
        {
            _gameModel.Cards.Add(new(cardId));
            _gameModel.Cards.Add(new(cardId));
        }

        private void ShuffleCards()
        {
            Random random = new();
            var currentCardIndex = _gameModel.Cards.Count;  
            while (currentCardIndex > 1)
            {  
                currentCardIndex--;  
                int randomIndex = random.Next(currentCardIndex + 1);  

                var randomChosenCardModel = _gameModel.Cards[randomIndex];
                randomChosenCardModel.ListId = currentCardIndex;

                var currentCardModel = _gameModel.Cards[currentCardIndex];
                currentCardModel.ListId = randomIndex;
                
                _gameModel.Cards[currentCardIndex] = randomChosenCardModel;
                _gameModel.Cards[randomIndex] = currentCardModel;  
            }  
        }

        private ICardView GetCardViewFromCardModel(CardModel card)
        {
            return _cardViewManager.GetCardViewAt(card.ListId);
        }

        public async Task RevealACard(CardModel cardModel)
        {
            ICardView cardView = GetCardViewFromCardModel(cardModel);
            cardModel.IsRevealing = true;
            await cardView.RevealCard();
        }

        public async Task HideACard(CardModel cardModel)
        {
            ICardView cardView = GetCardViewFromCardModel(cardModel);
            cardModel.IsRevealing = false;
            await cardView.HideCard();
        }
        
        public async Task RemoveACard(CardModel cardModel)
        {
            ICardView cardView = GetCardViewFromCardModel(cardModel);
            cardModel.IsRevealing = false;
            cardModel.IsRemoved = true;
            await cardView.RemoveCard();
        }
    }
}
