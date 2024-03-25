using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int limitOfWrongGuesses = 10;

    public GuessManager guessManager;
    public UIManager uiManager;
    public SFXManager sfxManager;

    public int countOfWrongGuesses;
    public int level;
    public int hp;
    public int score;

    void Awake()
    {
        ResetGameVariables();

        uiManager.Initialize();

        StartPlay();
    }


    public void StartPlay()
    {
        guessManager.ChooseWord();

        uiManager.StartUISession();
    }

    public void PlayAgainButtonClick()
    {
        Debug.Log("restart game");

        sfxManager.PlaySound("clickButtonFinalSound");

        // reset the player game variables
        ResetGameVariables();

        StartPlay();
    }

    private void ResetGameVariables()
    {
        level = 1;
        hp = 3;
        score = 0;
    }

    public void OnLetterSelect(char letter)
    {
        // play the button clicked sound effect
        sfxManager.PlaySound("clickLetterButtonSound");

        // check if selected letter is correct
        bool IsCorrectlyGuessed = guessManager.SelectLetter(letter);

        // if wrong guess
        if (!IsCorrectlyGuessed)
        {
            Debug.Log("Incorrect letter!");

            sfxManager.PlaySound("incorrectLetterSound");
            
            uiManager.DisplayHWNextPart();

            countOfWrongGuesses += 1;
        }
        else
        {
            Debug.Log("Correct letter!");
        }

        if (countOfWrongGuesses >= limitOfWrongGuesses)
        {
            OnRoundEnd(false);
        }
        else if (guessManager.CountOfCorrectGuesses == guessManager.GuessedWordTrueLength)
        {
            OnRoundEnd(true);
        }
    }

    private void OnRoundEnd(bool isWin)
    {
        uiManager.OnRoundEnd();

        if (isWin)
        {
            sfxManager.PlaySound("activatingWinWindowSound");

            level++;
            score += guessManager.GuessedWordTrueLength;

            uiManager.OnRoundWin();
        }
        else
        {
            sfxManager.PlaySound("activatingLoseWindowSound");

            hp--;
            if (hp > 0)
            {
                uiManager.OnRoundLose(isAlive: true);
            }
            else
            {
                uiManager.OnRoundLose(isAlive: false);
            }
        }
    }

    public void NextWordButtonClick()
    {
        sfxManager.PlaySound("clickButtonFinalSound");

        StartPlay();
    }

    public void WatchAdForHP()
    {
        /// TODO:
        Debug.Log("+1 HP");

        sfxManager.PlaySound("clickButtonFinalSound");

        hp++;
        StartPlay();
    }

    public void WatchAdForX2()
    {
        /// TODO:
        Debug.Log("X2 score");

        sfxManager.PlaySound("clickButtonFinalSound");

        score += guessManager.GuessedWord.Length;

        StartPlay();
    }
}

