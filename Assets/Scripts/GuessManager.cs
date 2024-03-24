using System.Collections.Generic;
using System.Linq;
using TMPro.Examples;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    public GameManager gameManager;

    public WordPlaceholderGenerator wordPlaceholderGenerator;

    public string guessedWord;
    public string guessedWordTheme;

    public int countOfGuessedLetters;

    public List<string> words;

    public Dictionary<string, List<string>> playableThemeWordMapper;
    public Dictionary<string, List<string>> originalThemeWordMapper = new Dictionary<string, List<string>>
    {
        { "Животные", new List<string> { "собака", "кот", "черепаха", "кролик", "хомяк", "лошадь" } },
        { "Еда", new List<string> { "яблоко", "банан", "пицца", "сосиска", "чипсы", "огурец" } },
        { "Аниме", new List<string> { "наруто", "блич", "ванпанчмен", "токийские мстители", "ван пис", "семья шпиона", "магическая битва" } },
        { "Супергерои", new List<string> { "железный человек", "тор", "халк", "флеш", "супермен", "человек паук", "бэтмен" } },
        { "Игрушки", new List<string> { "лего", "барби", "поп ит", "робот", "спиннер", "кубик рубика" } },
        { "Школа", new List<string> { "учитель", "карандаш", "рюкзак", "домашняя работа", "ручка", "перемена" } },
        { "Цвета", new List<string> { "красный", "оранжевый", "желтый", "зеленый", "синий", "фиолетовый" } },
        { "Космос", new List<string> { "звезда", "планета", "галактика", "комета", "космонавт", "ракета" } },
        { "Подводный мир", new List<string> { "рыба", "осьминог", "водоросли", "дельфин", "акула" } },
        { "Фантастика", new List<string> { "дракон", "фея", "волшебник", "единорог", "тролль", "огр" } },
        { "Спорт", new List<string> { "футбол", "баскетбол", "бейсбол", "теннис", "гимнастика", "гольф" } },
        { "Технологии", new List<string> { "ноутбук", "телефон", "гаджет", "планшет", "дрон", "интернет" } },
        { "Видеоигры", new List<string> { "майнкрафт", "роблокс", "фортнайт", "амонг ас", "контр страйк" } },
        { "Праздники", new List<string> { "рождество", "день рождения", "пасха", "день победы", "новый год" } },
        { "Природа", new List<string> { "цветок", "лес", "гора", "река", "пустыня", "вулкан", "море", "ураган" } },
    };

    public void PrepareWord()
    {
        countOfGuessedLetters = 0;

        ChooseWord();
    }

    public void ChooseWord()
    {
        if (playableThemeWordMapper is null)
        {
            playableThemeWordMapper = new Dictionary<string, List<string>>(originalThemeWordMapper);
        }

        // Select the theme
        var themeList = playableThemeWordMapper.Keys.ToList();
        var themeIndex = Random.Range(0, themeList.Count);
        guessedWordTheme = themeList[themeIndex];

        // Select the word from the theme

        // if the list is empty, reset it from the original mapper
        if (!playableThemeWordMapper[guessedWordTheme].Any())
        {
            playableThemeWordMapper[guessedWordTheme] = new List<string>(originalThemeWordMapper[guessedWordTheme]);
        }

        // Choose a random word from the theme
        int wordIndex = Random.Range(0, playableThemeWordMapper[guessedWordTheme].Count);
        guessedWord = playableThemeWordMapper[guessedWordTheme][wordIndex];

        // Remove the chosen word from the list
        playableThemeWordMapper[guessedWordTheme].RemoveAt(wordIndex);

        Debug.Log($"Выбранная тема: {guessedWordTheme}, выбранное слово: {guessedWord}");

        wordPlaceholderGenerator.Generate();
    }

    public void SelectLetter(char letter)
    {
        int nonSpaceCharIndex = -1;

        for (int i = 0; i < guessedWord.Length; i++)
        {
            if (guessedWord[i] == ' ')
            {
                continue;
            }

            nonSpaceCharIndex++;

            if (guessedWord[i] == letter)
            {
                wordPlaceholderGenerator.wordLetters[nonSpaceCharIndex].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        Guess(letter);
    }

    public void Guess(char letter)
    {
        bool IsGuessed = false;
        gameManager.countOfGuesses += 1;

        for(int i = 0; i < guessedWord.Length; i++)
        {
            if (guessedWord[i] == letter)
            {
                countOfGuessedLetters++;
                IsGuessed = true;
            }
        }

        if (!IsGuessed)
        {
            Debug.Log("Incorrect letter!");
            if(gameManager.countOfGuesses > gameManager.limitOfGuesses)
            {
                // window
                gameManager.OpenFinalWindow(false);
            }
        }
        else
        {
            Debug.Log("Correct letter!");
            if (countOfGuessedLetters == guessedWord.Length - guessedWord.Count(c => c == ' '))
            {
                Debug.Log("U win!");
                gameManager.OpenFinalWindow(true);
            }
        }
    }

    internal void RevealWord()
    {
        for (int i = 0; i < guessedWord.Length; i++)
        {
             wordPlaceholderGenerator.wordLetters[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
