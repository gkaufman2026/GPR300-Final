using UnityEngine;
using ImGuiNET;

public class ShaderUI : MonoBehaviour {

    private void OnLayout() {
        if (!ImGui.Begin("Graphics Final")) {
            ImGui.End();
            return;
        }
        // Render Function
        RenderUIElements();

        ImGui.End();
    }

    private void RenderUIElements() {
        if (ImGui.CollapsingHeader("Grass Variations")) {
            AddGrass();
        }

        if (ImGui.CollapsingHeader("Day Night Cycle")) {
            AddDayNightCycleUI();
        }
    }

    private void AddGrass() {
        AddGrassColorUI();
        AddGrassSwayUI();
    }

    private void AddGrassColorUI() {

    }

    private void AddGrassSwayUI() {

    }

    private void AddDayNightCycleUI() {

    }

    private void OnEnable() {
        ImGuiUn.Layout += OnLayout;
    }

    private void OnDisable() {
        ImGuiUn.Layout -= OnLayout;
    }
}
