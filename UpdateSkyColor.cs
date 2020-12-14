using UnityEngine;

public class UpdateSkyColor : MonoBehaviour
{
    // "color" will be in RGBA mode
    private Color nextColor;
    private Color color;
    private Light light;

    private float red = 0.0f;
    private float green = 1.0f;
    private float blue = 1.0f;
    private float alpha = 1.0f;

    public LookAtSun rotationReference;
    public float colorChangeAmount;

    // Use this for initialization    
    void Start()
    {
        light = GetComponent<Light>();
        nextColor = new Color(red, blue, green, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationReference.PlayerLookingAtSun())
        {
            //UpdateColor();
            UpdateSkyboxColor();
            Debug.Log("LOOKING AT SUN");
        }
        else
        {
            Debug.Log(" ");
        }
    }

    //void UpdateColor()
    //{
    //    red += colorUpdateSpeed % 1.0f;
    //    color = new Color(red, blue, green, alpha);
    //    light.color = color;
    //}

    void UpdateSkyboxColor()
    {
        float lerp = Mathf.PingPong(Time.time, 1.0f) / 1.0f;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(color, nextColor, lerp));

        color = nextColor;
        SetNewNextColor();
    }

    void SetNewNextColor()
    {
        //red = ((red + colorChangeAmount) % 255.0f) / 255.0f;
        //nextColor = new Color(red, blue, green, alpha);

        red = ((red * 1.25f) % 255.0f) / 255.0f;
        nextColor = new Color(red, blue, green, alpha);
    }
}
