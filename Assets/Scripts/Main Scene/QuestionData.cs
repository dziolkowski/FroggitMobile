using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Quiz/Question")]
public class QuestionData : ScriptableObject
{
    [TextArea]
    public string question; // Tresc pytania
    public string[] answers = new string[4]; // Cztery odpowiedzi
    public int correctAnswerIndex; // Index poprawnej odpowiedzi 
}
