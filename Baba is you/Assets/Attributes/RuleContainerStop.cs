using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleContainerStop : RuleContainer
{
    public override System.Type RuleChange { get { return typeof(RuleStop); } set { } }
}

