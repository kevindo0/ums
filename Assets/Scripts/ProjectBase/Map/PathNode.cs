using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NODE_TYPE
{
    Normal,  // 普通可以行走的点
    Stop     // 不可通行的点
}

public class PathNode
{
    // 坐标
    public float x;
    public float y;

    // 点类型 0
    public NODE_TYPE type;

    // 父节点
    private PathNode parent;

    // 消耗值
    public float f;
    //距离起点的值
    public float g;
    //距离终点的值
    public float h;

    public PathNode(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public PathNode Parent
    {
        get => parent;
        set => parent = value;
    }

    public override string ToString()
    {
        return string.Format("x={0}, y={1}, f={2}, type={3}", x, y, f, type);
    }

    public bool Equal(PathNode other)
    {
        if (other == null) return false;
        return x == other.x && y == other.y;
    }
}
