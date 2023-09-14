using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBasicEngine : MonoBehaviour
{
    public Vector2 startSpeedMinValue;
    public Vector2 startSpeedMaxValue;
    private Engines engines;

    private void Start() 
    {
        engines = GetComponent<Engines>();
        engines.speed = new Vector2(Random.Range(startSpeedMinValue.x, startSpeedMaxValue.x), Random.Range(startSpeedMinValue.y, startSpeedMaxValue.y));
    }
}
