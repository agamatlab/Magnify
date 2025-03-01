using UnityEngine;
using Unity.Mathematics;
using System.Runtime.CompilerServices;

public class AlignToCursor : MonoBehaviour
{
    private Camera mainCamera; // Reference to the main camera
    [SerializeField]
    private Transform playerCenter;  // Reference to the player object for determining facing direction

    private Vector2 initParentPosition;

    private Vector3 initialScale; // Store the initial scale of the object
    [SerializeField]
    private Transform effector;
    float radius;
    void Start()
    {
        initParentPosition = transform.parent.localPosition;
        radius = transform.localPosition.x - transform.parent.localPosition.x;
        // Assign the main camera if not set
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Store the initial scale of the gun
        initialScale = transform.localScale;

        print($"Initial Parent Position: transform.parent.localPosition --> {initParentPosition.ToString()}");
        print($"radius {transform.localPosition.x} - {transform.parent.localPosition.x} --> {radius}");

    }

    void FixedUpdate()
    {
        if (!Application.isFocused) return;
        
        AlignWithCursor();
    }

    void AlignWithCursor()
    {
        // Get the cursor position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;

        Vector3 cursorWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        // Calculate the direction from the object to the cursor
        Vector3 direction = cursorWorldPosition - playerCenter.position;

        // Calculate the angle and apply it to the object
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float changeX= Mathf.Cos(angle * Mathf.Deg2Rad), changeY = Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.localPosition = new Vector2(changeX * radius + initParentPosition.x, changeY * radius);

        //int i = 0;
        //while ((transform.position.x < effector.position.x - 0.2 || transform.position.x > effector.position.x + 0.2
        // || transform.position.y < effector.position.y - 0.2 || transform.position.y > effector.position.y + 0.2) && i < 40
        //)
        //{
        //    float diffX = (effector.position.x - transform.position.x) / Mathf.Abs((effector.position.x - transform.position.x));
        //    float diffY = (effector.position.y - transform.position.y) / Mathf.Abs((effector.position.y - transform.position.y));
        //    Vector2 oldPos = transform.localPosition;
        //    transform.localPosition = new Vector2(oldPos.x + diffX*changeX, oldPos.y + diffY * changeY);
        //    i++;
        //}

        // Check if the cursor is to the left or right of the player
        if (cursorWorldPosition.x < playerCenter.position.x)
        {
            // Flip the gun horizontally when the cursor is on the left
            //transform.localScale = new Vector3(initialScale.x, -initialScale.y, initialScale.z);
        }
        else
        {
            // Keep the gun's original scale when the cursor is on the right
            //transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
    }
}
