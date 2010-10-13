using System;
using System.Collections.ObjectModel;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.DesignData
{
    public class DesignTimeData
    {
        public static ObservableCollection<WordViewModel> InitializeDummyData()
        {

            var spaceWord = GetSpaceLetterWord();
            var result = new ObservableCollection<WordViewModel>();
            var paragraphLength = new Random().Next(15, 30);
            Random random = new Random();
            for (int i = 0; i < paragraphLength; i++)
            {
                WordViewModel wordViewModel = null;
                wordViewModel = GetRandomWord(random.Next(1, 10));
                result.Add(wordViewModel);
                result.Add(spaceWord);
            }
            return result;
        }

        public static WordViewModel GetRandomWord(int wordLength)
        {
            var mainViewModel = new MainViewModel();
            var wordViewModel = new WordViewModel(mainViewModel);

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