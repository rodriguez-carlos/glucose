using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager instance;
    public int activeTurretCount;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        activeTurretCount = 0;
    }
    public void CountTurrets()
    {
        activeTurretCount = GameObject.FindGameObjectsWithTag("ActiveTurret").Length;
        if (activeTurretCount >= 2)
        {
            UIManager.instance.TurretCounter();
        }
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
