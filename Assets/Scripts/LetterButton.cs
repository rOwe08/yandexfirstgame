using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class LetterButton : MonoBehaviour
{
    public char letter;

    private GuessManager guessManager;
    private SFXManager sfxManager;

    void Start()
    {   
        GetComponent<Button>().onClick.AddListener(OnButtonClick);

        guessManager = FindObjectOfType<GuessManager>();
        sfxManager = FindObjectOfType<SFXManager>();

        if (guessManager == null)
        {
            Debug.LogError("GameManager is not initialized");
        }
    }

    public void OnButtonClick()
    {
        Debug.Log(letter);

        sfxManager.PlaySound("clickLetterButtonSound");
        guessManager.SelectLetter(letter);

        this.gameObject.SetActive(false);
    }
}
