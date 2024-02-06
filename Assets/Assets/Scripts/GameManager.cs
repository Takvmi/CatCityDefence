using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static readonly object Padlock = new object();

    public bool GameGoing;
    
    public static GameManager Instance
    {
        get
        {
            lock (Padlock)
                return _instance ?? (_instance = new GameManager());
        }
    }

    public void StartGame() => GameGoing = true;
    public void EndGame() => GameGoing = false;
    
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
