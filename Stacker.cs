using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    private float timer;
    private float lastSpawnTime;
    private float currentY;
    private bool spawnAvailable;
    private bool dropCubeAvailable;
    private Vector3 startPosCube;
    private Vector3 startPosDropCube;
    private GameObject dropCubeRef;
    private GameObject cubeRef;

    public GameObject prefabCube;
    public GameObject prefabDropCube;
    public float prefabHeight;
    public float minimumWait;
    public bool dropIn;


    // Start is called before the first frame update
    void Start()
    {
        currentY = 0.0f;
        spawnAvailable = true;
        dropCubeAvailable = true;
        startPosCube = prefabCube.transform.position;
        startPosDropCube = prefabDropCube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAvailable();
        UpdateDropAvailable();
        timer = timer + Time.deltaTime;
    }

    private void OnMouseDown()
    {
        Debug.Log("CLICKED");

        if (dropIn)
        {
            SpawnDroppedCube();
        }
        else
        {
            SpawnCube();
        }
        Debug.Log("~ ~ ~");
    }

    void UpdateAvailable()
    {
        if ((timer - lastSpawnTime) > minimumWait)
        {
            spawnAvailable = true;
        }
    }

    void UpdateDropAvailable()
    {
        if (dropCubeRef && dropCubeRef.activeInHierarchy)
        {
            // TODO: Use dynamic size
            float minimumHeight = prefabDropCube.transform.position.y - prefabHeight;
            if (dropCubeRef.transform.position.y < minimumHeight) { dropCubeAvailable = true; }
        }
    }

    void SpawnCube()
    {
        Vector3 nextSpawnPosition = startPosCube;
        nextSpawnPosition.y = currentY;

        cubeRef = Instantiate(prefabCube, nextSpawnPosition, prefabCube.transform.rotation);
        cubeRef.SetActive(true);

        lastSpawnTime = timer;
        currentY = currentY + prefabHeight;
    }

    void SpawnDroppedCube()
    {
        dropCubeRef = Instantiate(prefabDropCube, startPosDropCube, prefabDropCube.transform.rotation);
        dropCubeRef.SetActive(true);

        dropCubeAvailable = false;
        lastSpawnTime = timer;
    }
}