using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Turret", fileName = "NewCubeTurretData", order = 1)]
public class CubeTurretData : ScriptableObject
{
    public float turretRange;
    public LayerMask layerMask;
    public float turretAttackPower;
}

