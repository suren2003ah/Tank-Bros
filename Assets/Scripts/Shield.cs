using UnityEngine;

public class Shield : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // other.gameObject.GetComponent<Bullet>().player != GetComponentInParent<Player>() && 
        if (other.tag == "Bullet")
        {
            /*float distance1 = Vector3.Distance(other.gameObject.transform.position, transform.position);
            float distance2 = Vector3.Distance(other.gameObject.transform.position + other.gameObject.GetComponent<Rigidbody>().velocity, transform.position);*/
            if (other.gameObject.GetComponent<Bullet>().alive >= 0.05f)
            {
                Destroy(other.gameObject);
                gameObject.SetActive(false);
            }   
        }

    }
}
