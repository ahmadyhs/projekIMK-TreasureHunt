using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveAlongFunction : MonoBehaviour
{
    public float speed = 1f;
    public float scale = 1f;
    public float offset = 0f;
    private Vector3 originalPosition;
    private float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        offset = transform.localPosition.x  + transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = Vector3.up * Mathf.Sin(offset + t) * scale;
        transform.position = originalPosition + displacement;
        t += Time.deltaTime * speed;

    }
}
