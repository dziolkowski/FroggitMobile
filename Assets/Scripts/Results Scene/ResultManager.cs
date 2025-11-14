using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public TMP_Text resultText; // Tekst z wynikiem
    public Image[] stars; // 3 gwiazdki
    public Sprite grayStar; // Szara gwiazdka
    public Sprite goldStar; // Zlota gwiazdka

    void Start()
    {
        // Pobieramy wynik z PlayerPrefs
        int score = PlayerPrefs.GetInt("Score", 0);
        resultText.text = score + "/21";

        UpdateStars(score);
    }

    // Ustawienia spritow gwiazdek
    private void UpdateStars(int score) 
    {
        // Na poczatek szare
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = grayStar;
        }

        // Logika ile gwiazdek ma byc zlote
        if (score >= 7)
            stars[0].sprite = goldStar;

        if (score >= 14)
            stars[1].sprite = goldStar;

        if (score >= 21)
            stars[2].sprite = goldStar;
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