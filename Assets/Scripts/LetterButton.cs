using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LetterButton : MonoBehaviour
{
    public char letter;

    private GameManager gameManager;

    private Button letterButton;

    void Start()
    {
        letterButton = GetComponent<Button>();
        letterButton.onClick.AddListener(OnButtonClick);

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager is not initialized");
        }

        transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnButtonClick()
    {
        letterButton.interactable = false;

        // call game manager to handle the button click
        gameManager.OnLetterSelect(letter);

        // animate the button fade
        transform.DOScale(0f, 1f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
