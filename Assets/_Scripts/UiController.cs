using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
	#region Singleton
	public static UiController instance;
	void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}
	#endregion

	public GameObject winPanel, gamePanel, losePanel,tapToStartPanel,incrementPanel;
	public TextMeshProUGUI scoreText,levelText,levelGaz,levelHiz,paraGaz,paraHiz,levelMesafe,paraMesafe,totalGaz,totalHiz,totalMesafe,highScore;
	public Button gazButton, hizButton, mesafeButton;
	public Slider slider;

	private void Start()
	{
		
   //     if (GameManager.instance.scoreArtisMiktari==0)
   //     {
			//gazButton.interactable = false;
		 //    hizButton.interactable = false;
			//mesafeButton.interactable = false;

   //     }
    
		
		UiController.instance.slider.maxValue = RoketManager.instance.maxgaz;
		UiController.instance.slider.minValue = RoketManager.instance.gaz;
		if (PlayerPrefs.HasKey("gaz"))
		{
			RoketManager.instance.maxgaz = PlayerPrefs.GetFloat("gaz");
			if (RoketManager.instance.maxgaz == 0)
			{
				RoketManager.instance.maxgaz = 100;
				RoketManager.instance.levelGaz = 1;
			}
			UiController.instance.totalGaz.text = "Gaz " + PlayerPrefs.GetFloat("gaz");
			UiController.instance.levelGaz.text = "Level  " + PlayerPrefs.GetInt("levelgaz").ToString();
			UiController.instance.paraGaz.text = "Para  " + PlayerPrefs.GetInt("gazpara").ToString();
		}
		if (PlayerPrefs.HasKey("hiz"))
		{
			RoketManager.instance.hiz = PlayerPrefs.GetFloat("hiz");
			if (RoketManager.instance.hiz == 0)
			{
				RoketManager.instance.hiz = 0;
				RoketManager.instance.levelHiz = 1;

			}
			UiController.instance.totalHiz.text = "Hýz  " + PlayerPrefs.GetFloat("hiz");
			UiController.instance.levelHiz.text = "Level  " + PlayerPrefs.GetInt("levelhiz");
			UiController.instance.paraHiz.text = "Para  " + PlayerPrefs.GetInt("hizpara");
		}
		if (PlayerPrefs.HasKey("mesafe"))
		{
			RoketManager.instance.maxMesafe = PlayerPrefs.GetFloat("mesafe");
			if (RoketManager.instance.maxMesafe == 0)
			{
				RoketManager.instance.maxMesafe = 1;
				RoketManager.instance.mesafeLevel = 1;

			}
			UiController.instance.totalMesafe.text = "Mesafe  " + PlayerPrefs.GetFloat("mesafe") + "x";
			UiController.instance.levelMesafe.text = "Level  " + PlayerPrefs.GetInt("mesafelevel");
			UiController.instance.paraMesafe.text = "Para  " + PlayerPrefs.GetInt("mesafepara");
		}

		gamePanel.SetActive(true);
		tapToStartPanel.SetActive(true);
		winPanel.SetActive(false);
		losePanel.SetActive(false);
		scoreText.text = PlayerPrefs.GetInt("score").ToString();
		levelText.text = "LEVEL " + LevelController.instance.totalLevelNo.ToString();
	}
	
	public void NextLevelButtonClick()
	{
		winPanel.SetActive(false);
		tapToStartPanel.SetActive(true);
		PlayerController.instance.PreStartingEvents();
		LevelController.instance.NextLevelEvents();
	}

	public void RestartButtonClick()
	{
		losePanel.SetActive(false);
		tapToStartPanel.SetActive(true);
		PlayerController.instance.PreStartingEvents();
		LevelController.instance.RestartLevelEvents();
	}

	public void SetScoreText()
	{
		StartCoroutine(SetScoreTextAnim());
	}

	// bu fonksiyon sayesinde score textimiz birer birer artýyor veya azalýyor hýzlý þekilde..
	// artýþ azalýþ animasyonu diyebiliriz.
	// eger alinan scorelar buyukse ve birer birer artirmak sacma derece uzun suruyorsa fonksiyon icinden tempscore artis miktarini artirin.
	IEnumerator SetScoreTextAnim()
	{
		int tempScore = int.Parse(scoreText.text);
		if(tempScore < GameManager.instance.score)
		{
			while (tempScore < GameManager.instance.score)
			{
				tempScore+=10;
				scoreText.text = tempScore.ToString();
				yield return new WaitForSeconds(.05f);
			}
		}
		else if(tempScore > GameManager.instance.score)
		{
			while (tempScore > GameManager.instance.score)
			{
				tempScore-=10;
				scoreText.text = tempScore.ToString();
				yield return new WaitForSeconds(.05f);
			}
		}		
	}

	public void SetLevelText()
	{
		levelText.text = "LEVEL " + LevelController.instance.totalLevelNo.ToString();
	}

	public void OpenWinPanel()
	{
	
		winPanel.SetActive(true);
		highScore.text = "High Score  "+PlayerPrefs.GetInt("highscore").ToString();	

	}


	public void OpenLosePanel()
	{
		losePanel.SetActive(true);
	}
	public void gaz() 
	{
        if (GameManager.instance.score>RoketManager.instance.gazPara)
        {
			RoketManager.instance.maxgaz += 10;
			RoketManager.instance.levelGaz = PlayerPrefs.GetInt("levelgaz") +1;
			RoketManager.instance.gazPara =PlayerPrefs.GetInt("gazpara")+50;
			GameManager.instance.scoreArtisMiktari = RoketManager.instance.gazPara;

			PlayerPrefs.SetFloat("gaz", RoketManager.instance.maxgaz);
			PlayerPrefs.SetInt("levelgaz", RoketManager.instance.levelGaz);
			PlayerPrefs.SetInt("gazpara", RoketManager.instance.gazPara);

			totalGaz.text = "Gaz  " + RoketManager.instance.maxgaz;
			levelGaz.text = "Level  " + RoketManager.instance.levelGaz;
			paraGaz.text = "Para  " + RoketManager.instance.gazPara;
			GameManager.instance.DecreaseScore();

        }
        else
        {
			gazButton.interactable = false;
        }

    }
	public void hiz()
    {
		if (GameManager.instance.score > RoketManager.instance.hizPara)
		{
			RoketManager.instance.hiz += 1;
			RoketManager.instance.levelHiz = PlayerPrefs.GetInt("levelhiz") + 1;
			RoketManager.instance.hizPara = PlayerPrefs.GetInt("hizpara") + 50;


			PlayerPrefs.SetFloat("hiz", RoketManager.instance.hiz);
			PlayerPrefs.SetInt("levelhiz", RoketManager.instance.levelHiz);
			PlayerPrefs.SetInt("hizpara", RoketManager.instance.hizPara);

			totalHiz.text = "Hýz  " + RoketManager.instance.hiz;
			levelHiz.text = "Level  " + RoketManager.instance.levelHiz.ToString();
			paraHiz.text = "Para  " + RoketManager.instance.hizPara.ToString();
			GameManager.instance.DecreaseScore();

		}
		else
        {
			hizButton.interactable = false;
        }
	}
	public void mesafe()
    {
        if (GameManager.instance.score>RoketManager.instance.mesafePara)
        {
			RoketManager.instance.maxMesafe += 1;
			//RoketManager.instance.mesafe += 2;
			RoketManager.instance.mesafeLevel = PlayerPrefs.GetInt("mesafelevel")+1;
			RoketManager.instance.mesafePara = PlayerPrefs.GetInt("mesafepara")+50;

			PlayerPrefs.SetFloat("mesafe", RoketManager.instance.maxMesafe);
			PlayerPrefs.SetInt("mesafelevel", RoketManager.instance.mesafeLevel);
			PlayerPrefs.SetInt("mesafepara", RoketManager.instance.mesafePara);

			totalMesafe.text = "Mesafe  "+RoketManager.instance.maxMesafe.ToString()+"x";
			levelMesafe.text = "Level  " + RoketManager.instance.mesafeLevel.ToString();
			paraMesafe.text = "Para  " + RoketManager.instance.mesafePara.ToString();
			GameManager.instance.DecreaseScore();


		}
		else
        {
			mesafeButton.interactable = false;
        }


    }
}

