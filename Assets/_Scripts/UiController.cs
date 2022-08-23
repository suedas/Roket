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
	public Slider slider,turboslider;
	public GameObject particleGas,turboParticle;
	public GameObject uiHand;

	private void Start()
	{
		//PlayerPrefs.DeleteAll();
		interactable();
		//Debug.Log(RoketManager.instance.maxMesafe);
		particleGas.SetActive(false);
        //if (RoketManager.instance.gazPara > GameManager.instance.score)
        //{
        //    gazButton.interactable = false;
        //}
        //if (RoketManager.instance.hizPara > GameManager.instance.score)
        //{
        //    hizButton.interactable = false;
			
        //}
        //if (RoketManager.instance.mesafePara > GameManager.instance.score)
        //{
        //    mesafeButton.interactable = false;
        //}
        slider.maxValue = RoketManager.instance.maxgaz;
		slider.minValue = RoketManager.instance.gaz;
		slider.value = RoketManager.instance.maxgaz;


		//if (PlayerPrefs.HasKey("gaz"))
		//{
		//	RoketManager.instance.maxgaz = PlayerPrefs.GetFloat("gaz");
		//	if (RoketManager.instance.maxgaz == 0)
		//	{
		//		RoketManager.instance.maxgaz = 100;
		//		RoketManager.instance.levelGaz = 1;
		//	}
		//	totalGaz.text = "Gaz " ;
		//	levelGaz.text = "Level  " + PlayerPrefs.GetInt("levelgaz").ToString();
		//	paraGaz.text = "Para  " + PlayerPrefs.GetInt("gazpara").ToString();
		//}
		//if (PlayerPrefs.HasKey("hiz"))
		//{
		//	RoketManager.instance.hiz = PlayerPrefs.GetFloat("hiz");
		//	if (RoketManager.instance.hiz == 0)
		//	{
		//		RoketManager.instance.hiz = 0;
		//		RoketManager.instance.levelHiz = 1;

		//	}
		//	totalHiz.text = "Speed  " ;
		//	levelHiz.text = "Level  " + PlayerPrefs.GetInt("levelhiz");
		//	paraHiz.text = "Money  " + PlayerPrefs.GetInt("hizpara");
		//}
		//if (PlayerPrefs.HasKey("mesafe"))
		//{
		//	RoketManager.instance.maxMesafe = PlayerPrefs.GetFloat("mesafe");
		//	if (RoketManager.instance.maxMesafe == 0)
		//	{
		//		RoketManager.instance.maxMesafe = 1;
		//		RoketManager.instance.mesafeLevel = 1;

		//	}
		//	totalMesafe.text = "Mesafe  " ;
		//	levelMesafe.text = "Level  " + PlayerPrefs.GetInt("mesafelevel").ToString();
		//	paraMesafe.text = "Para  " + PlayerPrefs.GetInt("mesafepara").ToString();
		//}

		gamePanel.SetActive(true);
		tapToStartPanel.SetActive(true);
		winPanel.SetActive(false);
		losePanel.SetActive(false);
		scoreText.text = PlayerPrefs.GetInt("score").ToString();
	}
	
	public void NextLevelButtonClick()
	{
		winPanel.SetActive(false);
		tapToStartPanel.SetActive(true);
		PlayerController.instance.PreStartingEvents();
	
	}

	public void RestartButtonClick()
	{
		losePanel.SetActive(false);
		tapToStartPanel.SetActive(true);
		PlayerController.instance.PreStartingEvents();
	}

	public void SetScoreText()
	{
		scoreText.text = GameManager.instance.score.ToString();
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
        if (GameManager.instance.score>=RoketManager.instance.gazPara)
        {
			RoketManager.instance.maxgaz += 10;//10
			GameManager.instance.DecreaseScore(RoketManager.instance.gazPara);
			RoketManager.instance.levelGaz = PlayerPrefs.GetInt("levelgaz") +1;
			RoketManager.instance.gazPara = PlayerPrefs.GetInt("gazpara") + PlayerPrefs.GetInt("gazpara")/2;
			GameManager.instance.scoreArtisMiktari = RoketManager.instance.gazPara;
			slider.maxValue = RoketManager.instance.maxgaz;
			slider.value = RoketManager.instance.maxgaz;

			PlayerPrefs.SetFloat("gaz", RoketManager.instance.maxgaz);
			PlayerPrefs.SetInt("levelgaz", RoketManager.instance.levelGaz);
			PlayerPrefs.SetInt("gazpara", RoketManager.instance.gazPara);

			levelGaz.text = "Level  " + RoketManager.instance.levelGaz;
			paraGaz.text = "Money  " + RoketManager.instance.gazPara;
			interactable();

		}
        else
        {
			gazButton.interactable = false;
        }
    
    }
	public void hiz()
    {
		if (GameManager.instance.score >= RoketManager.instance.hizPara)
		{
			RoketManager.instance.hiz += 1;//1
			GameManager.instance.DecreaseScore(RoketManager.instance.hizPara);
			RoketManager.instance.levelHiz = PlayerPrefs.GetInt("levelhiz") + 1;
			RoketManager.instance.hizPara = PlayerPrefs.GetInt("hizpara") +  PlayerPrefs.GetInt("hizpara")/2;


			PlayerPrefs.SetFloat("hiz", RoketManager.instance.hiz);
			PlayerPrefs.SetInt("levelhiz", RoketManager.instance.levelHiz);
			PlayerPrefs.SetInt("hizpara", RoketManager.instance.hizPara);

			levelHiz.text = "Level  " + RoketManager.instance.levelHiz.ToString();
			paraHiz.text = "Money  " + RoketManager.instance.hizPara.ToString();
			interactable();

		}
        else
        {
			hizButton.interactable = false;
        }

	}
	public void mesafe()
    {
        if (GameManager.instance.score>=RoketManager.instance.mesafePara)
        {
			//RoketManager.instance.maxMesafe += 1;
			//RoketManager.instance.mesafe += 2;
			GameManager.instance.DecreaseScore(RoketManager.instance.mesafePara);
			RoketManager.instance.income = PlayerPrefs.GetFloat("mesafe")+1;
			RoketManager.instance.mesafeLevel = PlayerPrefs.GetInt("mesafelevel")+1;
			RoketManager.instance.mesafePara = PlayerPrefs.GetInt("mesafepara")+ PlayerPrefs.GetInt("mesafepara")/2;

			PlayerPrefs.SetFloat("mesafe", RoketManager.instance.income);
			PlayerPrefs.SetInt("mesafelevel", RoketManager.instance.mesafeLevel);
			PlayerPrefs.SetInt("mesafepara", RoketManager.instance.mesafePara);

			levelMesafe.text = "Level  " + RoketManager.instance.mesafeLevel.ToString();
			paraMesafe.text = "Money  " + RoketManager.instance.mesafePara.ToString();
			interactable();
		}
        else
        {
            mesafeButton.interactable = false;

        }

    }
	public void interactable()
    {
		
		RoketManager.instance.maxgaz = PlayerPrefs.GetFloat("gaz");
		if (RoketManager.instance.maxgaz == 0)
		{
			RoketManager.instance.maxgaz = 100;
			RoketManager.instance.levelGaz = 1;
			RoketManager.instance.gazPara = 10;
			PlayerPrefs.SetInt("levelgaz", RoketManager.instance.levelGaz);
			PlayerPrefs.SetInt("gazpara", RoketManager.instance.gazPara);

		}
		else
		{
			RoketManager.instance.levelGaz = PlayerPrefs.GetInt("levelgaz");
			RoketManager.instance.gazPara = PlayerPrefs.GetInt("gazpara");
		}
		levelGaz.text = "Level  " + PlayerPrefs.GetInt("levelgaz").ToString();
		paraGaz.text =  PlayerPrefs.GetInt("gazpara").ToString();
		
	
		RoketManager.instance.hiz = PlayerPrefs.GetFloat("hiz");
		if (RoketManager.instance.hiz == 0)
		{
			RoketManager.instance.hiz = 0;
			RoketManager.instance.levelHiz = 1;
			RoketManager.instance.hizPara = 10;
			PlayerPrefs.SetInt("levelhiz", RoketManager.instance.levelHiz);
			PlayerPrefs.SetInt("hizpara", RoketManager.instance.hizPara);
		}
		else
		{
			RoketManager.instance.levelHiz = PlayerPrefs.GetInt("levelhiz");
			RoketManager.instance.hizPara = PlayerPrefs.GetInt("hizpara");
		}
		levelHiz.text = "Level  " + PlayerPrefs.GetInt("levelhiz");
		paraHiz.text = PlayerPrefs.GetInt("hizpara").ToString();
		
	
		//Debug.Log("mesafe level"+ RoketManager.instance.mesafeLevel);
		RoketManager.instance.income = PlayerPrefs.GetFloat("mesafe");
		if (RoketManager.instance.income == 0)
		{
				
			RoketManager.instance.income = 1;
			RoketManager.instance.mesafeLevel = 1;
			RoketManager.instance.mesafePara = 10;
			PlayerPrefs.SetInt("mesafelevel", RoketManager.instance.mesafeLevel);
			PlayerPrefs.SetInt("mesafepara", RoketManager.instance.mesafePara);
			PlayerPrefs.SetFloat("mesafe", RoketManager.instance.income);
		}
		else
		{
			RoketManager.instance.mesafeLevel = PlayerPrefs.GetInt("mesafelevel");
			RoketManager.instance.mesafePara = PlayerPrefs.GetInt("mesafepara");
		}
		levelMesafe.text = "Level  " + PlayerPrefs.GetInt("mesafelevel").ToString();
		paraMesafe.text =PlayerPrefs.GetInt("mesafepara").ToString();


		incrementPanel.SetActive(true);
		//totalHiz.text = "Speed  " ;
		//levelHiz.text = "Level  " + RoketManager.instance.levelHiz.ToString();
		//paraHiz.text = "Money  " + RoketManager.instance.hizPara.ToString();
		//totalMesafe.text = "Income  " ;
		//levelMesafe.text = "Level  " + RoketManager.instance.mesafeLevel.ToString();
		//paraMesafe.text = "Money  " + RoketManager.instance.mesafePara.ToString();
		//totalGaz.text = "Gas  " ;
		//levelGaz.text = "Level  " + RoketManager.instance.levelGaz;
		//paraGaz.text = "Money  " + RoketManager.instance.gazPara;
		if (RoketManager.instance.gazPara > GameManager.instance.score)
		{
			gazButton.interactable = false;
		}
		else
		{
			gazButton.interactable = true;
		}
		if (RoketManager.instance.hizPara > GameManager.instance.score)
		{
			hizButton.interactable = false;
		}
		else
		{
			hizButton.interactable = true;
		}
		if (RoketManager.instance.mesafePara > GameManager.instance.score)
		{
			mesafeButton.interactable = false;
		}
		else
		{
			mesafeButton.interactable = true;
		}
	}
}

