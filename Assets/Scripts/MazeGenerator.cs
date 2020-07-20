using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour {

	public GameObject Wall;
	public GameObject ball;
	public float Walldistance;
	public int GridX, GridY;
	public float balanceCoefficient;

	void Start() {
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
				Vector3 center = transform.position + new Vector3((x) * Walldistance, 0, (y - 0.5f) * Walldistance);

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
	}
}
