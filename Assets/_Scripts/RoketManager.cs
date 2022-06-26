using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
                    hiz += 0.5f;
                }
                else if (gaz>maxgaz*9/10)
                {
                    hiz -= 0.5f;
                }
                gaz += 1;
                rb.velocity = new Vector3(transform.position.x, hiz, transform.position.z);                
                yield return new WaitForEndOfFrame();
            }
            mesafe = transform.position.y;
            Debug.Log(mesafe);
            GameManager.instance.IncreaseScore();
            yield return new WaitForSeconds(.2f);
            
            rb.useGravity = true;
            cb.enabled = false;
            
        }

    }
}
