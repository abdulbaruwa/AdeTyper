using System.Linq;
using System.Linq.Expressions;
using AdemolaTyper.DesignData;
using AdemolaTyper.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemolaTyperTest.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void Should_set_the_CurrentWord_when_the_CurrentWordIsProcessed_Command_is_fired()
        {
            var viewModel = new MainViewModel();

            viewModel.AddWord(DesignTimeData.GetRandomWord(4));
            viewModel.AddWord(DesignTimeData.GetSpaceLetterWord());
            viewModel.AddWord(DesignTimeData.GetRandomWord(3));

            viewModel.CurrentWordIndex = 0;
            viewModel.CurrentWord = viewModel.Words[0];
            viewModel.ProcessStart.Execute(null);
            var letter = viewModel.Words[0].Letters[0].Letter;

            //Act
            for (int i = 0; i < 4 ; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
            }

            //Assert
            Assert.AreEqual(viewModel.CurrentWord.Letters[0].Letter.ToString(),  " ");
        }

        [TestMethod]
        public void Should_set_the_completed_flag_when_the_last_word_has_been_processed()
        {
            var viewModel = new MainViewModel();

            viewModel.AddWord(DesignTimeData.GetRandomWord(4));
            viewModel.AddWord(DesignTimeData.GetSpaceLetterWord());
            viewModel.AddWord(DesignTimeData.GetRandomWord(3));

            viewModel.CurrentWordIndex = 0;
            viewModel.CurrentWord = viewModel.Words[0];
            viewModel.ProcessStart.Execute(null);
            var letter = viewModel.Words[0].Letters[0].Letter;

            //Act
            for (int i = 0; i < 8; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
            }

            Assert.IsTrue(viewModel.ProcessCompleted);
        }


        [TestMethod]
        public void Should_calulate_and_return_type_speed()
        {
            var viewModel = new MainViewModel();
            viewModel.AddWords(DesignTimeData.InitializeDummyData());

            viewModel.CurrentWordIndex = 0;
            viewModel.CurrentWord = viewModel.Words[0];
            viewModel.ProcessStart.Execute(null);
            var letter = viewModel.Words[0].Letters[0].Letter;

            var totalletters = viewModel.Words.Sum(x => x.Letters.Count());
            

            //Act
            for (int i = 0; i < totalletters; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
            }


        }
    }
}