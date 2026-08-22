using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisualGrid : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1.0f;

    void Start()
    {
        LineRenderer lr = GetComponent<LineRenderer>();

        // Calculate total points needed to draw the grid in one continuous path
        int segmentCount = (width + 1) + (height + 1);
        lr.positionCount = segmentCount * 2;

        int pointIndex = 0;

        // Draw vertical lines
        for (int x = 0; x <= width; x++)
        {
            float xPos = x * cellSize;
            lr.SetPosition(pointIndex++, new Vector3(xPos, 0, 0));
            lr.SetPosition(pointIndex++, new Vector3(xPos, height * cellSize, 0));
        }

        // Draw horizontal lines
        for (int y = 0; y <= height; y++)
        {
            float yPos = y * cellSize;
            lr.SetPosition(pointIndex++, new Vector3(0, yPos, 0));
            lr.SetPosition(pointIndex++, new Vector3(width * cellSize, yPos, 0));
        }
    }
}
