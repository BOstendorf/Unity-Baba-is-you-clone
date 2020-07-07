using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RuleIs : Rule
{
    public override int Priority { get { return 0; } set { } }

    public HashSet<string> Invalid { get; set; }


    /* A function to check if a valid rule statement forms.
     * 1. Check the fields left and right and up and down for neighbouring gameElements.
     * 2. Check Meta Attribute of neighbouring gameElements.
     * 3. Check if first element (i.e. left or top neighbour) is the representation text of a gameElement for which a rule can be applied.
     * 4. Check if second element (i.e. right or bottom neighbour) is the representation text of a gameElement or an Adjective element.
     * 5a. If representation text: change all gameElements of first representation texts type to those of the second ones.
     * 5b. If adjective: Give all gameElements of first representation texts type the rule held by the adjective element.
     */
    public override void PerformStep(GameController controller, InputHandler inputHandler, ObjectChanger objectChanger)
    {
        // 1. procedure step
        foreach (Vector2 direction in new Vector2[] {Vector2.left, Vector2.up })
        {
            RaycastHit2D[] hitsFirst = new RaycastHit2D[200];
            int numberOfHitsFirst = gameObject.GetComponent<Collider2D>().Cast(direction, hitsFirst, 0.24f);

            // 2. procedure step
            for (int i = 0; i < numberOfHitsFirst; i++)
            {
                // 3. procedure step
                GameElement elementFirst = hitsFirst[i].collider.gameObject.GetComponent<GameElement>();
                if (elementFirst.Meta.StartsWith("RepresentationText"))
                {
                    // 2. procedure step
                    RaycastHit2D[] hitsSecond = new RaycastHit2D[200];
                    int numberOfHitsSecond = gameObject.GetComponent<Collider2D>().Cast(direction * -1, hitsSecond, 0.24f);
                    string representedTypeFirst = elementFirst.Meta.Split(new char[] { '-' })[1];
                    for (int n = 0; n < numberOfHitsSecond; n++)
                    {
                        // 4. procedure step
                        GameElement elementSecond = hitsSecond[n].collider.gameObject.GetComponent<GameElement>();
                        if(elementSecond.Meta.StartsWith("RepresentationText"))
                        {
                            // 5a. procedure step
                            string representedTypeSecond = elementSecond.Meta.Split(new char[] { '-' })[1];
                            if (!Invalid.Contains(representedTypeFirst))
                            {
                                objectChanger.ChangeObjectType(representedTypeFirst, representedTypeSecond);
                                if (representedTypeFirst == "Text")
                                {
                                    objectChanger.ChangeObjectMeta(representedTypeFirst, "Object");
                                }
                                else if (representedTypeSecond == "Text")
                                {
                                    objectChanger.ChangeObjectMeta(representedTypeFirst, "RepresentationText-" + representedTypeFirst);
                                }
                            }
                        }
                        else if(elementSecond.Meta == "Adjective")
                        {
                            // 5b. procedure step
                            objectChanger.AddRule(representedTypeFirst, elementSecond.GetComponent<RuleContainer>().RuleChange);
                        }
                    }
                }
            }
        }
    }

    /* A function that checks if rules statements of type obj1 is obj1 forms. This is done to prevent other rules to change the type of obj1
     * 
     */
    public HashSet<string> InvalidToChangeType()
    {
        HashSet<string> resultSet = new HashSet<string>();
        foreach (Vector2 direction in new Vector2[] { Vector2.left, Vector2.up })
        {
            RaycastHit2D[] hitsFirst = new RaycastHit2D[200];
            int numberOfHitsFirst = gameObject.GetComponent<Collider2D>().Cast(direction, hitsFirst, 0.24f);

            for (int i = 0; i < numberOfHitsFirst; i++)
            {
                GameElement elementFirst = hitsFirst[i].collider.gameObject.GetComponent<GameElement>();
                if (elementFirst.Meta.StartsWith("RepresentationText"))
                {
                    RaycastHit2D[] hitsSecond = new RaycastHit2D[200];
                    int numberOfHitsSecond = gameObject.GetComponent<Collider2D>().Cast(direction * -1, hitsSecond, 0.24f);
                    for (int n = 0; n < numberOfHitsSecond; n++)
                    {
                        GameElement elementSecond = hitsSecond[n].collider.gameObject.GetComponent<GameElement>();
                        if(elementSecond.Meta == elementFirst.Meta)
                        {
                            resultSet.Add(elementFirst.Meta.Split(new char[] { '-' })[1]);
                        }
                    }
                }
            }
        }
        return resultSet;
    }
}
