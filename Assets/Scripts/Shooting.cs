using UnityEngine;
using EZCameraShake;
public class Shooting : MonoBehaviour {
	public GameObject Bullet;
	public GameObject Mine;
	public float bulletSpeed;
	public int shootMode = 0;
	public int bulletsLeft = 4;
	private MazeGenerator mg;
	void Start() {
		mg = GameObject.FindObjectOfType<MazeGenerator>();
	}

	public void Shoot() {
		if (shootMode == 1) {
			GameObject mine = Instantiate(Mine, transform.position - transform.forward * 12, Quaternion.identity);
			mine.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletSpeed, ForceMode.VelocityChange);
			shootMode = 0;
		} else if (shootMode == 0 && bulletsLeft > 0 && mg.bulletCount > 0) {
			bulletsLeft--;
			mg.bulletCount--;
			GameObject instBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
			instBullet.GetComponent<Bullet>().player = GetComponentInParent<Player>();
			instBullet.GetComponent<Bullet>().shooting = this;
			Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
			instBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
			CameraShaker.Shake(CameraShaker.Instance.shoot);
		}
		else if (shootMode == 2)
        {
			GameObject instBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
			instBullet.GetComponent<MeshRenderer>().enabled = false;
			instBullet.GetComponent<Bullet>().player = GetComponentInParent<Player>();
			instBullet.GetComponent<Bullet>().shooting = this;
			Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
			instBulletRigidbody.AddForce(transform.forward * bulletSpeed * 10, ForceMode.VelocityChange);
			CameraShaker.Shake(CameraShaker.Instance.shoot);
			gameObject.layer = 12;
			shootMode = 0;
        }
	}
}
