using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
	// Start is called before the first frame update
	private Rigidbody rb;
	private Transform playerPos;
	public GameObject MovingPlayer1;
	public GameObject MovingPlayer2;
	public GameObject ParticleDeath;
	private GameObject shield;
	public float forwardForce;
	public float turnForce;
	public int playerNumber;
	public Shooting shooting;
	private float initialForwardForce;

	private float speedTimeout = 0;

	void Awake(){
		shield = gameObject.GetComponentInChildren<Shield>().gameObject;
		shield.SetActive(false);
	}

	void Start() {
		playerPos = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
		initialForwardForce = forwardForce;
	}
	void SpeedPowerUp(GameObject powerup)
    {
		forwardForce *= 1.3f;
		Destroy(powerup);
		speedTimeout += 10f;
	}
	void Lazer(GameObject powerup)
    {
		shooting.shootMode = 2;
		Destroy(powerup);
	}
	void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "SpeedPowerUp")
        {
			SpeedPowerUp(collisionInfo.collider.gameObject);
			SoundManager.PlayPowerup();
        }
		if (collisionInfo.collider.tag == "ShieldPowerUp") 
        {
			shield.SetActive(true);
			Destroy(collisionInfo.collider.gameObject);
			SoundManager.PlayPowerup();
		}
		if (collisionInfo.collider.tag == "LazerPowerUp")
        {
			Lazer(collisionInfo.collider.gameObject);
			SoundManager.PlayPowerup();
        }
		if (collisionInfo.collider.tag == "MinePowerUp")
        {
			shooting.shootMode = 1;
			Destroy(collisionInfo.collider.gameObject);
			SoundManager.PlayPowerup();
		}
		if (collisionInfo.collider.tag == "Mine")
        {
			Destroy(collisionInfo.collider.gameObject);
			gameObject.SetActive(false);
			shooting.shootMode = 0;
			Instantiate(ParticleDeath, transform.position, Quaternion.identity);	
			SoundManager.PlayDie();
			EZCameraShake.CameraShaker.Shake(EZCameraShake.CameraShaker.Instance.die);
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
		if(speedTimeout > 0) {
			speedTimeout-=Time.deltaTime;
		}
		else{
			forwardForce = initialForwardForce;
			speedTimeout = 0;
		}
        if (Input.GetKeyDown("r"))
        {
			StartCoroutine(RickRoll());
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
	IEnumerator RickRoll()
    {
		GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(18.5f);
		GetComponent<AudioSource>().Stop();
    }
}
