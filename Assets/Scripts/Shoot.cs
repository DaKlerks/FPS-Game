using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //animation
    private bool shooting = false;
    private new Transform camera;
    private ParticleSystem flash;
    private Animator anim;

    [Header("Particles")]
    public GameObject particleSpawner;
    private ParticleSpawner spawner;
    private List<GameObject> ImpactPool;
    private List<GameObject> HitPool;
    private bool allImpactActive;
    private bool allHitActive;
    [SerializeField] GameObject line;
    [SerializeField] Transform barrel;
    LineRenderer lineRend;

    [Header("Stats")]
    public float damage;
    [SerializeField] float bulletSpread;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponent<Animator>();
        flash = GetComponent<ParticleSystem>();
        camera = transform.parent;
        lineRend = line.GetComponent<LineRenderer>();
        spawner = particleSpawner.GetComponent<ParticleSpawner>();
        ImpactPool = particleSpawner.GetComponent<ParticleSpawner>().ImpactPool;
        HitPool = particleSpawner.GetComponent<ParticleSpawner>().HitPool;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Calculation();
            Animation();
        }
        else
        {
            shooting = false;
            if (shooting == false)
            {
                anim.SetBool("Shooting", false);
            }
        }
        lineRend.SetPosition(0, barrel.position);
    }

    void Calculation()
    {
        RaycastHit hit;
        int layerMask = 1 << 3;

        //give raycast spread

        Vector3 fwd = camera.transform.forward;
        fwd = fwd + camera.TransformDirection(new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread)));

        if (Physics.Raycast(camera.position, fwd, out hit, Mathf.Infinity, layerMask))
        { 
            //enemy hit
            //deal damage
            line.SetActive(true);
            lineRend.SetPosition(1, hit.point);
            SpawnHitParticle(hit);
            hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if (Physics.Raycast(camera.position, fwd, out hit))
        {
            //something hit
            //spawn impact effect
            line.SetActive(true);
            lineRend.SetPosition(1, hit.point);
            SpawnImpactParticle(hit);
        }
        else
        {
            //nothing hit
            Vector3 endpoint = camera.position + fwd * 100;
            line.SetActive(true);
            lineRend.SetPosition(1, endpoint);
        }
    }

    void Animation()
    {
        if (anim != null)
        {
            shooting = true;
        }
        if (shooting == true && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && !anim.IsInTransition(0))
        {
            anim.SetBool("Shooting", true);
            flash.Play();
        }
    }

    void SpawnImpactParticle(RaycastHit hit)
    {
        allImpactActive = particleSpawner.GetComponent<ParticleSpawner>().allImpactActive;
        if (ImpactPool.Count == ImpactPool.Capacity && !allImpactActive)
        {
            spawner.ReuseImpact(hit);
        }
        else if (ImpactPool.Count == ImpactPool.Capacity && allImpactActive)
        {
            spawner.SpawnImpact(hit);
        }
        else
        {
            spawner.SpawnImpact(hit);
        }
    }

    void SpawnHitParticle(RaycastHit hit)
    {
        allHitActive = particleSpawner.GetComponent<ParticleSpawner>().allHitActive;
        if (HitPool.Count == HitPool.Capacity && !allHitActive) 
        {
            spawner.ReuseHit(hit);
        }
        else if (HitPool.Count == HitPool.Capacity && allHitActive)
        {
            spawner.SpawnHit(hit);
        }
        else
        {
            spawner.SpawnHit(hit);
        }
    }
}
