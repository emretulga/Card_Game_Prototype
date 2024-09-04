using System;
using System.Threading.Tasks;
using Zenject;
using LogiaGames.Game.Runtime.Model;

namespace LogiaGames.Game.Runtime.Controller
{
    public class GameController : IInitializable, IDisposable
    {
        [Inject] private readonly GameModel _gameModel;
        [Inject] private readonly ICardMainController _cardMainController;
        [Inject] private readonly IUIManagerController _uiManagerController;

        private bool _canControl = true;
        private CardModel _chosenFirstCard, _chosenSecondCard;

        private const int _moveAmount = 5;

        public void Initialize()
        {
            InitializeGame();
        }
        public void InitializeGame(bool shuffleCards = true)
        {
            _gameModel.MoveAmount = _moveAmount;

            _uiManagerController.ChangeMovementAmountText(_gameModel.MoveAmount.ToString());

            _cardMainController.OnCardSelected += OnCardChosen;

            _cardMainController.InitializeCards(shuffleCards);
        }
        
        public async void OnCardChosen(CardModel cardModel)
        {
            if(!_canControl) return;

            var revealingCardTask = _cardMainController.RevealACard(cardModel);

            bool isFirstCardChosen = _chosenFirstCard != null;
            if(!isFirstCardChosen)
            {
                _chosenFirstCard = cardModel;

                return;
            }

            _chosenSecondCard = cardModel;

            bool isMatching = _chosenFirstCard.TypeID == _chosenSecondCard.TypeID;

            _canControl = false;

            await revealingCardTask;

            Task waitingTask;

            if(isMatching)
            {
                var removingFirstCardTask = _cardMainController.RemoveACard(_chosenFirstCard);
                var removingSecondCardTask = _cardMainController.RemoveACard(_chosenSecondCard);

                waitingTask = Task.WhenAll(removingFirstCardTask, removingSecondCardTask);
            }
            else
            {
                var hidingFirstCardTask = _cardMainController.HideACard(_chosenFirstCard);
                var hidingSecondCardTask = _cardMainController.HideACard(_chosenSecondCard);

                waitingTask = Task.WhenAll(hidingFirstCardTask, hidingSecondCardTask);

                _gameModel.MoveAmount--;
                _uiManagerController.ChangeMovementAmountText(_gameModel.MoveAmount.ToString());
                if(_gameModel.MoveAmount <= 0)
                {
                    _uiManagerController.ShowLoseScreen();
                    return;
                }
            }

            _chosenFirstCard = null;
            _chosenSecondCard = null;

            await waitingTask;

            _canControl = true;
        }

        public void Dispose()
        {
            _cardMainController.OnCardSelected -= OnCardChosen;
        }
    }
}