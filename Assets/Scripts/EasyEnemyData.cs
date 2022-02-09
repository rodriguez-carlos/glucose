using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="ScriptableObjects/Enemy", fileName = "NewEasyEnemyData", order = 0)]
public class EasyEnemyData : ScriptableObject
{
    public float glucoseSpeed;
    public float glucoseAttackPower;
    public float attackThreshold; // Within this threshold, begin attacking and stop chasing
    public float detectionThreshold; // Within this threshold, start walking toward player menacingly
    public float runningThreshold; // Within this threshold, make them shit their pants
    public float rotationSpeed;
    public float attackCooldown;
    public int enemyHP;
}
