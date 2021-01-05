using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierDemo : MonoBehaviour
{
    // 三次贝塞尔控制点
    public Transform[] controlPoints;

    // LineRenderer 
    private LineRenderer lineRenderer;
    private int layerOrder = 0;

    // 设置贝塞尔插值个数
    private int _segmentNum = 50;


    void Start()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.sortingLayerID = layerOrder;

    }

    void Update()
    {

        DrawThreePowerCurve();

    }


    Vector3[] points3;
    void DrawThreePowerCurve()
    {
        // 获取三次贝塞尔方程曲线
        points3 = BezierManager.GetBeizerList(controlPoints[0].position, controlPoints[1].position, controlPoints[2].position, controlPoints[3].position, _segmentNum);
        // 设置 LineRenderer 的点个数，并赋值点值
        lineRenderer.positionCount = (_segmentNum);
        lineRenderer.SetPositions(points3);
    }
}


