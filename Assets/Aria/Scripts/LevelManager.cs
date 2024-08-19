using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager instance;

    public List<Transform> elevators = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }

    public int GetElevatorID(Transform obj)
    {
        int id = 0;
        for(int i = 0; i< elevators.Count; i++)
        {
            if (obj == elevators[i])
            {
                id = i;
                break;
            }
        }

        return id;
    }
}
