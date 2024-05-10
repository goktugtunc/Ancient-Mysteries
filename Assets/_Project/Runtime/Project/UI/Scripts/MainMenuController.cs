using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public AudioSource MainMenuMusic;
    public AudioSource MainMenuClickSound;
    public GameObject SettingsPanel;
    public Slider MusicVolumeSlider;
    public Slider SoundEffectVolumeSlider;
    public Slider SensitivitySlider;
    public VideoPlayer StartVideo;
    public GameObject VideoImage;
    public GameObject SkipSceneButtonForVideo;
    public VideoClip ClipTR;
    public VideoClip ClipEN;

    // Start is called before the first frame update

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);
        if (!PlayerPrefs.HasKey("audioVolume"))
            PlayerPrefs.SetFloat("audioVolume", 1);
        if (!PlayerPrefs.HasKey("Sensitivity"))
            PlayerPrefs.SetFloat("Sensitivity", 1);
        SensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SoundEffectVolumeSlider.value = PlayerPrefs.GetFloat("audioVolume");
        if (!PlayerPrefs.HasKey("CharacterPosX"))
        {
            PlayerPrefs.SetFloat("CharacterPosX", 38.87f);
            PlayerPrefs.SetFloat("CharacterPosY", -1.81f);
            PlayerPrefs.SetFloat("CharacterPosZ", 28.08f);
        }
    }

    public void ifClickSettingButton()
    {
        MainMenuClickSound.Play();
        SettingsPanel.SetActive(true);
    }

    public void ifClickBackButtonForSettings()
    {
        MainMenuClickSound.Play();
        SettingsPanel.SetActive(false);
    }

    public void ifChangeMusicSound()
    {
        PlayerPrefs.SetFloat("musicVolume", MusicVolumeSlider.value);
        MainMenuMusic.volume = MusicVolumeSlider.value;
    }

    public void ifChangeSoundEffectSound()
    {
        PlayerPrefs.SetFloat("audioVolume", SoundEffectVolumeSlider.value);
        MainMenuClickSound.volume = SoundEffectVolumeSlider.value;
    }

    public void ifClickSkipButton()
    {
        MainMenuClickSound.Play();
        SceneManager.LoadScene(1);
    }

    private IEnumerator ShowSkipButton()
    {
        yield return new WaitForSeconds(2);

        Color ButtonColor = SkipSceneButtonForVideo.GetComponent<Image>().color;
        Color TextColor = SkipSceneButtonForVideo.GetComponentInChildren<TextMeshProUGUI>().color;

        while (ButtonColor.a != 1)
        {
            ButtonColor.a += 0.1f;
            TextColor.a += 0.1f;
            SkipSceneButtonForVideo.GetComponent<Image>().color = ButtonColor;
            SkipSceneButtonForVideo.GetComponentInChildren<TextMeshProUGUI>().color = TextColor;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    private void SkipGame(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }

    public void ifClickPlayButton()
    {
        if (!PlayerPrefs.HasKey("PlayerGameStatus"))
            PlayerPrefs.SetString("PlayerGameStatus", "Finish");
        if (PlayerPrefs.GetString("PlayerGameStatus") == "InGame")
            SceneManager.LoadScene(1);
        else
            PlayerPrefs.SetString("PlayerGameStatus", "InGame");
        if (PlayerPrefs.GetString("Language") == "TR")
            StartVideo.clip = ClipTR;
        else
            StartVideo.clip = ClipEN;
        SkipSceneButtonForVideo.SetActive(true);
        StartCoroutine(ShowSkipButton());
        MainMenuMusic.Stop();
        MainMenuClickSound.Play();
        VideoImage.SetActive(true);
        StartVideo.Play();
        StartVideo.loopPointReached += SkipGame;
    }

    public void ifChangeSensitivity()
    {
        PlayerPrefs.SetFloat("Sensitivity", SensitivitySlider.value);
    }
}
