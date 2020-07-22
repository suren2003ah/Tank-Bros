using UnityEngine;
using EZCameraShake;
using System.Collections;
public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Mine;
    public float bulletSpeed;
    public void Shoot(int shootMode)
    {
        if (shootMode == 1)
        {
            StartCoroutine(BombPlant());
        }
        else if (shootMode == 0)
        {
            GameObject instBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
            instBullet.GetComponent<Bullet>().player = GetComponentInParent<Player>();
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
            CameraShaker.Instance.ShakeOnce(3f, 4f, 0.1f, 1f);
        }
    }
    IEnumerator BombPlant()
    {
        Vector3 initialPos = transform.position;
        yield return new WaitForSeconds(1f);
        Instantiate(Mine, initialPos, Quaternion.identity);
    }
}
