using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector3 FirstPosition = new Vector3(0,0,-10);
    public Transform SecondPosition, ThirdPosition;
    GameObject Player;
    bool MovementStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MoveCamera(Vector3 targetPosition)
    {
        MovementStarted= true;
        
        LeanTween.move(gameObject, targetPosition, 1f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() => { MovementStarted = false; });
    }


    // Update is called once per frame
    void Update()
    {
        if (MovementStarted) return; ;
        if(Player.transform.position.x < 9.7f)
        {
            if(gameObject.transform.position.x == SecondPosition.transform.position.x || gameObject.transform.position.x == ThirdPosition.transform.position.x)
            {
                MoveCamera(FirstPosition);
            }
        }
        else if(Player.transform.position.x > 9.7f)
        {
            if(FirstPosition.x == gameObject.transform.position.x|| gameObject.transform.position.x == ThirdPosition.transform.position.x)
            {
                MoveCamera(SecondPosition.transform.position);
            }
        }
        
        
    }
}
