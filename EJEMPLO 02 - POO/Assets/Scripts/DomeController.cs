using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0.02f, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Munition") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = Vector3.zero;
        }
    }
}
