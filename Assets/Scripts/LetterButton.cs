using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterButton : MonoBehaviour
{
    public char letter;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {   
        GetComponent<Button>().onClick.AddListener(onClick);

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager не найден в сцене!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        Debug.Log(letter);

        gameManager.GetLetter(letter);

        this.gameObject.SetActive(false);
    }
}
