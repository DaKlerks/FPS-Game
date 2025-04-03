using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [Header("Impact Particle")]
    public Transform impact;
    public int ImpactPoolCapacity;
    public List<GameObject> ImpactPool = new List<GameObject>();
    public bool allImpactActive;

    [Header("Enemy Hit Particle")]
    public Transform Hit;
    public int HitPoolCapacity;
    public List<GameObject> HitPool = new List<GameObject>();
    public bool allHitActive;

    void Start()
    {
        ImpactPool.Capacity = ImpactPoolCapacity;
        HitPool.Capacity = HitPoolCapacity;
    }

    void Update()
    {
        CheckImpact();
        CheckHit();
    }

    void CheckImpact()
    {
        if (ImpactPool.Count == ImpactPool.Capacity)
        {
            allImpactActive = true;
            for (int i = 0; i < ImpactPool.Capacity; i++)
            {
                if (!ImpactPool[i].activeInHierarchy)
                {
                    allImpactActive = false;
                    break;
                }
            }
        }
    }

    public void SpawnImpact(RaycastHit hit)
    {
        Transform trans = Instantiate(impact, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        trans.forward = hit.normal;
        if (ImpactPool.Count < ImpactPool.Capacity)
        {
            ImpactPool.Add(trans.gameObject);
        }
    }

    public void ReuseImpact(RaycastHit hit)
    {
        //Enable first in pool then make it the last of the pool
        GameObject impact = ImpactPool[0];
        impact.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        impact.transform.forward = hit.normal;
        impact.SetActive(true);
        ImpactPool.RemoveAt(0);
        ImpactPool.Add(impact);
    }

    void CheckHit()
    {
        if (HitPool.Count == HitPool.Capacity)
        {
            allHitActive = true;
            for (int i = 0; i < HitPool.Capacity; i++)
            {
                if (!HitPool[i].activeInHierarchy)
                {
                    allHitActive = false;
                    break;
                }
            }
        }
    }

    public void SpawnHit(RaycastHit hit)
    {
        Transform trans = Instantiate(Hit, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        trans.forward = hit.normal;
        if (HitPool.Count < HitPool.Capacity)
        {
            HitPool.Add(trans.gameObject);
        }
    }

    public void ReuseHit(RaycastHit hit)
    {
        //Enable first in pool then make it the last of the pool
        GameObject hitpoint = HitPool[0];
        hitpoint.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        hitpoint.transform.forward = hit.normal;
        hitpoint.SetActive(true);
        HitPool.RemoveAt(0);
        HitPool.Add(hitpoint);
    }
}
