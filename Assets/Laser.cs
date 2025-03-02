using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



// Unity Script | 0 references
public class Laser : MonoBehaviour
{
    // Line Of Renderer
    public LineRenderer LineOfSight;

    public int reflections;
    public float MaxRayDistance;
    public LayerMask LayerDetection;
    public float rotationSpeed;
    bool reload = false;

    // Unity Message | 0 references
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Unity Message | 0 references
    private void Update()
    {

        Vector3 startPoint = LineOfSight.GetPosition(0);
        Vector3 endPoint = LineOfSight.GetPosition(1);

        // Get all collisions along the line
        RaycastHit2D[] hits = Physics2D.LinecastAll(startPoint, endPoint);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.tag == "Player" && !reload)
                {
                    reload = true;
                    GameObject mask = GameObject.FindGameObjectWithTag("Mask");
                    LeanTween.scale(mask, new Vector3(0, 0, 0), 0.5f);
                    StartCoroutine(ReloadLevel(0.6f));

                }
            }
        }

        transform.Rotate(rotationSpeed * Vector3.forward * Time.deltaTime);
        LineOfSight.positionCount = 1;
        LineOfSight.SetPosition(0, transform.position);


        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, MaxRayDistance, LayerDetection);

        bool isMirror = false;
        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;

        for (int i = 0; i < reflections; i++)
        {
            LineOfSight.positionCount += 1;

            if (hitInfo.collider != null)
            {
                LineOfSight.SetPosition(LineOfSight.positionCount - 1, hitInfo.point);

                isMirror = false;
                if (hitInfo.collider.CompareTag("Mirror"))
                {
                    mirrorHitPoint = (Vector2)hitInfo.point;
                    mirrorHitNormal = (Vector2)hitInfo.normal;
                    hitInfo = Physics2D.Raycast((Vector2)hitInfo.point, Vector2.Reflect(hitInfo.point, hitInfo.normal), MaxRayDistance, LayerDetection);
                    isMirror = true;
                }
                else
                    break;
            }
            else
            {
                if (isMirror)
                {
                    LineOfSight.SetPosition(LineOfSight.positionCount - 1, mirrorHitPoint + Vector2.Reflect(mirrorHitPoint, mirrorHitNormal) * MaxRayDistance);
                    break;
                }
                else
                {
                    LineOfSight.SetPosition(LineOfSight.positionCount - 1, transform.position + transform.right * MaxRayDistance);
                    break;
                }
            }
        }
    }

    private IEnumerator ReloadLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("SampleScene");

    }
}


