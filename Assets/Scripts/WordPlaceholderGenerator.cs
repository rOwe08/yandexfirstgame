using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordPlaceholderGenerator : MonoBehaviour
{
    public GuessManager guessManager;

    public GameObject wordLetterPanelPrefab;

    public List<GameObject> wordLetters = new List<GameObject>();

    private float offsetXBetweenLetters;
    
    public void Generate()
    {
        float screenWidth = Screen.width;

        // Resetting the previous word letter objects
        if (wordLetters.Count > 0 )
        {
            RemoveCreatedPlaceholders();
        }

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

    private void RemoveCreatedPlaceholders()
    {
        foreach (GameObject placeHolder in wordLetters)
        {
            Destroy(placeHolder);
        }
        wordLetters.Clear();
    }
}
