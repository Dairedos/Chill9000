using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public List<GameObject> CollidingObjects { get; private set; } = new List<GameObject>();


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Enter - Colliding with name: " + collider.gameObject.name.ToString());
        Debug.Log("Enter - Colliding with tag: " + collider.gameObject.tag.ToString());
        CollidingObjects.Add(collider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Exit - Colliding with name: " + collider.gameObject.name.ToString());
        Debug.Log("Exit - Colliding with tag: " + collider.gameObject.tag.ToString());
        CollidingObjects.Remove(collider.gameObject);
    }
}
