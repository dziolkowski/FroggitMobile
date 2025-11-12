using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionData> allQuestions; // Wszystkie pytania 
    private List<QuestionData> quizQuestions; // Wylosowane pytania do rundy

    public TMP_Text questionText; // Tekst pytania
    public Button[] answerButtons; // Cztery przyciski odpowiedzi
    public TMP_Text questionCounterText; // Tekst np. "2/21"
    public Slider timerSlider; // Pasek czasu

    private int currentQuestionIndex = 0; // Index aktualnego pytania
    private int correctAnswers = 0; // Liczba poprawnych odpowiedzi
    private float timePerQuestion = 10f; // Czas na jedno pytanie 
    private float timer; // Licznik czasu

    private QuestionData currentQuestion; // Aktualne pytanie

    void Start()
    {
        // Kopiujemy liste pytan i losujemy kolejnosc
        quizQuestions = new List<QuestionData>(allQuestions);
        quizQuestions.Shuffle();

        // Jezeli jest wiecej niz 21, bierzemy tylko 21
        if (quizQuestions.Count > 21)
            quizQuestions = quizQuestions.GetRange(0, 21);

        LoadNextQuestion();
    }

    void Update()
    {
        // Odliczanie czasu
        timer -= Time.deltaTime;
        timerSlider.value = timer;// / timePerQuestion;

        // Jezeli czas sie skonczyl, przechodzimy dalej
        if (timer <= 0f)
            NextQuestion();
    }

    // Funkcja wywolywana po kliknieciu przycisku odpowiedzi
    public void OnAnswerSelected(int index)
    {
        // Sprawdzamy czy odpowiedz poprawna
        if (index == currentQuestion.correctAnswerIndex)
            correctAnswers++;

        NextQuestion();
    }

    // Zaladowanie nastepnego pytania
    void LoadNextQuestion()
    {
        // Jezeli nie ma wiecej pytan, przechodzimy do ekranu wynikow
        if (currentQuestionIndex >= quizQuestions.Count)
        {
            PlayerPrefs.SetInt("CorrectAnswers", correctAnswers);
            SceneManager.LoadScene("ResultsScene");
            return;
        }

        currentQuestion = quizQuestions[currentQuestionIndex];
        currentQuestionIndex++;

        // Losujemy kolejnosc odpowiedzi
        List<int> order = new List<int> { 0, 1, 2, 3 };
        order.Shuffle();

        // Ustawiamy tresc pytania
        questionText.text = currentQuestion.question;

        // Ustawiamy teksty przyciskow i ich akcje
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = order[i];
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[index];
            int capturedIndex = index; // Potrzebne, zeby uniknac bledow delegacji
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(capturedIndex));
        }

        // Aktualizujemy licznik pytan
        questionCounterText.text = $"{currentQuestionIndex}/{quizQuestions.Count}";

        // Resetujemy licznik czasu
        timer = timePerQuestion;
    }

    // Przechodzi do nastepnego pytania
    void NextQuestion()
    {
        LoadNextQuestion();
    }
}
