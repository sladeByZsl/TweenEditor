using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleManager : MonoBehaviourSingle<ParticleManager>
{
    public delegate void ParticleLoadedComplete(ParticleInstance particleSystem);
    public ParticleLoadedComplete OnParticleLoadedComplete;

    MonoBehaviourPool<ParticleInstance> mInstancePool = new MonoBehaviourPool<ParticleInstance>();

    protected override void OnAwake()
    {
        
    }

    protected override void OnRelease()
    {
        
    }

    public ParticleInstance GetParticleInstance(string name, ParticleLoadedComplete onComplete)
    {
        if(mInstancePool==null||string.IsNullOrEmpty(name))
        {
            return null;
        }
        ParticleInstance pi = mInstancePool.GetMonoBehaviour();
        if (pi==null)
        {
            return null;
        }
        pi.transform.parent = this.transform;
        pi.transform.localPosition = Vector3.zero;
        pi.gameObject.SetActive(true);
        pi.gameObject.name = name;

        if (onComplete!=null)
        {
            onComplete(pi);
        }
        //LoadRes
        return pi;
    }

}
