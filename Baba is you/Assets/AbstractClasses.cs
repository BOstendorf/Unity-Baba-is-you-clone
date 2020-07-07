using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* A abstract class for RuleContainers only implementing a function to remove the script from a GameObject
 * It is used to reference a RuleContainer without knowing exactly which rule is held.
 */
public abstract class RuleContainer : MonoBehaviour
{
    /* Each RuleContainer stores a Rule which can be assigned as component to a GameObject
     * RuleContainers are held by Adjective Elements, which can be evaluated as a rule.
     * We don't simply give the Adjective Element the Rule itself, because this way the Adjective Element
     * would evaluate the Rule in each time step, which leads to undesired results.
     */
    public abstract System.Type RuleChange { get; set; }

    public void RemoveSelf()
    {
        Destroy(this);
    }
}

/* A abstract class for Rules only implementing a function to remove the script from a GameObject
 * It is used to reference a Rule without knowing exactly which rule it.
 * */
public abstract class Rule : MonoBehaviour
{
    public abstract int Priority { get; set; }

    public void RemoveSelf()
    {
        //Debug.Log("RemoveSelf");
        Destroy(this);
    }
    /* Each Rule has to implement a PerformStep Method. This is the way different Rules
     * evaluate to different bahaviours. If a Rule should only be used to tag a GameObject,
     * the PerformStep body can be left empty and the tagging is evaluated by specific Rule Type.
     */
    public abstract void PerformStep(GameController controller, InputHandler inputHandler, ObjectChanger objectChanger);
}