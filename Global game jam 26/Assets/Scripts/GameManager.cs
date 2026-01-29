using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Stack<Mask> masks = new Stack<Mask>();
    private GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Debug.LogWarning("Mask count: " + masks.Count);
    }

    public void AddMask(Mask mask)
    {
        masks.Push(mask);
    }
    
    public void RemoveMask()
    {
        masks.Pop();
    }
}
