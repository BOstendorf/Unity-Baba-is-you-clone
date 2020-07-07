using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChanger
{
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimation(string destType)
    {
        GameElement[] gameElements = GameObject.FindObjectsOfType<GameElement>();
        foreach (GameElement gameElement in gameElements)
        {
            Animator animator = gameElement.gameObject.GetComponent<Animator>();
            
        }
    }
}
