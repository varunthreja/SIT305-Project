using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenuN : MonoBehaviour
{
    public GameObject LoadingImage /*, soundoff, soundon, BgMusicOff, bgMusicon*/;
    public Slider MusicSlider, SoundSlider;
    public AudioSource BGMusic;
    public AudioSource buttontap;
    public GameObject SettingsPannel;
    public Image loadingBar;
    private AsyncOperation async;

    public GameObject Bg;

    void Start()
    {
        Time.timeScale = 1f;
        
        //PlayerPrefs.SetInt("Sounds", 1);
        //PlayerPrefs.SetInt("Music", 1);
        if (PlayerPrefs.GetInt("Coins") == 0)
        {
            PlayerPrefs.SetInt("Coins", 20);
        }

        PlayerPrefs.SetFloat("Music", MusicSlider.value);
        PlayerPrefs.SetFloat("Sound", SoundSlider.value);
        BGMusic.volume = MusicSlider.value;
        buttontap.volume = SoundSlider.value;
    }

    void Update()
    {

        if (SettingsPannel.activeSelf == true)
        {
            BGMusic.volume = MusicSlider.value;
            buttontap.volume = SoundSlider.value;
            PlayerPrefs.SetFloat("Music", MusicSlider.value);
            PlayerPrefs.SetFloat("Sound", SoundSlider.value);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void LateUpdate()
    {
        Bg.transform.Translate(Vector3.left * 0.03f);
    }
    public void PlayButtonMethod()
    {
        buttontap.Play();
        LoadingImage.SetActive(true);
        StartCoroutine(LoadScene("LevelSelection"));
    }

    public void SettingPannelMethod()
    {
        buttontap.Play();
        SettingsPannel.SetActive(true);
    }

    //public void SoundsButton()
    //{
    //    if (soundoff.activeSelf == false)
    //    {
    //        buttontap.Play();
    //        soundoff.SetActive(true);
    //        soundon.SetActive(false);
    //        buttontap.volume = 0;
    //        PlayerPrefs.SetInt("Sounds", 0);
    //    }
    //    else
    //    {
    //        buttontap.Play();
    //        soundoff.SetActive(false);
    //        soundon.SetActive(true);
    //        buttontap.volume = 1;
    //        PlayerPrefs.SetInt("Sounds", 1);
    //    }
    //}

    //public void BGMusicmethod()
    //{

    //    if (BgMusicOff.activeSelf == false)
    //    {
    //        buttontap.Play();
    //        BgMusicOff.SetActive(true);
    //        bgMusicon.SetActive(false);
    //        BGMusic.volume = 0;
    //        PlayerPrefs.SetInt("Music", 0);
    //    }
    //    else
    //    {
    //        buttontap.Play();
    //        BgMusicOff.SetActive(false);
    //        bgMusicon.SetActive(true);
    //        BGMusic.volume = 1;
    //        PlayerPrefs.SetInt("Music", 1);
    //    }
    //}

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

    public void CloseDailogue(GameObject gm)
    {
        buttontap.Play();
        gm.SetActive(false);
    }
}


