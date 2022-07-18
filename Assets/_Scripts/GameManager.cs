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
	[HideInInspector] public int score; // bu deger birikimli olarak gidecektir. Her level sonu birikecek üzerine eklenecek. para v.s. olabilir.
	[HideInInspector]public int levelScore; // bu deger her levelin kendi score'u olacak. Her level basinda sifirlanacak. Level sonunda score'a eklenecek

	private void Start()
	{
		isContinue = false;
		score = PlayerPrefs.GetInt("score");
	}



	public void IncreaseScore(int paraMikatari)
    {
			//Debug.Log(RoketManager.instance.mesafe);
            //scoreArtisMiktari = Convert.ToInt32(RoketManager.instance.mesafe * RoketManager.instance.maxMesafe);
            score += paraMikatari;
            PlayerPrefs.SetInt("score", score);
		UiController.instance.SetScoreText();

    }


	public void DecreaseScore(int paraMikatari)
    {
        if (score>0)
        {
			
			score -= paraMikatari;
			PlayerPrefs.SetInt("score", score);
			UiController.instance.SetScoreText();

        }
        else
        {
			score =0;
        }
	}
}
