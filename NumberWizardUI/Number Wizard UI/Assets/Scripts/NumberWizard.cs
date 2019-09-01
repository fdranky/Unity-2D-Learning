using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading;

public class NumberWizard : MonoBehaviour
{

    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] TextMeshProUGUI guessText;

    HashSet<int> guesses = new HashSet<int>();
    int guess;

    // Use this for initialization
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        guesses.Clear();
        guess = Random.Range(min, max);
        guesses.Add(guess);
        guessText.text = guess.ToString();
        max = max + 1;
    }

    public void OnPressHigher()
    {
        min = guess;
        NextGuess();
    }

    public void OnPressLower()
    {
        max = guess;
        NextGuess();
    }
    
    void NextGuess()
    {
        guess = Random.Range(min, max + 1);

        if (guesses.Contains(guess))
        {
            guessText.text = "Let me try again...";
            max = 1000;
            min = 1;
            StartGame();
        }
        else
        {
            guesses.Add(guess);
            guessText.text = guess.ToString();
        }

    }
}