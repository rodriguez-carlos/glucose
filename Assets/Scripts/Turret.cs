using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Turret : MonoBehaviour
{
    private Collider[] inRange = new Collider[10];
    public event Action OnTurretActivation;
    public event Action OnTurretDeath;
    private int EnemiesInRangeDetection(in float turretRange, in LayerMask layerMask)
    {
        int enemiesInRange = Physics.OverlapSphereNonAlloc(transform.position, turretRange, inRange, layerMask); //detection sphere for every type of turret
        return enemiesInRange;
    }
    public virtual GameObject DetectEnemy(float turretRange, LayerMask layerMask)
    {

        var enemiesInRange = EnemiesInRangeDetection(turretRange, layerMask);
        if (enemiesInRange == 0) return default;
        //int i = 0;
        //distancesToEnemies = new List<float>();
        var minDistance = float.MaxValue;
        var selectedTarget = 0;
        for (int i = 0; i < enemiesInRange; i++)
        {
            var currEnemy = inRange[i];
            var currDistance = (currEnemy.transform.position - transform.position).magnitude;
            if (currDistance < minDistance)
            {
                minDistance = currDistance;
                selectedTarget = i;
            }
        }

        return inRange[selectedTarget].gameObject.GetComponent<Glucose>().enemyHead.gameObject;
        /*
        foreach (var enemy in inRange)
        {
            float distanceToEnemy = (enemy.transform.position - transform.position).magnitude;
            //Debug.Log($"El {enemy.name} se encuentra a {distanceToEnemy}");
            distancesToEnemies.Add(distanceToEnemy);
            i++;
        }*/
        /*
        currentTarget = inRange[distancesToEnemies.IndexOf(distancesToEnemies.AsQueryable().Min())].gameObject;
        Debug.Log($"Current target is: {currentTarget.name}");
        return currentTarget;*/
    }
    
    public virtual GameObject[] DetectEnemies (float turretRange, LayerMask layerMask)
    {
        var enemiesInRange = EnemiesInRangeDetection(turretRange, layerMask);

        GameObject[] enemiesArray = new GameObject[enemiesInRange];
        for (int i = 0; i < enemiesInRange; i++)
        {
            enemiesArray[i] = inRange[i].gameObject;
        }

        return enemiesArray;
    }
    public virtual void ActivateTurret()
    {
        gameObject.tag = "ActiveTurret";
        OnTurretActivation?.Invoke();
    }
    public virtual void DestroyTurret()
    {
        Destroy(gameObject);
        OnTurretDeath?.Invoke();
    }
    void Start()
    {
        OnTurretActivation += TurretManager.instance.CountTurrets;
        OnTurretDeath += TurretManager.instance.CountTurrets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
