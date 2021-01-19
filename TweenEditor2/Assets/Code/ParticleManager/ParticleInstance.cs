using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleInstance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print()
    {
        Debug.LogError(Guid.NewGuid().GetHashCode());
    }
}
