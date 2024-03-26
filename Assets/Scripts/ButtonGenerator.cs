using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject wordLetterPanelPrefab;

    private int numberOfColumns = 11;
    private int numberOfRows = 3;
    private float offsetXBetweenButtons;
    private float offsetYBetweenButtons;

    private float referenceResolutionX = 1920;
    private float referenceResolutionY = 1080;

    private float yDiffAnimation;

    public List<GameObject> letterButtons = new List<GameObject>();

    public void Generate()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float leftScreenSide = -(screenWidth / 2f);
        float offsetY = referenceResolutionY / 2f;
        float yForRow = 0;

        float buttonWidth = buttonPrefab.GetComponent<RectTransform>().rect.width;
        float buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;

        //buttonWidth = screenHeight * referenceRatio;
        //buttonHeight = buttonWidth;
        float totalWidthButtons = numberOfColumns * buttonWidth;
        float offsetBetweenButtons = buttonWidth / 2;
        float totalWidthWithOffset = totalWidthButtons + offsetBetweenButtons * (numberOfColumns - 1);
        float totalWidthForScreenOffset = screenWidth - totalWidthWithOffset;

        float x;

        offsetXBetweenButtons = buttonWidth + buttonWidth / 2;
        offsetYBetweenButtons = buttonHeight / 2;

        for (int row = 0; row < numberOfRows; row++)
        {
            x = leftScreenSide + totalWidthForScreenOffset / 2 + buttonWidth / 2;

            if (row == 0)
            {
                yForRow += offsetYBetweenButtons * 2;
            }
            else
            {
                yForRow += offsetYBetweenButtons;
            }

            yDiffAnimation = yForRow;
            for (int col = 0; col < numberOfColumns; col++)
            {
                GameObject newButton = Instantiate(buttonPrefab, transform);

                letterButtons.Add(newButton);
                float y = row * buttonHeight - offsetY;

                newButton.transform.localPosition = new Vector3(x, y + yForRow, 0f);

                char letter = GetRussianLetter(row, col);

                newButton.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();
                newButton.GetComponent<LetterButton>().letter = letter;

                x += offsetXBetweenButtons;
            }
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
