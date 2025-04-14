using UnityEngine;
using ImGuiNET;

public class ShaderUI : MonoBehaviour {

    private void OnLayout() {
        ImGui.ShowDemoWindow();
    }

    private void OnEnable() {
        ImGuiUn.Layout += OnLayout;
    }

    private void OnDisable() {
        ImGuiUn.Layout -= OnLayout;
    }
}
