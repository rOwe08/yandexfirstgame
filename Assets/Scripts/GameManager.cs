using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GuessManager guessManager;
    public UIManager uiManager;
    public GameObject windowFinal;
    public WordPlaceholderGenerator wordPlaceholderGenerator;

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
        Button buttonFinal = windowFinal.transform.Find("ButtonFinal").GetComponent<Button>();

        if (textResultComponent != null)
        {
            // Resetting the previous word letter objects
            wordPlaceholderGenerator.RemoveCreatedPlaceholders();

            if (IsWin)
            {
                textResultComponent.text = "слово угадано!";
                score += guessManager.guessedWord.Length;

                buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "новое слово";
                buttonFinal.onClick.RemoveAllListeners();
                buttonFinal.GetComponent<Button>().onClick.AddListener(NextWordButtonClick);
            }
            else
            {
                //guessManager.RevealWord();   // TODO: Just to show the word as a text in windowFinal gameobject
                hp--;
                if (hp > 0)
                {
                    textResultComponent.text = "слово не угадано!";
                    buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "новое слово";
                    buttonFinal.onClick.RemoveAllListeners();
                    buttonFinal.GetComponent<Button>().onClick.AddListener(NextWordButtonClick);
                }
                else
                {
                    textResultComponent.text = "слово не угадано! \n не хватает жизней!";
                    buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "начать заново";
                    buttonFinal.onClick.RemoveAllListeners();
                    buttonFinal.GetComponent<Button>().onClick.AddListener(PlayAgainButtonClick);
                }

            }
        }
        else
        {
            Debug.LogError("Text component not found in children of windowFinal.");
        }
    }

    public void NextWordButtonClick()
    {
        StartPlay();
    }

    public void PlayAgainButtonClick()
    {
        /// TODO:
        Debug.Log("U gei watch the ad");
    }
}

