using UnityEngine;

public class LookAtSun : MonoBehaviour
{
    public float lookAtSunTimer;

    public Transform sun;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    public bool printOutDifference;

    // Start is called before the first frame update
    void Start()
    {
        lookAtSunTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateSun();

        if (PlayerLookingAtSun())
        {
            lookAtSunTimer = lookAtSunTimer + Time.deltaTime;
            PrintInformation();
        }
    }

    public bool PlayerLookingAtSun()
    {
        Vector3 diff = transform.forward + sun.transform.forward;

        bool xTolerance = (diff.x >= minX) && (diff.x <= maxX);
        bool yTolerance = (diff.y >= minY) && (diff.y <= maxY);
        bool zTolerance = (diff.z >= minZ) && (diff.z <= maxZ);

        return xTolerance && yTolerance && zTolerance;
    }

    void RotateSun()
    {
        sun.transform.LookAt(transform);
    }

    void PrintInformation()
    {
        if (printOutDifference)
        {
            Vector3 diff = transform.forward + sun.transform.forward;
            Debug.Log("[X: " + diff.x + "] [Y: " + diff.y + "] [Z: " + diff.z + "] " + "LOOKING AT SUN");
        }
        else
        {
            Debug.Log("LOOKING AT SUN");
        }
    }
}
