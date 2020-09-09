using System.Collections.Generic;
using UnityEngine;

class PlayerJump
{
    private RangeValue<float> JumpValue = new RangeValue<float>(0f, 20f, 0f);
    private RangeValue<float> JumpResetTimer = new RangeValue<float>(0f, 0.2f, 0f);

    private RangeValue<int> JumpCounterValue = new RangeValue<int>(0, 2, 0);
    
    public float OutputJumpValue { get; private set; }

    private const float MOVE_JUMP = 1;
    private const string FLOOR = "Floor";
   
    private bool jumpInitiation;
    private float LastStateOfJump;

    private bool doubleJumpEnabled = true;
    private bool midJumpAvailable;
   

    //Jumping procedure handling
    public void JumpHandling(float jumpInput, List<GameObject> collidingObjects)
    {
        OutputJumpValue = JumpValue.Minimum;



        //jump of the floor
        if (FloorFound(collidingObjects))
        {
            midJumpAvailable = true;
            JumpCounterValue.Current = JumpCounterValue.Minimum;

            if (jumpInputRiseEdge(jumpInput))
            {
                midJumpAvailable = false;
                ++JumpCounterValue.Current;
                SetJumpValues();
            }
        }

        //double jump
        else if (JumpCounterValue.Current > JumpCounterValue.Minimum)
        {
            if (doubleJumpEnabled && jumpInputRiseEdge(jumpInput))
            {
                if (JumpCounterValue.Current < JumpCounterValue.Maximum)
                {
                    SetJumpValues();
                    JumpCounterValue.Current++;
                }
                else
                {
                    JumpCounterValue.Current = JumpCounterValue.Minimum;
                }
            }
        }

        //jump midair (no double jump)
        else if (midJumpAvailable)
        {
            if (jumpInputRiseEdge(jumpInput))
            {
                midJumpAvailable = false;
                SetJumpValues();
            } 
        }

        LastStateOfJump = jumpInput;
    }


    private bool jumpInputRiseEdge(float jumpInput) {

        if (jumpInput.Equals(1f) && (!jumpInput.Equals(LastStateOfJump)))
            return true;

        return false;
    }

    private void SetJumpValues()
    {
        OutputJumpValue = JumpValue.Maximum;
    }

    private bool FloorFound(List<GameObject> collidingObjects) {
        bool floorFound = false;

        collidingObjects.RemoveAll(x => (x == null));

        foreach (GameObject gameObject in collidingObjects)
        {
            if (gameObject.tag.Equals(FLOOR))
                floorFound = true;
        }
        return floorFound;
    }
}
