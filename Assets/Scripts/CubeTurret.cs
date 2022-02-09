using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTurret : Turret
{
    [SerializeField] private CubeTurretData data;
    void CastElectricityField()
    {
        // here be electricity field script
    }
    private void Update()
    {
        base.DetectEnemy(data.turretRange, data.layerMask); // Cube turret hurts in an area; therefore, needs not a single current target
    }
    private void OnDrawGizmosSelected()
    {
        //let's draw the detection sphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.turretRange);
    }
}
