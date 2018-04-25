using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public GameObject Loading, MissionContentObject, PlayBtn;
    public AudioSource bgmusic, tap;
    public List<Button> TotalLevels = new List<Button>();
    public GameObject[] TotalLevelsBG;
    public List<Image> TotalLevelsLock = new List<Image>();
    public string[] MissionsList;
    public Slider ExperienceSlider;
    public Text MissionText, CoinsText, ExperienceText;
    public Image loadingBar;
    private AsyncOperation async;


    void Start ()
    {
        Time.timeScale = 1f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        bgmusic.volume = PlayerPrefs.GetFloat("Music");
        tap.volume = PlayerPrefs.GetFloat("Sound");

        if (PlayerPrefs.GetInt("Exp") == 0)
        {
            PlayerPrefs.SetInt("Exp", 50);
        }

        if (PlayerPrefs.GetInt("Coins") == 0)
        {
            PlayerPrefs.SetInt("Coins", 50);
        }

        if (TotalLevels.Count>0)
        {
            foreach(Button btn in TotalLevels)
            {
                btn.interactable = false;
            }
            foreach (Image img in TotalLevelsLock)
            {
                img.transform.gameObject.SetActive(true);
            }
            for (int i=0; i<=PlayerPrefs.GetInt("Level");i++)
            {
                TotalLevels[i].interactable = true;
                TotalLevelsLock[i].transform.gameObject.SetActive(false);
            }
        }

        CoinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        ExperienceSlider.value = PlayerPrefs.GetInt("Exp");
        ExperienceText.text = PlayerPrefs.GetInt("Exp").ToString() + "/" + "500";

    }

    public void LevelBtn(int LevelNumber)
    {
        tap.Play();
        MissionContentObject.SetActive(true);
        MissionText.text = MissionsList[LevelNumber].ToString();
        for(int i = 0; i < TotalLevelsBG.Length; i++)
        {
            TotalLevelsBG[i].SetActive(false);
        }
        TotalLevelsBG[LevelNumber].SetActive(true);
        PlayerPrefs.SetInt("SelectedLevel", LevelNumber);
        PlayBtn.SetActive(true);
    }

    public void PlayButton()
    {
        tap.Play();
        Loading.SetActive(true);
        StartCoroutine(LoadScene("GunSelection"));
    }

    //public void RightButtonMethod()
    //{
    //    tap.Play();
    //    LeftButton.SetActive(true);
    //    RightButton.SetActive(false);
    //    Page2.SetActive(true);
    //    Page1.SetActive(false);
    //}
    //public void LeftButtonMethod()
    //{
    //    tap.Play();
    //    LeftButton.SetActive(false);
    //    RightButton.SetActive(true);
    //    Page2.SetActive(false);
    //    Page1.SetActive(true);
    //}

    public void BackBtn()
    {
        tap.Play();
        if (Loading)
            Loading.SetActive(true);
        Application.LoadLevel(Application.loadedLevel - 1);
    }

    IEnumerator LoadScene(string scene)
    {
        //int level_toLoad = PlayerPrefs.GetInt(Constants.current_levels);
        async = SceneManager.LoadSceneAsync(scene);
        Time.timeScale = 1;

        while (!async.isDone)
        {
            if (loadingBar) loadingBar.fillAmount = async.progress;

            yield return null;
        }
    }
}
