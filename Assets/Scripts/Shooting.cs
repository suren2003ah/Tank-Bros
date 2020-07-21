using UnityEngine;
using EZCameraShake;
public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public float bulletSpeed;
    public void Shoot()
    {   
        GameObject instBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
		instBullet.GetComponent<Bullet>().player = GetComponentInParent<Player>();
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
		CameraShaker.Instance.ShakeOnce(3f, 4f, 0.1f, 1f);
    }
}
