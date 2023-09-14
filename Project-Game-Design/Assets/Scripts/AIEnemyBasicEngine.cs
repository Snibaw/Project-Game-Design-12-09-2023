using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBasicEngine : MonoBehaviour
{
    [SerializeField] private float timeBtwZigZag;
    private float timeSinceLastZigZag;
    public Vector2 startSpeedMinValue;
    public Vector2 startSpeedMaxValue;
    private Engines engines;
    public bool isZigZaging = true;

    private void Start() 
    {
        engines = GetComponent<Engines>();
        float ySpeed = 0;
        if(isZigZaging) ySpeed = Random.Range(startSpeedMinValue.y, startSpeedMaxValue.y);
        engines.speed = new Vector2(Random.Range(startSpeedMinValue.x, startSpeedMaxValue.x), ySpeed);
        timeSinceLastZigZag = 0;
    }
    private void Update() {
        if(isZigZaging)
        {
            timeSinceLastZigZag+=Time.deltaTime;
            if(timeSinceLastZigZag >= timeBtwZigZag)
            {
                timeSinceLastZigZag = 0;
                engines.speed.y *= -1;
            }
        }
        
    }
}
