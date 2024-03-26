using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GuessManager guessManager;
    public HuggiWaggi huggyWaggi;

    public ButtonGenerator buttonGenerator;
    public WordPlaceholderGenerator wordPlaceholderGenerator;

    public List<ParticleSystem> particleSystems;

    public GameObject levelText;
    public GameObject hpText;
    public GameObject scoreText;
    public GameObject themeText;
    public GameObject windowFinal;
    
    public TextMeshProUGUI textResultComponent;
    public TextMeshProUGUI textWordComponent;
    public Button buttonFinal;
    public Button yandexButton;

    public void Initialize()
    {
        textResultComponent = windowFinal.transform.Find("ResultText").GetComponent<TextMeshProUGUI>();
        textWordComponent = windowFinal.transform.Find("WordText").GetComponent<TextMeshProUGUI>();

        buttonFinal = windowFinal.transform.Find("ButtonFinal").GetComponent<Button>();
        yandexButton = windowFinal.transform.Find("YandexButton").GetComponent<Button>();

        buttonGenerator.Generate();

        windowFinal.SetActive(false);
    }

    public void StartUISession()
    {
        huggyWaggi.ResetBody();

        DisableParticleSystem();

        AnimatePanelDisappear(windowFinal, () =>
        {
            gameManager.countOfWrongGuesses = 0;
            buttonGenerator.SetActiveButtons(true);
            UpdateUI();
        });
    }

    public void UpdateUI()
    {
        levelText.GetComponent<TextMeshProUGUI>().text = "Уровень " + Convert.ToString(gameManager.level);
        hpText.GetComponent<TextMeshProUGUI>().text = "Жизни " + Convert.ToString(gameManager.hp);
        scoreText.GetComponent<TextMeshProUGUI>().text = "Очки " + Convert.ToString(gameManager.score);
        themeText.GetComponent<TextMeshProUGUI>().text = "Тема: " + Convert.ToString(guessManager.GuessedWordTheme);
    }

    public void OnRoundEnd()
    {
        AnimatePanelAppear(windowFinal);

        buttonGenerator.SetActiveButtons(false);
        
        textWordComponent.text = "";

        wordPlaceholderGenerator.RemoveCreatedPlaceholders();
    }

    public void OnRoundWin()
    {
        EnableParticleSystem();

        textResultComponent.text = "слово угадано!";
        buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "новое слово";
        buttonFinal.onClick.RemoveAllListeners();
        buttonFinal.onClick.AddListener(() =>
        {
            gameManager.NextWordButtonClick();
            gameManager.SaveData();
            DisableParticleSystem();
        });

        yandexButton.GetComponentInChildren<TextMeshProUGUI>().text = "смотреть рекламу за x2 очков";
        yandexButton.onClick.RemoveAllListeners();
        yandexButton.onClick.AddListener(() =>
        {
            gameManager.ExampleOpenRewardAd(2);
            DisableParticleSystem();
            gameManager.SaveData();
        });
    }

    public void OnRoundLose(bool isAlive)
    {
        textWordComponent.text = "Твое слово: " + guessManager.GuessedWord;
        yandexButton.GetComponentInChildren<TextMeshProUGUI>().text = "смотреть рекламу за +1 жизнь";

        if (isAlive)
        {
            OnRoundLoseWithHealth();
        }
        else
        {
            OnRoundLoseWithoutHealth();
        }
    }

    public void OnRoundLoseWithHealth()
    {
        textResultComponent.text = "слово не угадано!";
        buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "новое слово";
        buttonFinal.onClick.RemoveAllListeners();
        buttonFinal.onClick.AddListener(() =>
        {
            gameManager.NextWordButtonClick();
            gameManager.SaveData();
        });

        yandexButton.onClick.RemoveAllListeners();
        yandexButton.onClick.AddListener(() =>
        {
            gameManager.ExampleOpenRewardAd(1);
            gameManager.SaveData();
        });
    }

    public void OnRoundLoseWithoutHealth()
    {
        textResultComponent.text = "слово не угадано! \n не хватает жизней!";
        buttonFinal.GetComponentInChildren<TextMeshProUGUI>().text = "начать заново";
        buttonFinal.onClick.RemoveAllListeners();
        buttonFinal.onClick.AddListener(() =>
        {
            gameManager.PlayAgainButtonClick();
        });

        yandexButton.onClick.RemoveAllListeners();
        yandexButton.onClick.AddListener(() =>
        {
            gameManager.WatchAdForHP();
        });
    }

    public void DisplayHWNextPart()
    {
        huggyWaggi.ActivateNextPart();
    }

    public void EnableParticleSystem()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.enableEmission = true;
            // sfxManager.PlaySound("confettiSound");
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
        foreach (GameObject buttonLetter in buttonGenerator.letterButtons)
        {
            buttonGenerator.AnimateButton(buttonLetter);
        }
    }
}
