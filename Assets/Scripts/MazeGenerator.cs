using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

	public GameObject Wall;
	public GameObject ball;
	public GameObject Player1;
	public GameObject Player2;
	public GameObject Player3;
	public float Walldistance;
	private int GridX, GridY;
	public float balanceCoefficient;
	private bool gameOver = false;
	private Vector2 player1;
	private Vector2 player2;
	private Vector2 player3;
	private Vector3 bottomLeft;

	private int gameNumber;

	public GameObject[] powerups;
	private List<GameObject> spawnedPowerups = new List<GameObject>();

	void Start() {
		GenerateMaze();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void GenerateMaze() {
		gameNumber++;
		//Remove all walls from previous game
		foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall")) {
			Destroy(wall);
			wall.SetActive(false);
		}
		// Remove all mines
		foreach (GameObject mine in GameObject.FindGameObjectsWithTag("Mine")) {
			Destroy(mine);
		}
		//Respawn all players
		if (Player1) {
			Player1.SetActive(true);
			Player1.GetComponentInChildren<Shooting>().shootMode = 0;
			if (Player1.GetComponentInChildren<Shield>()) {
				Player1.GetComponentInChildren<Shield>().gameObject.SetActive(false);
			}
			Player1.GetComponent<Player>().shooting.bulletsLeft = 4;
		}
		if (Player2) {
			Player2.SetActive(true);
			Player2.GetComponentInChildren<Shooting>().shootMode = 0;
			if (Player2.GetComponentInChildren<Shield>()) {
				Player2.GetComponentInChildren<Shield>().gameObject.SetActive(false);
			}
			Player2.GetComponent<Player>().shooting.bulletsLeft = 4;
		}
		if (Player3) {
			Player3.SetActive(true);
			Player3.GetComponentInChildren<Shooting>().shootMode = 0;
			if (Player3.GetComponentInChildren<Shield>()) {
				Player3.GetComponentInChildren<Shield>().gameObject.SetActive(false);
			}
			Player3.GetComponent<Player>().shooting.bulletsLeft = 4;
		}

		//Remove all bullets from last game

		foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet")) {
			Destroy(bullet);
			bullet.SetActive(false);
		}


		//Clear all powerups

		foreach (GameObject b in spawnedPowerups)
			Destroy(b);
		spawnedPowerups.Clear();

		gameOver = false;

		GridX = Random.Range(4, 12);
		GridY = Random.Range(4, 7);


		bottomLeft = transform.position - new Vector3(GridX * Walldistance, 0, GridY * Walldistance) * 0.5f;

		int length = 1;
		for (int u = 0; u < length; u++) {
			player1 = new Vector2(Random.Range(1, GridX + 1), Random.Range(1, GridY + 1));
			player2 = new Vector2(Random.Range(1, GridX + 1), Random.Range(1, GridY + 1));
			player3 = new Vector2(Random.Range(1, GridX + 1), Random.Range(1, GridY + 1));
			if (player1.x == player2.x && player1.y == player2.y || player1.x == player3.x && player1.y == player3.y || player3.x == player2.x && player3.y == player2.y) {
				length++;
			}
		}

		for (int x = 1; x < GridX + 1; x++) {
			for (int y = 0; y < GridY + 1; y++) {
				Instantiate(Wall, bottomLeft + new Vector3(x * Walldistance, 0, y * Walldistance), Quaternion.identity);
			}
		}
		Quaternion rotated = Quaternion.Euler(0, 90, 0);

		for (int x = 0; x < GridX + 1; x++) {
			for (int y = 1; y < GridY + 1; y++) {
				Instantiate(Wall, bottomLeft + new Vector3((x + 0.5f) * Walldistance, 0, (y - 0.5f) * Walldistance), rotated);
			}
		}


		for (int x = 1; x < GridX + 1; x++) {
			for (int y = 1; y < GridY + 1; y++) {
				Vector3 center = bottomLeft + new Vector3((x) * Walldistance, 2.2f, (y - 0.5f) * Walldistance);
				if (Player1 && x == player1.x && y == player1.y) {
					Player1.transform.position = center;
					Player1.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
				}
				if (Player2 && x == player2.x && y == player2.y) {
					Player2.transform.position = center;
					Player2.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
				}
				if (Player3 && x == player3.x && y == player3.y) {
					Player3.transform.position = center;
					Player3.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
				}

				Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right };
				if (x == GridX) directions[2] = Vector3.zero;
				if (y == 1) directions[1] = Vector3.zero;
				else if (y == GridY) directions[0] = Vector3.zero;

				List<GameObject> objs = new List<GameObject>();
				foreach (Vector3 direction in directions) {
					RaycastHit hit;
					if (Physics.Raycast(center, direction, out hit, Walldistance * 0.65f, LayerMask.GetMask("Wall"))) {
						objs.Add(hit.transform.gameObject);
					}
				}

				//Instantiate(ball, center, rotated);
				if (objs.Count == 0) continue;
				int i = Random.Range(0, objs.Count);
				objs[i].SetActive(false);
				Destroy(objs[i]);
			}
		}
	}

	// Update is called once per frame
	void Update() {
		if (GameObject.FindGameObjectsWithTag("Player").Length <= 1 && !gameOver) {
			StartCoroutine(Reload(2f));
			gameOver = true;
		}
		if (gameNumber < 2) {
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet")) {
				go.GetComponent<Bullet>().destroy = true;
			}
			if (Input.GetKeyDown("p")) {
				GenerateMaze();
			}
			if (GameObject.FindGameObjectsWithTag("Player").Length <= 2 && !gameOver) {
				StartCoroutine(Reload(0.5f));
				gameOver = true;
			}
		}
	}

	//At this point after what Suren has done to the code I don't care about
	//performance anymore, I just want this shit to be maintainable
	void FixedUpdate() {
		if (gameNumber > 2)
			if (spawnedPowerups.Count < 3)
				if (Random.Range(0f, 1f) <= (1 / 50f) / 8f) { // About one in every 8 seconds
					Vector2 coords = new Vector2(Random.Range(1, GridX + 1), Random.Range(1, GridY + 1));
					GameObject powerup = powerups[Random.Range(0, powerups.Length)];
					spawnedPowerups.Add(SpawnAtCoordinates(powerup, coords));
				}
	}

	IEnumerator Reload(float time) {
		yield return new WaitForSeconds(time);
		GenerateMaze();
	}

	GameObject SpawnAtCoordinates(GameObject obj, Vector2 coords) {
		Vector3 center = bottomLeft + new Vector3(coords.x * Walldistance, 2.5f, (coords.y - 0.5f) * Walldistance);
		return Instantiate(obj, center, Quaternion.identity) as GameObject;
	}
}
