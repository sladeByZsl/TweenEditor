using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 并行队列
*/
public class ParallelQueue
{
    List<Action> actionList = new List<Action>();
    public ParallelQueue()
    {

    }

    public void PushAction(float delay,Action action)
    {
        ActionManager.GetInstance().PushAction(delay,action);
    }

    public bool IsFinish()
    {
        return true;
    }
}
