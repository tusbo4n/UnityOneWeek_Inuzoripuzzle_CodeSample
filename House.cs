using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Present")
        {
            GameSceneController.instance.Success();
            collision.gameObject.GetComponent<Present>().Success();
            Debug.Log("OK");
        }
    }
}
