using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DelayType
{
    Invalid,
    HpBar,
    MpBar,
    BuffBar,
    CheatChip,
    StepText,
    TransformHex,
}

public class ActionUnit
{
    public int Id;
    public float StartTime;
    public float EndTime;
    public Action action;
    public DelayType delayType;


    public void Execute()
    {
        action?.Invoke();
        Recycle();
    }

    private void Reset()
    {
        this.Id = 0;
        this.StartTime = 0;
        this.EndTime = 0;
        this.action = null;
        this.delayType = DelayType.Invalid;
    }

    public void Recycle()
    {
        if (!ActionManager.GetInstance().m_removeList.Contains(this))
        {
            Reset();
            ActionManager.GetInstance().m_removeList.Add(this);
        }
    }
}

public class ActionManager : SingletonBase<ActionManager>
{
    public List<ActionUnit> m_Pool = new List<ActionUnit>();
    public List<ActionUnit> m_Active = new List<ActionUnit>();
    public List<ActionUnit> m_removeList = new List<ActionUnit>();
    ActionUnit m_NewEvent = null;

    private int actionID = 0;//action的id，从0开始自增，范围是int.max；如果超过max则成负数，理论上不影响id的使用

    public void Init()
    {
        m_Pool = new List<ActionUnit>();
        m_Active = new List<ActionUnit>();
        m_removeList = new List<ActionUnit>();
        m_NewEvent = null;
    }

    private ActionUnit GetNewAction()
    {
        if (m_Pool.Count > 0)
        {
            m_NewEvent = m_Pool[0];
            m_Pool.Remove(m_NewEvent);
        }
        else
            m_NewEvent = new ActionUnit();
        return m_NewEvent;
    }

    /// <summary>
    /// 普通延迟事件
    /// </summary>
    /// <param name="delay">秒</param>
    /// <param name="action">回调</param>
    /// <returns>action的ID</returns>
    public int PushAction(float delay, Action action)
    {
        if (delay > 0)
        {
            GetNewAction();
            InitAction(delay, action);
            m_Active.Add(m_NewEvent);
            return actionID;
        }
        else
        {
            action?.Invoke();
            return -1;
        }
    }

    /// <summary>
    /// 排除互斥事件的延迟，同种类型的事件只能触发一个
    /// 比如战斗的数值是预计算的，A先放一个技能，3s后造成伤害；B再放一个技能，1s后造成伤害
    /// 这个时候如果按照action的实际情况去push，就会出现血条显示异常的情况，采用这种互斥事件，可以排除掉A的那个action
    /// </summary>
    /// <param name="delayType">延迟类型</param>
    /// <param name="delay">延迟时间</param>
    /// <param name="action">回调</param>
    /// <returns>action的ID</returns>
    public int PushAction(DelayType delayType, float delay, Action action)
    {
        if (delay > 0)
        {
            GetNewAction();
            InitAction(delay, action);
            m_NewEvent.delayType = delayType;
            for (int i = m_Active.Count - 1; i >= 0; i--)
            {
                if (m_Active[i].delayType == delayType && m_Active[i].EndTime >= m_NewEvent.EndTime)
                {
                    m_Active[i].Recycle();
                }
            }
            m_Active.Insert(0, m_NewEvent);
            return actionID;
        }
        else
        {
            //如果delay=0，表示立刻执行的事件，这个时候回收所有的相同类型事件，立刻执行最后push的事件
            for (int i = m_Active.Count - 1; i >= 0; i--)
            {
                if (m_Active[i].delayType == delayType)
                {
                    m_Active[i].Recycle();
                }
            }
            action?.Invoke();
            return -1;
        }
    }

    private void InitAction(float delay, Action action)
    {
        m_NewEvent.Id = ++actionID;
        m_NewEvent.StartTime = Time.time;
        m_NewEvent.EndTime = Time.time + delay;
        m_NewEvent.action = action;
    }

    public bool CancelAction(int id)
    {
        for (int i = 0; i < m_Active.Count; i++)
        {
            if (m_Active[i].Id == id)
            {
                m_Active[i].Recycle();
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        if (m_Pool != null)
        {
            m_Pool.Clear();
        }
        if (m_Active != null)
        {
            m_Active.Clear();
        }
        if (m_removeList != null)
        {
            m_removeList.Clear();
        }
        m_NewEvent = null;
    }

    public void Update()
    {
        if (m_Active.Count > 0)
        {
            for (int i = m_Active.Count - 1; i >= 0; i--)
            {
                if (Time.time >= m_Active[i].EndTime)
                {
                    m_Active[i].Execute();
                }
            }
        }

        if (m_removeList.Count > 0)
        {
            for (int i = 0; i < m_removeList.Count; i++)
            {
                m_Active.Remove(m_removeList[i]);
                m_Pool.Add(m_removeList[i]);
            }
            m_removeList.Clear();
        }
    }
}
