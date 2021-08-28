using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    
    [SerializeField] float period = 2f;

    Vector3 startingPosition;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleMoving();
    }

    void ObstacleMoving()
    {
        if(period <= Mathf.Epsilon)
        {
            period += 1f; 
        }
        float cycles = Time.time / period;
        const float delta = Mathf.PI * 2;
        float sinWave = Mathf.Sin(cycles * delta);
        movementFactor = (sinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
