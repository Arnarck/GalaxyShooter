using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 4f);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * 0.1f);
    }
}
