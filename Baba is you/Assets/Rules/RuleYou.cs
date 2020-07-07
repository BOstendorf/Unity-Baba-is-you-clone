using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleYou : ElementMover
{
    public override int Priority { get { return 1; } set { } }
    public override void PerformStep(GameController controller, InputHandler inputHandler, ObjectChanger objectChanger){
        Vector3 userInput = inputHandler._userInput;
        if (userInput != Vector3.zero && userInput.z == 0)
        {
            Move(userInput);
        }
    }
}
