using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MoveController
{
    Transform mTrans;
    public MoveController(Transform t)
    {
        mTrans = t;
    }

    public void MoveImmediately(Vector3 targetPos,Action callback=null)
    {
        mTrans.position = targetPos;
    }

    public void MoveTo(float duration,Vector3 targetPos, Action callback = null)
    {

    }
}
