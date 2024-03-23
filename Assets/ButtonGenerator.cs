using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    private int numberOfColumns = 11;
    private int numberOfRows = 3;
    private float offsetXBetweenButtons = 16f;
    private float offsetYBetweenButtons = 16f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateButtons();
    }

    void GenerateButtons()
    {
        float buttonWidth = buttonPrefab.GetComponent<RectTransform>().rect.width;
        float buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;

        float offsetX = (numberOfColumns * buttonWidth) / 2f;
        float offsetY = (numberOfRows * buttonHeight);
        float yForRow = 0;

        for (int row = 0; row < numberOfRows; row++)
        {
            float xForRow = 0;

            for (int col = 0; col < numberOfColumns; col++)
            {
                GameObject newButton = Instantiate(buttonPrefab, transform);

                float x = col * buttonWidth - offsetX;
                float y = row * buttonHeight - offsetY;

                newButton.transform.localPosition = new Vector3(xForRow + x, y + yForRow, 0f);

                char letter = GetRussianLetter(row, col);

                newButton.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();

                xForRow += offsetXBetweenButtons;
            }

            yForRow += offsetYBetweenButtons;
        }
    }

    char GetRussianLetter(int row, int col)
    {
        char[,] russianLetters = {
            {'à', 'á', 'â', 'ã', 'ä', 'å', '¸', 'æ', 'ç', 'è', 'é'},
            {'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ð', 'ñ', 'ò', 'ó', 'ô'},
            {'õ', 'ö', '÷', 'ø', 'ù', 'ú', 'û', 'ü', 'ý', 'þ', 'ÿ'}
        };

        int rowClamped = Mathf.Clamp(row, 0, russianLetters.GetLength(0) - 1);
        int colClamped = Mathf.Clamp(col, 0, russianLetters.GetLength(1) - 1);

        return russianLetters[rowClamped, colClamped];
    }
}
