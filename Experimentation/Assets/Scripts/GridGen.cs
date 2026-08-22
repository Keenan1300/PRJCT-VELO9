using UnityEngine;

public class GridGen : MonoBehaviour
{

    public  int width;
    public int height;
    public GameObject tileObject;
    public Vector3 offset;
    public int spacingx;
    public int spacingy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate position (assuming 1-unit sized tiles)
                Vector3 spawnPosition = new Vector3(x + spacingx, y + spacingy, - 10f) + offset; // Use (x, y, 0) for 2D games

                GameObject newTile = Instantiate(tileObject, spawnPosition, Quaternion.identity);
                newTile.name = $"Tile_{x}_{y}";
                newTile.transform.parent = transform; // Keeps the hierarchy organized
            }
        }
    }
}
