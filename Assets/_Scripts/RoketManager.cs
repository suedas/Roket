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
    public int mesafePara;
    public Rigidbody rb;
    public CinemachineBrain cb;
    public GameObject target;
    public GameObject distanceImage;
    public GameObject distance;
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
                    hiz += 0.2f;
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
            float distanceValue= transform.position.y;
            Vector3 ts = new Vector3(-1.057f, distance.transform.position.y, 0);
            Debug.Log(mesafe);         
            Instantiate(distanceImage, ts, Quaternion.identity);
            distanceImage.GetComponent<TextMeshPro>().text = Convert.ToInt32(distanceValue) +"m";
           // distanceText.text = mesafe.ToString() + "m";
            yield return new WaitForSeconds(.2f);
            GameManager.instance.IncreaseScore();

            rb.useGravity = true;
            cb.enabled = false;
            
        }

    }
}
