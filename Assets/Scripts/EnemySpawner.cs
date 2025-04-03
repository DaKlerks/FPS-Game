using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private GameObject player;
    public float distance;
    public Transform[] spawnPoints;
    public float cooldown = 10;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        if (Vector3.Distance(spawnPoints[spawnPointIndex].position, player.transform.position) > distance && timer >= cooldown)
        {
            GameObject zombie = Instantiate(enemy, spawnPoints[spawnPointIndex].position + new Vector3(1, 1, 0), Quaternion.identity);
            timer = 0;
        }
    }
}
