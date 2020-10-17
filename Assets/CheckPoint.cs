using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private string DetectedColliderName;

    private Animator animator;

    public bool Available { get; set; }
    public System.DateTime AvailableTimestamp { get; private set; }

    private void Start() {

        animator = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals(DetectedColliderName))
        {
            if (!Available)
            {
                AvailableTimestamp = System.DateTime.Now;
            }

            animator.SetBool("CheckpointTaken", true);
            Available = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals(DetectedColliderName))
        {
            animator.SetBool("CheckpointTaken", false);
        }
    }

}
