using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    private float timer;

    private GameObject spawner;
    private List<GameObject> HitPool;
    private ParticleSystem hit;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        HitPool = spawner.GetComponent<ParticleSpawner>().HitPool;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            if (HitPool.Contains(this.gameObject))
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnEnable()
    {
        hit = GetComponent<ParticleSystem>();
        hit.Play();
    }

    void OnDisable()
    {
        timer = 0;
    }
}
