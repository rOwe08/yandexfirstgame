using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    public GuessManager guessManager;

    public GameObject buttonPrefab;
    public GameObject wordLetterPanelPrefab;

    private int numberOfColumns = 11;
    private int numberOfRows = 3;
    private float offsetXBetweenButtons;
    private float offsetYBetweenButtons;

    private float offsetXBetweenLetters;

    List<GameObject> letterButtons = new List<GameObject>();
    public List<GameObject> wordLetters = new List<GameObject>();
    public void Generate()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        GenerateLetters(screenWidth, screenHeight);
        GenerateWord(screenWidth);
    }

    private void GenerateWord(float screenWidth)
    {
        string quessedWord = guessManager.guessedWord;
        float offsetX = screenWidth / 2f;
        int numOfLetters = quessedWord.Length;

        float wordLetterWidth = wordLetterPanelPrefab.GetComponent<RectTransform>().rect.width;
        float totalWidthWordLetter = numOfLetters * wordLetterWidth;

        float totalWidthForOverallOffset = screenWidth - totalWidthWordLetter;
        float offsetBetweenLetters = wordLetterWidth / 2;
        float totalWidthWithOffset = totalWidthWordLetter + offsetBetweenLetters * (numOfLetters - 1);
        float totalWidthForScreenOffset = screenWidth - totalWidthWithOffset;

        float x = -(screenWidth / 2) + totalWidthForScreenOffset / 2 + wordLetterWidth / 2;


        offsetXBetweenLetters = wordLetterWidth + wordLetterWidth / 2;

        for (int i = 0; i < numOfLetters; i++)
        {
            GameObject newWordLetter = Instantiate(wordLetterPanelPrefab, transform);

            wordLetters.Add(newWordLetter);

            newWordLetter.transform.localPosition = new Vector3(x, (wordLetterWidth / 2), 0f);

            newWordLetter.GetComponentInChildren<TextMeshProUGUI>().text = quessedWord[i].ToString();
            newWordLetter.transform.GetChild(0).gameObject.SetActive(false);

            x += offsetXBetweenLetters;
        }
        
    }

    private void GenerateLetters(float screenWidth, float screenHeight)
    {
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
    char GetRussianLetter(int row, int col)
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
