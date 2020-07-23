using UnityEngine;

public class PowerupAnimation : MonoBehaviour
{
	public float rate;

    void Update()
    {
		transform.Rotate(Vector3.up, Time.deltaTime * rate);
    }
}
