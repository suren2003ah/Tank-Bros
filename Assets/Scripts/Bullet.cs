using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    private float deathTime;
    public GameObject ParticleDeath;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Destroy(collisionInfo.collider.gameObject);
            Instantiate(ParticleDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        deathTime = Time.time + lifeTime;
    }
    void Update()
    {
        if (deathTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}
