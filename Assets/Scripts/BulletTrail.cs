using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private float timer;
    LineRenderer lineRend;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.05f)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        timer = 0;
    }
}
