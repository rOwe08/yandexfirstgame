﻿using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using DG.Tweening;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public GuessManager guessManager;
    public UIManager uiManager;
    public GameObject windowFinal;
    public WordPlaceholderGenerator wordPlaceholderGenerator;
    public SFXManager sfxManager;
    public HuggiWaggi huggiWaggi;

    public List<ParticleSystem> particleSystems;

    private TextMeshProUGUI textResultComponent;
    private TextMeshProUGUI textWordComponent;

    public ButtonGenerator buttonGenerator;

    public int countOfWrongGuesses;
    public int limitOfWrongGuesses;
    public int level;
    public int hp;
    public int score;

    void Awake()
    {
        limitOfWrongGuesses = 10;
        level = 0;
        hp = 3;
        score = 0;

        textResultComponent = windowFinal.transform.Find("ResultText").GetComponent<TextMeshProUGUI>();
        textWordComponent = windowFinal.transform.Find("WordText").GetComponent<TextMeshProUGUI>();

        buttonGenerator.Generate();

        windowFinal.SetActive(false);
        StartPlay();
    }

    public void StartPlay()
    {
        level++;
        guessManager.PrepareWord();
        huggiWaggi.ResetBody();
        DisableParticleSystem();

        AnimatePanelDisappear(windowFinal, () =>
        {
            countOfWrongGuesses = 0;
            buttonGenerator.SetActiveButtons(true);
            uiManager.UpdateUI();
        });
    }

    public void OpenFinalWindow(bool IsWin)
    {
        AnimatePanelAppear(windowFinal);
        buttonGenerator.SetActiveButtons(false);

        textWordComponent.text = "";

        Button buttonFinal = windowFinal.transform.Find("ButtonFinal").GetComponent<Button>();
        Button yandexButton = windowFinal.transform.Find("YandexButton").GetComponent<Button>();

        if (textResultComponent != null)
        {
            // Resetting the previous word letter objects
            wordPlaceholderGenerator.RemoveCreatedPlaceholders();

            if (IsWin)
            {
                sfxManager.PlaySound("activatingWinWindowSound");
                score += guessManager.guessedWord.Length;
                EnableParticleSystem();

                textResultComponent.text = "слово угадано!";
                buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "новое слово";
                buttonFinal.onClick.RemoveAllListeners();
                buttonFinal.onClick.AddListener(() => {
                    NextWordButtonClick();
                    sfxManager.PlaySound("clickButtonFinalSound");
                    DisableParticleSystem();
                });

                yandexButton.onClick.RemoveAllListeners();
                yandexButton.onClick.AddListener(() => {
                    WatchAdForX2();
                    sfxManager.PlaySound("clickButtonFinalSound");
                    DisableParticleSystem();
                });

            }
            else
            {
                textWordComponent.text = "Твое слово: " + guessManager.guessedWord;
                sfxManager.PlaySound("activatingLoseWindowSound");

                hp--;
                if (hp > 0)
                {
                    textResultComponent.text = "слово не угадано!";
                    buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "новое слово";
                    buttonFinal.onClick.RemoveAllListeners();
                    buttonFinal.onClick.AddListener(() => {
                        NextWordButtonClick();
                        sfxManager.PlaySound("clickButtonFinalSound");
                    });

                    yandexButton.onClick.RemoveAllListeners();
                    yandexButton.onClick.AddListener(() => {
                        WatchAdForHP();
                        sfxManager.PlaySound("clickButtonFinalSound");
                    });
                }
                else
                {
                    textResultComponent.text = "слово не угадано! \n не хватает жизней!";
                    buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "начать заново";
                    buttonFinal.onClick.RemoveAllListeners();
                    buttonFinal.onClick.AddListener(() => {
                        PlayAgainButtonClick();
                        sfxManager.PlaySound("clickButtonFinalSound");
                    });

                    yandexButton.onClick.RemoveAllListeners();
                    yandexButton.onClick.AddListener(() => {
                        WatchAdForHP();
                        sfxManager.PlaySound("clickButtonFinalSound");
                    });
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

    public void WatchAdForHP()
    {
        /// TODO:
        Debug.Log("+1 HP");
        hp++;
        StartPlay();
    }

    public void WatchAdForX2()
    {
        /// TODO:
        Debug.Log("X2 score");

        score += guessManager.guessedWord.Length;

        StartPlay();
    }

    public void PlayAgainButtonClick()
    {
        /// TODO:
        Debug.Log("restart game");

        // restart game
    }

    public void AnimatePanelAppear(GameObject panel)
    {
        panel.SetActive(false);

        panel.transform.localScale = Vector3.zero;

        panel.SetActive(true);
        panel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

    public void AnimatePanelDisappear(GameObject panel, System.Action callback)
    {
        panel.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            panel.SetActive(false);
            AnimateAllLetterButtons();
            AnimateAllWordLettersAppear();
            callback?.Invoke();
        });
    }

    public void AnimateAllWordLettersAppear()
    {
        wordPlaceholderGenerator.SetActiveWordLetters(true);
        foreach (GameObject wordLetter in wordPlaceholderGenerator.wordLetters)
        {
            wordPlaceholderGenerator.AnimateLetterAppear(wordLetter);
        }
    }

    public void AnimateAllLetterButtons()
    {
        foreach(GameObject buttonLetter in buttonGenerator.letterButtons)
        {
            buttonGenerator.AnimateButton(buttonLetter);
        }
    }
    void Start()
    {
        // Для примера, выключим Particle System при старте
        DisableParticleSystem();
    }

    public void EnableParticleSystem()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.enableEmission = true;
            sfxManager.PlaySound("confettiSound");
            particleSystem.Play();
        }
    }

    public void DisableParticleSystem()
    {

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.enableEmission = false;
            particleSystem.Stop();
        }
    }


}

