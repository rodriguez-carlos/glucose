using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private float maxPlayerHP;
    private float playerHP;
    

    public void DamagePlayer (float damageOnPlayer)
    {
        playerHP -= damageOnPlayer;
        if (playerHP <= 0) GameManager.instance.LoadFirstScene();
        float healthPercertange = playerHP / maxPlayerHP;
        UIManager.instance.HealthBar(healthPercertange);
    }
    public void HealPlayer (float healingOnPlayer)
    {
        playerHP += healingOnPlayer;
        if (playerHP > maxPlayerHP) playerHP = maxPlayerHP;
        float healthPercertange = playerHP / maxPlayerHP;
        UIManager.instance.HealthBar(healthPercertange);
    }
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
    }
    private void Start()
    {
        playerHP = maxPlayerHP;
    }

}
