using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName3DValidator : MonoBehaviour
{
    [Header("3D TMP Text Objects")]
    public TMP_Text playerNameText;  // Shows the current player name
    public TMP_Text warningText;     // Shows floating warning text

    [Header("Blacklist")]
    public List<string> blacklistedNames = new List<string>(); // Add blacklisted names in inspector

    private string previousName;

    private void Start()
    {
        // If the name text is blank, assign placeholder
        if (playerNameText != null)
        {
            string startingName = playerNameText.text.Trim();
            if (string.IsNullOrEmpty(startingName))
            {
                startingName = GeneratePlaceholderName();
                playerNameText.text = startingName;
            }

            previousName = startingName;
        }

        // Hide warning at start
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Call this method whenever the player submits a new name
    /// </summary>
    public void SetPlayerName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            newName = "";

        newName = newName.Trim();

        // Check for blank or blacklisted
        if (string.IsNullOrEmpty(newName) || IsBlacklisted(newName))
        {
            ShowWarning("Invalid name!");

            string fallback = !string.IsNullOrEmpty(previousName) ? previousName : GeneratePlaceholderName();
            previousName = fallback;

            if (playerNameText != null)
                playerNameText.text = fallback;

            return;
        }

        // Valid name → update previous
        previousName = newName;

        if (playerNameText != null)
            playerNameText.text = newName;
    }

    private bool IsBlacklisted(string name)
    {
        foreach (string banned in blacklistedNames)
        {
            if (name.ToLower() == banned.ToLower())
                return true;
        }
        return false;
    }

    private void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
            warningText.gameObject.SetActive(true);
            Invoke(nameof(HideWarning), 2f); // hide after 2 seconds
        }
        else
        {
            Debug.LogWarning(message);
        }
    }

    private void HideWarning()
    {
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    private string GeneratePlaceholderName()
    {
        int randomNumber = Random.Range(10000, 99999);
        return $"Merthling{randomNumber}";
    }

    public string GetPlayerName()
    {
        return previousName;
    }
}