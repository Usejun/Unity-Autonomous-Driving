using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum Direction
{
    Stop = 0b0000,
    Forward = 0b0001,
    Right = 0b0010,
    Left = 0b0100,
    Back = 0b1000
}

public class GridMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;

    [SerializeField] GridManagement gridManagement;

    [SerializeField] Grid nowGrid;

    [SerializeField] int layer;

    public bool isActive = true;

    bool isTouched = false;

    BluetoothManagement bluetooth;

    List<Direction> directions = new List<Direction>();

    void Awake()
    {
        bluetooth = BluetoothManagement.Instance;
    }

    void Start()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        if (gridManagement.columns > x && x <= 0 && gridManagement.rows > y && y <= 0 && gridManagement.grids[x, y])
        {
            nowGrid = gridManagement.grids[x, y].GetComponent<Grid>();
            gameObject.transform.position = new Vector3(nowGrid.x * gridManagement.scale, transform.position.y, nowGrid.y * gridManagement.scale);
        }
        else
        {
            print("Out of range");
        }
    }

    void FixedUpdate()
    {       
        if (isTouched && gridManagement.path.Count > 0 && gridManagement.path[0].GetComponent<Grid>().Equals(nowGrid))
            isTouched = false;

        Touch();
    }

    void Touch()
    {
        if (!isActive) return;

        if (Input.touchCount > 0 && !isTouched)
        {
            isTouched = true;

            Touch touch = Input.GetTouch(0);
            Grid grid = Raycasting(touch.position);

            if (grid != null)
            {
                gridManagement.SetPosition(nowGrid, grid);
                if (gridManagement.Finding())
                {
                    AddDirection();
                    StartCoroutine(SetDirection());
                    SendDirection();
                }
            }
            else
            {
                isTouched = false;
            }
        }
    }

    Grid Raycasting(Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.layer == layer)
        {
            Grid gird = hit.transform.GetComponent<Grid>();

            int x = Mathf.RoundToInt(gird.x);
            int y = Mathf.RoundToInt(gird.y);          

            return gridManagement.grids[x, y].GetComponent<Grid>();
        }
        return nowGrid;
    }

    IEnumerator SetDirection()
    {
        foreach (var grid in gridManagement.path.ToArray().Reverse().Select(i => i.GetComponent<Grid>()))
        {
            nowGrid.Init(grid.x, grid.y);
            yield return StartCoroutine(Move(grid));          
        }
    }

    IEnumerator Move(Grid grid)
    {
        float percent = 0;
        float current = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveSpeed;

            transform.position = Vector3.Lerp(transform.position, new Vector3(grid.x * gridManagement.scale, transform.position.y, grid.y * gridManagement.scale), percent);

            yield return null;
        }
    }

    void AddDirection()
    {
        List<Grid> grids = gridManagement.path.ToArray().Reverse().Select(i => i.GetComponent<Grid>()).ToList();
        if (grids.Count < 1)        
            return;

        directions.Clear();

        directions.Add(CalculateDirection(nowGrid, grids[0]));

        for (int i = 0; i < grids.Count - 1; i++)
            directions.Add(CalculateDirection(grids[i], grids[i + 1]));
    }

    Direction CalculateDirection(Grid start, Grid end)
    {
        int nx = end.x - start.x;
        int ny = end.y - start.y;

        if (nx == 1)       return Direction.Forward;
        else if (ny == 1)  return Direction.Left;
        else if (nx == -1) return Direction.Back;
        else if (ny == -1) return Direction.Right;
        else               return Direction.Stop;
    }    

    IEnumerator DealyToTouch()
    {
        yield return new WaitForSeconds(0.5f);

        isTouched = false;
    }

    void SendDirection()
    {
        foreach (var dir in directions)
            bluetooth.Send(((char)dir).ToString());
    }
}
