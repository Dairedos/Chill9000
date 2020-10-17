using UnityEngine.Tilemaps;
using UnityEngine;

public class RestrictedAreaTilemap : MonoBehaviour
{
    [SerializeField]
    private bool collidingWithPlayer;

    [SerializeField]
    private string DetectedColliderName;

    [SerializeField]
    public GameObject ValidAreaCheck;

    void Start()
    {
        collidingWithPlayer = false;

        if (!this.GetComponent<TilemapCollider2D>())
        {
            Debug.Log("Restricted area in List does not have an Collider. Name: " + this.name);
        }
        else if (!this.GetComponent<Rigidbody2D>())
        {
            Debug.Log("Restricted area in List does not have a RigidBody. Name: " + this.name);
        }
        else
        {
            if (!this.GetComponent<TilemapCollider2D>().isTrigger)
            {
                Debug.Log("Restricted area in List does not have Trigger in Collider set. Please Do so. Name: " + this.name);
                Debug.Log("Internaly Setting trigger on. Name: " + this.name);
                this.GetComponent<TilemapCollider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((ValidAreaCheck != null) &&
            col.tag.Equals(DetectedColliderName) &&
            (!collidingWithPlayer))
        {
            collidingWithPlayer = true;
            ValidAreaCheck.GetComponent<ValidAreaController>().ToCheckPoint();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if ((ValidAreaCheck != null) &&
            col.tag.Equals(DetectedColliderName))
        {
            collidingWithPlayer = false;

        }
    }

    public void AckRestrictedArea(GameObject ValidAreaCheck)
    {
        if (this.ValidAreaCheck == null)
            this.ValidAreaCheck = ValidAreaCheck;
        else
            Debug.Log("Trying to acknowledge Restricted Area box multiple times! RAB Name: " + this.name);
    }
}
