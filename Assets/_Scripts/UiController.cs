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
	public TextMeshProUGUI scoreText,levelText,levelGaz,levelHiz,paraGaz,paraHiz,levelMesafe,paraMesafe;

	private void Start()
	{
		gamePanel.SetActive(true);
		tapToStartPanel.SetActive(true);
		winPanel.SetActive(false);
		losePanel.SetActive(false);
		scoreText.text = PlayerPrefs.GetInt("score").ToString();
		levelText.text = "LEVEL " + LevelController.instance.totalLevelNo.ToString();
        if (PlayerPrefs.HasKey("gaz"))
        {
			
			PlayerPrefs.GetFloat("gaz");
			levelGaz.text="Level  "+PlayerPrefs.GetInt("levelgaz").ToString();
			paraGaz.text="Para  "+PlayerPrefs.GetInt("gazpara").ToString();
        }
        if (PlayerPrefs.HasKey("hiz"))
        {
			PlayerPrefs.GetFloat("hiz");
			levelHiz.text = "Level  " + PlayerPrefs.GetInt("levelhiz");
			paraHiz.text = "Para  " + PlayerPrefs.GetInt("hizpara");
        }
        if (PlayerPrefs.HasKey("mesafe"))
        {
			PlayerPrefs.GetInt("mesafe");
			levelMesafe.text = "Level  " + PlayerPrefs.GetInt("mesafelevel");
			paraMesafe.text = "Para  " + PlayerPrefs.GetInt("mesafepara");
        }
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
				tempScore++;
				scoreText.text = tempScore.ToString();
				yield return new WaitForSeconds(.05f);
			}
		}
		else if(tempScore > GameManager.instance.score)
		{
			while (tempScore > GameManager.instance.score)
			{
				tempScore--;
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
	}


	public void OpenLosePanel()
	{
		losePanel.SetActive(true);
	}
	public void gaz() 
	{
	

		RoketManager.instance.maxgaz += 10;
		RoketManager.instance.levelGaz = PlayerPrefs.GetInt("levelgaz") +1;
		RoketManager.instance.gazPara =PlayerPrefs.GetInt("gazpara")+50;

		PlayerPrefs.SetFloat("gaz", RoketManager.instance.maxgaz);
		PlayerPrefs.SetInt("levelgaz", RoketManager.instance.levelGaz);
		PlayerPrefs.SetInt("gazpara", RoketManager.instance.gazPara);

		levelGaz.text = "Level  " + RoketManager.instance.levelGaz;
        paraGaz.text = "Para  " + RoketManager.instance.gazPara;
    }
	public void hiz()
    {
		RoketManager.instance.hiz += 1;
	    RoketManager.instance.levelHiz = PlayerPrefs.GetInt("levelhiz")+ 1;
		RoketManager.instance.hizPara = PlayerPrefs.GetInt("hizpara")+ 50;


		PlayerPrefs.SetFloat("hiz", RoketManager.instance.hiz);
		PlayerPrefs.SetInt("levelhiz", RoketManager.instance.levelHiz);
		PlayerPrefs.SetInt("hizpara", RoketManager.instance.hizPara);

		levelHiz.text = "Level  " +RoketManager.instance.levelHiz.ToString();
		paraHiz.text = "Para  " + RoketManager.instance.hizPara.ToString();
	}
	public void mesafe()
    {
		RoketManager.instance.mesafe += 2;
		RoketManager.instance.mesafeLevel = PlayerPrefs.GetInt("mesafelevel")+1;
		RoketManager.instance.mesafePara = PlayerPrefs.GetInt("mesafepara")+50;

		PlayerPrefs.SetInt("mesafe", RoketManager.instance.mesafe);
		PlayerPrefs.SetInt("mesafelevel", RoketManager.instance.mesafeLevel);
		PlayerPrefs.SetInt("mesafepara", RoketManager.instance.mesafePara);

		levelMesafe.text = "Level  " + RoketManager.instance.mesafeLevel.ToString();
		paraHiz.text = "Para  " + RoketManager.instance.mesafePara.ToString();


    }
}

