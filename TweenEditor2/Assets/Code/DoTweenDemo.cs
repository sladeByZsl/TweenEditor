using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenDemo : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);

        SerialQueue queue = new SerialQueue();
        queue.AppendAction(() =>
        {
            Debug.LogError("time:" + Time.time + ",move");
        });
        queue.AppendAction(() =>
        {
            Debug.LogError("time:" + Time.time + ",animation");
        });

        int actionID = ActionManager.GetInstance().PushAction(2, () => { });
        
    }

    public void DoStartCallback()
    {
        Debug.LogError("start time:" + Time.time);
    }

    public void DoComplete()
    {
        Debug.LogError("end time:" + Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        //create dotween
        //1.DoPrefix
        if (Input.GetKeyDown(KeyCode.A))
        {
            cube.transform.DOMoveX(12, 3).SetSpeedBased(true).OnStart(DoStartCallback).OnComplete(DoComplete);

            //DOMoveX(10, 3)  10个单位，移动3秒
            //DOMoveX(10, 3).SetSpeedBased(true)  10个单位，每秒钟移动3个单位
        }
        //2.DOTween.To
        if (Input.GetKeyDown(KeyCode.S))
        {
            Tweener mTweener = DOTween.To((float ratio) =>
            {
                Debug.LogError(ratio);
            }, 0, 1, 1);

            //Color temColor = RenderSettings.fogColor;
            //Tweener tweener = DOTween.To(() => temColor, x => temColor = x, Color.black, 1);

            //float val = 0;
            //DOTween.To(() => val, x => val = x, 1, 1).onUpdate=()=> {
            //    Debug.Log("val"+val);
            //};
        }

        //3.sequence
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Grab a free Sequence to use
            Sequence mySequence = DOTween.Sequence();
            // Add a movement tween at the beginning
            mySequence.Append(cube.transform.DOMoveX(12, 3));
            // Add a rotation tween as soon as the previous one is finished
            mySequence.Append(cube.transform.DORotate(new Vector3(0, 180, 0), 3));
            // Delay the whole Sequence by 1 second
            mySequence.PrependInterval(1);
            // Insert a scale tween for the whole duration of the Sequence
            mySequence.Insert(0, cube.transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));

            /*
             * Sequence mySequence = DOTween.Sequence();
               ﻿﻿﻿﻿﻿mySequence.Append(transform.DOMoveX(45, 1))
                ﻿﻿﻿﻿﻿  .Append(transform.DORotate(new Vector3(0,180,0), 1))
                ﻿﻿﻿﻿﻿  .PrependInterval(1)
                ﻿﻿﻿﻿﻿  .Insert(0, transform.DOScale(new Vector3(3,3,3), mySequence.Duration()));
             */
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(cube.transform.DOMoveX(12, 3));
            mySequence.Append(cube.transform.DOMoveX(-12, 3));
        }
    }
}
