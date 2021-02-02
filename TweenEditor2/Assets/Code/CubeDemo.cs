using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeDemo : MonoBehaviour
{
    DOTweenAnimation dt;
    // Start is called before the first frame update
    void Start()
    {
        dt = this.GetComponent<DOTweenAnimation>();
        dt.DOPlayById("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
