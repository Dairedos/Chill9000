using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private string DetectedColliderName;
    

    public bool Available { get; set; }
    public System.DateTime AvailableTimestamp { get; private set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals(DetectedColliderName))
        {
            if (!Available)
                AvailableTimestamp = System.DateTime.Now;

            Available = true;
            
        }
    }

}
