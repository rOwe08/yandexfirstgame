﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject wordLetterPanelPrefab;

    private int numberOfColumns = 11;
    private int numberOfRows = 3;
    private float offsetXBetweenButtons;
    private float offsetYBetweenButtons;

    List<GameObject> letterButtons = new List<GameObject>();

    public void Generate()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float offsetX = screenWidth / 2f;
        float offsetY = screenHeight / 2f;
        float yForRow = 0;

        float buttonWidth = buttonPrefab.GetComponent<RectTransform>().rect.width;
        float buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;

        float totalWidthButtons = numberOfColumns * buttonWidth;

        float totalWidthForOffset = screenWidth - totalWidthButtons;

        offsetXBetweenButtons = totalWidthForOffset / (numberOfColumns);
        offsetYBetweenButtons = buttonHeight / 2;

        for (int row = 0; row < numberOfRows; row++)
        {
            float xForRow = 0;
            if (row == 0)
            {
                yForRow += offsetYBetweenButtons * 2;
            }
            else
            {
                yForRow += offsetYBetweenButtons;
            }

            for (int col = 0; col < numberOfColumns; col++)
            {
                xForRow += offsetXBetweenButtons;

                GameObject newButton = Instantiate(buttonPrefab, transform);

                letterButtons.Add(newButton);

                float x = col * buttonWidth - offsetX;
                float y = row * buttonHeight - offsetY;

                newButton.transform.localPosition = new Vector3(xForRow + x, y + yForRow, 0f);

                char letter = GetRussianLetter(row, col);

                newButton.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();
                newButton.GetComponent<LetterButton>().letter = letter;
            }
        }
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
}
