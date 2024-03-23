using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int countOfGuesses;
    public int limitOfGuesses;
    public GuessManager guessManager;
    public UIManager uiManager;
    public ButtonGenerator buttonGenerator;
    public GameObject windowFinal;
    public int level;
    public int hp;
    public int score;

    private bool IsFirstTime = false;
    // Start is called before the first frame update
    void Awake()
    {
        limitOfGuesses = 10;
        level = 0;
        hp = 3;
        score = 0;

        guessManager.StartPlay();
        buttonGenerator.Generate();
        StartPlay();
    }

    public void GetLetter(char letter)
    {
        guessManager.Guess(letter);

        for(int i = 0; i < guessManager.guessedWord.Length; i++)
        {
            if (guessManager.guessedWord[i] == letter)
            {
                buttonGenerator.wordLetters[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPlay()
    {
        if (!IsFirstTime)
        {
            IsFirstTime = true;
        }
        else
        {
            guessManager.StartPlay();
        }

        level++;
        windowFinal.SetActive(false);

        countOfGuesses = 0;

        buttonGenerator.SetActiveButtons(true);

        uiManager.UpdateUI();
    }

    public void OpenFinalWindow(bool IsWin)
    {
        windowFinal.SetActive(true);
        buttonGenerator.SetActiveButtons(false);

        TextMeshProUGUI textResultComponent = windowFinal.transform.Find("ResultText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textWordComponent = windowFinal.transform.Find("WordText").GetComponent<TextMeshProUGUI>();

        textWordComponent.text = "Твое слово: " + guessManager.guessedWord;
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
                hp--;
            }
        }
        else
        {
            Debug.LogError("Text component not found in children of windowFinal.");
        }
    }
}
