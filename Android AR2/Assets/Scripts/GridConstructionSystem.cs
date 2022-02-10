using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridConstructionSystem : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    private GridXZ<GridObject> grid;

    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-gridWidth * (gridWidth/2),0,-gridHeight * (gridHeight/2)), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }
    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return x + ", " + z;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(testTransform, DptMouse.GetMouseWorldPosition(), Quaternion.identity);
        }
    }

}
