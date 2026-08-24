using System.Collections.Generic;
using UnityEngine;

public class BeaconGenerator : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] public GameObject beaconPrefab;
    public Transform BeaconOrigin;
    [Header("Map Boundaries")]
    [SerializeField] private Vector2 mapSize = new Vector2(371.8f, 206.1f);

    [Header("Generation Rules")]
    [SerializeField] public int BeaconCount;
    [SerializeField] public float minimumGap = 5f;
    [SerializeField] private int maxAttemptsPerBeacon = 100; // Protects against infinite loops

    // Keeps track of already spawned positions
    public List<Vector3> spawnedBeaconPositions = new List<Vector3>();

    private void Start()
    {
        BeaconCount = Random.Range(12, 20);
        GenerateSpaceMap();
    }

    private void GenerateSpaceMap()
    {
        float squaredMinGap = minimumGap * minimumGap;

        for (int i = 0; i < BeaconCount; i++)
        {
            Vector3 proposedPosition = Vector3.zero;
            bool isValidPosition = false;
            int attempts = 0;

            // Re-run loop if proximity check fails
            while (!isValidPosition && attempts < maxAttemptsPerBeacon)
            {
                attempts++;
                proposedPosition = GetRandomPointInBounds();

                // First of type optimization: if it's the very first beacon, it automatically passes
                if (spawnedBeaconPositions.Count == 0)
                {
                    isValidPosition = true;
                    break;
                }

                // 2nd+ Generation: Compare against all existing spots
                isValidPosition = true;
                foreach (Vector3 existingPos in spawnedBeaconPositions)
                {
                    // Compute magnitude calculation via squared distance
                    float sqrDistance = (proposedPosition - existingPos).sqrMagnitude;

                    if (sqrDistance < squaredMinGap)
                    {
                        isValidPosition = false; // Too close! Break out and generate a new point
                        break;
                    }
                }
            }

            // If we found a valid spot within the attempt limit, instantiate it
            if (isValidPosition)
            {
                InstantiateBeacon(proposedPosition);
            }
            else
            {
                Debug.LogWarning($"Could not find a valid spot for beacon {i} after {maxAttemptsPerBeacon} tries. Map might be too crowded.");
            }
        }


    }



    private Vector3 GetRandomPointInBounds()
    {
        // Generates random coordinates inside your map constraints
        float randomX = Random.Range(-mapSize.x / 2f, mapSize.x / 2f);
        float randomY = Random.Range(-mapSize.y / 2f, mapSize.y / 2f);

        // Maps 2D generation data onto the 3D XZ Plane
        return new Vector3(randomX, randomY);
    }

    private void InstantiateBeacon(Vector3 position)
    {
        GameObject newBeacon = Instantiate(beaconPrefab, BeaconOrigin.position + position, Quaternion.identity, transform);
        newBeacon.name = $"Beacon_{spawnedBeaconPositions.Count}";

        // Store position into the index tracker for future proximity verification loops
        spawnedBeaconPositions.Add(position);
    }



    // Visualizes the map size bounds inside the Scene View
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(mapSize.x, mapSize.y, 0.1f));
    }



}
