using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] Vector3 rotationAngle = new Vector3(0.0f, 1.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(rotationAngle, rotationSpeed * Time.deltaTime);
    }
}
