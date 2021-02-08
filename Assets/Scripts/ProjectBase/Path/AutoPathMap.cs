using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AutoPathMap : MonoBehaviour
{
    [Tooltip("将一个unit分成几份")]
    public float size = 1;

    private float wHalf = 2.3f;
    private float hHalf = 5;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private float cellSize = 1;

    private void Awake()
    {
        MapInit();
    }

    // 对地图进行初始化
    private void MapInit()
    {
        Camera mainCamera = Camera.main;
        hHalf = mainCamera.orthographicSize;
        wHalf = (float)Math.Round(hHalf * mainCamera.aspect, 1);
        cellSize = (float)Math.Round(1.0f / size, 1);
        Debug.Log(hHalf + ":" + wHalf + ":" + cellSize);
        Debug.Log(mainCamera.aspect);
        // x最小值
        float i = 0;
        while (i >= -wHalf)
        {
            i -= cellSize;
        }
        xMin = i + cellSize;
        // x最大值
        i = cellSize;
        while (wHalf >= i)
        {
            i += cellSize;
        }
        xMax = i - cellSize;
        // y最小值
        i = 0;
        while (i >= -hHalf)
        {
            i -= cellSize;
        }
        yMin = i + cellSize;
        // y最大值
        i = cellSize;
        while (hHalf >= i)
        {
            i += cellSize;
        }
        yMax =  i - cellSize;
        Debug.Log("xMin:" + xMin + " xMax" + ":" + xMax);
        Debug.Log("yMin:" + yMin + " yMax" + ":" + yMax);
    }

    public void Scan()
    {
        Debug.Log("scan");
        MapInit();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        // x轴竖线
        for(float i = xMin; i <= xMax; i+= cellSize)
        {
            Gizmos.DrawLine(new Vector3(i, yMin, 0), new Vector3(i, yMax, 0));
        }
        // y轴横线
        for (float i = yMin; i <= yMax; i += cellSize)
        {
            Gizmos.DrawLine(new Vector3(xMin, i, 0), new Vector3(xMax, i, 0));
        }
    }
}
