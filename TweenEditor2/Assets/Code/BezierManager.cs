using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierManager
{
    // 线性
    public static Vector3 Bezier(Vector3 p0,Vector3 p1,float t)
    {
        return (1 - t) * p0 + t * p1;
    }
    // 二阶曲线
    public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 result = (1 - t) * p0p1 + t * p1p2;
        return result;
    }

    // 三阶曲线
   public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 result;
        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 p2p3 = (1 - t) * p2 + t * p3;
        Vector3 p0p1p2 = (1 - t) * p0p1 + t * p1p2;
        Vector3 p1p2p3 = (1 - t) * p1p2 + t * p2p3;
        result = (1 - t) * p0p1p2 + t * p1p2p3;
        return result;
    }

    // n阶曲线，递归实现
    public static Vector3 Bezier(float t, List<Vector3> p)
    {
        if (p.Count < 2)
            return p[0];
        List<Vector3> newp = new List<Vector3>();
        for (int i = 0; i < p.Count - 1; i++)
        {
            Debug.DrawLine(p[i], p[i + 1]);
            Vector3 p0p1 = (1 - t) * p[i] + t * p[i + 1];
            newp.Add(p0p1);
        }
        return Bezier(t, newp);
    }
    // transform转换为vector3，在调用参数为List<Vector3>的Bezier函数
    public static Vector3 Bezier(float t, List<Transform> p)
    {
        if (p.Count < 2)
            return p[0].position;
        List<Vector3> newp = new List<Vector3>();
        for (int i = 0; i < p.Count; i++)
        {
            newp.Add(p[i].position);
        }
        return Bezier(t, newp);
    }

    /// <summary>
    /// 获取存储贝塞尔曲线点的数组
    /// </summary>
    /// <param name="p0"></param>起始点
    /// <param name="controlPoint"></param>控制点
    /// <param name="p1"></param>目标点
    /// <param name="segmentNum"></param>采样点的数量
    /// <returns></returns>存储贝塞尔曲线点的数组
    public static Vector3[] GetBeizerList(Vector3 p0, Vector3 p1, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum];
        for (int i = 0; i < segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = Bezier(p0, p1,t);
            path[i] = pixel;
            Debug.Log(path[i]);
        }
        return path;

    }

    /// <summary>
    /// 获取存储的二次贝塞尔曲线点的数组
    /// </summary>
    /// <param name="p0"></param>起始点
    /// <param name="p1"></param>控制点
    /// <param name="p2"></param>目标点
    /// <param name="segmentNum"></param>采样点的数量
    /// <returns></returns>存储贝塞尔曲线点的数组
    public static Vector3[] GetBeizerList(Vector3 p0, Vector3 p1, Vector3 p2, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum];
        for (int i = 0; i < segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = Bezier(p0,p1, p2,t);
            path[i] = pixel;
            Debug.Log(path[i]);
        }
        return path;
    }

    /// <summary>
    /// 获取存储的三次贝塞尔曲线点的数组
    /// </summary>
    /// <param name="p0"></param>起始点
    /// <param name="p1"></param>控制点1
    /// <param name="p2"></param>控制点2
    /// <param name="p3"></param>目标点
    /// <param name="segmentNum"></param>采样点的数量
    /// <returns></returns>存储贝塞尔曲线点的数组
    public static Vector3[] GetBeizerList(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum];
        for (int i = 0; i < segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = Bezier(p0,p1, p2, p3,t);
            path[i] = pixel;
            Debug.Log(path[i]);
        }
        return path;
    }

    public static Vector3[] GetBeizerList(List<Vector3> p, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum];
        for (int i = 0; i < segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = Bezier(t,p);
            path[i] = pixel;
            Debug.Log(path[i]);
        }
        return path;
    }
}
