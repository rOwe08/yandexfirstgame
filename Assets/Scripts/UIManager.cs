using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelText;
    public GameObject hpText;
    public GameObject scoreText;
    public GameObject themeText;
    public GameManager gameManager;
    public GuessManager guessManager;

    public void UpdateUI()
    {
        levelText.GetComponent<TextMeshProUGUI>().text = "Уровень " + Convert.ToString(gameManager.level);
        hpText.GetComponent<TextMeshProUGUI>().text = "Жизни " + Convert.ToString(gameManager.hp);
        scoreText.GetComponent<TextMeshProUGUI>().text = "Очки " + Convert.ToString(gameManager.score);
        themeText.GetComponent<TextMeshProUGUI>().text = "Тема: " + Convert.ToString(guessManager.GuessedWordTheme);
    }

  /*  public void OpenFinalWindow(bool IsWin)
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
                level++;
                score += guessManager.guessedWord.Length;

                sfxManager.PlaySound("activatingWinWindowSound");
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
    }*/

}
