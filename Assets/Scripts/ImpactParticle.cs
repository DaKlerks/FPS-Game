using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticle : MonoBehaviour
{
    private float timer;

    private GameObject spawner;
    private List<GameObject> ImpactPool;
    private ParticleSystem impact;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        ImpactPool = spawner.GetComponent<ParticleSpawner>().ImpactPool;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            if (ImpactPool.Contains(this.gameObject))
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
        impact = GetComponent<ParticleSystem>();
        impact.Play();
    }

    void OnDisable()
    {
        timer = 0;
    }
}
