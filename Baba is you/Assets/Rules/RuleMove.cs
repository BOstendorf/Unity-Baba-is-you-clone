using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleMove :  Rule
{
    public override int Priority { get { return 10; } set { } }

    public override void PerformStep(GameController controller, InputHandler inputHandler, ObjectChanger objectChanger)
    {
        gameObject.transform.position = gameObject.transform.position + new Vector3(0.24f, 0, 0);
    }
}
