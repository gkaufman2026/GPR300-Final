using UnityEngine;

public class ShaderUI : MonoBehaviour {
    [SerializeField] private Material grassMaterial;

    [Header("Default States")]
    [SerializeField] private Color defaultNearColor;
    [SerializeField] private Color defaultFarColor;
    [SerializeField] private Color defaultBottomColor;
    [SerializeField] private float defaultAlphaThreshold, defaultHeightBlend;
    [SerializeField] private Vector2 defaultNearFarPlane;

    [Header("New Terrain Stuff")]
    [SerializeField] private float defaultTerrainStrength;
    [SerializeField] private Color defaultShadowColor;
    [SerializeField] private float defaultWindSpeed;
    [SerializeField] private float defaultWindIntensity;
    [SerializeField] private Vector2 defaultWindNoiseScale;
    [SerializeField] private float defaultWindNoiseSpeed;
    [SerializeField] private Vector2 defaultWindContrast;
    [SerializeField] private float defaultWindHeight;

    [Header("Time Contrller")]
    [SerializeField] private TimeController timeController;

    private bool showMenu = true;
    private Vector2 scrollPosition;

    private Vector2 nearFarRange;
    private Color nearColor, farColor, bottomColor;
    private float alphaTreshold, heightBlend;
    private float dayCycleSlider;
    private float defaultDayDuration;

    private float terrainStrength, windSpeed, windIntensity, windNoiseSpeed, windHeight;
    private Color shadowColor;
    private Vector2 windNoiseScale, windNoiseContrast;

    private void Awake() {
        // Setting Uniforms
        grassMaterial.SetColor("_NearColor", defaultNearColor);
        grassMaterial.SetColor("_FarColor", defaultFarColor);
        grassMaterial.SetColor("_BottomColor", defaultBottomColor);
        grassMaterial.SetFloat("_AlphaThreshold", defaultAlphaThreshold);
        grassMaterial.SetFloat("_HeightBlend", defaultHeightBlend);
        grassMaterial.SetVector("_NearFarRange", new Vector4(defaultNearFarPlane.x, defaultNearFarPlane.y, 0, 0));
        grassMaterial.SetFloat("_TerrainStrength", defaultTerrainStrength);
        grassMaterial.SetColor("_ShadowColor", defaultShadowColor);
        grassMaterial.SetFloat("_WindSpeed", defaultWindSpeed);
        grassMaterial.SetFloat("_WindIntensity", defaultWindIntensity);
        grassMaterial.SetVector("_WindNoiseScale", new Vector4(defaultWindNoiseScale.x, defaultWindNoiseScale.y, 0, 0));
        grassMaterial.SetFloat("_WindNoiseSpeed", defaultWindNoiseSpeed);
        grassMaterial.SetVector("_WindNoiseContrast", new Vector4(defaultWindContrast.x, defaultWindContrast.y, 0, 0));
        grassMaterial.SetFloat("_WindHeight", defaultWindHeight);

        // Setting Default for Sliders
        nearColor = grassMaterial.GetColor("_NearColor");
        farColor = grassMaterial.GetColor("_FarColor");
        bottomColor = grassMaterial.GetColor("_BottomColor");
        alphaTreshold = grassMaterial.GetFloat("_AlphaThreshold");
        heightBlend = grassMaterial.GetFloat("_HeightBlend");
        nearFarRange = grassMaterial.GetVector("_NearFarRange");
        terrainStrength = grassMaterial.GetFloat("_TerrainStrength");
        shadowColor = grassMaterial.GetColor("_ShadowColor");
        windSpeed = grassMaterial.GetFloat("_WindSpeed");
        windIntensity = grassMaterial.GetFloat("_WindIntensity");
        windNoiseScale = grassMaterial.GetVector("_WindNoiseScale");
        windNoiseSpeed = grassMaterial.GetFloat("_WindNoiseSpeed");
        windNoiseContrast = grassMaterial.GetVector("_WindNoiseContrast");
        windHeight = grassMaterial.GetFloat("_WindHeight");

        dayCycleSlider = timeController.GetSecondsPerDay();
        defaultDayDuration = timeController.GetSecondsPerDay();
    }

