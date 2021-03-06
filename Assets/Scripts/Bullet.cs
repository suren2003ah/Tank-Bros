﻿using UnityEngine;
using EZCameraShake;
public class Bullet : MonoBehaviour {
	public float lifeTime;
	private float deathTime;
	public GameObject ParticleDeath;
	public Player player;
	public GameObject Shield;
	public Shooting shooting;
	private AudioSource audioSrc;

	public bool destroy;

	public AudioClip shoot;
	public AudioClip hit;

	public float alive;

	private bool lazer;
	public float shakeMultiplier = 1f;

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "Player") {
			if (alive >= 0.1f || collisionInfo.collider.gameObject != player.gameObject) {
				if (!destroy)
					collisionInfo.collider.gameObject.SetActive(false);
				else
					Destroy(collisionInfo.collider.gameObject);
				Instantiate(ParticleDeath, transform.position, Quaternion.identity);
				CameraShaker.Shake(CameraShaker.Instance.die);
				Destroy(gameObject);
				SoundManager.PlayDie();
			}
		} else if (collisionInfo.collider.tag == "Wall") {
			CameraShaker.Shake(CameraShaker.Instance.bounce);
			if (player)
				if (Vector3.Distance(transform.position, player.transform.position) <= 7f) {
					if (!destroy)
						player.gameObject.SetActive(false);
					else
						Destroy(player.gameObject);
					CameraShaker.Shake(CameraShaker.Instance.die);
					Instantiate(ParticleDeath, transform.position, Quaternion.identity);
					Destroy(gameObject);
					SoundManager.PlayDie();
				}
			if (!lazer)
				SoundManager.PlayHit();
		} else if (collisionInfo.collider.tag == "Bullet" && player.GetComponentInChildren<Shooting>().shootMode != 2) {
			SoundManager.PlayHit();
			Debug.Log("Collision");
		} else if (collisionInfo.collider.tag == "MinePowerUp" || collisionInfo.collider.tag == "SpeedPowerUp" || collisionInfo.collider.tag == "ShieldPowerUp") {
			Destroy(collisionInfo.collider.gameObject);
			Destroy(gameObject);
			CameraShaker.Shake(CameraShaker.Instance.bounce);
			Instantiate(ParticleDeath, transform.position, Quaternion.identity);
			SoundManager.PlayDie();
		}

		if (deathTime <= Time.time) {
			Destroy(gameObject);
		}
	}
	void Start() {
		deathTime = Time.time + lifeTime;
		audioSrc = GetComponent<AudioSource>();
		SoundManager.PlayShoot();
		lazer = !GetComponent<MeshRenderer>().enabled;
	}
	void Update() {
		alive += Time.deltaTime;
	}
	void OnDestroy() {
		//if (shooting)
		shooting.bulletsLeft++;
		GameObject.FindObjectOfType<MazeGenerator>().bulletCount++;
	}
}
