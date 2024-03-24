using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelText;
    public GameObject hpText;
    public GameObject scoreText;
    public GameManager gameManager;

    public void UpdateUI()
    {
        levelText.GetComponent<TextMeshProUGUI>().text = "Уровень " + Convert.ToString(gameManager.level);
        hpText.GetComponent<TextMeshProUGUI>().text = "Жизни " + Convert.ToString(gameManager.hp);
        scoreText.GetComponent<TextMeshProUGUI>().text = "Очки " + Convert.ToString(gameManager.score);
    }
}
