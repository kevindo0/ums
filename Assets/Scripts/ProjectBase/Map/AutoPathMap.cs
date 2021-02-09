using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObstacleMap
{
    private float xMin, xMax;
    private float yMin, yMax;

    public ObstacleMap(Vector3 center, Vector3 extents)
    {
        Debug.Log(center + ":" + extents);
        xMin = center.x - extents.x;
        xMax = center.x + extents.x;
        yMin = center.y - extents.y;
        yMax = center.y + extents.y;
    }

    public bool isInvolved(float x, float y)
    {
        // Node 中心点在collider中即算作障碍点
        if(x > xMin && x < xMax && y > yMin && y < yMax)
        {
            return true;
        }
        return false;
    }
}

public class AutoPathMap : MonoBehaviour
{
    [Tooltip("将一个unit分成几份")]
    public float size = 1;
    public static PathNode[,] pathMap;

    private static float xMin = -2;
    private static float xMax = 2;
    private static float yMin = -5;
    private static float yMax = 5;
    // 单位方格大小
    private static float nodeSize = 1;

    // 行列总数
    private static int col;
    private static int row;
    private List<ObstacleMap> obstacles = new List<ObstacleMap>();

    private float oldSize = 1;

    private void Awake()
    {
        FindObstacle();
        MapInit();
    }

    public static float XMin
    {
        get => xMin;
    }

    public static float XMax
    {
        get => xMax;
    }

    public static float YMin    {
        get => yMin;
    }

    public static float YMax
    {
        get => yMax;
    }

    public static float NodeSize
    {
        get => nodeSize;
    }

    public static int Col
    {
        get => col;
    }

    public static int Row
    {
        get => row;
    }

    // 对地图进行初始化
    private void MapInit()
    {
        Camera mainCamera = Camera.main;
        float hHalf = mainCamera.orthographicSize;
        float wHalf = (float)Math.Round(hHalf * mainCamera.aspect, 1);
        nodeSize = (float)Math.Round(1.0f / size, 1);
        //Debug.Log(hHalf + ":" + wHalf + ":" + cellSize);
        //Debug.Log(mainCamera.aspect);
        // x最小值
        float i = 0;
        while (i >= -wHalf)
        {
            i -= nodeSize;
        }
        xMin = i + nodeSize;
        // x最大值
        i = nodeSize;
        while (wHalf >= i)
        {
            i += nodeSize;
        }
        xMax = i - nodeSize;
        // y最小值
        i = 0;
        while (i >= -hHalf)
        {
            i -= nodeSize;
        }
        yMin = i + nodeSize;
        // y最大值
        i = nodeSize;
        while (hHalf >= i)
        {
            i += nodeSize;
        }
        yMax =  i - nodeSize;
        col = (int)((xMax - xMin) / nodeSize);
        row = (int)((yMax - yMin) / nodeSize);
        //Debug.Log("xMin:" + xMin + " xMax" + ":" + xMax);
        //Debug.Log("yMin:" + yMin + " yMax" + ":" + yMax);
        //print("col:" + col + " row:" + row);
        pathMap = new PathNode[col, row];
        for(int j = 0; j < col; j++)
        {
            for(int z = 0; z < row; z++)
            {
                float centerX = xMin + j * nodeSize + nodeSize / 2;
                float centerY = yMax - z * nodeSize - nodeSize / 2;
                PathNode node = new PathNode(centerX, centerY);
                foreach (ObstacleMap c in obstacles)
                {
                    if(c.isInvolved(centerX, centerY))
                    {
                        node.type = NODE_TYPE.Stop;
                    }
                }
                pathMap[j, z] = node;
            }
        }
        //print(pathMap[0, 0].ToString());
    }

    public void Scan()
    {
        FindObstacle();
        MapInit();
    }

    private void OnDrawGizmos()
    {
        if(oldSize != size)
        {
            MapInit();
            oldSize = size;
        }
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        // x轴竖线
        for (float i = xMin; i <= xMax; i += nodeSize)
        {
            Gizmos.DrawLine(new Vector3(i, yMin, 0), new Vector3(i, yMax, 0));
        }
        // y轴横线
        for (float i = yMin; i <= yMax; i += nodeSize)
        {
            Gizmos.DrawLine(new Vector3(xMin, i, 0), new Vector3(xMax, i, 0));
        }
        if (pathMap != null)
        {
            for (int j = 0; j < col; j++)
            {
                for (int z = 0; z < row; z++)
                {
                    PathNode node = pathMap[j, z];
                    float width = nodeSize / 2;
                    switch (node.type)
                    {
                        case NODE_TYPE.Stop:
                            Gizmos.color = new Color(1, 0, 0, 0.5f);
                            break;
                        default:
                            Gizmos.color = new Color(0, 1, 0, 0.5f);
                            break;
                    }
                    Gizmos.DrawCube(new Vector2(node.x, node.y), new Vector2(width, width));
                }
            }
        }
    }

    private void FindObstacle()
    {
        obstacles.Clear();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obj in objs)
        {
            BoxCollider collider = obj.GetComponent<BoxCollider>();
            if (collider != null)
            {
                obstacles.Add(new ObstacleMap(collider.bounds.center, collider.bounds.extents));
            }
        }
    }
}