    private void Update() {
        grassMaterial.SetColor("_NearColor", nearColor);
        grassMaterial.SetColor("_FarColor", farColor);
        grassMaterial.SetColor("_BottomColor", bottomColor);
        grassMaterial.SetFloat("_AlphaThreshold", alphaTreshold);
        grassMaterial.SetFloat("_HeightBlend", heightBlend);
        grassMaterial.SetColor("_BottomColor", bottomColor);
        grassMaterial.SetVector("_NearFarRange", nearFarRange);

        grassMaterial.SetFloat("_TerrainStrength", terrainStrength);
        grassMaterial.SetColor("_ShadowColor", shadowColor);
        grassMaterial.SetFloat("_WindSpeed", windSpeed);
        grassMaterial.SetFloat("_WindIntensity", windIntensity);
        grassMaterial.SetVector("_WindNoiseScale", windNoiseScale);
        grassMaterial.SetFloat("_WindNoiseSpeed", windNoiseSpeed);
        grassMaterial.SetVector("_WindNoiseContrast", windNoiseContrast);
        grassMaterial.SetFloat("_WindHeight", windHeight);
    }

    void OnGUI() {
        GUIStyle boxStyle = new(GUI.skin.box) {
            fontSize = 12,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.UpperCenter
        };
        GUIStyle buttonStyle = new(GUI.skin.button) {
            fontSize = 12,
            fontStyle = FontStyle.Normal
        };
        GUIStyle headerStyle = new(GUI.skin.label) {
            fontSize = 15,
            fontStyle = FontStyle.Bold,
        };

        showMenu = GUI.Toggle(new Rect(10, 10, 25, 25), showMenu, "=", buttonStyle);

        if (showMenu) {
            GUI.Box(new Rect(10, 40, 250, 225), "", boxStyle);

            // Scroll Bar - Change last rect param (~400) to increase y-axis
            scrollPosition = GUI.BeginScrollView(new Rect(15, 40, 260, 225), scrollPosition, new Rect(0, 0, 180, 1000));

            GUI.Label(new Rect(5, 0, 120, 25), "Grass Uniforms", headerStyle);

            // Add More UI Elements Under Here 
            nearColor = ColorSliders(new Rect(5, 25, 80, 20), "Near Color", nearColor);
            farColor = ColorSliders(new Rect(5, 105, 80, 20), "Far Color", farColor);
            nearFarRange = Float2Sliders(new Rect(5, 185, 100, 20), "Near Far Range", nearFarRange, 0f, 20f);
            alphaTreshold = FloatSlider(new Rect(5, 250, 100, 20), "Alpha Threshold", alphaTreshold, 0f, 1f);
            heightBlend = FloatSlider(new Rect(5, 275, 100, 20), "Height Blend", heightBlend, 0f, 1.5f);
            bottomColor = ColorSliders(new Rect(5, 300, 80, 20), "Bottom Color", bottomColor);

            terrainStrength = FloatSlider(new Rect(5, 385, 100, 20), "Terrain Strength", terrainStrength, 0, 1);
            shadowColor = ColorSliders(new Rect(5, 410, 80, 20), "Shadow Color", shadowColor);
            windSpeed = FloatSlider(new Rect(5, 495, 100, 20), "Wind Speed", windSpeed, 0f, 1f);
            windIntensity = FloatSlider(new Rect(5, 515, 100, 20), "Wind Intensity", windIntensity, 0f, 1f);
            windNoiseScale = Float2Sliders(new Rect(5, 535, 120, 20), "Wind Noise Scale", windNoiseScale, 0f, 200f);
            windNoiseSpeed = FloatSlider(new Rect(5, 600, 120, 20), "Wind Noise Speed", windNoiseSpeed, -10f, 20f);
            windNoiseContrast = Float2Sliders(new Rect(5, 625, 120, 20), "Wind Noise Contrast", windNoiseContrast, 0f, 4f);
            windHeight = FloatSlider(new Rect(5, 690, 120, 20), "Wind Height", windHeight, 0f, 1f);

            GUI.Label(new Rect(5, 715, 120, 25), "Day Night Cycle", headerStyle);
            timeController.SetSecondsPerDay(dayCycleSlider = FloatSlider(new Rect(5, 740, 100, 20), "Sec Per Day", dayCycleSlider, 0.0f, 20f));

            if (GUI.Button(new Rect(5, 775, 50, 20), "Reset")) {
                Reset();
            }

            // End of Scroll View
            GUI.EndScrollView();
        }
    }

