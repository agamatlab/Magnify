using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector2 FirstPosition = new Vector2(0,0);
    public Transform SecondPosition, ThirdPosition;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MoveCamera(Vector3 targetPosition)
    {
        LeanTween.move(gameObject, targetPosition, 5f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() => Debug.Log("Camera movement completed"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.x > 9.7f)
        {
            MoveCamera(SecondPosition.transform.position);
        }
        
        
    }
}
