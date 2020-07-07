using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleWin : Rule
{
    // Start is called before the first frame update
    public override int Priority { get { return 100; } set { } }
    public override void PerformStep(GameController controller, InputHandler inputHandler, ObjectChanger objectChanger)
    {

        if (gameObject.GetComponent<GameElement>().HasRule(typeof(RuleYou)))
        {
            controller.Win();
        }
        else
        {
            RaycastHit2D[] hits = new RaycastHit2D[200];
            int numberOfHits = gameObject.GetComponent<Collider2D>().Cast(Vector3.up, hits, 0.0f);

            for (int i = 0; i < numberOfHits; i++)
            {
                if (hits[i].collider.gameObject.GetComponent<GameElement>().HasRule(typeof(RuleYou)))
                {
                    controller.Win();
                }
            }
        }
    }
}
