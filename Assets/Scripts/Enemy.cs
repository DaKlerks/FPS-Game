using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private GameObject player;
    [SerializeField] private Slider slider;
    private GameObject camera;
    [SerializeField] private Transform Explosion;

    [Header("Stats")]
    public float maxHealth;
    private float currentHealth;
    public float damage;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = Random.Range(5, 16);
        damage = Random.Range(1, 4);
        speed = Random.Range(3, 7);
        agent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        slider.value = currentHealth;
        slider.transform.rotation = camera.transform.rotation;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Transform trans = Instantiate(Explosion, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
        slider.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
