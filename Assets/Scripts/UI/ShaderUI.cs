using UnityEngine;

public class ShaderUI : MonoBehaviour {
    [SerializeField] private Material grassMaterial;

    [Header("Default States")]
    [SerializeField] private Color defaultNearColor;
    [SerializeField] private Color defaultFarColor;
    [SerializeField] private Color defaultBottomColor;
    [SerializeField] private float defaultAlphaThreshold, defaultHeightBlend;
    [SerializeField] private Vector2 defaultNearFarPlane;

    [Header("Time Contrller")]
    [SerializeField] private TimeController timeController;

    private bool showMenu = true;
    private Vector2 scrollPosition;

    private Vector2 nearFarRange;
    private Color nearColor, farColor, bottomColor;
    private float alphaTreshold, heightBlend;
    private float dayCycleSlider;
    private float defaultDayDuration;

    [SerializeField] private GameObject capsule;

    private void Awake() {
        // Setting Uniforms
        grassMaterial.SetColor("_NearColor", defaultNearColor);
        grassMaterial.SetColor("_FarColor", defaultFarColor);
        grassMaterial.SetColor("_BottomColor", defaultBottomColor);
        grassMaterial.SetFloat("_AlphaThreshold", defaultAlphaThreshold);
        grassMaterial.SetFloat("_HeightBlend", defaultHeightBlend);
        grassMaterial.SetVector("_NearFarRange", new Vector4(defaultNearFarPlane.x, defaultNearFarPlane.y, 0, 0));

        // Setting Default for Sliders
        nearColor = grassMaterial.GetColor("_NearColor");
        farColor = grassMaterial.GetColor("_FarColor");
        bottomColor = grassMaterial.GetColor("_BottomColor");
        alphaTreshold = grassMaterial.GetFloat("_AlphaThreshold");
        heightBlend = grassMaterial.GetFloat("_HeightBlend");
        nearFarRange = grassMaterial.GetVector("_NearFarRange");

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

        grassMaterial.SetVector("_PressurePosition", capsule.transform.position);
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
            GUI.Box(new Rect(10, 40, 215, 225), "", boxStyle);

            // Scroll Bar - Change last rect param (~400) to increase y-axis
            scrollPosition = GUI.BeginScrollView(new Rect(15, 40, 225, 225), scrollPosition, new Rect(0, 0, 180, 600));

            GUI.Label(new Rect(5, 0, 120, 25), "Grass Uniforms", headerStyle);

            // Add More UI Elements Under Here 
            nearColor = ColorSliders(new Rect(5, 25, 80, 20), "Near Color", nearColor);
            farColor = ColorSliders(new Rect(5, 105, 80, 20), "Far Color", farColor);
            nearFarRange = Float2Sliders(new Rect(5, 185, 100, 20), "Near Far Range", nearFarRange, 0f, 20f);
            alphaTreshold = FloatSlider(new Rect(5, 250, 100, 20), "Alpha Threshold", alphaTreshold, 0f, 1f);
            heightBlend = FloatSlider(new Rect(5, 275, 100, 20), "Height Blend", heightBlend, 0f, 1.5f);
            bottomColor = ColorSliders(new Rect(5, 300, 80, 20), "Bottom Color", bottomColor);

            // @TODO - Implement a style to distinguish between others
            GUI.Label(new Rect(5, 385, 120, 25), "Day Night Cycle", headerStyle);
            timeController.SetSecondsPerDay(dayCycleSlider = FloatSlider(new Rect(5, 410, 100, 20), "Sec Per Day", dayCycleSlider, 0.0f, 20f));

            if (GUI.Button(new Rect(5, 435, 50, 20), "Reset")) {
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

        nearColor = grassMaterial.GetColor("_NearColor");
        farColor = grassMaterial.GetColor("_FarColor");
        bottomColor = grassMaterial.GetColor("_BottomColor");
        alphaTreshold = grassMaterial.GetFloat("_AlphaThreshold");
        heightBlend = grassMaterial.GetFloat("_HeightBlend");
        nearFarRange = grassMaterial.GetVector("_NearFarRange");

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
