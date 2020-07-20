using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour {

	public GameObject Wall;
	public GameObject ball;
	public Transform Player1;
	public Transform Player2;
	public Transform Player3;
	private int length = 1;
	public float Walldistance;
	public int GridX, GridY;
	public float balanceCoefficient;
	private bool gameOver = false;
	private Vector2 player1;
	private Vector2 player2;
	private Vector2 player3;
	void Start() {
		for (int u = 0; u < length; u++) {
			player1 = new Vector2(Random.Range(1, GridX + 1), Random.Range(1, GridY + 1));
			player2 = new Vector2(Random.Range(1, GridX + 1), Random.Range(1, GridY + 1));
			player3 = new Vector2(Random.Range(1, GridX + 1), Random.Range(1, GridY + 1));
			if (player1.x == player2.x || player1.x == player3.x || player3.x == player2.x || player1.y == player2.y || player1.y == player3.y || player3.y == player2.y) {
				length++;
			}
		}
		GenerateMaze();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void GenerateMaze() {
		for (int x = 1; x < GridX + 1; x++) {
			for (int y = 0; y < GridY + 1; y++) {
				Instantiate(Wall, transform.position + new Vector3(x * Walldistance, 0, y * Walldistance), Quaternion.identity);
			}
		}
		Quaternion rotated = Quaternion.Euler(0, 90, 0);

		for (int x = 0; x < GridX + 1; x++) {
			for (int y = 1; y < GridY + 1; y++) {
				Instantiate(Wall, transform.position + new Vector3((x + 0.5f) * Walldistance, 0, (y - 0.5f) * Walldistance), rotated);
			}
		}


		for (int x = 1; x < GridX + 1; x++) {
			for (int y = 1; y < GridY + 1; y++) {
				Vector3 center = transform.position + new Vector3((x) * Walldistance, 2.2f, (y - 0.5f) * Walldistance);
				if (x == player1.x && y == player1.y) {
					Player1.position = center;
					Player1.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
				}
				if (x == player2.x && y == player2.y) {
					Player2.position = center;
					Player2.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
				}
				if (x == player3.x && y == player3.y) {
					Player3.position = center;
					Player3.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
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
			StartCoroutine(Reload());
			gameOver = true;
		}
	}

	IEnumerator Reload() {
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
