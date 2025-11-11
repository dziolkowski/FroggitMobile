using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;// Panel z menu pauzy
    public Slider musicSlider;// Suwak muzyki
    public Slider sfxSlider;// Suwak efektow dzwiekowych
    public TMP_Text musicValueText;// Tekst pokazujacy wartosc muzyki 
    public TMP_Text sfxValueText;// Tekst pokazujacy wartosc efektow 

    private AudioSource musicSource;// Zrodlo muzyki (tag "Music")

    void Start()
    {
        // Znajdz obiekt z muzyka w tle
        musicSource = GameObject.FindGameObjectWithTag("Music")?.GetComponent<AudioSource>();

        // Wczytaj zapisane ustawienia
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        // Zaktualizuj teksty procentowe
        UpdateVolumeTexts();

        // Ustawienie glosnosci
        ApplyVolumeSettings();

        // Ukryj menu na start
        pauseMenuUI.SetActive(false);

        // Podlacz zdarzenia do suwakow
        musicSlider.onValueChanged.AddListener(delegate { OnMusicSliderChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnSFXSliderChanged(); });
    }

    void Update()
    {
        // Aktualizacja glosnosci muzyki w czasie rzeczywistym
        if (musicSource != null)
            musicSource.volume = musicSlider.value;

        // Zapisanie ustawien
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    // Otwiera menu pauzy
    public void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // Zamkniecie menu pauzy (przycisk Return)
    public void ClosePauseMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Wyjscie do glownego menu (przycisk Exit)
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Zmiana glosnosci muzyki
    private void OnMusicSliderChanged()
    {
        ApplyVolumeSettings();
        UpdateVolumeTexts();
    }

    // Zmiana glosnosci efektow
    private void OnSFXSliderChanged()
    {
        UpdateVolumeTexts();
    }

    // Ustawienie glosnosci muzyki
    private void ApplyVolumeSettings()
    {
        if (musicSource != null)
            musicSource.volume = musicSlider.value;
    }

    // Aktualizacja tekstow procentowych obok suwakow
    private void UpdateVolumeTexts()
    {
        if (musicValueText != null)
            musicValueText.text = Mathf.RoundToInt(musicSlider.value * 100f) + "%";

        if (sfxValueText != null)
            sfxValueText.text = Mathf.RoundToInt(sfxSlider.value * 100f) + "%";
    }
}
