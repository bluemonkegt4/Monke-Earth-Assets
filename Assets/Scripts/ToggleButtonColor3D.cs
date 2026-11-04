using UnityEngine;

public class ToggleButtonColor3D : MonoBehaviour
{
    public Renderer buttonRenderer;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    private bool isSelected;

    void Start()
    {
        SetColor(normalColor);
    }

    // Call this from your button interaction
    public void Toggle()
    {
        isSelected = !isSelected;
        SetColor(isSelected ? selectedColor : normalColor);
    }

    private void SetColor(Color color)
    {
        buttonRenderer.material.color = color;
    }
}