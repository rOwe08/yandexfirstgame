using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterButton : MonoBehaviour
{
    public char letter;

    private GuessManager guessManager;

    void Start()
    {   
        GetComponent<Button>().onClick.AddListener(OnButtonClick);

        guessManager = FindObjectOfType<GuessManager>();
        if (guessManager == null)
        {
            Debug.LogError("GameManager is not initialized");
        }
    }

    public void OnButtonClick()
    {
        Debug.Log(letter);

        guessManager.SelectLetter(letter);

        this.gameObject.SetActive(false);
    }
}
