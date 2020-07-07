using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleHot : Rule
{
    public override int Priority { get { return 2; } set { } }

    public override void PerformStep(GameController controller, InputHandler inputHandler, ObjectChanger objectChanger)
    {
    }
}
