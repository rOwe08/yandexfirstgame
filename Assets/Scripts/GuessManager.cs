﻿using System.Collections.Generic;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    public GameManager gameManager;

    public WordPlaceholderGenerator wordPlaceholderGenerator;

    public string guessedWord;
    public int countOfGuessedLetters;

    public List<string> words;

    public void PrepareWord()
    {
        countOfGuessedLetters = 0;

        ChooseWord();
    }

    public void ChooseWord()
    {
        if (words is null || words.Count == 0)
        {
            words = new List<string>
            {
               "майнкрафт", "барби", "футбол", "робот", "динозавр", "космос", "спорткар", "супергерой", "елса", "олаф",
                "принцесса", "пират", "смартфон", "планшет", "наушники", "скейтборд", "ролики", "лего", "бэтмен", "человекпаук",
                "йогурт", "кексы", "смузи", "трансформер", "пиксель", "эмодзи", "хэштег", "селфи", "ютуб", "блогер",
                "ангрибёрдс", "покемон", "зумер", "фортнайт", "симс", "пеппа", "мультфильм", "аниме", "марио", "луиджи",
                "соник", "хогвартс", "джедай", "ситх", "губка", "боб", "патрик", "смешарики", "лунтик", "фиксики",
                "нуб", "про", "влог", "дрифт", "сабвейсерф", "эльза", "дисней", "ниндзяго", "тролли", "бейблэйд",
                "гаджет", "лайк", "мирко", "дота", "шахматы", "кен", "модник", "футболка", "джинсы", "кроссовки",
                "фрисби", "конструктор", "звёзды", "снежок", "гарри", "поттер", "фродо", "хоббит", "эндермен", "крипер",
                "сайди", "мисс", "кэти", "герой", "квест", "меч", "щит", "волшебник", "дракон", "приключение",
                "киндер", "сюрприз", "лазертаг", "джумпер", "моана", "маугли", "неймар", "месси", "капитан", "америка"
            };
        }

        int wordIndex = Random.Range(0, words.Count - 1);
        guessedWord = words[wordIndex];
        Debug.Log($"Guessed word: {guessedWord}");
        words.RemoveAt(wordIndex);

        wordPlaceholderGenerator.Generate();
    }

    public void SelectLetter(char letter)
    {
        Guess(letter);

        for (int i = 0; i < guessedWord.Length; i++)
        {
            if (guessedWord[i] == letter)
            {
                wordPlaceholderGenerator.wordLetters[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
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
            if (countOfGuessedLetters == guessedWord.Length)
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
