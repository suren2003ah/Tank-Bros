using UnityEngine;

public class Player : MonoBehaviour {
	// Start is called before the first frame update
	private Rigidbody rb;
	public GameObject Bullet;
	private Transform playerPos;
	public float forwardForce;
	public float turnForce;
	public int playerNumber;
	public float bulletSpeed;
	void Start() {
		playerPos = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
		if (playerNumber == 1) {
			if (Input.GetKeyDown("space")) {
				GameObject instBullet = Instantiate(Bullet, playerPos.position + playerPos.forward * 3, Quaternion.identity) as GameObject;
				Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
				instBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
			}
		} else if (playerNumber == 2) {
			if (Input.GetKeyDown("/")) {
				GameObject instBullet = Instantiate(Bullet, playerPos.position + playerPos.forward * 3, Quaternion.identity) as GameObject;
				Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
				instBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
			}
		} else if (playerNumber == 3) {
			// Aregak
		}
	}
	void FixedUpdate() {
		if (playerNumber == 1) {
			if (Input.GetKey("w")) {
				rb.AddForce(playerPos.forward * forwardForce, ForceMode.Force);
			} else if (Input.GetKey("s")) {
				rb.AddForce(playerPos.forward * -forwardForce, ForceMode.Force);
			}
			if (Input.GetKey("d")) {

				//playerPos.Rotate(0, 1, 0);
				rb.AddTorque(0, turnForce, 0);
			} else if (Input.GetKey("a")) {
				// playerPos.Rotate(0, -1, 0);
				rb.AddTorque(0, -turnForce, 0);
			}

		} else if (playerNumber == 2) {
			if (Input.GetKey("up")) {
				rb.AddForce(playerPos.forward * forwardForce, ForceMode.Force);
			} else if (Input.GetKey("down")) {
				rb.AddForce(playerPos.forward * -forwardForce, ForceMode.Force);
			}
			if (Input.GetKey("right")) {
				rb.AddTorque(0, turnForce, 0);
			} else if (Input.GetKey("left")) {
				rb.AddTorque(0, -turnForce, 0);
			}
		} else if (playerNumber == 3) {
			//Aregak

		}
	}
}
