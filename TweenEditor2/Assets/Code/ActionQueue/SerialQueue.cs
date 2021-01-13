using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTask
{
    public float delay;
    public Action action;

    public ActionTask(float _delay,Action _action)
    {
        this.delay = _delay;
        this.action = _action;
    }
}

public class SerialQueue
{
    public List<ActionTask> actionTaskList = new List<ActionTask>();
    public ActionTask currentTask=null;
    public SerialQueue()
    {

    }

    public void PushAction(float delay, Action action)
    {
        if(currentTask==null)
        {
            currentTask = new ActionTask(delay, action);
        }
        else
        {
            actionTaskList.Add(new ActionTask(delay, action));
        }
    }

    public void Update()
    {
        if (actionTaskList.Count>0)
        {
            currentTask = actionTaskList[0];
            actionTaskList.RemoveAt(0);
        }
    }
}
