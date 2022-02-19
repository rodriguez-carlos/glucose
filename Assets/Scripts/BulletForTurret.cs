using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForTurret : MonoBehaviour
{
    [SerializeField] private SphereTurretData data;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Glucose")
        {
            Debug.Log("shot zombie");
            collision.gameObject.GetComponent<Glucose>().TakeDamage(data.turretAttackPower);
            Destroy(gameObject);
        }
    }

}
