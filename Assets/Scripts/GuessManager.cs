using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    public GameManager gameManager;
    public string word;
    public int countOfGuessedLetters;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ChooseOneWord()
    {
        int wordIndex = Random.Range(0, gameManager.words.Count - 1);
        string word = gameManager.words[wordIndex];

        gameManager.words.RemoveAt(wordIndex);

        return word;
    }

    public void Guess(char letter)
    {
        bool IsGuessed = false;
        gameManager.countOfGuesses += 1;

        for(int i = 0; i < word.Length; i++)
        {
            if (word[i] == letter)
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
            if (countOfGuessedLetters == word.Length)
            {
                Debug.Log("U win!");
                gameManager.OpenFinalWindow(true);
            }
        }
    }
}
