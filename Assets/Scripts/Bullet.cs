using UnityEngine;
using EZCameraShake;
public class Bullet : MonoBehaviour {
	public float lifeTime;
	private float deathTime;
	public GameObject ParticleDeath;
	public Player player;
	private int ticks = 0;
	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "Player") {
			if (ticks > (0.1f/Time.deltaTime) || collisionInfo.collider.gameObject != player.gameObject) {
				collisionInfo.collider.gameObject.SetActive(false);
				Instantiate(ParticleDeath, transform.position, Quaternion.identity);
				CameraShaker.Instance.ShakeOnce(13f, 5f, 0.3f, 2f);
				Destroy(gameObject);
			}
		}
		if (collisionInfo.collider.tag == "Wall") {
			CameraShaker.Instance.ShakeOnce(1f, 2f, 0.1f, .5f);
			if (ticks == 0) {
				player.gameObject.SetActive(false);
				Instantiate(ParticleDeath, transform.position, Quaternion.identity);
				CameraShaker.Instance.ShakeOnce(13f, 5f, 0.3f, 2f);
				Destroy(gameObject);
			}
		}
		if (deathTime <= Time.time) {
			Destroy(gameObject);
		}
	}
	void Start() {
		deathTime = Time.time + lifeTime;
	}
	void Update() {
		ticks++;
	}
}
