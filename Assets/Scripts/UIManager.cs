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
        levelText.GetComponent<TextMeshProUGUI>().text = "Level " + Convert.ToString(gameManager.level);
        hpText.GetComponent<TextMeshProUGUI>().text = "HP " + Convert.ToString(gameManager.hp);
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score " + Convert.ToString(gameManager.score);
    }
}
