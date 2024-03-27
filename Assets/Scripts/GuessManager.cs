using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    #region Memebers

    public GameManager gameManager;
    public HuggiWaggi huggiWaggi;
    public WordPlaceholderGenerator wordPlaceholderGenerator;

    public Dictionary<string, List<string>> playableThemeWordMapper;
    public Dictionary<string, List<string>> originalThemeWordMapper = new Dictionary<string, List<string>>
    {
        { "Животные", new List<string> { "собака", "кот", "черепаха", "кролик", "хомяк", "лошадь" } },
        { "Еда", new List<string> { "яблоко", "банан", "пицца", "сосиска", "чипсы", "огурец" } },
        { "Аниме", new List<string> { "наруто", "блич", "ванпанчмен", "ван пис", "семья шпиона" } },
        { "Супергерои", new List<string> { "тор", "халк", "флеш", "супермен", "человек паук", "бэтмен" } },
        { "Игрушки", new List<string> { "лего", "барби", "поп ит", "робот", "спиннер" } },
        { "Школа", new List<string> { "учитель", "карандаш", "рюкзак", "ручка", "перемена" } },
        { "Цвета", new List<string> { "красный", "оранжевый", "желтый", "зеленый", "синий", "фиолетовый" } },
        { "Космос", new List<string> { "звезда", "планета", "галактика", "комета", "космонавт", "ракета" } },
        { "Подводный мир", new List<string> { "рыба", "осьминог", "водоросли", "дельфин", "акула" } },
        { "Фантастика", new List<string> { "дракон", "фея", "волшебник", "единорог", "тролль", "огр" } },
        { "Спорт", new List<string> { "футбол", "баскетбол", "бейсбол", "теннис", "гимнастика", "гольф" } },
        { "Технологии", new List<string> { "ноутбук", "телефон", "гаджет", "планшет", "дрон", "интернет" } },
        { "Видеоигры", new List<string> { "майнкрафт", "роблокс", "фортнайт", "амонг ас", "контр страйк" } },
        { "Праздники", new List<string> { "рождество", "пасха", "день победы", "новый год" } },
        { "Природа", new List<string> { "цветок", "лес", "гора", "река", "пустыня", "вулкан", "море", "ураган" } },
    };

    #endregion

    #region Properties

    public string GuessedWord {  get; private set; }
    public string GuessedWordTheme { get; private set; }
    public int CountOfCorrectGuesses { get; set; }
    public int GuessedWordTrueLength => GuessedWord.Length - GuessedWord.Count(c => c == ' ');

    #endregion

    #region Public Methods

    public void ChooseWord()
    {
        // initialize the game
        CountOfCorrectGuesses = 0;

        if (playableThemeWordMapper is null)
        {
            playableThemeWordMapper = new Dictionary<string, List<string>>(originalThemeWordMapper);
        }
        
        // set the word and theme
        PrepareThemeAndWord();
    }

    public bool SelectLetter(char letter)
    {
        int nonSpaceCharIndex = -1;

        for (int i = 0; i < GuessedWord.Length; i++)
        {
            if (GuessedWord[i] == ' ')
            {
                continue;
            }

            nonSpaceCharIndex++;

            if (GuessedWord[i] == letter)
            {   
                wordPlaceholderGenerator.wordLetters[nonSpaceCharIndex].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        return Guess(letter);
    }

    #endregion

    #region Private methods

    private void PrepareThemeAndWord()
    {
        // Select the theme
        var themeList = playableThemeWordMapper.Keys.ToList();
        var themeIndex = Random.Range(0, themeList.Count);
        GuessedWordTheme = themeList[themeIndex];

        // Select the word from the theme

        // if the list is empty, reset it from the original mapper
        if (!playableThemeWordMapper[GuessedWordTheme].Any())
        {
            playableThemeWordMapper[GuessedWordTheme] = new List<string>(originalThemeWordMapper[GuessedWordTheme]);
        }

        // Choose a random word from the theme
        int wordIndex = Random.Range(0, playableThemeWordMapper[GuessedWordTheme].Count);
        GuessedWord = playableThemeWordMapper[GuessedWordTheme][wordIndex];

        // Remove the chosen word from the list
        playableThemeWordMapper[GuessedWordTheme].RemoveAt(wordIndex);

        Debug.Log($"Выбранная тема: {GuessedWordTheme}, выбранное слово: {GuessedWord}");

        wordPlaceholderGenerator.Generate();
    }

    private bool Guess(char letter)
    {
        bool IsGuessed = false;

        for(int i = 0; i < GuessedWord.Length; i++)
        {
            if (GuessedWord[i] == letter)
            {
                CountOfCorrectGuesses++;
                IsGuessed = true;
            }
        }

        return IsGuessed;
    }

    #endregion

    #region Internal Methods

    internal void RevealWord()
    {
        for (int i = 0; i < GuessedWord.Length; i++)
        {
             wordPlaceholderGenerator.wordLetters[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    #endregion
}
