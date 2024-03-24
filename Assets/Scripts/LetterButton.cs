using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using DG.Tweening;

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

        transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnButtonClick()
    {
        Debug.Log(letter);

        sfxManager.PlaySound("clickLetterButtonSound");
        guessManager.SelectLetter(letter);

        transform.DOScale(0f, 1f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
