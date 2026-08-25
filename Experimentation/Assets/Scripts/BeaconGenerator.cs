using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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


    //Entry and Exit logic
    private float lowestXspot;
    private float highestXspot;
    float yspot;
    float Lyspot;

    //Beacon Nav Overlay
    public GameObject ExitSign;
    public GameObject Target;
    public GameObject ShipIcon;
    public LineRenderer LineRenderer;
    


    // Keeps track of already spawned positions
    public List<Vector3> spawnedBeaconPositions = new List<Vector3>();

    private void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.enabled = false;
        LineRenderer.positionCount = 2;

        //to be used in entry/exit beacons
        highestXspot = mapSize.x * -100f;
        lowestXspot = mapSize.x * 100f;

        GenerateSector();
    }

    private void GenerateSector()
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

            //transition to finding entry and exit - After all spots have been generated
            if (spawnedBeaconPositions.Count == BeaconCount)
            {
            
                MakeEntryExit();
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
        newBeacon.GetComponent<HoverInteractionBeacon>().BeaconGen = gameObject;

        // Store position into the index tracker for future proximity verification loops
        spawnedBeaconPositions.Add(position);

       
    }


    public void MoveTarget(Vector3 MoveLocation) 
    {
        Target.transform.position = MoveLocation;
        LineRenderer.SetPosition(0, ShipIcon.transform.position);
        LineRenderer.SetPosition(1, MoveLocation);

        LineRenderer.enabled = true;
    
    }

    // Visualizes the map size bounds inside the Scene View
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(mapSize.x, mapSize.y, 0.1f));
    }


    private void MakeEntryExit() 
    {
        //find entry -beacon with lowest x value-
        for (int i = 0; i < BeaconCount; i++)
        {
            if (spawnedBeaconPositions[i].x < lowestXspot)
            {
                lowestXspot = spawnedBeaconPositions[i].x;
                Lyspot = spawnedBeaconPositions[i].y;
            }

        }


        //find exit beacon -coordinate with highest x value-
        for (int i = 0; i < BeaconCount; i++)
        {
            if (spawnedBeaconPositions[i].x > highestXspot)
            {
                highestXspot = spawnedBeaconPositions[i].x;
                yspot = spawnedBeaconPositions[i].y;
            }

        }

        //Target
        Vector3 EntryLocation = new Vector3(lowestXspot, Lyspot,0f);
        Target.transform.position = BeaconOrigin.position + EntryLocation;

        //Spawn ship icon here
        ShipIcon.transform.position = BeaconOrigin.position + EntryLocation;

        Vector3 ExitBeaconLoc = new Vector3(highestXspot, yspot, 0f);

        GameObject ExitBeacon = Instantiate(ExitSign, BeaconOrigin.position + ExitBeaconLoc, Quaternion.identity, transform);
        ExitBeacon.name = $"ExitBeacon";

    



    }

}
