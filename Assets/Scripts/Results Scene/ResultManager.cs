using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public TMP_Text resultText; // Tekst z wynikiem
    public Image[] squares; // 3 kwadraty (obrazy)
    public Color activeColor = Color.green; // Kolor aktywny 
    public Color inactiveColor = Color.yellow; // Kolor nieaktywny 

    void Start()
    {
        // Pobieramy wynik z PlayerPrefs
        int correct = PlayerPrefs.GetInt("CorrectAnswers", 0);
        resultText.text = $"{correct}/21";

        // Reset kolorow
        for (int i = 0; i < squares.Length; i++)
            squares[i].color = inactiveColor;

        // Podswietlamy odpowiednie kwadraty w zaleznosci od wyniku
        if (correct >= 7) squares[0].color = activeColor;
        if (correct >= 14) squares[1].color = activeColor;
        if (correct >= 21) squares[2].color = activeColor;
    }

    // Uruchamia nowa gre
    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Powrot do glownego menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}