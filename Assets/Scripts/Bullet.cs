using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    private float deathTime;
    private CameraShake boom;
    public GameObject ParticleDeath;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            boom.ShakeKill();
            Destroy(collisionInfo.collider.gameObject);
            Instantiate(ParticleDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collisionInfo.collider.tag == "Wall")
        {
            boom.ShakeHit();
        }
        if (deathTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        deathTime = Time.time + lifeTime;
        boom = GameObject.FindGameObjectWithTag("CameraControl").GetComponent<CameraShake>();
    }
    void Update()
    {

    }
}
