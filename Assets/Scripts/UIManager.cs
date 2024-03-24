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
        themeText.GetComponent<TextMeshProUGUI>().text = "Тема: " + Convert.ToString(guessManager.guessedWordTheme);
    }
}
