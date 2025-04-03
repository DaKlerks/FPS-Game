using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeParticle : MonoBehaviour
{
    private float timer;

	void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            Destroy(this.gameObject);
        }
    }
}
