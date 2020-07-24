using UnityEngine;

public class Shield : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // other.gameObject.GetComponent<Bullet>().player != GetComponentInParent<Player>() &&
        if (other.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<Bullet>().alive >= 0.1f)
            {
                Destroy(other.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
