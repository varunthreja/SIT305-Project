using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
 public class Multidimension
{
    public int[] GunDetails;
}

public class TruckSelectionScript : MonoBehaviour 
{
    public static TruckSelectionScript instance;
	public GameObject[] cars;
    public Transform SpawnPosition;
    public Multidimension[] Slider;
    public Slider[] CarSliders;
	public int[] UnlockPrice;
	public GameObject LockImg;
    public Slider ExperienceSlider;
	public Text LockInfo,ExperienceText;
	public GameObject NextBtn;
	public GameObject BuyBtn;
	public GameObject LeftButton;
	public GameObject RightButton;
    public GameObject BuySprite;
	public GameObject PurchasedDailogue ,InsufficientCashDialogue;
    public GameObject loading;
    GameObject CurrentCar;
    public Text CoinText;
	float TempFloat;
	int counter=0;
	public AudioSource Music;
	public AudioSource TapSound;
    public Image loadingBar;
    private AsyncOperation async;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        CurrentCar = Instantiate(cars[0], SpawnPosition.position, SpawnPosition.rotation);

        counter = 0;
        TapSound.volume = PlayerPrefs.GetFloat("Sound");
        Music.volume = PlayerPrefs.GetFloat("Music");

        CoinText.text = PlayerPrefs.GetInt("Coins").ToString();
        ExperienceSlider.value = PlayerPrefs.GetInt("Exp");
        ExperienceText.text = PlayerPrefs.GetInt("Exp").ToString() + "/" + "500";

        CarSliders[0].value = Slider[0].GunDetails[0];
        CarSliders[1].value = Slider[0].GunDetails[1];
        CarSliders[2].value = Slider[0].GunDetails[2];
        CarSliders[3].value = Slider[0].GunDetails[3];

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("Blocks"));
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        TempFloat = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt(cars[0].name, 1);
    }


	void Update () {
        cars[counter].transform.Rotate(new Vector3(0, 0.35f , 0));
		CoinText.text = PlayerPrefs.GetInt ("Coins").ToString();
	}

	public void RightClickMethod()
	{
		TapSound.Play ();
        
        cars [counter].SetActive (false);
        Destroy(CurrentCar);
		counter++;
        cars[counter].SetActive(true);
        CurrentCar = Instantiate(cars[counter], SpawnPosition.position, SpawnPosition.rotation);
        //CurrentCar.transform.SetParent(Floor.transform);


        CarSliders[0].value = Slider[counter].GunDetails[0];
        CarSliders[1].value = Slider[counter].GunDetails[1];
        CarSliders[2].value = Slider[counter].GunDetails[2];

        LockInfo.text = UnlockPrice [counter].ToString ();
		if (PlayerPrefs.GetInt(cars[counter].name)==0) 
		{
			LockImg.SetActive (true);
			BuyBtn.SetActive (true);
            BuySprite.SetActive(true);
			NextBtn.SetActive (false);
		} 
		else 
		{
			LockImg.SetActive (false);
            BuySprite.SetActive(false);
            NextBtn.SetActive (true);
			BuyBtn.SetActive (false);
		}
		LeftButton.SetActive (true);
		if (counter == cars.Length-1) 
		{
			RightButton.SetActive (false);
		}
	}

	public void LeftClickMethod()
	{
		TapSound.Play ();
        Destroy(CurrentCar);
        cars[counter].SetActive(false);
        counter--;
        cars[counter].SetActive(true);
        CurrentCar = Instantiate(cars[counter], SpawnPosition.position, SpawnPosition.rotation);
        //CurrentCar.transform.SetParent(Floor.transform);

        CarSliders[0].value = Slider[counter].GunDetails[0];
        CarSliders[1].value = Slider[counter].GunDetails[1];
        CarSliders[2].value = Slider[counter].GunDetails[2];

        LockInfo.text = UnlockPrice [counter].ToString ();
		if (PlayerPrefs.GetInt(cars[counter].name)==0) 
		{
			LockImg.SetActive (true);
			BuyBtn.SetActive (true);
            BuySprite.SetActive(true);
            NextBtn.SetActive (false);
		} 
		else 
		{
			LockImg.SetActive (false);
            BuySprite.SetActive(false);
            NextBtn.SetActive (true);
			BuyBtn.SetActive (false);
		}
		RightButton.SetActive (true);
		if (counter == 0) 
		{
			LeftButton.SetActive (false);
		}
	}

    public void GunButton(int CarId)
    {
        TapSound.Play();
        Destroy(CurrentCar);
        //cars[CarId].SetActive(false);
        //counter--;
        //cars[counter].SetActive(true);
        CurrentCar = Instantiate(cars[CarId], SpawnPosition.position, SpawnPosition.rotation);

        CarSliders[0].value = Slider[CarId].GunDetails[0];
        CarSliders[1].value = Slider[CarId].GunDetails[1];
        CarSliders[2].value = Slider[CarId].GunDetails[2];
        CarSliders[3].value = Slider[CarId].GunDetails[3];

        LockInfo.text = UnlockPrice[CarId].ToString();
        if (PlayerPrefs.GetInt(cars[CarId].name) == 0)
        {
            LockImg.SetActive(true);
            BuyBtn.SetActive(true);
            BuySprite.SetActive(true);
            NextBtn.SetActive(false);
        }
        else
        {
            LockImg.SetActive(false);
            BuySprite.SetActive(false);
            NextBtn.SetActive(true);
            BuyBtn.SetActive(false);
        }
        counter = CarId;
    }

	public void SelectButton()
	{
        TapSound.Play ();
        PlayerPrefs.SetInt ("Gun", counter);
        loading.SetActive(true);
        StartCoroutine(LoadScene("GamePlay"));
	}
	public void BackBtn()
	{
        TapSound.Play();
        loading.SetActive(true);
		Application.LoadLevel (Application.loadedLevel - 1);
	}
	public void UnlockGun()
	{
		TapSound.Play ();
		if (PlayerPrefs.GetInt ("Coins") < UnlockPrice [counter]) 
		{
			InsufficientCashDialogue.SetActive (true);
        } 
		else 
		{
			PurchasedDailogue.SetActive (true);
            PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") - UnlockPrice [counter]);
			PlayerPrefs.SetInt (cars[counter].name, 1);
			CoinText.text= TempFloat.ToString ();
			LockImg.SetActive (false);
			NextBtn.SetActive (true);
			BuyBtn.SetActive (false);
		}
	}
	public void Ok(GameObject D)
	{
		TapSound.Play ();
		D.SetActive (false);

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
