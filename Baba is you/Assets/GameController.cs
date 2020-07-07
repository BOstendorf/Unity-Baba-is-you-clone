using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Priority_Queue;

public class GameController : MonoBehaviour
{
    private double _nextTime = 0.0f;
    private float _fieldWidth = 0.24f;
    private ObjectChanger _objectChanger;
    private InputHandler _inputHandler;
    GameObject[] _gameElements, _isOperators;
    SimplePriorityQueue<Rule> _RuleQueue = new SimplePriorityQueue<Rule>();
    Action[] _stateFunction;
    int _stepPointer = 0;


    // Start is called before the first frame update
    /* 1. find all GameElements
     * 2. seperate between isOperators and other GameElements and save as _gameElements and _isOperators respectively
     * This is done because we want to remove the Rules from each GameElement but isOperators at each time step to be able
     * to reassign the newly appropriate rules according to rule statements.
     * The isOperator should never lose it's Rule, because we can't reassign rules if we have no evaluable isOperators.
     * If isOperators and other GameElements are stored seperately, we don't have to differentiate both of them at each time step.
     * */
    void Start()
    {
        _stateFunction =  new Action[] { delegate () { EvaluateRuleStatements(); }, 
                                         delegate () { PerformHighPriorityRules(); }, 
                                         delegate () { PerformLowPriorityRules(); },
                                         delegate () { ResetGameState(); }};
        _objectChanger = gameObject.GetComponent<ObjectChanger>();
        _inputHandler = gameObject.GetComponent<InputHandler>();
        _inputHandler._fieldWidth = _fieldWidth;
        GameElement[] gameObjects = (GameElement[]) GameObject.FindObjectsOfType(typeof(GameElement));
        List<GameObject> isOperatorList = new List<GameObject>();
        List<GameObject> gameElementList = new List<GameObject>();
        foreach (GameElement obj in gameObjects)
        {
            if (obj.HasRule(typeof(RuleIs)))
            {
                isOperatorList.Add(obj.gameObject);
            }
            else
            {
                gameElementList.Add(obj.gameObject);
            }
        }
        _gameElements = gameElementList.ToArray();
        _isOperators = isOperatorList.ToArray();

        _objectChanger.SetGameElementsAndOperators(_gameElements, _isOperators);
        _stateFunction[_stepPointer]();
    }

    private void EvaluateRuleStatements()
    {
        HashSet<string> invalidChangeSet = ComputeInvalidChangeSet();
        EvaluateIsRules(invalidChangeSet);
        _objectChanger.AddRule("Text", typeof(RulePush));
        foreach (GameObject obj in _gameElements.Concat(_isOperators))
        {
            obj.GetComponent<GameElement>().ReturnRulePriorities(this);
        }
        _stepPointer++;
    }

    private void PerformHighPriorityRules()
    {
        if(_inputHandler._userInput != new Vector3(0, 0, 0) && Time.time >= _nextTime)
        {
            while (_RuleQueue.Count != 0 && _RuleQueue.First.Priority < 50)
            {
                Rule rule = _RuleQueue.Dequeue();
                rule.PerformStep(this, _inputHandler, _objectChanger);
            }
            _stepPointer++;
        }
    }

    private void PerformLowPriorityRules()
    {
        while (_RuleQueue.Count != 0)
        {
            Rule rule = _RuleQueue.Dequeue();
            rule.PerformStep(this, _inputHandler, _objectChanger);
        }
        _stepPointer++;
    }

    private void ResetGameState()
    {
        _nextTime = Time.time + 0.2f;
        _inputHandler._userInput = new Vector3(0, 0, 0);
        _objectChanger.RemoveScriptFromGameSubjects(typeof(Rule));
        _stepPointer = 0;
    }

    private void FixedUpdate()
    {
        _stateFunction[_stepPointer]();
    }

    private HashSet<string> ComputeInvalidChangeSet()
    {
        HashSet<string> invalidChangeSet = new HashSet<string>();
        foreach (GameObject obj in _isOperators)
        {
            HashSet<string> invSet = obj.GetComponent<RuleIs>().InvalidToChangeType();
            invalidChangeSet.Union(invSet);
        }
        return invalidChangeSet;
    }

    private void EvaluateIsRules(HashSet<string> invalidChangeSet)
    {
        foreach (GameObject obj in _isOperators)
        {
            RuleIs rule = obj.GetComponent<RuleIs>();
            rule.Invalid = invalidChangeSet;
            rule.PerformStep(this, _inputHandler, _objectChanger);
        }
    }

    public void RecieveRulePriority(int priority, Rule rule)
    {
        _RuleQueue.Enqueue(rule, priority);
    }

    public void Win()
    {
        Debug.Log("Win");
    }
}
