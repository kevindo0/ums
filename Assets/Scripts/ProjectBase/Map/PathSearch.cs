using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PathSearchNode
{
    public int col;
    public int row;
    public PathNode node;

    public PathSearchNode(int _col, int _row, PathNode _node)
    {
        col = _col;
        row = _row;
        node = _node;
    }

    // 判断两对象是否相等
    public bool Equal(PathSearchNode other)
    {
        if (other == null) return false;
        return col == other.col && row == other.row;
    }

    // 判断两对象是否相等
    public bool Equal(int _col, int _row)
    {
        return col == _col && row == _row;
    }

    public override string ToString()
    {
        return string.Format("col={0}, row={1}", col, row);
    }

    public Vector2 ToVector2()
    {
        return new Vector2(node.x, node.y);
    }
}

public class PathSearch : MonoBehaviour
{
    private PathSearchNode startPosition;
    private PathSearchNode targetPosition;

    public Transform target;

    private List<PathSearchNode> openNodes = new List<PathSearchNode>();
    private List<PathSearchNode> closeNodes = new List<PathSearchNode>();

    private List<Vector2> pathNodes = new List<Vector2>();

    void Start()
    {
        GetStartNode();
        print(startPosition.node.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetStartNode();
        }
        if (target != null && targetPosition == null)
        {
            GetTargetNode();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Search();
            PathFinding();
        }
    }

    private void OnDrawGizmos()
    {
        if (startPosition != null)
        {
            Gizmos.color = new Color(0, 0, 1, 1);
            Gizmos.DrawCube(new Vector2(startPosition.node.x, startPosition.node.y), new Vector2(1, 1));
        }
        if (targetPosition != null)
        {
            Gizmos.color = new Color(1, 1, 0, 1);
            Gizmos.DrawCube(new Vector2(targetPosition.node.x, targetPosition.node.y), new Vector2(1, 1));
        }
        Gizmos.color = new Color(0, 1, 1, 1);
        foreach (PathSearchNode s in openNodes)
        {
            Gizmos.DrawCube(new Vector2(s.node.x, s.node.y), new Vector2(0.8f, 0.8f));
        }
        Gizmos.color = new Color(1, 0.5f, 1, 1);
        foreach (PathSearchNode s in closeNodes)
        {
            Gizmos.DrawCube(new Vector2(s.node.x, s.node.y), new Vector2(0.8f, 0.8f));
        }
        Gizmos.color = new Color(0.5f, 1, 0.5f, 1);
        if(pathNodes.Count > 0)
        {
            for (int i = 0; i < pathNodes.Count - 1; i++)
            {
                Gizmos.DrawLine(pathNodes[i], pathNodes[i + 1]);
            }
        }
    }

    public void SetTarget(Transform tf)
    {
        targetPosition = null;
        target = tf;
    }

    private void GetStartNode()
    {
        Vector3 position = transform.position;
        startPosition = GetNode(position);
    }

    private void GetTargetNode()
    {
        if (target != null)
        {
            targetPosition = GetNode(target.position);
        }
    }

    // record 是否记录在二维数组中的行列数值
    private PathSearchNode GetNode(Vector3 position)
    {
        int x, y;
        float xDiff = position.x - AutoPathMap.XMin;
        if (xDiff >= 0 && position.x < AutoPathMap.XMax)
            x = Mathf.FloorToInt((position.x - AutoPathMap.XMin) / AutoPathMap.NodeSize);
        else
            return null;
        float yDiff = AutoPathMap.YMax - position.y;
        if (yDiff >= 0 && position.y > AutoPathMap.YMin)
            y = Mathf.FloorToInt((AutoPathMap.YMax - position.y) / AutoPathMap.NodeSize);
        else
            return null;
        PathNode node = AutoPathMap.pathMap[x, y];
        return new PathSearchNode(x, y, node);
    }

    public void Search()
    {
        openNodes.Clear();
        closeNodes.Clear();
        closeNodes.Add(startPosition);
        PathSearchNode movePosition = startPosition;
        while (true)
        //for(int i = 0; i < 5; i++)
        {
            CalculateParentNodes(movePosition);
            movePosition = FindBestPath();
            if (movePosition == null || movePosition.Equal(targetPosition))
            {
                break;
            }
        }
        //Debug.Log("openNodes count ****** :" + openNodes.Count);
        //Debug.Log("closeNodes count ****** :" + closeNodes.Count);
        foreach (PathSearchNode n in closeNodes)
        {
            if(n.node.Parent != null)
                Debug.Log(string.Format("close sort list col={0}, row={1}, node={2}, parent:{3}", n.col, n.row, n.node, n.node.Parent.ToString()));
        }
        //foreach (PathSearchNode n in openNodes)
        //{
        //    Debug.Log(string.Format("open sort list col={0}, row={1}, f={2}, parent:{3}", n.col, n.row, n.node.f, n.node.Parent.ToString()));
        //}
    }

