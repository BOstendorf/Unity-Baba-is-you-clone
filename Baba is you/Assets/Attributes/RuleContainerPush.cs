using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleContainerPush : RuleContainer
{
    public override System.Type RuleChange { get { return typeof(RulePush); } set { } }
}
