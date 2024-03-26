using DG.Tweening.Core.Easing;
using System;
using UnityEngine;
using YG;

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
        LoadData();

        uiManager.Initialize();

        StartPlay();
    }


    public void StartPlay()
    {
        SaveData();
        guessManager.ChooseWord();
        uiManager.StartUISession();
    }

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    void Rewarded(int id)
    {
        if (id == 1)
        {
            WatchAdForHP();
        }
        else if (id == 2)
        {
            WatchAdForX2();
        }
    }

    public void LoadData()
    {
        level = YandexGame.savesData.levelDataSave;
        hp = YandexGame.savesData.hpDataSave;
        score = YandexGame.savesData.scoreDataSave;
    }

    public void SaveData()
    {
        YandexGame.savesData.levelDataSave = level;
        YandexGame.savesData.hpDataSave = hp;
        YandexGame.savesData.scoreDataSave = score;
        YandexGame.SaveProgress();
    }
    public void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
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
        SaveData();
    }

    public void NextWordButtonClick()
    {
        sfxManager.PlaySound("clickButtonFinalSound");

        StartPlay();
    }

    public void WatchAdForHP()
    {
        Debug.Log("+1 HP");

        sfxManager.PlaySound("clickButtonFinalSound");

        hp++;
        StartPlay();
    }

    public void WatchAdForX2()
    {
        Debug.Log("X2 score");

        sfxManager.PlaySound("clickButtonFinalSound");

        score += guessManager.GuessedWord.Length;

        StartPlay();
    }
}

