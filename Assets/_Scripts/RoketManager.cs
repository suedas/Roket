using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float maxgaz = 100;
    public float hiz = 0;
    public float maxhiz = 10;
    public Rigidbody rb;


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
                Debug.Log(hiz);
                //transform.Translate(0, y*Time.deltaTime, 0);
               // transform.position = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);
                rb.velocity = new Vector3(transform.position.x, hiz, transform.position.z);
                
                yield return new WaitForEndOfFrame();
            }
            rb.useGravity = true;
            //else
            //{
            //    //aþaðý düþecek
            //    Debug.Log("gaz bitti");

            //}
          

        }

    }
}
