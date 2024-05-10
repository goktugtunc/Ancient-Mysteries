using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using deneme;
using EnvController;

public class Paper : MonoSingleton<Paper>
{
    public bool status = false;
    public GameObject Chest;
    public GameObject PaperPanel;
    public Image PaperImage;
    public Sprite PaperSprite;

    private void OnMouseDown()
    {
        if(Chest.GetComponent<Chest>().a==true)
        {
            if (gameObject.tag == "Paper")
            {
                SoundManager.Instance.Audio(5,PlayerPrefs.GetFloat("audioVolume"));
                deneme.canvasManagerforGameEpisodes.Instance.AddItemForEnv(PaperSprite);
                PaperPanel.SetActive(true);
                gameObject.SetActive(false);
                status = true;
                if (PlayerPrefs.GetString("Language") == "EN")
                    SoundManager.Instance.Audio(24, 0.8f);
                else
                    SoundManager.Instance.Audio(37, 0.8f);
            }
        }
    }

    public void PaperOnClick() 
    {
        PaperPanel.SetActive(false);
        if (EnvController.EnvController.Instance.WhichButtonSelected())
            EnvController.EnvController.Instance.clearSelectedStatus();
    }
}
