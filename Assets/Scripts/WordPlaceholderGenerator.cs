﻿using System.Collections.Generic;
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

        string guessedWord = guessManager.guessedWord;
        float leftScreenSide = -(screenWidth / 2f);
        int numOfLetters = guessedWord.Length;

        float wordLetterWidth = wordLetterPanelPrefab.GetComponent<RectTransform>().rect.width;
        float totalWidthWordLetter = numOfLetters * wordLetterWidth;

        float offsetBetweenLetters = wordLetterWidth / 2;
        float totalWidthWithOffset = totalWidthWordLetter + offsetBetweenLetters * (numOfLetters - 1);
        float totalWidthForScreenOffset = screenWidth - totalWidthWithOffset;

        float x = leftScreenSide + totalWidthForScreenOffset / 2 ;

        offsetXBetweenLetters = wordLetterWidth + wordLetterWidth / 2;

        for (int i = 0; i < numOfLetters; i++)
        {
            if (guessedWord[i] != ' ')
            {
                GameObject newWordLetter = Instantiate(wordLetterPanelPrefab, transform);

                wordLetters.Add(newWordLetter);

                newWordLetter.transform.localPosition = new Vector3(x, (wordLetterWidth / 2), 0f);

                newWordLetter.GetComponentInChildren<TextMeshProUGUI>().text = guessedWord[i].ToString();
                newWordLetter.transform.GetChild(0).gameObject.SetActive(false);

            }

            x += offsetXBetweenLetters;
        }
    }

    public void RemoveCreatedPlaceholders()
    {
        foreach (GameObject placeHolder in wordLetters)
        {
            Destroy(placeHolder);
        }
        wordLetters.Clear();
    }
}
