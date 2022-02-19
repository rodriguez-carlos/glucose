using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTurret : Turret
{
    [SerializeField] private SphereTurretData data;
    [SerializeField] private Transform turretBarrel;
    int _bulletsLeft;
    float _firingTimer;
    bool _readyToShoot;
    private GameObject currentTarget;
    void Shoot()
    {
        _firingTimer = data.timeBetweenShots;
        Vector3 directionForBullet = currentTarget.transform.position - transform.position;
        GameObject newBullet = Instantiate(data.bullet, turretBarrel.position, Quaternion.identity);
        newBullet.transform.forward = directionForBullet.normalized;
        newBullet.GetComponent<Rigidbody>().AddForce(directionForBullet.normalized * data.shootForce, ForceMode.Impulse);
        _readyToShoot = false;
        _bulletsLeft--;
        if (_bulletsLeft == 0)
        {
            base.DestroyTurret();
        }
        //if (_firingTimer - Time.deltaTime == 0)
        //{
        //    ResetShooting();
        //}
        Invoke("ResetShooting", data.timeBetweenShots);
    }
    void ResetShooting()
    {
        _readyToShoot = true;
    }
    void Awake()
    {
        _bulletsLeft = data.magSize;
        _readyToShoot = true;
    }
    void Update()
    {
        currentTarget = DetectEnemy(data.turretRange, data.layerMask);
        Quaternion newRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, data.rotationSpeed * Time.deltaTime);
        if (currentTarget != null && _readyToShoot && _bulletsLeft > 0 /*&& gameObject.tag == "ActiveTurret"*/)
            Shoot();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.turretRange);
    }
}
