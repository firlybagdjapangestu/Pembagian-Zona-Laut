using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    [Header("Quiz Settings")]
    [SerializeField] private QuizSO[] questionBank;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI[] optionTexts;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject scoreDisplay;

    private int currentQuestionIndex = 0;
    private int score = 0;

    void Start()
    {
        scoreDisplay.SetActive(false);
        DisplayQuestion();
    }


    public void OnOptionSelected(int selectedIndex)
    {
        if (IsCorrectAnswer(selectedIndex))
        {
            score++;
        }

        if (HasNextQuestion())
        {
            currentQuestionIndex++;
            DisplayQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    private bool IsCorrectAnswer(int selectedIndex)
    {
        return questionBank[currentQuestionIndex].correctAnswerIndex == selectedIndex;
    }

    private bool HasNextQuestion()
    {
        return currentQuestionIndex < questionBank.Length - 1;
    }

    private void DisplayQuestion()
    {
        scoreText.text = (score * 10).ToString();

        QuizSO currentQuestion = questionBank[currentQuestionIndex];
        questionText.text = currentQuestion.question;

        string[] shuffledOptions = ShuffleOptions(currentQuestion.options);
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].text = shuffledOptions[i];
        }
    }

    private string[] ShuffleOptions(string[] options)
    {
        string[] shuffled = (string[])options.Clone();
        for (int i = 0; i < shuffled.Length; i++)
        {
            int randomIndex = Random.Range(0, shuffled.Length);
            string temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }
        return shuffled;
    }

    private void EndQuiz()
    {
        scoreDisplay.SetActive(true);
        // Additional actions for ending the quiz can be added here.
    }
}
