using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InuBouncer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Inu>(out Inu inu))
        {
            inu.Reverse();
        }
    }
}
