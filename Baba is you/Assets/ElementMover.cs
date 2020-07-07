using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementMover : Rule
{

    private bool _checked = false;

    public bool Move(Vector3 direction)
    {
        bool movable = true;
        if (!_checked)
        {
            _checked = true;
            RaycastHit2D[] hits = new RaycastHit2D[200];
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            if (collider == null)
            {
                throw new System.Exception("ElementMover has to be able to access a collider2D");
            }
            int numberOfHits = gameObject.GetComponent<Collider2D>().Cast(direction, hits, 0.24f);
            if (numberOfHits > 0)
            {
                for (int i = 0; i < numberOfHits; i++)
                {
                    GameElement gameElement = hits[i].collider.gameObject.GetComponent<GameElement>();
                    if (gameElement.HasRule(typeof(RuleStop)))
                    {
                        movable = false;
                    }
                    else if (gameElement.HasRule(typeof(RulePush)))
                    {
                        RulePush rulePush = gameElement.GetComponent<RulePush>();
                        movable = rulePush.Push(direction);
                    }

                }
            }
            if (movable)
            {
                gameObject.transform.position = gameObject.transform.position + direction;
            }
        }
        return movable;
    }
}
