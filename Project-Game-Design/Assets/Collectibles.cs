using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private string itemType;
    [SerializeField] private float itemValue;
    [SerializeField] private float speed;
    [SerializeField] private float minXValueBeforeDestroy;

    private void FixedUpdate() {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if(transform.position.x < minXValueBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            GameManager.instance.CollectItem(itemType, itemValue);
            Destroy(gameObject);
        }
    }
}
