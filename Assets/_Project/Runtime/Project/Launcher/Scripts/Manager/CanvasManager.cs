using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


namespace deneme
{
    public class canvasManagerforGameEpisodes : MonoBehaviour
    {
        public static canvasManagerforGameEpisodes Instance { get; private set; }
        // Start is called before the first frame update
        public GameObject Menu;
        public GameObject BackPack;
        public GameObject FlashLight;
        public GameObject EnvPanel;
        public GameObject[] EnvPanelButtons;
        public GameObject[] EnvPanelButtonsSelectedImages;
        public GameObject[] EnvItemImages;
        public GameObject MenuPanel;
        public GameObject ComeBackGameButtonForMenuPanel;
        public GameObject SettingsButtonForMenuPanel;
        public GameObject MainMenuButtonForMenuPanel;
        public GameObject SettingsPanel;
        public GameObject MusicSlider;
        public GameObject EffectSlider;
        public GameObject ComeBackButtonForSettings;
        public GameObject MenuPanelforBackground;
        public GameObject Character;
        //public Sprite FlashLigtIcon;
        public Sprite EnvBackGroundSprite;
        public Slider SensitivitySlider;
        private bool[] ButtonisActive;
        private bool EnvOpenorClose;
        private float envstartposy;
        private RectTransform EnvPanelTransform;
        private RectTransform MenuTransform;
        private RectTransform MenuPanelTransform;
        private RectTransform BackPackTransform;
        private RectTransform FlashLightTransform;
        protected Dictionary<int, bool> EnvDictionary;

        private void Awake()
        {
            Instance = this;
            Time.timeScale = 1;
            ButtonisActive = new bool[6];
            EnvDictionary = new Dictionary<int, bool>();
            for (int i = 0; i < EnvPanelButtonsSelectedImages.Length; i++)
            {
                ButtonisActive[i] = false;
                EnvDictionary[i] = false;
                //EnvPanelButtonsSelectedImages[i].active = false;
            }
            EnvOpenorClose = false;
            EnvPanelTransform = EnvPanel.GetComponent<RectTransform>();
            MenuPanelTransform = MenuPanel.GetComponent<RectTransform>();
            MenuTransform = Menu.GetComponent<RectTransform>();
            BackPackTransform = BackPack.GetComponent<RectTransform>();
            FlashLightTransform = FlashLight.GetComponent<RectTransform>();
            envstartposy = EnvPanelTransform.anchoredPosition.y;
            SensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }

        private int SearchFreeEnvIndex()
        {
            for (int i = 0; i < EnvPanelButtonsSelectedImages.Length; i++)
            {
                if (EnvDictionary[i] == false)
                    return (i);
            }

            return (-1);
        }

        private int SearchItemForEnv(Sprite Item)
        {
            for (int i = 0; i < EnvPanelButtonsSelectedImages.Length; i++)
                if (EnvItemImages[i].GetComponent<Image>().sprite == Item)
                    return (i);
            return (-1);
        }

        public void AddItemForEnv(Sprite additem)
        {
            EnvItemImages[SearchFreeEnvIndex()].GetComponent<Image>().sprite = additem;
            EnvItemImages[SearchFreeEnvIndex()].SetActive(true);
            EnvDictionary[SearchFreeEnvIndex()] = true;
        }

        public int WhichImageSelected()
        {
            int index = 0;
            while (index < EnvPanelButtons.Length)
            {
                if (EnvPanelButtonsSelectedImages[index].active)
                    return (index);
                index++;
            }
            return (-1);
        }

        public void DeleteItemForEnv(Sprite deleteItem)
        {
            int index = SearchItemForEnv(deleteItem);
            EnvDictionary[index] = false;
            EnvItemImages[index].GetComponent<Image>().sprite = EnvBackGroundSprite;
            EnvItemImages[index].SetActive(false);
        }

        public void ifclickbackpack()
        {
            SoundManager.Instance.Audio(8,PlayerPrefs.GetFloat("audioVolume"));
            float targetPosition = EnvOpenorClose ? envstartposy : (envstartposy + 100);
            EnvPanelTransform.DOAnchorPosY(targetPosition, 1);
            EnvOpenorClose = !EnvOpenorClose;
        }

        public void ifClickMenuButton()
        {
            SoundManager.Instance.Audio(14,PlayerPrefs.GetFloat("audioVolume"));
            Time.timeScale = 0;
            MenuPanel.SetActive(true);
            MenuPanelforBackground.SetActive(true);
            MenuTransform.DOAnchorPosX(MenuTransform.anchoredPosition.x - 100, 0.5f).SetUpdate(true);
            BackPackTransform.DOAnchorPosX(BackPackTransform.anchoredPosition.x + 100, 0.5f).SetUpdate(true);
            FlashLightTransform.DOAnchorPosX(FlashLightTransform.anchoredPosition.x + 100, 0.5f).SetUpdate(true);
            EnvPanelTransform.DOAnchorPosY(EnvPanelTransform.anchoredPosition.y - 100, 0.5f).SetUpdate(true);
        }

        public void ifClickComeBackButton()
        {
            SoundManager.Instance.Audio(14,PlayerPrefs.GetFloat("audioVolume"));
            Time.timeScale = 1;
            MenuPanel.SetActive(false);
            MenuPanelforBackground.SetActive(false);
            MenuTransform.DOAnchorPosX(MenuTransform.anchoredPosition.x + 100, 0.5f).SetUpdate(true);
            BackPackTransform.DOAnchorPosX(BackPackTransform.anchoredPosition.x - 100, 0.5f).SetUpdate(true);
            FlashLightTransform.DOAnchorPosX(FlashLightTransform.anchoredPosition.x - 100, 0.5f).SetUpdate(true);
            EnvPanelTransform.DOAnchorPosY(EnvPanelTransform.anchoredPosition.y + 100, 0.5f).SetUpdate(true);
        }

        public void ifClickSettingsButton()
        {
            SoundManager.Instance.Audio(14,PlayerPrefs.GetFloat("audioVolume"));
            MenuPanel.SetActive(false);
            SettingsPanel.SetActive(true);
            MusicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVolume");
            EffectSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("audioVolume");
        }

        public void ifClickComeBackButtonForSettings()
        {
            SoundManager.Instance.Audio(14,PlayerPrefs.GetFloat("audioVolume"));
            MenuPanel.SetActive(true);
            SettingsPanel.SetActive(false);
        }

        public void ifChangeMusicVolume()
        {
            PlayerPrefs.SetFloat("musicVolume", MusicSlider.GetComponent<Slider>().value);
        }

        public void ifChangeEffectVolume()
        {
            PlayerPrefs.SetFloat("audioVolume", EffectSlider.GetComponent<Slider>().value);
        }

        public void ifClickMainMenuButton()
        {
            Vector3 Position = Character.GetComponent<Transform>().position;
            SoundManager.Instance.Audio(14,PlayerPrefs.GetFloat("audioVolume"));
            PlayerPrefs.SetFloat("CharacterPosX", Position.x);
            PlayerPrefs.SetFloat("CharacterPosY", Position.y);
            PlayerPrefs.SetFloat("CharacterPosZ", Position.z);
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void ifChangeSensitivity()
        {
            PlayerPrefs.SetFloat("Sensitivity", SensitivitySlider.value);
        }
    }
}
