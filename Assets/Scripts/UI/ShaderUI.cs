using UnityEngine;

public class ShaderUI : MonoBehaviour {
    [SerializeField] private Material grassMaterial;

    [Header("Default States")]
    [SerializeField] private Color defaultNearColor;
    [SerializeField] private Color defaultFarColor;
    [SerializeField] private Color defaultBottomColor;

    private bool showMenu = true;
    private Vector2 scrollPosition;

    private Vector2 nearFarRange;
    private Color nearColor, farColor, bottomColor;
    private float alphaTreshold, heightBlend;

    // @TODO - Create default values
    private void Awake() {
        grassMaterial.SetColor("_NearColor", defaultNearColor);
        grassMaterial.SetColor("_FarColor", defaultFarColor);
        grassMaterial.SetColor("_BottomColor", defaultBottomColor);
        //grassMaterial.SetVector("_BottomColor", defaultBottomColor);
    }

    // @TODO - Finish Uniform Update Loop
    private void Update() {
        grassMaterial.SetColor("_NearColor", nearColor);
        grassMaterial.SetColor("_FarColor", farColor);
        grassMaterial.SetColor("_BottomColor", bottomColor);
        grassMaterial.SetFloat("_AlphaThreshold", alphaTreshold);
        grassMaterial.SetFloat("_HeightBlend", heightBlend);
        grassMaterial.SetColor("_BottomColor", bottomColor);
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

        showMenu = GUI.Toggle(new Rect(10, 10, 25, 25), showMenu, "=", buttonStyle);

        if (showMenu) {
            GUI.Box(new Rect(10, 40, 215, 225), "", boxStyle);

            scrollPosition = GUI.BeginScrollView(new Rect(15, 40, 215, 225), scrollPosition, new Rect(0, 0, 180, 400));

            // Add More UI Elements Under Here 

            nearColor = ColorSliders(new Rect(5, 0, 80, 20), "Near Color", nearColor);
            farColor = ColorSliders(new Rect(5, 80, 80, 20), "Far Color", farColor);
            nearFarRange = Vector2Sliders(new Rect(5, 160, 100, 20), "Near Far Range", nearFarRange, 0f, 20f);
            alphaTreshold = FloatSlider(new Rect(5, 225, 80, 20), "Alpha Threshold", alphaTreshold, 0f, 1f);
            heightBlend = FloatSlider(new Rect(5, 250, 80, 20), "Height Blend", heightBlend, 0f, 5f);
            bottomColor = ColorSliders(new Rect(5, 300, 80, 20), "Bottom Color", bottomColor);

            // End of Scroll View
            GUI.EndScrollView();
        }
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
    private Vector2 Vector2Sliders(Rect screenRect, string label, Vector2 vector, float min, float max) {
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
}
