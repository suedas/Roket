using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager instance;
	void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}
	#endregion

	public bool isContinue; // player hareket etmesi veya dokunmatik calismasi buna bagli
	public int scoreArtisMiktari; // bu deðer inspektör üzerinden ayarlanacak. her collectible a carpinca ne kadar score artisi olacagini bu sabit deger kontrol edecek
	[HideInInspector]public int score; // bu deger birikimli olarak gidecektir. Her level sonu birikecek üzerine eklenecek. para v.s. olabilir.
	[HideInInspector]public int levelScore; // bu deger her levelin kendi score'u olacak. Her level basinda sifirlanacak. Level sonunda score'a eklenecek

	private void Start()
	{

	
		isContinue = false;	
      
       
	    score = PlayerPrefs.GetInt("score");

		
	}


	/// <summary>
	/// Bu fonksiyon score deðerinin artirilmasi icin kullanilir. 
	/// Collectiblelara carpinca cagrilir.
	/// Farkli sekilde kullanmak icin developer kendisi fonksiyonu modifiye etmelidir.
	/// </summary>
	public void IncreaseScore()
    {
        if (score>=0)
        {
			//scoreArtisMiktari = RoketManager.instance.Highscore ;
			score += scoreArtisMiktari;
			levelScore += scoreArtisMiktari;
			PlayerPrefs.SetInt("score", score);
			UiController.instance.SetScoreText();
			Debug.Log("score artiþ"+scoreArtisMiktari);

		}
   //     else
   //     {
			//score = 0;
   //     }
	
	}


	/// <summary>
	/// Bu fonksiyon score deðerinin azaltilmasi icin kullanilir. 
	/// Obstaclelara carpinca cagrilir.
	/// Farkli sekilde kullanmak icin developer kendisi fonksiyonu modifiye etmelidir.
	/// </summary>
	public void DecreaseScore()
    {
        if (score>0)
        {
		score -= scoreArtisMiktari;
		levelScore -= scoreArtisMiktari;
		PlayerPrefs.SetInt("score", score);
		UiController.instance.SetScoreText();

        }
        else
        {
			score = 0;
        }
	}
}
