using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserBehaviour : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing from this GameObject.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lineRenderer.positionCount < 2)
        {
            Debug.LogWarning("LineRenderer does not have enough points to perform a linecast.");
            return;
        }

        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        RaycastHit hit;
        if (Physics.Linecast(startPoint, endPoint, out hit))
        {
            Debug.Log("Collision detected with: " + hit.collider.name);
        }
    }

    void Start()
    {
        // You can also initialize or configure your line here if needed.
    }

    void Update()
    {
        // Update the line dynamically if necessary.
    }
}
