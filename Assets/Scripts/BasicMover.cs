using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour
{
    public float spinSpeed = 180.0f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector2.up * spinSpeed * Time.deltaTime);
    }
}
