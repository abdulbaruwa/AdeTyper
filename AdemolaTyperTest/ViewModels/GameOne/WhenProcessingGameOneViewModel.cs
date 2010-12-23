using System.Linq;
using System.Threading;
using AdemolaTyper.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemolaTyperTest.ViewModels.GameOne
{
    [TestClass]
    public class WhenProcessingGameOneViewModel 
    {
        [TestMethod]
        public void Should_set_the_CurrentWord_when_the_CurrentWordIsProcessed_Command_is_fired()
        {
            var viewModel = new GameOneViewModelTestFactory().CreateViewModel() as GameOneViewModel;
            viewModel.ProcessStart.Execute(null);
            var letter = viewModel.Words[0].Letters[0].Letter;
            
            //Act
            for (int i = 0; i < viewModel.Words[0].Letters.Count; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
            }

            //Assert
            Assert.AreEqual(viewModel.CurrentWord.Letters[0].Letter.ToString(), " ");
        }

        [TestMethod]
        public void Should_set_the_completed_flag_when_the_last_word_has_been_processed()
        {
            var viewModel = new GameOneViewModelTestFactory().CreateViewModel() as GameOneViewModel;
            viewModel.ProcessStart.Execute(null);
            var letter = viewModel.Words[0].Letters[0].Letter;
            var totalletters = viewModel.Words.Sum(x => x.Letters.Count);
            //Act
            for (int i = 0; i < totalletters; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
            }

            Assert.IsTrue(viewModel.ProcessCompleted);
        }

        [TestMethod]
        public void Should_calulate_and_return_type_speed()
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
            Assert.IsTrue(homeWindowViewModel.WordsPerMinute > 0);
            Assert.IsTrue(viewModel.WordsPerMinute > 0);
        }
    }
}