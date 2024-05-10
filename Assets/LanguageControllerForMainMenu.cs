using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LanguageControllerForMainMenu : MonoBehaviour
{
    public TextMeshProUGUI StartButtonText;
    public TextMeshProUGUI SettingsButtonText;
    public TextMeshProUGUI SkipButtonText;
    public TextMeshProUGUI SettingsPanelTitleText;
    public TextMeshProUGUI MusicSliderText;
    public TextMeshProUGUI SoundEffectsSliderText;
    public TextMeshProUGUI LanguageButtonText;
    public TextMeshProUGUI SensitivityText;
    public Image LanguageButtonImage;
    public Sprite LangTR;
    public Sprite LangEN;
    public TextMeshProUGUI BackButtonText;
    public AudioSource ClickSound;

    // Start is called before the first frame update

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Entry"))
            PlayerPrefs.SetInt("Entry", 1);
        CultureInfo ci = CultureInfo.CurrentCulture;
        if (ci.Name == "tr-TR" && PlayerPrefs.GetInt("Entry") == 1)
        {
            PlayerPrefs.SetInt("Entry", 2);
            PlayerPrefs.SetString("Language", "TR");
        }
        else
        {
            if (PlayerPrefs.GetInt("Entry") == 1)
            {
                PlayerPrefs.SetInt("Entry", 2);
                PlayerPrefs.SetString("Language", "EN");
            }
        }
        ChangeLanguage(0);
    }

    public void ChangeLanguage(int i)
    {

        if (i == 1)
        {
            ClickSound.Play();
            if (PlayerPrefs.GetString("Language") == "EN")
                PlayerPrefs.SetString("Language", "TR");
            else
                PlayerPrefs.SetString("Language", "EN");
        }
        if (PlayerPrefs.GetString("Language") == "EN")
        {
            StartButtonText.text = "Start";
            SettingsButtonText.text = "Settings";
            SkipButtonText.text = "Skip";
            SettingsPanelTitleText.text = "SETTINGS";
            MusicSliderText.text = "Music";
            SoundEffectsSliderText.text = "Sound Effects";
            LanguageButtonText.text = "Language";
            LanguageButtonImage.sprite = LangEN;
            BackButtonText.text = "Back";
            SensitivityText.text = "Sensitivity";
        }
        else
        {
            StartButtonText.text = "Basla";
            SettingsButtonText.text = "Ayarlar";
            SkipButtonText.text = "Atla";
            SettingsPanelTitleText.text = "AYARLAR";
            MusicSliderText.text = "Muzik";
            SoundEffectsSliderText.text = "Efektler";
            LanguageButtonText.text = "Dil";
            LanguageButtonImage.sprite = LangTR;
            BackButtonText.text = "Geri";
            SensitivityText.text = "Hassasiyet";
        }
    }
}
