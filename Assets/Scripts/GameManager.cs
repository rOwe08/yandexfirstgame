using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> words;
    public int countOfGuesses;
    public int limitOfGuesses;
    public GuessManager guessManager;
    public ButtonGenerator buttonGenerator;
    public GameObject windowFinal;

    // Start is called before the first frame update
    void Awake()
    {
        limitOfGuesses = 10;
        words = new List<string>
        {
            "а",
            "б",
            "в"
        };
        buttonGenerator.GenerateButtons();
        StartPlay();
    }

    public void GetLetter(char letter)
    {
        guessManager.Guess(letter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPlay()
    {
        windowFinal.SetActive(false);

        countOfGuesses = 0;
        guessManager.word = guessManager.ChooseOneWord();
        guessManager.countOfGuessedLetters = 0;

        buttonGenerator.SetActiveButtons(true);
    }

    public void OpenFinalWindow(bool IsWin)
    {
        windowFinal.SetActive(true);
        buttonGenerator.SetActiveButtons(false);

        TextMeshProUGUI textComponent = windowFinal.transform.Find("ResultText").GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
            if (IsWin)
            {
                textComponent.text = "Победа!";
            }
            else
            {
                textComponent.text = "Проигрыш!";
            }
        }
        else
        {
            Debug.LogError("Text component not found in children of windowFinal.");
        }
    }
}
