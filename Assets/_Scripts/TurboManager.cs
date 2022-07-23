using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TurboManager : MonoBehaviour
{
    #region Singleton
    public static TurboManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion

    public float beklemeSüresi;
    public int turbo;
    public CinemachineVirtualCamera vcam;

    void Update()
    {
        //UiController.instance.slider.value = RoketManager.instance.maxgaz - RoketManager.instance.gaz;

        if (GameManager.instance.isContinue == true)
        {
            if (Input.GetMouseButton(0))
            {
                //PlayerController.instance.particleObs.SetActive(false);
            
                StartCoroutine(Shake());
                beklemeSüresi += Time.fixedDeltaTime;
                if (beklemeSüresi>.2f)
                {
                    Turbo();
                }
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                beklemeSüresi = 0;
                UiController.instance.particleGas.SetActive(true);
                UiController.instance.turboParticle.SetActive(false);


            }
        }
    }
    public void Turbo()
    {
        if (RoketManager.instance.gaz>0&& turbo>0)
        {
            beklemeSüresi = 0;

            RoketManager.instance.hiz += 5;
            turbo--;
            UiController.instance.turboslider.value = turbo;
            Debug.Log(turbo);
            UiController.instance.particleGas.SetActive(false);
            UiController.instance.turboParticle.SetActive(true);
        }
        else
        {
            UiController.instance.particleGas.SetActive(true);
            UiController.instance.turboParticle.SetActive(false);
        }

        
    }
    public  IEnumerator Shake()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.5f);
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}
