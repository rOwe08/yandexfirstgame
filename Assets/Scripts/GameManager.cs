using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GuessManager guessManager;
    public UIManager uiManager;
    public GameObject windowFinal;

    public ButtonGenerator buttonGenerator;

    public int countOfGuesses;
    public int limitOfGuesses;
    public int level;
    public int hp;
    public int score;

    void Awake()
    {
        limitOfGuesses = 10;
        level = 0;
        hp = 3;
        score = 0;

        buttonGenerator.Generate();
        StartPlay();
    }

    public void StartPlay()
    {
        level++;
        windowFinal.SetActive(false);

        countOfGuesses = 0;

        buttonGenerator.SetActiveButtons(true);

        guessManager.PrepareWord();

        uiManager.UpdateUI();
    }

    public void OpenFinalWindow(bool IsWin)
    {
        windowFinal.SetActive(true);
        buttonGenerator.SetActiveButtons(false);

        TextMeshProUGUI textResultComponent = windowFinal.transform.Find("ResultText").GetComponent<TextMeshProUGUI>();
        
        if (textResultComponent != null)
        {
            if (IsWin)
            {
                textResultComponent.text = "Победа!";
                score += guessManager.guessedWord.Length;
            }
            else
            {
                textResultComponent.text = "Поражение!";
                guessManager.RevealWord();
                hp--;
            }
        }
        else
        {
            Debug.LogError("Text component not found in children of windowFinal.");
        }
    }
}
