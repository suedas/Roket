using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboManager : MonoBehaviour
{
   
    public float beklemeS�resi;
    public int turbo=5;
    void Update()
    {
        UiController.instance.slider.value = RoketManager.instance.maxgaz - RoketManager.instance.gaz;

        if (GameManager.instance.isContinue == true)
        {
            if (Input.GetMouseButton(0))
            {
                beklemeS�resi += Time.fixedDeltaTime;
                if (beklemeS�resi>.2f)
                {
                    Turbo();
                }
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                beklemeS�resi = 0;
            }
        }
    }
    public void Turbo()
    {
        if (RoketManager.instance.gaz>0&& turbo>0)
        {
            beklemeS�resi = 0;

            RoketManager.instance.hiz += 5;
            turbo--;
            Debug.Log(turbo);
        }

        
    }
}
