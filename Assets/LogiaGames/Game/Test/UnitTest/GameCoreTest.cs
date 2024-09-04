using LogiaGames.Game.Runtime.Controller;
using LogiaGames.Game.Runtime.Model;
using LogiaGames.Game.Test.UnitTest.Injection;
using NUnit.Framework;
using Zenject;

namespace LogiaGames.Game.Test.UnitTest
{
    [TestFixture]
    public class GameCoreTest : ZenjectUnitTestFixture
    {
        private GameController _gameController;
        private GameModel _gameModel;

        [SetUp]
        public void InstallBindings()
        {
            GameCoreTestInstaller.Install(Container);
            
            _gameController = Container.Resolve<GameController>();
            _gameModel = Container.Resolve<GameModel>();

            _gameController.InitializeGame(false);

        }

        [Test]
        public void TestPlatform()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestBinding()
        {
            Assert.AreNotEqual(_gameController, null);
            Assert.AreNotEqual(_gameModel, null);
        }

        [Test]
        public void TestCardsInitializedCorrectly()
        {
            Assert.Greater(_gameModel.Cards.Count, 0);

            for(int i = 0; i < _gameModel.Cards.Count; i++)
            {
                int sameAmount = 0;
                for(int b = 0; b < _gameModel.Cards.Count; b++)
                {
                    if(_gameModel.Cards[i].TypeID == _gameModel.Cards[b].TypeID) sameAmount++;
                }

                Assert.AreEqual(0, sameAmount % 2, default, $"Error Index : {i}");
            }
        }

        [Test]
        public void TestCanMatchCorrectly()
        {
            _gameController.OnCardChosen(_gameModel.Cards[0]);
            _gameController.OnCardChosen(_gameModel.Cards[1]);

            Assert.IsTrue(_gameModel.Cards[0].IsRemoved && _gameModel.Cards[1].IsRemoved);
        }

        [Test]
        public void TestCanFailCorrectly()
        {
            _gameController.OnCardChosen(_gameModel.Cards[0]);
            _gameController.OnCardChosen(_gameModel.Cards[2]);

            Assert.IsTrue(!_gameModel.Cards[0].IsRemoved && !_gameModel.Cards[2].IsRemoved);
        }

        [Test]
        public void TestCanWin()
        {
            foreach(CardModel card in _gameModel.Cards)
            {
                _gameController.OnCardChosen(card);
            }

            foreach(CardModel card in _gameModel.Cards)
            {
                Assert.IsTrue(_gameModel.Cards[0].IsRemoved);
            }
        }

        [Test]
        public void TestCanLose()
        {
            int moveAmount = _gameModel.MoveAmount;

            for(int i = 0; i < moveAmount; i++)
            {
                _gameController.OnCardChosen(_gameModel.Cards[0]);
                _gameController.OnCardChosen(_gameModel.Cards[2]);
            }

            Assert.AreEqual(0, _gameModel.MoveAmount);
        }

        [Test]
        public void TestOneMoveSpent()
        {
            int moveAmount = _gameModel.MoveAmount;

            _gameController.OnCardChosen(_gameModel.Cards[0]);
            _gameController.OnCardChosen(_gameModel.Cards[2]);

            Assert.AreEqual(moveAmount - 1, _gameModel.MoveAmount);
        }

        [TearDown]
        public void ReleaseBindings()
        {
            Container.UnbindAll();
        }
    }
}