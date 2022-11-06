using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skittle : MonoBehaviour
{
    [SerializeField] private int number;
    private Vector3 _startRotation;
    private Rigidbody _skittleRb;

    private void Start()
    {
        _startRotation = transform.eulerAngles;
        _skittleRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var deltaRotation = _startRotation - transform.eulerAngles;
        if (Mathf.Abs(deltaRotation.x) > 50 || Mathf.Abs(deltaRotation.z) > 50)
        {
            Destroy(this);
            transform.GetComponentInParent<SkittlesController>().onSkittleFallen(number);
        }
    }

    public void StopMove()
    {
        transform.eulerAngles = _startRotation;
        _skittleRb.velocity = Vector3.zero;
        _skittleRb.angularVelocity = Vector3.zero;
    }
}
