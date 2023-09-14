using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : MonoBehaviour
{
    private BaseAvatar avatar;
    public Vector2 speed;

    private void Start() 
    {
        avatar = GetComponent<BaseAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed.x, speed.y, 0) * avatar.maxSpeed * Time.fixedDeltaTime;
    }
}
