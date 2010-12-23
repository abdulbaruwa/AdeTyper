using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AdemolaTyper.DataSources;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.DesignData
{
    public class GameOneDesignTimeDataSource : IGameOneDataSource
    {
        #region IGameOneDataSource Members

        public IList<WordViewModel> GetGameData()
        {
            return GetGameData(null);
        }

        public IList<WordViewModel> GetGameData(GameOneViewModel gameViewModel)
        {
            var result = new ObservableCollection<WordViewModel>();
            int paragraphLength = new Random().Next(15, 30);
            var random = new Random();
            WordViewModel firstWordViewModel = GetRandomWord(random.Next(1, 10));
            firstWordViewModel.StartAnimation = true;
            result.Add(firstWordViewModel);
            result.Add(GetSpaceLetterWord());
            for (int i = 0; i < paragraphLength; i++)
            {
                WordViewModel randomWord = GetRandomWord(random.Next(1, 10));
                randomWord.GameViewModel = gameViewModel;
                result.Add(randomWord);

                WordViewModel spaceletterWord = GetSpaceLetterWord();
                spaceletterWord.GameViewModel = gameViewModel;
                result.Add(spaceletterWord);
            }
            return result;
        }

        #endregion

        public static ObservableCollection<WordViewModel> InitializeDummyData()
        {
            var result = new ObservableCollection<WordViewModel>();
            int paragraphLength = new Random().Next(15, 30);
            var random = new Random();
            WordViewModel firstWordViewModel = GetRandomWord(random.Next(1, 10));
            firstWordViewModel.StartAnimation = true;
            result.Add(firstWordViewModel);
            result.Add(GetSpaceLetterWord());
            for (int i = 0; i < paragraphLength; i++)
            {
                WordViewModel wordViewModel = null;
                wordViewModel = GetRandomWord(random.Next(1, 10));
                result.Add(wordViewModel);
                result.Add(GetSpaceLetterWord());
            }
            return result;
        }

        public static WordViewModel GetRandomWord(int wordLength)
        {
            var homeWindowViewModel = new GameOneViewModel();
            var wordViewModel = new WordViewModel(homeWindowViewModel);

            wordViewModel.WordHeight = 15;

            string alphabets = "abdcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            for (int i = 0; i < wordLength; i++)
            {
                char letter = alphabets[random.Next(alphabets.Length)];
                wordViewModel.Letters.Add(new TypeFaceViewModel(letter, 14));
            }

            return wordViewModel;
        }

        public static WordViewModel GetSpaceLetterWord()
        {
            char spaceLetter = Char.Parse(" ");
            var spaceWord = new WordViewModel();
            spaceWord.Letters.Add(new TypeFaceViewModel(spaceLetter, 14));
            return spaceWord;
        }
    }
}