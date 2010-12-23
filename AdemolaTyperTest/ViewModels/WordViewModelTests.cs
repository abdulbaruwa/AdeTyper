using System.Linq;
using AdemolaTyper.DataSources;
using AdemolaTyper.DesignData;
using AdemolaTyper.ViewModels;
using AdemolaTyperTest.ViewModels.GameOne;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemolaTyperTest.ViewModels
{
    [TestClass]
    public class WordViewModelTests
    {
        [TestMethod]
        public void ShouldMatchCharKeyWithWordLetter()
        {
            //Arrange
            var viewModel = new GameOneViewModel();
            viewModel.Words = GameOneDesignTimeDataSource.InitializeDummyData();

            //Act             
            
        }

        [TestMethod]
        public void ShouldSetTheStartTimeOnProcessStart()
        {
            //Arrange
            var viewModel = new GameOneViewModel();

            viewModel.ProcessStart.Execute(null);

            Assert.IsNotNull(viewModel.ProcessStartTime);
        }

        [TestMethod]
        public void ShouldStoreTheCurrentTypedLetter()
        {
            var viewModel = new GameOneViewModel();
            viewModel.Words = GameOneDesignTimeDataSource.InitializeDummyData();

            viewModel.CurrentWord = viewModel.Words[0];
            viewModel.KeyPressReceivedCommand.Execute("b");

            //Assert
            Assert.IsTrue(viewModel.TypedLetter == "b");    
        }

        //[TestMethod]
        //public void ShouldCompareTheTypedCharAgainstTheCurrentLetterOfTheCurrentWord()
        //{
        //    var viewModel = new GameOneViewModel();
        //    viewModel.Words = GameOneDesignTimeDataSource.InitializeDummyData();

        //    viewModel.CurrentWord = viewModel.Words[0];
        //    viewModel.KeyPressReceivedCommand.Execute("a");

        //    //Assert
        //    Assert.IsTrue(viewModel.CurrentWord.CurrentLetterIndex == 1);
        //}

        [TestMethod]
        public void Should_Pass_The_letter_to_the_current_word_viewmodel()
        {
            var viewModel = new GameOneViewModel();
            viewModel.Words = GameOneDesignTimeDataSource.InitializeDummyData();

            viewModel.CurrentWord = viewModel.Words[0];
            var firstLetterOfFirstWord = viewModel.Words[0].Letters[0].Letter;
            viewModel.KeyPressReceivedCommand.Execute(firstLetterOfFirstWord);

            Assert.AreEqual(viewModel.Words[0].CurrentLetter, firstLetterOfFirstWord.ToString());
            
        }

        [TestMethod]
        public void Should_fire_command_on_main_viewmodel_to_indicate_the_word_has_received_all_chars()
        {
            var viewModel = new GameOneViewModelTestFactory().CreateViewModel() as GameOneViewModel;
            
            var currentWordLenght = viewModel.CurrentWord.Letters.Count;
            var letter = viewModel.Words[0].Letters[0].Letter;
            for (int i = 0; i < currentWordLenght; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
            }
            //Assert we are on the next word in the list of words
            Assert.IsTrue(viewModel.CurrentWord.Letters.First() == viewModel.Words[1].Letters.First());
        }

    }
}
