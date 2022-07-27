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

    public int gaz = 0;
    public int levelGaz=1;
    public int gazPara=50;
    public float maxgaz = 100;
    public float hiz = 0;
    public int levelHiz = 1;
    public int hizPara = 50;
    public float maxhiz = 10;
    public float mesafe;
    public float income=1;
    public int mesafeLevel=1;
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
        if (GameManager.instance.isContinue)
        {
            while (gaz <maxgaz) 
            {
                if (gaz<maxgaz/10 && hiz<maxhiz)
                {
                    hiz += .5f;
                }
           
                else if (gaz>maxgaz*8/10)
                {
                    hiz -= 0.3f;
                }
                //UiController.instance.particleGas.SetActive(false);
                gaz += 1;
                rb.velocity = new Vector3(0, hiz, 0);
                target.GetComponent<Rigidbody>().velocity= new Vector3(target.transform.position.x, hiz,target.transform.position.z);
                 //yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(0.012f);
                UiController.instance.slider.value = maxgaz - gaz;

            }
            UiController.instance.particleGas.SetActive(false);
            yield return new WaitForSeconds(.5f);
            mesafe = transform.position.y;
            Highscore = Convert.ToInt32(mesafe * income);
            distanceImage.GetComponent<TextMeshPro>().text = (int)mesafe * (int)income + "m";
           
                if (Highscore > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", Highscore);
                }
                          
            yield return new WaitForSeconds(.2f);
            rb.useGravity = true;
            while (rb.velocity.y>=0)
            {
                yield return new WaitForEndOfFrame();
                if (rb.velocity.y<=0)
                {
                    Vector3 ts = new Vector3(-1.3f, distance.transform.position.y, 0);
                    Instantiate(distanceImage, ts, Quaternion.identity,DistanceParent);
                }
               
            }
           
            cb.enabled = false;
            yield return new WaitForSeconds(2f);
            UiController.instance.OpenWinPanel();
            yield return new WaitForSeconds(1f);
            //Debug.Log("winpanelden sonra =" + maxMesafe);
            Debug.Log("win panelden sonra measafe" + mesafe);
            GameManager.instance.IncreaseScore(Convert.ToInt32(mesafe * income));
            //if (GameManager.instance.score==0)
            //{
            //    Debug.Log("score 0 burda þuan");
            //    Debug.Log(mesafe + "mesafe ");
            //    Debug.Log(maxMesafe + "maxmesafe");
            //    GameManager.instance.score = Convert.ToInt32( mesafe * maxMesafe);
            //    PlayerPrefs.SetInt("score", GameManager.instance.score);
            //    UiController.instance.scoreText.text = GameManager.instance.score.ToString();
            //    Debug.Log(GameManager.instance.score + "  score 0 ken gelen deðer ");
            //}
            //else
            //{
               
            //   Debug.Log("score 0 deðil "+ GameManager.instance.score);
            //   GameManager.instance.IncreaseScore();
            //}
           
        }
    }
}
