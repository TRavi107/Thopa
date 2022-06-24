using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RamailoGames;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<UIPanel> uiPanels;
    public TMP_Text highscoreText;
    public GameObject musicBtn;

    public UIPanel activeUIPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SwitchCanvas(uiPanels[0].uiPanelType);
        if (soundManager.instance.backGroundAudioVolume == 0)
        {
            EnableDisableMusicBtns(musicBtn, 0.5f);
        }
        else
        {
            EnableDisableMusicBtns(musicBtn, 1f);
        }

        soundManager.instance.PlaySound(SoundType.backgroundSound);
        
    }
    void GetHighScore()
    {
        ScoreAPI.GetData((bool s, Data_RequestData d) => {
            if (s)
            {
                highscoreText.text = d.high_score.ToString();
            }
        });
    }
    private void EnableDisableMusicBtns(GameObject MainGameBTN, float alphaVAlue)
    {
        if (MainGameBTN == null)
            return;
        MainGameBTN.GetComponent<Image>().color = new Color(
                MainGameBTN.GetComponent<Image>().color.r,
                MainGameBTN.GetComponent<Image>().color.g,
                MainGameBTN.GetComponent<Image>().color.b,
                alphaVAlue);

    }
    public void OnMusicBTNClickded()
    {
        if (musicBtn.GetComponent<Image>().color.a == 1)
        {
            EnableDisableMusicBtns(musicBtn, 0.5f);


            soundManager.instance.MusicVolumeChanged(0);
        }
        else
        {
            EnableDisableMusicBtns(musicBtn, 1f);

            soundManager.instance.MusicVolumeChanged(1);
        }
    }


    public void SwitchCanvas(UIPanelType targetPanel)
    {

        foreach (UIPanel panel in uiPanels)
        {

            if (panel.uiPanelType == targetPanel)
            {
                
                activeUIPanel = panel;
            }
            else
            {
                panel.gameObject.SetActive(false);
            }
        }
        
        activeUIPanel.gameObject.SetActive(true);
    }

    public void OnMusicVolumeChanged()
    {

        //soundManager.instance.MusicVolumeChanged(musicSlider.value);
        //soundManager.instance.SaveMusicVoulme(musicSlider.value);
    }

    public void OnSoundVolumeChanged()
    {
        //soundManager.instance.SoundVolumeChanged(soundSlider.value);
        //soundManager.instance.SaveSoundVoulme(soundSlider.value);
    }

}
