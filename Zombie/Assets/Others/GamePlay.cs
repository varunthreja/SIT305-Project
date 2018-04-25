using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour {

    public static GamePlay instance;
    public PlayerWeapons weaponNumber;
    public WeaponBehavior[] weapons;
    public GameObject LevelCompletePanel, LevelFailPanel, PausePanel, ObjectivePanel, LoadingImage, ReloadAdButton , SaveMeAdButton , PickUpButton /*, WalkAbleUI, FireUI*/;
    public GameObject[] Levels;
    public Transform[] PlayerPositions;
    public int[] CurrentLevelKills;
    public string[] ObjectiveMissions;
    public Text ObjectiveText, EnemiesToKillText, ExperienceText , EarnedCoins;
    public GameObject Player;
    internal int TotalKills, TargetKills, CurrentLevel;
    public AudioSource tap, BgSound, BlockPickupSound, BlockyDeathSound, victorySound;
    public Image loadingBar;
    private AsyncOperation async;
    internal bool GunReloadAd, SaveMeAd;

    private void Awake()
    {
        instance = this;
        CurrentLevel = PlayerPrefs.GetInt("SelectedLevel");

        switch (PlayerPrefs.GetInt("Gun"))
        {
            case 0:
                weapons[0].haveWeapon = true;
                weaponNumber.firstWeapon = 4;
                break;

            case 1:
                weapons[1].haveWeapon = true;
                weaponNumber.firstWeapon = 5;
                break;

            case 2:
                weapons[2].haveWeapon = true;
                weaponNumber.firstWeapon = 6;
                break;

            case 3:
                weapons[3].haveWeapon = true;
                weaponNumber.firstWeapon = 3;
                break;

            case 4:
                weapons[4].haveWeapon = true;
                weaponNumber.firstWeapon = 7;
                break;
        }


        //if(CurrentLevel == 3 || CurrentLevel == 4 || CurrentLevel == 7)
        //{
        //    weapon.haveWeapon = true;
        //    FireUI.SetActive(true);
        //    WalkAbleUI.SetActive(false);
        //}

        //if(CurrentLevel == 3 || CurrentLevel == 6)
        //{
        //    FPSPlayer.instance.hungerPoints = 93;
        //}


        Levels[CurrentLevel].SetActive(true);
        Player.transform.position = PlayerPositions[CurrentLevel].position;
        Player.transform.rotation = PlayerPositions[CurrentLevel].rotation;
        Levels[CurrentLevel].SetActive(true);
    }
    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        TargetKills = CurrentLevelKills[CurrentLevel];
        print("Current Level : " + PlayerPrefs.GetInt("SelectedLevel") + "  Level : " + PlayerPrefs.GetInt("Level"));
        ObjectiveText.text = ObjectiveMissions[CurrentLevel];
        ObjectivePanel.SetActive(true);
        BgSound.volume = PlayerPrefs.GetFloat("Music");
        tap.volume = BlockPickupSound.volume = PlayerPrefs.GetFloat("Sound");

        EnemiesToKillText.text = TargetKills.ToString();
    }

    public void PauseMethod()
    {
        tap.Play();
        Time.timeScale = 0.0000001f;
        PausePanel.SetActive(true);
    }
    public void ResumeMethod()
    {
        tap.Play();
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
    public void CheckLevelComplete()
    {
        if(CurrentLevelKills[CurrentLevel] == TotalKills)
        {
            Invoke("LevelCompleteMethod", 3f);
        }
    }
    public void LevelCompleteMethod()
    {
        //Time.timeScale = 0.00001f;
        PlayerPrefs.SetInt("Exp", PlayerPrefs.GetInt("Exp") + 30);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50);


        LevelCompletePanel.SetActive(true);
        victorySound.Play();
        if(CurrentLevel ==  PlayerPrefs.GetInt("Level"))
        {
            if(PlayerPrefs.GetInt("Level") < Levels.Length - 1)
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
    }
    public void NextLevelMethod()
    {
        tap.Play();
        LoadingImage.SetActive(true);
        StartCoroutine(LoadScene("LevelSelection"));

    }
    public void LevelFailMethod()
    {
        SaveMeAdButton.SetActive(false);
        ReloadAdButton.SetActive(false);
        LevelFailPanel.SetActive(true);
        Invoke("ShowAds", 3);
        Time.timeScale = 0f;
    }
    public void HomeMethod()
    {
        tap.Play();
        LoadingImage.SetActive(true);
        StartCoroutine(LoadScene("MainMenu"));
    }
    public void RestartMethod()
    {
        tap.Play();
        LoadingImage.SetActive(true);
            StartCoroutine(LoadScene("GamePlay"));
    }
    public void CloseDailogue(GameObject Gm)
    {
        tap.Play();
        Gm.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SaveMeAdMethod()
    {
        SaveMeAdButton.SetActive(true);
        Time.timeScale = 0.00000000000001f;
    }

    public void CallSavemeAdMethod()
    {
        SaveMeAd = true;
    }

    public void SaveAfterAdMethod()
    {
        FPSPlayer.instance.HealPlayer(50f);
        SaveMeAd = false;
        Time.timeScale = 1f;
        SaveMeAdButton.SetActive(false);
    }

    public void ReloadAdButttonMethod()
    {
        GunReloadAd = true;
    }

    public void ReloadButtonMethod()
    {
        weapons[PlayerPrefs.GetInt("Gun")].ammo = 30;
        ReloadAdButton.SetActive(false);
        GunReloadAd = false;
        ReloadAdButton.SetActive(false);
    }

    public void NoThanksButtonMethod()
    {
        SaveMeAdButton.SetActive(false);
        LevelFailMethod();
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
