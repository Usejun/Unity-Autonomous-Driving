using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GridManagement : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector3 leftBottomLocation = new (0, 0, 0);

    public GameObject[,] grids;

    public int startX = 0;
    public int startY = 0;
    public int endX = 2;
    public int endY = 2;

    public List<GameObject> path = new List<GameObject>();

    void Awake()
    {   
        grids = new GameObject[columns, rows];

        if (gridPrefab)
            GeneateGrid();
        else print("Missing grid prefab, Please assign");
    }

    public bool Finding()
    {
        SetDistance();
        return SetPath();
    }

    void GeneateGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);
                obj.transform.parent = transform;
                obj.GetComponent<Grid>().Init(i, j);

                grids[i, j] = obj;
            }
        }
    }

    void Init()
    {
        foreach (GameObject obj in grids)
            if (obj)
                obj.GetComponent<Grid>().visit = -1;

        grids[startX, startY].GetComponent<Grid>().visit = 0;    
    }

    bool Move(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            case 4:
                if (x > 0 && grids[x - 1, y] && grids[x - 1, y].GetComponent<Grid>().visit == step)
                    return true;
                else
                    return false;
            case 3:
                if (y > 0 && grids[x, y - 1] && grids[x, y - 1].GetComponent<Grid>().visit == step)
                    return true;
                else
                    return false;
            case 2:
                if (x + 1 < columns && grids[x + 1, y] && grids[x + 1, y].GetComponent<Grid>().visit == step)
                    return true;
                else
                    return false;
            case 1:
                if (y + 1 < rows && grids[x, y + 1] && grids[x, y + 1].GetComponent<Grid>().visit == step)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    void SetDistance()
    {
        Init();

        for (int step = 1; step < rows * columns; step++)
            foreach (GameObject obj in grids)
                if (obj && obj.GetComponent<Grid>().visit == step - 1)
                    PathFind(obj.GetComponent<Grid>().x, obj.GetComponent<Grid>().y, step);

    }

    void PathFind(int x, int y, int step)
    {
        if (Move(x, y, -1, 1))
            SetVisit(x, y + 1, step);
        if (Move(x, y, -1, 2))
            SetVisit(x + 1, y, step);
        if (Move(x, y, -1, 3))
            SetVisit(x, y - 1, step);
        if (Move(x, y, -1, 4))
            SetVisit(x - 1, y, step);
    }

    void SetVisit(int x, int y, int step)
    {
        if (grids[x, y])
            grids[x, y].GetComponent<Grid>().visit = step;
    }

    bool SetPath()
    {
        path.Clear();

        int x = endX;
        int y = endY ;
        int step = 0;

        List<GameObject> list = new List<GameObject>();

        if (grids[endX, endY] && grids[endX, endY].GetComponent<Grid>().visit > 0)
        {
            path.Add(grids[x, y]);
            step = grids[x, y].GetComponent<Grid>().visit - 1;
        }
        else
        {
            Log.AddLog("This is a place you can't go.");
            return false;
        }

        for (; step > -1; step--)
        {
            if (Move(x, y, step, 1))
                list.Add(grids[x, y + 1]);
            if (Move(x, y, step, 2))
                list.Add(grids[x + 1, y]);
            if (Move(x, y, step, 3))
                list.Add(grids[x, y - 1]);
            if (Move(x, y, step, 4))
                list.Add(grids[x - 1, y]);

            GameObject obj = NextDirection(grids[x, y].transform, list);

            path.Add(obj);

            x = obj.GetComponent<Grid>().x;
            y = obj.GetComponent<Grid>().y;

            list.Clear();           
        }

        return true;
    }

    GameObject NextDirection(Transform target, List<GameObject> list)
    {
        float currentDistance = scale * rows * columns;
        int index = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(target.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(target.position, list[i].transform.position);
                index = i;
            }
        }

        return list[index];   
    }    

    public void SetPosition(Grid start, Grid end)
    {
        startX = start.x;
        startY = start.y;

        endX = end.x;
        endY = end.y;
    }
}
