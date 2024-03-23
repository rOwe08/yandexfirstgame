using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelText;
    public GameObject hpText;
    public GameObject scoreText;
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        levelText.GetComponent<TextMeshProUGUI>().text = "Level " + Convert.ToString(gameManager.level);
        hpText.GetComponent<TextMeshProUGUI>().text = "HP " + Convert.ToString(gameManager.hp);
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score " + Convert.ToString(gameManager.score);
    }
}
