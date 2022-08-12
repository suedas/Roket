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
    [HideInInspector]public int turbo;
    [HideInInspector]public int maxTurbo;
    public Animator playerAnimator;
    public CinemachineVirtualCamera vcam;

	private void Start()
	{
        maxTurbo = 10;
        turbo = maxTurbo;
        UiController.instance.turboslider.maxValue = turbo;
        UiController.instance.turboslider.value = turbo;
	}

	void Update()
    {
        //UiController.instance.slider.value = RoketManager.instance.maxgaz - RoketManager.instance.gaz;

        if (GameManager.instance.isContinue == true)
        {
            if(Input.GetMouseButtonDown(0) && turbo > 0)
			{
                playerAnimator.SetTrigger("bak");
			}

            if (Input.GetMouseButton(0))
            {
                //PlayerController.instance.particleObs.SetActive(false);
            
               
                Debug.Log(beklemeSüresi);
                beklemeSüresi += Time.deltaTime;
                //Debug.Log(Time.deltaTime);
                if (beklemeSüresi >= .2f)
                {
                    beklemeSüresi = 0;
                    Turbo();
                }
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                beklemeSüresi = 0;
                UiController.instance.particleGas.SetActive(true);
                UiController.instance.turboParticle.SetActive(false);
                StartCoroutine(TurboSonrasi());

            }
        }
    }

    public IEnumerator SliderSet()
	{
        float tempValue = UiController.instance.turboslider.value;
		while(tempValue > turbo){
            tempValue = tempValue - .1f;
            UiController.instance.turboslider.value = tempValue;
            yield return new WaitForSeconds(.019f);
		}
	}

    public void Turbo()
    {
        UiController.instance.uiHand.SetActive(false);
        StopCoroutine(SliderSet());
        StartCoroutine(SliderSet());
        if (RoketManager.instance.gaz>0 && turbo>0)
        {
            StartCoroutine(Shake());
            RoketManager.instance.hiz += 2;
            turbo--;
            
            UiController.instance.particleGas.SetActive(false);
            UiController.instance.turboParticle.SetActive(true);
        }
        else
        {
           StartCoroutine( TurboSonrasi());
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

    IEnumerator TurboSonrasi()
	{
        float tempHiz = PlayerPrefs.GetFloat("hiz");
		while (tempHiz < RoketManager.instance.hiz)
		{
            RoketManager.instance.hiz -= .2f;
            yield return new WaitForSeconds(.05f);
		}

	}
}
