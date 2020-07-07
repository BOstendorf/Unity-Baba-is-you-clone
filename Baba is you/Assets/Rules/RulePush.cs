using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulePush : ElementMover
{
    public override int Priority { get { return 2; } set { } }

    public override void PerformStep(GameController controller, InputHandler inputHandler, ObjectChanger objectChanger)
    {
    }

    public bool Push(Vector3 direction)
    {
        return Move(direction);
    }
}
