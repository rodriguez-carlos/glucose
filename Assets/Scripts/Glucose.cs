using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose : MonoBehaviour
{
    [SerializeField] private EasyEnemyData data;
    public Transform enemyHead;
    private Transform target;
    private float _timer;
    private float enemySpeed;
    private Animator animator;
    private void ChaseAndAttack()
    {
        var vectorEnemyPlayer = target.position - enemyHead.position;
        if (vectorEnemyPlayer.magnitude > data.runningThreshold && vectorEnemyPlayer.magnitude < data.detectionThreshold)
        {
            enemySpeed = data.glucoseSpeed;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if (vectorEnemyPlayer.magnitude <= data.runningThreshold && vectorEnemyPlayer.magnitude > data.attackThreshold)
        {
            enemySpeed = data.glucoseSpeed * 2;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            enemySpeed = 0;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        if (vectorEnemyPlayer.magnitude <= data.attackThreshold)
        {
            if (_timer >= data.attackCooldown)
            {
                _timer = 0;
                PlayerManager.instance.DamagePlayer(data.glucoseAttackPower);
            }
        }
        var chaseInX = transform.position.x + (enemySpeed * vectorEnemyPlayer.normalized.x * Time.deltaTime);
        var chaseInZ = transform.position.z + (enemySpeed * vectorEnemyPlayer.normalized.z * Time.deltaTime);
        transform.position = new Vector3(chaseInX, 0, chaseInZ);
        Quaternion newRotation = Quaternion.LookRotation(target.position - transform.position);
        newRotation.x = 0; 
        newRotation.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, data.rotationSpeed * Time.deltaTime);
    }
    private void Start()
    {
        target = EnemyManager.instance.playerFollowTarget;
        animator = GetComponent<Animator>();   

    }
    private void Update()
    {
        _timer += Time.deltaTime;
        ChaseAndAttack();
    }
}
