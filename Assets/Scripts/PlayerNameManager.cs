using UnityEngine;
using TMPro; // For TextMeshPro 3D input

public class PlayerName3D : MonoBehaviour
{
    [Header("3D TMP Input Field")]
    public TMP_InputField nameInputField; // Drag your 3D TMP_InputField here

    private string previousName;

    private void Start()
    {
        if (nameInputField != null)
        {
            string startingName = nameInputField.text.Trim();

            // Assign placeholder if blank
            if (string.IsNullOrEmpty(startingName))
            {
                startingName = GenerateDefaultName();
                nameInputField.text = startingName;
            }

            previousName = startingName;

            // Listen for when editing ends
            nameInputField.onEndEdit.AddListener(OnNameSubmitted);
        }
    }

    private void OnNameSubmitted(string newName)
    {
        newName = newName.Trim();

        if (string.IsNullOrEmpty(newName))
        {
            // Warn player and revert
            ShowBlankNameWarning();

            if (nameInputField != null)
                nameInputField.text = previousName;

            return;
        }

        // Update previous name
        previousName = newName;
        Debug.Log($"Player name updated to: {newName}");
    }

    private void ShowBlankNameWarning()
    {
        // Editor popup
#if UNITY_EDITOR
        UnityEditor.EditorUtility.DisplayDialog("Invalid Name", "You cannot have a blank name!", "OK");
#else
        // Replace with in-game 3D popup if desired
        Debug.LogWarning("You cannot have a blank name!");
#endif
    }

    private string GenerateDefaultName()
    {
        int randomNumber = Random.Range(10000, 99999);
        return $"Merthling{randomNumber}";
    }

    public string GetPlayerName()
    {
        return previousName;
    }
}