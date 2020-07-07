using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour
{
   
    public string Type;
    public string Meta;
    public Animator _animator; 
    public void ReturnRulePriorities(GameController controller)
    {
        Rule[] rules = GetComponents<Rule>();
        foreach (Rule rule in rules)
        {
            if(rule.GetType() != typeof(RuleIs))
            {
                controller.RecieveRulePriority(rule.Priority, rule);
            }
        }
    }

    public bool HasRule(Type ruleType)
    {
        Rule[] rules = GetComponents<Rule>();
        foreach (Rule rule in rules)
        {
            if (rule.GetType() == ruleType)
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeAnimation(string trigger)
    {
        Debug.Log(trigger);
        _animator.SetTrigger("Reset");
        _animator.SetTrigger(trigger);
    }
}
