using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using System;

public class RoketManager : MonoBehaviour
{
    #region Singleton
    public static RoketManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion

    public float gaz = 0;
    public int levelGaz=1;
    public int gazPara=50;
    public float maxgaz = 100;
    public float hiz = 0;
    public int levelHiz = 1;
    public int hizPara = 50;
    public float maxhiz = 10;
    public float mesafe;
    public float maxMesafe=1;
    public int mesafeLevel;
    public int mesafePara=50;
    public Rigidbody rb;
    public CinemachineBrain cb;
    public GameObject target;
    public GameObject distanceImage;
    public GameObject distance;
    public Transform DistanceParent;
    public int Highscore;
   // public TextMeshProUGUI distanceText;
    public IEnumerator firlat()
    {
        rb = GetComponent<Rigidbody>();
        float  y= transform.position.y;
        if (GameManager.instance.isContinue)
        {
            while (gaz <maxgaz)
            {
                if (gaz<maxgaz/10 && hiz<maxhiz)
                {
                    hiz += .5f;
                }
           
                else if (gaz>maxgaz*9/10)
                {
                    hiz -= 0.2f;
                }
                gaz += 1f;
                rb.velocity = new Vector3(transform.position.x, hiz, transform.position.z);
                target.GetComponent<Rigidbody>().velocity= new Vector3(target.transform.position.x, hiz,target.transform.position.z);
                yield return new WaitForEndOfFrame();
                
            }
            
            yield return new WaitForSeconds(.5f);
            mesafe = transform.position.y;
            Highscore = Convert.ToInt32(mesafe * maxMesafe);
            distanceImage.GetComponent<TextMeshPro>().text = Convert.ToInt32(mesafe * maxMesafe) + "m";
           
                if (Highscore > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", Highscore);
                }
                          
            yield return new WaitForSeconds(.2f);
            Vector3 ts = new Vector3(-1.057f, distance.transform.position.y, 0);
            Instantiate(distanceImage, ts, Quaternion.identity,DistanceParent);
            rb.useGravity = true;
            cb.enabled = false;
            yield return new WaitForSeconds(1f);
            UiController.instance.OpenWinPanel();
            yield return new WaitForSeconds(1f);
            if (GameManager.instance.score==0)
            {
                Debug.Log("score 0 burda þuan");
                GameManager.instance.score = Convert.ToInt32( mesafe * maxMesafe);
                //PlayerPrefs.SetInt("score", GameManager.instance.score);
               // UiController.instance.scoreText.text = GameManager.instance.score.ToString();
            }
            else
            {
               Debug.Log("score 0 deðil ");
                GameManager.instance.scoreArtisMiktari = Convert.ToInt32(mesafe * maxMesafe);
                GameManager.instance.IncreaseScore();
            }
        }
    }
}
