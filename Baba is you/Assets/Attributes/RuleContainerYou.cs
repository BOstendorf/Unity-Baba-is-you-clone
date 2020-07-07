using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleContainerYou : RuleContainer
{
    public override System.Type RuleChange { get { return typeof(RuleYou); } set { } }
}
