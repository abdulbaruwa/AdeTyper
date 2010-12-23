using System.Linq;
using System.Threading;
using AdemolaTyper.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemolaTyperTest.ViewModels.GameOne
{
    [TestClass]
    public class GameOneOverViewModelTestFixture
    {

        [TestMethod]
        public void Should_close_the_GameOverDialog_when_the_Play_Again_is_clicked()
        {
            var homeWindowViewModel = new HomeWindowViewModel();
            var viewModel = new GameOneViewModelTestFactory().CreateViewModel(homeWindowViewModel) as GameOneViewModel;
            viewModel.ProcessStart.Execute(null);
            var letter = viewModel.Words[0].Letters[0].Letter;
            var totalletters = viewModel.Words.Sum(x => x.Letters.Count());

            //Act
            for (int i = 0; i < totalletters; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
                Thread.Sleep(5);
            }

            viewModel.GameOneOver.PlayAgain.Execute(null);
            Assert.IsFalse(viewModel.ProcessCompleted);
        }

        [TestMethod]
        public void Should_close_the_GameOverDialog_when_the_Play_New_button_is_clicked()
        {
            var homeWindowViewModel = new HomeWindowViewModel();
            var viewModel = new GameOneViewModelTestFactory().CreateViewModel(homeWindowViewModel) as GameOneViewModel;
            viewModel.ProcessStart.Execute(null);
            var letter = viewModel.Words[0].Letters[0].Letter;
            var totalletters = viewModel.Words.Sum(x => x.Letters.Count());

            //Act
            for (int i = 0; i < totalletters; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
                Thread.Sleep(5);
            }

            viewModel.GameOneOver.PlayNew.Execute(null);
            Assert.IsFalse(viewModel.ProcessCompleted);
        }



    }
}