using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject TopText;
    [SerializeField] private GameObject TurretCounterText;
    [SerializeField] private GameObject DNANoticeText;
    [SerializeField] private float noticePeriod;
    // Start is called before the first frame update
    public void HealthBar(float healthPercentage)
    {
        HUD.GetComponentInChildren<Slider>().value = healthPercentage;
    }
    public void GlucoseCounter()
    {
        TopText.GetComponentInChildren<Text>().text = $"Glucose left: {EnemyManager.instance.glucoseCount}";
    }
    public void TurretCounter()
    {
        TurretCounterText.SetActive(true);
        TurretCounterText.GetComponentInChildren<Text>().text = $"Active turrets: ";
    }
    public void DNANotice()
    {
        //float _noticeTimer = 0;
        DNANoticeText.SetActive(true);
        Invoke("DeactivateDNANotice", noticePeriod);
    }
    void DeactivateDNANotice()
    {
        DNANoticeText.SetActive(false);
    }
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        TurretCounterText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GlucoseCounter();
    }
}
