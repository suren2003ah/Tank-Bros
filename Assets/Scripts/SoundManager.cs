using UnityEngine;

public class SoundManager : MonoBehaviour {

	private AudioSource src;

	public AudioClip shoot;
	public AudioClip hit;
	public AudioClip die;

	static SoundManager mgr;

	private void RandomPitch() {
		src.pitch = Random.Range(0.75f, 1.25f);
	}

	void Start() {
		src = GetComponent<AudioSource>();
		mgr = this;
	}

	public static void PlayHit() {
		mgr.RandomPitch();
		mgr.src.PlayOneShot(mgr.hit);
	}
	public static void PlayShoot() {
		mgr.RandomPitch();
		mgr.src.PlayOneShot(mgr.shoot);
	}
	public static void PlayDie() {
		mgr.RandomPitch();
		mgr.src.PlayOneShot(mgr.die);
	}

}
