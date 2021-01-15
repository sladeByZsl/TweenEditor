using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class TweenController: BaseController
{
    Transform mTrans;
    public TweenController(Transform t)
    {
        mTrans = t;
    }

    public void MoveImmediately(Vector3 targetPos)
    {
        mTrans.position = targetPos;
    }

    public void MoveTo(float duration,Vector3 targetPos, Action callback = null)
    {
        mTrans.DOMove(targetPos, duration).onComplete=()=> {
            callback?.Invoke();
        };
    }
}


public static class TweenControllerExtension
{
    public static void MoveTo()
    {

    }
}
