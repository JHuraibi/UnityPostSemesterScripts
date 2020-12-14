using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSunColorChange : MonoBehaviour
{
    // "color" will be in RGBA mode
    private Color nextColor;
    private Color color;
    private float lookAtSunTimer;

    private float red = 255.0f;
    private float green = 255.0f;
    private float blue = 255.0f;
    private float alpha = 1.0f;

    private bool disableAll;

    public Light light;
    public Transform sunObject;
    public bool printOutDifference = true;
    public float minX = -1.0f;
    public float maxX = 1.0f;
    public float minY = -1.0f;
    public float maxY = 1.0f;
    public float minZ = -1.0f;
    public float maxZ = 1.0f;
    public float redChangeAmount = 0.0f;
    public float blueChangeAmount = 0.0f;
    public float greenChangeAmount = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        lookAtSunTimer = 0.0f;
        nextColor = new Color(red, blue, green, alpha);
        disableAll = false;

        if (!sunObject || !light)
        {
            disableAll = true;
            MissingItems();
        }
    }

    void MissingItems()
    {
        Debug.Log("Sun Object or Light Source is not set.");
    }

    // Update is called once per frame
    void Update()
    {
        RotateSun();

        if (PlayerLookingAtSun() && !disableAll)
        {
            lookAtSunTimer = lookAtSunTimer + Time.deltaTime;
            UpdateLightColor();
            //UpdateSkyboxColor();    // Skybox color changing not working yet
            PrintInformation();
        }
        else if (!disableAll)
        {
            PrintInformation();
        }
        else
        {
            MissingItems();
        }
    }

    public bool PlayerLookingAtSun()
    {
        Vector3 diff = transform.forward + sunObject.transform.forward;

        bool xTolerance = (diff.x >= minX) && (diff.x <= maxX);
        bool yTolerance = (diff.y >= minY) && (diff.y <= maxY);
        bool zTolerance = (diff.z >= minZ) && (diff.z <= maxZ);

        return xTolerance && yTolerance && zTolerance;
    }

    void RotateSun()
    {
        sunObject.transform.LookAt(transform);
    }

    void PrintInformation()
    {
        if (PlayerLookingAtSun() && printOutDifference)
        {
            Vector3 diff = transform.forward + sunObject.transform.forward;
            Debug.Log("(x, y, z): " + diff + " LOOKING AT SUN! Timer: " + lookAtSunTimer);
        }
        else if (printOutDifference)
        {
            Vector3 diff = transform.forward + sunObject.transform.forward;
            Debug.Log("(x, y, z): " + diff);
        }
        else
        {
            Debug.Log(" ");
        }
    }

    void UpdateLightColor()
    {
        red = (red + redChangeAmount) % 256.0f;
        blue = (blue + blueChangeAmount) % 256.0f;
        green = (green + greenChangeAmount) % 256.0f;

        color = new Color((red / 255.0f), (blue / 255.0f), (green / 255.0f), alpha);
        light.color = color;
    }
}
