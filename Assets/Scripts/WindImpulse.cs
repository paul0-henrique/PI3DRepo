using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindImpulse : MonoBehaviour
{
    [SerializeField] float force = 300;
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
