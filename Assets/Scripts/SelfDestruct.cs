using UnityEngine;

public class SelfDestruct : MonoBehaviour {
	// Start is called before the first frame update
	public float lifetime;
	private float deathtime;
	void Start() {
		deathtime = Time.time + lifetime;
	}

	// Update is called once per frame
	void Update() {
		if (Time.time >= deathtime) Destroy(gameObject);
	}
}
