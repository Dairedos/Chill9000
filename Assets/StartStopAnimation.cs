using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopAnimation : MonoBehaviour
{
    [SerializeField]
    private string DetectedColliderName;

    Animator animator;

    private const string start = "StartAnimation";
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals(DetectedColliderName))
        {
               
            animator.SetBool(start, true);

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals(DetectedColliderName))
        {
            animator.SetBool(start, false);
        }
    }
}
