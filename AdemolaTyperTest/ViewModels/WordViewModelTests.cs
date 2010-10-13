using System;
using AdemolaTyper.DesignData;
using AdemolaTyper.ViewModels;
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
            var viewModel = new MainViewModel();
            viewModel.Words = DesignTimeData.InitializeDummyData();

            //Act             
            
        }

        [TestMethod]
        public void ShouldSetTheStartTimeOnProcessStart()
        {
            //Arrange
            var viewModel = new MainViewModel();
            viewModel.Words = DesignTimeData.InitializeDummyData();

            viewModel.ProcessStart.Execute(null);

            Assert.IsNotNull(viewModel.ProcessStartTime);
        }

        [TestMethod]
        public void ShouldStoreTheCurrentTypedLetter()
        {
            var viewModel = new MainViewModel();
            viewModel.Words = DesignTimeData.InitializeDummyData();

            viewModel.CurrentWord = viewModel.Words[0];
            viewModel.KeyPressReceivedCommand.Execute("b");

            //Assert
            Assert.IsTrue(viewModel.TypedLetter == "b");    
        }

        //[TestMethod]
        //public void ShouldCompareTheTypedCharAgainstTheCurrentLetterOfTheCurrentWord()
        //{
        //    var viewModel = new MainViewModel();
        //    viewModel.Words = DesignTimeData.InitializeDummyData();

        //    viewModel.CurrentWord = viewModel.Words[0];
        //    viewModel.KeyPressReceivedCommand.Execute("a");

        //    //Assert
        //    Assert.IsTrue(viewModel.CurrentWord.CurrentLetterIndex == 1);
            
        //}

        [TestMethod]
        public void Should_Pass_The_letter_to_the_current_word_viewmodel()
        {
            var viewModel = new MainViewModel();
            viewModel.Words = DesignTimeData.InitializeDummyData();

            viewModel.CurrentWord = viewModel.Words[0];
            var firstLetterOfFirstWord = viewModel.Words[0].Letters[0].Letter;
            viewModel.KeyPressReceivedCommand.Execute(firstLetterOfFirstWord);

            Assert.AreEqual(viewModel.Words[0].CurrentLetter, firstLetterOfFirstWord.ToString());
            
        }

        [TestMethod]
        public void Should_fire_command_on_main_viewmodel_to_indicate_the_word_has_received_all_chars()
        {
            var viewModel = new MainViewModel();

            viewModel.Words.Add(DesignTimeData.GetRandomWord(4));
            viewModel.Words.Add(DesignTimeData.GetSpaceLetterWord());
            viewModel.Words.Add(DesignTimeData.GetRandomWord(3));

            viewModel.CurrentWord = viewModel.Words[0];
            var currentWordLenght = viewModel.CurrentWord.Letters.Count;
            var letter = viewModel.Words[0].Letters[0].Letter;
            for (int i = 0; i < currentWordLenght; i++)
            {
                viewModel.KeyPressReceivedCommand.Execute(letter);
            }
            Assert.IsTrue(viewModel.CurrentWord.IsComplete);
        }

    }
}
