using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuiz", menuName = "Quiz/Create New Quiz")]
public class QuizSO : ScriptableObject
{
    [TextArea(3, 5)]
    public string question; // Pertanyaan

    public string[] options = new string[4]; // Opsi jawaban (4 opsi)

    [Range(0, 3)]
    public int correctAnswerIndex; // Indeks jawaban benar (0-3)
}
