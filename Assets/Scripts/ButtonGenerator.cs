using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject wordLetterPanelPrefab;

    private int numberOfColumns = 11;
    private int numberOfRows = 3;
    private float offsetXBetweenButtons;
    private float offsetYBetweenButtons;

    public List<GameObject> letterButtons = new List<GameObject>();

    public void Generate()
    {
        float screenWidth = 1920f;
        RectTransform canvasRect = GetComponent<RectTransform>();

        float buttonWidth = buttonPrefab.GetComponent<RectTransform>().rect.width;
        float buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;

        float totalWidthButtons = numberOfColumns * buttonWidth;
        float totalHeightButtons = numberOfRows * buttonHeight;

        //float startX = buttonWidth / 2 + (screenWidth - totalWidthButtons - (buttonWidth / 2) * (numberOfColumns - 1)) / 2;

        float startX = buttonWidth / 2 - screenWidth / 2 + (screenWidth - totalWidthButtons - (buttonWidth / 2) * (numberOfColumns - 1)) / 2;
        float startY = buttonHeight / 2;

        float x = startX;
        float y = startY;

        for (int row = 0; row < numberOfRows; row++)
        {
            for (int col = 0; col < numberOfColumns; col++)
            {
                GameObject newButton = Instantiate(buttonPrefab, transform);

                letterButtons.Add(newButton);

                RectTransform buttonRect = newButton.GetComponent<RectTransform>();

                // Set anchorMin and anchorMax to the bottom of canvas
                buttonRect.anchorMin = new Vector2(0.5f, 0);
                buttonRect.anchorMax = new Vector2(0.5f, 0);

                // Set anchoredPosition to position relative to anchorMin and anchorMax
                buttonRect.anchoredPosition = new Vector2(x, y);

                char letter = GetRussianLetter(row, col);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();
                newButton.GetComponent<LetterButton>().letter = letter;

                x += buttonWidth * 1.5f;
            }

            x = startX;
            y += buttonHeight * 1.5f;
        }

        SetActiveButtons(false);
    }


    private char GetRussianLetter(int row, int col)
    {
        char[,] russianLetters = {
            {'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'},
            {'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф'},
            {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й'}
        };

        return russianLetters[row, col];
    }

    public void SetActiveButtons(bool IsActive)
    {
        foreach(GameObject button in letterButtons)
        {
            button.SetActive(IsActive);

            // Now, get the Button component and set its 'interactable' property
            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null) // Check if the GameObject actually has a Button component
            {
                buttonComponent.interactable = IsActive;
            }
        }
    }

    public void AnimateButton(GameObject buttonLetter)
    {
        float x_final = buttonLetter.transform.localPosition.x;
        float y_final = buttonLetter.transform.localPosition.y;

        buttonLetter.transform.localPosition = new Vector3(x_final, y_final - offsetYBetweenButtons, 0f);

        buttonLetter.transform.DOLocalMoveY(y_final, 1f).SetEase(Ease.OutBounce);
    }
}
