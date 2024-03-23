using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    private int numberOfColumns = 11;
    private int numberOfRows = 3;
    private float offsetXBetweenButtons;
    private float offsetYBetweenButtons;

    // Start is called before the first frame update
    void Start()
    {
        GenerateButtons();
    }

    void GenerateButtons()
    {
        float buttonWidth = buttonPrefab.GetComponent<RectTransform>().rect.width;
        float buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float totalWidthButtons = numberOfColumns * buttonWidth;

        float totalWidthForOffset = screenWidth - totalWidthButtons;

        offsetXBetweenButtons = totalWidthForOffset / (numberOfColumns);
        offsetYBetweenButtons = buttonHeight / 2;

        float offsetX = screenWidth / 2f;
        float offsetY = screenHeight / 2f;
        float yForRow = 0;

        for (int row = 0; row < numberOfRows; row++)
        {
            float xForRow = 0;
            if(row == 0)
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

                float x = col * buttonWidth - offsetX;
                float y = row * buttonHeight - offsetY;

                newButton.transform.localPosition = new Vector3(xForRow + x, y + yForRow, 0f);

                char letter = GetRussianLetter(row, col);

                newButton.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();
            }
        }
    }

    char GetRussianLetter(int row, int col)
    {
        char[,] russianLetters = {
            {'õ', 'ö', '÷', 'ø', 'ù', 'ú', 'û', 'ü', 'ý', 'þ', 'ÿ'},
            {'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ð', 'ñ', 'ò', 'ó', 'ô'},
            {'à', 'á', 'â', 'ã', 'ä', 'å', '¸', 'æ', 'ç', 'è', 'é'}
        };

        return russianLetters[row, col];
    }
}
