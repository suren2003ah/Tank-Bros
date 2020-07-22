using UnityEngine;
using EZCameraShake;
public class Bullet : MonoBehaviour {
	public float lifeTime;
	private float deathTime;
	public GameObject ParticleDeath;
	public Player player;

	private AudioSource audioSrc;

	public AudioClip shoot;
	public AudioClip hit;

	private float alive;

	public float shakeMultiplier = 1f;

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "Player") {
			if (alive >= 0.1f || collisionInfo.collider.gameObject != player.gameObject) {
				collisionInfo.collider.gameObject.SetActive(false);
				Instantiate(ParticleDeath, transform.position, Quaternion.identity);
				CameraShaker.Instance.ShakeOnce(13f * shakeMultiplier, 5f, 0.3f, 2f);
				Destroy(gameObject);
				SoundManager.PlayDie();
			}
		} else if (collisionInfo.collider.tag == "Wall") {
			CameraShaker.Instance.ShakeOnce(1f * shakeMultiplier, 2f, 0.1f, .5f);
			if (Vector3.Distance(transform.position, player.transform.position) <= 7f) {
				player.gameObject.SetActive(false);
				Instantiate(ParticleDeath, transform.position, Quaternion.identity);
				CameraShaker.Instance.ShakeOnce(13f * shakeMultiplier, 5f, 0.3f, 2f);
				Destroy(gameObject);
				SoundManager.PlayDie();
			}
			SoundManager.PlayHit();
		} else if (collisionInfo.collider.tag == "Bullet") {
			SoundManager.PlayHit();
		}

		if (deathTime <= Time.time) {
			Destroy(gameObject);
		}
	}
	void Start() {
		deathTime = Time.time + lifeTime;
		audioSrc = GetComponent<AudioSource>();
		SoundManager.PlayShoot();
	}
	void Update() {
		alive += Time.deltaTime;
	}
}