    // 根据父节点计算
    private void CalculateParentNodes(PathSearchNode searchNode)
    {
        int col, row;
        // 左侧
        if (searchNode.col > 0)
        {
            col = searchNode.col - 1;
            row = searchNode.row - 1;
            if (row >= 0)
                CheckAddOpenNodes(col, row, searchNode);
            row = searchNode.row + 1;
            if(row < AutoPathMap.Row)
                CheckAddOpenNodes(col, row, searchNode);
            row = searchNode.row;
            CheckAddOpenNodes(col, row, searchNode);
        }
        // 中间部分
        col = searchNode.col;
        row = searchNode.row - 1;
        if (row >= 0)
            CheckAddOpenNodes(col, row, searchNode);
        row = searchNode.row + 1;
        if (row < AutoPathMap.Row)
            CheckAddOpenNodes(col, row, searchNode);
        // 右侧部分
        if(searchNode.col + 1 < AutoPathMap.Col)
        {
            col = searchNode.col + 1;
            row = searchNode.row - 1;
            if (row >= 0)
                CheckAddOpenNodes(col, row, searchNode);
            row = searchNode.row;
            CheckAddOpenNodes(col, row, searchNode);
            row = searchNode.row + 1;
            if (row < AutoPathMap.Row)
                CheckAddOpenNodes(col, row, searchNode);
            
        }
    }

    private void CheckAddOpenNodes(int col, int row, PathSearchNode _sNode)
    {
        // 判断新加入的点是否已经包含在开启或关闭列表中
        if (IsInvolveOpenClose(col, row)) return;
        // 判断是否是起点
        if (startPosition.Equal(col, row)) return;
        PathNode node = AutoPathMap.pathMap[col, row];
        // 计算总体消耗
        node.f = CalculateF(node);
        // 设置父元素
        node.Parent = _sNode.node;

        PathSearchNode newNode = new PathSearchNode(col, row, node);

        if (node.type == NODE_TYPE.Normal)
        {
            openNodes.Add(newNode);
        }
    }

    // 计算总体消耗
    private float CalculateF(PathNode node)
    {
        return CalculateG(node) + CalculateH(node);
    }

    // 计算距离起点的消耗
    private float CalculateG(PathNode node)
    {
        float xDiff = Mathf.Abs(node.x - startPosition.node.x) / AutoPathMap.NodeSize;
        float yDiff = Mathf.Abs(node.y - startPosition.node.y) / AutoPathMap.NodeSize;
        float minDiff = Mathf.Min(xDiff, yDiff);
        return minDiff * 1.4f + Mathf.Abs(xDiff - yDiff);
    }

    // 计算距离终点的消耗 曼哈顿距离
    private float CalculateH(PathNode node)
    {
        float xDiff = Mathf.Abs(node.x - targetPosition.node.x) / AutoPathMap.NodeSize;
        float yDiff = Mathf.Abs(node.y - targetPosition.node.y) / AutoPathMap.NodeSize;
        return xDiff + yDiff;
    }

    // 检查当前对象是否在开启列表中或关闭列表中
    private bool IsInvolveOpenClose(int col, int row)
    {
        foreach (PathSearchNode snode in closeNodes)
        {
            if (snode.Equal(col, row)) return true;
        }
        foreach (PathSearchNode snode in openNodes)
        {
            if (snode.Equal(col, row)) return true;
        }
        return false;
    }

    // 对开启列表里的对象进行排序并取出消耗值最小的值放入关闭列表中
    private PathSearchNode FindBestPath()
    {
        openNodes.Sort((x, y) => x.node.f.CompareTo(y.node.f));
        PathSearchNode sNode = null;
        if (openNodes.Count > 0)
        {
            sNode = openNodes[0];
            openNodes.Remove(sNode);
            closeNodes.Add(sNode);
        }
        return sNode;
    }

    public List<Vector2> PathFinding()
    {
        pathNodes.Clear();
        if (closeNodes.Count > 0)
        {
            PathSearchNode currentPosition = targetPosition;
            List<int> index = new List<int>();
            for(int i = 0; i < closeNodes.Count; i++)
            {
                index.Add(i);
            }

            for (int i = 0; i < closeNodes.Count; i++)
            {
                for(int j = 0; j < index.Count; j++)
                {
                    int idx = index[j];
                    if (closeNodes[idx].node.Equal(currentPosition.node.Parent))
                    {
                        pathNodes.Add(currentPosition.ToVector2());
                        currentPosition = closeNodes[idx];
                        print("current position:" + currentPosition.ToString());
                        index.Remove(idx);
                        break;
                    }
                }
            }
        }
        pathNodes.Add(startPosition.ToVector2());
        Debug.Log("path node count:" + pathNodes.Count);
        return pathNodes;
    }
}