using UnityEngine;

public class Player : MonoBehaviour {
	// Start is called before the first frame update
	private Rigidbody rb;
	private Transform playerPos;
	public float forwardForce;
	public float turnForce;
	public int playerNumber;
	public Shooting shooting;
	void Start() {
		playerPos = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
		if (playerNumber == 1) {
			if (Input.GetKeyDown("space")) {
				shooting.Shoot();
			}
		} else if (playerNumber == 2) {
			if (Input.GetKeyDown("/")) {
				shooting.Shoot();
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
