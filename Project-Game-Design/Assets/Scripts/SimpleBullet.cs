using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Bullet
{
    public string collideWithTag;
    private void Start() {
        targetTag = collideWithTag;
    }
}
