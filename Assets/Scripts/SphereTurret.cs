using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTurret : Turret
{
    [SerializeField] private SphereTurretData data;
    [SerializeField] private Transform turretBarrel;
    [SerializeField] private AudioClip gunshotSound;
    [SerializeField] private ParticleSystem muzzleFlash;
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
        AudioSource.PlayClipAtPoint(gunshotSound, transform.position);
        muzzleFlash.Play();
        _readyToShoot = false;
        _bulletsLeft--;
        if (_bulletsLeft == 0)
        {
            base.DestroyTurret();
        }
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
        if (currentTarget)
        {
            Quaternion newRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, data.rotationSpeed * Time.deltaTime);
            if (_readyToShoot && _bulletsLeft > 0 /*&& gameObject.tag == "ActiveTurret"*/)
                Shoot();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.turretRange);
    }
}
