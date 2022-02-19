using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Turret", fileName = "NewSphereTurretData", order = 0)]
public class SphereTurretData : ScriptableObject
{
    public float turretRange;
    public LayerMask layerMask;
    public int turretAttackPower;
    public float rotationSpeed;
    public GameObject bullet;
    public float shootForce;
    public float timeBetweenShots;
    public int magSize;
}
