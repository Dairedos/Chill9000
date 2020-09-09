class PlayerSideMove
{


    private RangeValue<float> MovementValue = new RangeValue<float>(0f, 10f, 0f);

    private float speedUpStep = 0.5f;
    private float slowDownStep = 1f;

    private bool movesPositive = false;
    private bool movesNegative = false;

    public float OutputMovementValue { get; private set; }
    
    private const float MOVE_POSITIVE = 1;
    private const float MOVE_NEGATIVE = -1;
    private const float MOVE_IDLE = 0;

    //sets to exact direction that player currently faces
    public void SetPlayersDirection(float movementInput)
    {
        if (movementInput.Equals(MOVE_POSITIVE))
        {
            if (movesNegative)
                MovementValue.Current = MovementValue.Minimum;

            movesPositive = true;
            movesNegative = false;

        }
        else if (movementInput.Equals(MOVE_NEGATIVE))
        {
            if (movesPositive)
                MovementValue.Current = MovementValue.Minimum;

            movesNegative = true;
            movesPositive = false;
        }
    }

    //handles speed regulation
    public void MovementSpeedHandling(float movementInput)
    {

        //speedUp movement
        if ((!movementInput.Equals(MOVE_IDLE)) && (MovementValue.Current < MovementValue.Maximum))
            MovementValue.Current += speedUpStep;

        //slowDown movement
        else if ((movementInput.Equals(MOVE_IDLE)) && (MovementValue.Current > MovementValue.Minimum))
            MovementValue.Current -= slowDownStep;

        OutputMovementValue = movementInput * MovementValue.Current;
    }
}