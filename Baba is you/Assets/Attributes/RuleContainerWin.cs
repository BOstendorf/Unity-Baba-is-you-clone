using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleContainerWin : RuleContainer
{
    public override System.Type RuleChange { get { return typeof(RuleWin); } set { } }
}
