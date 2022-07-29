using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion
    public GameObject particleObs;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            RoketManager.instance.gaz -= 5;         
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("obstacle"))
        {
            RoketManager.instance.gaz += 5;
            particleObs.SetActive(true);
            StartCoroutine(delay());
                
            Destroy(other.gameObject);

        }
       
    }

    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlanýr. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon içinde yapilir.
    /// </summary>
    public void PreStartingEvents()
    {
        UiController.instance.interactable();
        RoketManager.instance.rb.useGravity = false;
        RoketManager.instance.rb.velocity = Vector3.zero;
        RoketManager.instance.target.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        RoketManager.instance.transform.position = new Vector3 (0,0,0);
        transform.position = new Vector3(0, 0, 0);
        RoketManager.instance.target.transform.position = new Vector3(0, 0, 0);
        GameManager.instance.isContinue = false;
        RoketManager.instance.cb.enabled = true;
        RoketManager.instance.gaz = 0;
        UiController.instance.particleGas.SetActive(false);
        UiController.instance.slider.value = RoketManager.instance.maxgaz;
        StopCoroutine(TurboManager.instance.SliderSet());
        TurboManager.instance.turbo = TurboManager.instance.maxTurbo;
        UiController.instance.turboslider.value = TurboManager.instance.maxTurbo;
        //for (int i = 0; i < SpawnManger.instance.objects.transform.childCount; i++)
        //{
        //    Destroy(SpawnManger.instance.objects.transform.GetChild(i).gameObject);
        //}
        for (int j = 0; j < RoketManager.instance.DistanceParent.transform.childCount; j++)
        {
            Destroy(RoketManager.instance.DistanceParent.GetChild(j).gameObject);
        }
        
    }
    public IEnumerator delay()
    {
        yield return new WaitForSeconds(1.3f);
        particleObs.SetActive(false);
    }
    /// <summary>
    /// taptostart butonuna týklanýnca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlanýr, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>
    public void PostStartingEvents()
    {
        UiController.instance.particleGas.SetActive(true);
        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
        StartCoroutine(RoketManager.instance.firlat());
        UiController.instance.incrementPanel.SetActive(false);
        UiController.instance.tapToStartPanel.SetActive(false);
        
    }
}
