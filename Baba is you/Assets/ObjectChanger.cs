using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ObjectChanger : MonoBehaviour
{
    public GameObject[] _gameElements { get; set; }
    public GameObject[] _isOperators { get; set; }

    


    public void SetGameElementsAndOperators(GameObject[] gameElements, GameObject[] isOperators)
    {
        _gameElements = gameElements;
        _isOperators = isOperators;
    }

    /*A Function to change the type of a GameElememt into another
     * 1. search all appropriate GameElements by Type
     * 2. set new GameElement type string
     * */
    public void ChangeObjectType(string typeOfToChangeObjects, string destType)
    {
        // 1. procedure step
        foreach (GameObject obj in _gameElements.Concat(_isOperators))
        {
            GameElement gameElement = obj.GetComponent<GameElement>();
            if (gameElement.Type == typeOfToChangeObjects)
            {
                // 2. procedure step
                Debug.Log("before change");
                gameElement.ChangeAnimation(destType);
                gameElement.Type = destType;
            }
        }
    }


    /* A function to change the meta field of a GameElement into another
     * 1. search all appropriate GameElements by Type
     * 2. set new GameElement meta string
     */
    public void ChangeObjectMeta(string typeOfToChangeObjects, string destMeta)
    {
        // 1. procedure step
        foreach (GameElement gameElement in (GameElement[])GameObject.FindObjectsOfType(typeof(GameElement)))
        {
            if (gameElement.Type == typeOfToChangeObjects)
            {
                // 2. procedure step
                gameElement.Meta = destMeta;
            }
        }
    }


    /*A function to add a rule to a GameElement
     * 1. search all appropriate GameElements by Type
     * 2. add a new script of given rule type
     * */
    public void AddRule(string sourceAttribute, Type ruleType)
    {
        // Debug.Log("AddRule");
        // 1. procedure step
        foreach (GameObject obj in _gameElements.Concat(_isOperators))
        {
            GameElement gameElement = obj.GetComponent<GameElement>();
            if (gameElement.Type == sourceAttribute)
            {
                // 2. procedure step
                obj.AddComponent(ruleType);
            }
        }
    }

    //A function that can be used to either remove a rule or a ruleContainer Component
    public void RemoveScriptFromGameSubjects(Type t)
    {
        foreach (GameObject obj in _gameElements.Concat(_isOperators))
        {
            if (t == typeof(Rule))
            {
                Rule[] rules = obj.gameObject.GetComponents<Rule>();
                foreach (Rule rule in rules)
                {
                    //Debug.Log(obj.gameObject);
                    if (rule.GetType() != typeof(RuleIs))
                    {
                        rule.RemoveSelf();
                        //Debug.Log(obj.gameObject.GetComponent<GameElement>().HasRule(typeof(RuleYou)));
                    }
                }
            }
            /*else if (t == typeof(RuleContainer))
            {
                RuleContainer[] ruleContainers = obj.gameObject.GetComponents<RuleContainer>();
                foreach (RuleContainer ruleContainer in ruleContainers)
                {
                    ruleContainer.RemoveSelf();
                }
            }*/
            else
            {
                throw new System.ArgumentException("Type Parameter has to be either Rule or RuleContainer");
            }
        }
    }
}
