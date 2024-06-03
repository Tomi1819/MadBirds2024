using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float destroyImpactMagnitude = 5;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Bird>())
        {
            Destroy(gameObject);
        }

        if (collision.relativeVelocity.magnitude > destroyImpactMagnitude)
        {
            Destroy(gameObject);
        }
    }
}
