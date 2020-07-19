using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public float bulletSpeed;
    public void Shoot()
    {   
        GameObject instBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
    }
}