    private void Reset() {
        grassMaterial.SetColor("_NearColor", defaultNearColor);
        grassMaterial.SetColor("_FarColor", defaultFarColor);
        grassMaterial.SetColor("_BottomColor", defaultBottomColor);
        grassMaterial.SetFloat("_AlphaThreshold", defaultAlphaThreshold);
        grassMaterial.SetFloat("_HeightBlend", defaultHeightBlend);
        grassMaterial.SetVector("_NearFarRange", new Vector4(defaultNearFarPlane.x, defaultNearFarPlane.y, 0, 0));
        grassMaterial.SetFloat("_TerrainStrength", defaultTerrainStrength);
        grassMaterial.SetColor("_ShadowColor", defaultShadowColor);
        grassMaterial.SetFloat("_WindSpeed", defaultWindSpeed);
        grassMaterial.SetFloat("_WindIntensity", defaultWindIntensity);
        grassMaterial.SetVector("_WindNoiseScale", new Vector4(defaultWindNoiseScale.x, defaultWindNoiseScale.y, 0, 0));
        grassMaterial.SetFloat("_WindNoiseSpeed", defaultWindNoiseSpeed);
        grassMaterial.SetVector("_WindNoiseContrast", new Vector4(defaultWindContrast.x, defaultWindContrast.y, 0, 0));
        grassMaterial.SetFloat("_WindHeight", defaultWindHeight);

        nearColor = grassMaterial.GetColor("_NearColor");
        farColor = grassMaterial.GetColor("_FarColor");
        bottomColor = grassMaterial.GetColor("_BottomColor");
        alphaTreshold = grassMaterial.GetFloat("_AlphaThreshold");
        heightBlend = grassMaterial.GetFloat("_HeightBlend");
        nearFarRange = grassMaterial.GetVector("_NearFarRange");
        terrainStrength = grassMaterial.GetFloat("_TerrainStrength");
        shadowColor = grassMaterial.GetColor("_ShadowColor");
        windSpeed = grassMaterial.GetFloat("_WindSpeed");
        windIntensity = grassMaterial.GetFloat("_WindIntensity");
        windNoiseScale = grassMaterial.GetVector("_WindNoiseScale");
        windNoiseSpeed = grassMaterial.GetFloat("_WindNoiseSpeed");
        windNoiseContrast = grassMaterial.GetVector("_WindNoiseContrast");
        windHeight = grassMaterial.GetFloat("_WindHeight");

        dayCycleSlider = defaultDayDuration;
        timeController.SetSecondsPerDay(dayCycleSlider);
    }

    private float FloatSlider(Rect screenRect, string labelText, float sliderValue, float min, float max) {
        GUI.Label(screenRect, labelText);

        screenRect.x += screenRect.width;

        sliderValue = GUI.HorizontalSlider(screenRect, sliderValue, min, max);
        return sliderValue;
    }

    private Color ColorSliders(Rect screenRect, string label, Color color) {
        GUI.Label(screenRect, label);
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "R:");
        screenRect.x += screenRect.width;
        color.r = GUI.HorizontalSlider(screenRect, color.r, 0.0f, 1.0f);

        screenRect.x -= screenRect.width;
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "G:");
        screenRect.x += screenRect.width;
        color.g = GUI.HorizontalSlider(screenRect, color.g, 0.0f, 1.0f);

        screenRect.x -= screenRect.width;
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "B:");
        screenRect.x += screenRect.width;
        color.b = GUI.HorizontalSlider(screenRect, color.b, 0.0f, 1.0f);

        return color;
    }

    private Vector2 Float2Sliders(Rect screenRect, string label, Vector2 vector, float min, float max) {
        GUI.Label(screenRect, label);
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "X:");
        screenRect.x += screenRect.width;
        vector.x = GUI.HorizontalSlider(screenRect, vector.x, min, max);

        screenRect.x -= screenRect.width;
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "Y:");
        screenRect.x += screenRect.width;
        vector.y = GUI.HorizontalSlider(screenRect, vector.y, min, max);

        screenRect.x -= screenRect.width;
        screenRect.y += screenRect.height;

        return vector;
    }

    private Vector2 Float3Sliders(Rect screenRect, string label, Vector3 vector, float min, float max) {
        GUI.Label(screenRect, label);
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "X:");
        screenRect.x += screenRect.width;
        vector.x = GUI.HorizontalSlider(screenRect, vector.x, min, max);

        screenRect.x -= screenRect.width;
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "Y:");
        screenRect.x += screenRect.width;
        vector.y = GUI.HorizontalSlider(screenRect, vector.y, min, max);

        screenRect.x -= screenRect.width;
        screenRect.y += screenRect.height;

        GUI.Label(screenRect, "Z:");
        screenRect.x += screenRect.width;
        vector.z = GUI.HorizontalSlider(screenRect, vector.z, min, max);

        return vector;
    }
}
