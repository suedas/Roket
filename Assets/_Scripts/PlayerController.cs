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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            // score islemleri.. animasyon.. efect.. collectiblen destroy edilmesi.. 
            //Debug.Log("collectible");
            //GameManager.instance.IncreaseScore();
            RoketManager.instance.gaz -= 5f;
            Destroy(other.gameObject);
            //RoketManager.instance.maxgaz += 5;
           // Debug.Log("çarptý");

        }
        else if (other.CompareTag("obstacle"))
        {
            // score islemleri.. animasyon.. efect.. obstaclein destroy edilmesi.. 
            // oyun bitebilir bunun kontrolu de burada yapilabilir..
            //Debug.Log("obstacle");
            //GameManager.instance.DecreaseScore();
            RoketManager.instance.gaz += 5f;
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("finish"))
        {
            // oyun sonu olaylari... animasyon.. score.. panel acip kapatmak
            // oyunu kazandi mi kaybetti mi kontntrolu gerekirse yapilabilir.
            // player durdurulur. tagi finish olan obje level prefablarinin icinde yolun sonundadýr.
            // ornek olarak asagidaki kodda score 10 dan buyukse kazan degilse kaybet dedik ancak
            // bazý oyunlarda farkli parametlere göre kontrol etmek veya oyun sonunda karakterin yola devam etmesi gibi
            // durumlarda developer burayý kendisi duzenlemelidir.
            GameManager.instance.isContinue = false;
            if (GameManager.instance.levelScore > 10) UiController.instance.OpenWinPanel();
            else UiController.instance.OpenLosePanel();
        }
    }

    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlanýr. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon içinde yapilir.
    /// </summary>
    public void PreStartingEvents()
	{
        RoketManager.instance.rb.useGravity = false;
        RoketManager.instance.rb.velocity = Vector3.zero;
        RoketManager.instance.target.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        RoketManager.instance.transform.position = new Vector3 (-0.89f,0.6f,0);
        RoketManager.instance.target.transform.position = new Vector3(0, 0, 0);
        GameManager.instance.isContinue = false;
        RoketManager.instance.cb.enabled = true;
        RoketManager.instance.gaz = PlayerPrefs.GetFloat("gaz");
        RoketManager.instance.hiz = PlayerPrefs.GetFloat("hiz");
        RoketManager.instance.mesafe = PlayerPrefs.GetFloat("mesafe");
        for (int i = 0; i < SpawnManger.instance.objects.transform.childCount; i++)
        {
            Destroy(SpawnManger.instance.objects.transform.GetChild(i).gameObject);
        }
        for (int j = 0; j < RoketManager.instance.DistanceParent.transform.childCount; j++)
        {
            Destroy(RoketManager.instance.DistanceParent.GetChild(j).gameObject);
        }
        UiController.instance.incrementPanel.SetActive(true);

        if (RoketManager.instance.gazPara > GameManager.instance.score)
        {
           UiController.instance.gazButton.interactable = false;
        }
        else
        {
            UiController.instance.gazButton.interactable = true;
        }
        if (RoketManager.instance.hizPara > GameManager.instance.score)
        {
            UiController.instance.hizButton.interactable = false;
        }
        else
        {
            UiController.instance.hizButton.interactable = true;
        }
        if (RoketManager.instance.mesafePara > GameManager.instance.score)
        {
            UiController.instance.mesafeButton.interactable = false;
        }
        else
        {
            UiController.instance.mesafeButton.interactable = true;
        }
    }

    /// <summary>
    /// taptostart butonuna týklanýnca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlanýr, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>
    public void PostStartingEvents()
	{
        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
        StartCoroutine(RoketManager.instance.firlat());
        UiController.instance.incrementPanel.SetActive(false);
        UiController.instance.tapToStartPanel.SetActive(false);
        SpawnManger.instance.spawn();
        //RoketManager.instance.cb.enabled = true;
    }
}
