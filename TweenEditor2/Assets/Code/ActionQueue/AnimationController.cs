using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void ControllerCallback();
public class BaseController
{
    public ControllerCallback onComplete;
}


public static class BaseControllerExtensions
{
    public static T OnComplete<T>(this T t, ControllerCallback action) where T : BaseController
    {
        if (t != null)
        {
            t.onComplete = action;
        }
        return t;
    }
}


public class AnimationController:BaseController
{
    public AnimationController()
    {

    }

    public void PlayAnimation(float delay,string aniName,bool loop,Action action=null)
    {

    }
}
