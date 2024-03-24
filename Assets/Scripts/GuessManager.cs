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
        { "Животные", new List<string> { "щенок", "котенок", "черепаха", "кролик", "хомяк", "пони" } },
        { "Фрукты", new List<string> { "яблоко", "банан", "вишня", "финик", "киви", "манго" } },
        { "Мультгерои", new List<string> { "Эльза", "Моана", "Человек-паук", "Пикачу", "Губка Боб", "Бэтмен" } },
        { "Супергерои", new List<string> { "Железный человек", "Тор", "Халк", "Флеш", "Супермен", "Бэтгерл" } },
        { "Игрушки", new List<string> { "лего", "барби", "единорог", "робот", "спиннер", "головоломка" } },
        { "Школа", new List<string> { "учитель", "карандаш", "рюкзак", "домашняя работа", "мелки", "ланчбокс" } },
        { "Цвета", new List<string> { "красный", "оранжевый", "желтый", "зеленый", "синий", "фиолетовый" } },
        { "Космос", new List<string> { "звезда", "планета", "галактика", "комета", "космонавт", "ракета" } },
        { "Подводный мир", new List<string> { "рыба", "коралл", "осьминог", "водоросли", "дельфин", "акула" } },
        { "Фантастика", new List<string> { "дракон", "фея", "волшебник", "единорог", "тролль", "огр" } },
        { "Спорт", new List<string> { "футбол", "баскетбол", "бейсбол", "теннис", "гимнастика", "катание" } },
        { "Технологии", new List<string> { "ноутбук", "робот", "гаджет", "планшет", "дрон", "приложение" } },
        { "Видеоигры", new List<string> { "Майнкрафт", "Роблокс", "Фортнайт", "Амонг Ас", "Покемон", "Зельда" } },
        { "Праздники", new List<string> { "рождество", "хэллоуин", "пасха", "благодарение", "валентинов день", "новый год" } },
        { "Природа", new List<string> { "цветок", "лес", "гора", "река", "пустыня", "вулкан" } },
        // Добавляйте больше тем и слов по мере необходимости.
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
