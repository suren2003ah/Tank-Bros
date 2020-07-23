using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Start is called before the first frame update
	private Rigidbody rb;
	private Transform playerPos;
	public GameObject MovingPlayer1;
	public GameObject MovingPlayer2;
	public GameObject ParticleDeath;
	public float forwardForce;
	public float turnForce;
	public int playerNumber;
	public Shooting shooting;
	private float initialForwardForce;
	void Start() {
		playerPos = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
	}
	IEnumerator SpeedPowerUp(GameObject powerup)
    {
		initialForwardForce = forwardForce;
		forwardForce *= 2;
		Destroy(powerup);
		yield return new WaitForSeconds(10f);
		forwardForce = initialForwardForce;
	}
	void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "SpeedPowerUp")
        {
			StartCoroutine(SpeedPowerUp(collisionInfo.collider.gameObject));
        }
		if (collisionInfo.collider.tag == "MinePowerUp")
        {
			shooting.shootMode = 1;
			Destroy(collisionInfo.collider.gameObject);
		}
		if (collisionInfo.collider.tag == "Mine")
        {
			Destroy(collisionInfo.collider.gameObject);
			gameObject.SetActive(false);
			Instantiate(ParticleDeath, transform.position, Quaternion.identity);
			shooting.shootMode = 0;
		}
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
			if (Input.GetMouseButtonDown(0)) {
				shooting.Shoot();
			}
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
			if (Input.GetMouseButton(1)) {
				rb.AddForce(playerPos.forward * forwardForce, ForceMode.Force);
			}
			if (Input.GetMouseButton(2)) {
				rb.AddForce(playerPos.forward * -forwardForce, ForceMode.Force);
			}
			float input = Input.GetAxis("Mouse X");
			rb.AddTorque(0, input * turnForce, 0);
		}
	}
}
