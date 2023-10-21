using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int visit = -1;
    public int x = 0;
    public int y = 0;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public bool Equals(Grid other)
    {
        return x == other.x && y == other.y;
    }
}
