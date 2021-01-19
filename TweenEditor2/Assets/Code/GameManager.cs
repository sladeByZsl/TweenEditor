using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<10;i++)
        {
            ParticleManager.GetInstance().GetParticleInstance("name", (instance) => {
                instance.Print();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
