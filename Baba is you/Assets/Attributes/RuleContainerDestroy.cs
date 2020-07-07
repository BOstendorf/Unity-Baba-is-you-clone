using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleContainerDestroy : RuleContainer
{
    public override System.Type RuleChange { get { return typeof(RuleDestroy); } set { } }
}
