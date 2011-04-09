using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using AdemolaTyper.DataSources;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.DesignData
{
    public class GameOneDataSource : IGameOneDataSource
    {
        #region IGameOneDataSource Members

        public IList<WordViewModel> GetGameData()
        {
            return GetGameData(null);
        }
        
        public IList<WordViewModel> ImportGameData(GameOneViewModel gameViewModel)
        {
            var words = new List<WordViewModel>();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof (WordViewModel)).Location), @"DesignData\GameOneData.txt");
            var fileData = File.ReadAllText(fileName);
            var v = (char) 32;
            var result = fileData.Split(v);
            var durationStart = DateTime.Now;
            var firstWord = CreateWordViewModelFromWordString(result[0]);
            firstWord.StartAnimation = true;
            words.Add(firstWord);
            words.Add(GetSpaceLetterWord());

            for (int i = 1; i < result.Length; i++)
            {
                words.Add(CreateWordViewModelFromWordString(result[i]));
                words.Add(GetSpaceLetterWord());
            }
            var duration = DateTime.Now.Subtract(durationStart);

            return words;
        }

        private WordViewModel CreateWordViewModelFromWordString(string word)
        {
            var gameOneViewModel = new GameOneViewModel(new HomeWindowViewModel());
            var wordViewModel = new WordViewModel(gameOneViewModel);
            wordViewModel.WordHeight = 17;
    
            foreach (var character in word)
            {
                char letter = character;
                wordViewModel.Letters.Add(new TypeFaceViewModel(letter, 16));
            }
            return wordViewModel;
        }

        public IList<WordViewModel> GetGameData(GameOneViewModel gameViewModel)
        {
            return ImportGameData(gameViewModel);
        }

        #endregion

        public static ObservableCollection<WordViewModel> InitializeDummyData()
        {
            
            var result = new ObservableCollection<WordViewModel>();
            int paragraphLength = new Random().Next(15, 60);
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
            var gameOneViewModel = new GameOneViewModel(new HomeWindowViewModel());
            var wordViewModel = new WordViewModel(gameOneViewModel);
            wordViewModel.WordHeight = 17;

            string alphabets = "abdcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            for (int i = 0; i < wordLength; i++)
            {
                char letter = alphabets[random.Next(alphabets.Length)];
                wordViewModel.Letters.Add(new TypeFaceViewModel(letter, 16));
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