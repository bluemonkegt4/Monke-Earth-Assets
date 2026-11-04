using UnityEngine;
using System.Collections;
using System.IO;

public class SimpleVRRecorder : MonoBehaviour
{
    [Header("Recording")]
    public Camera recordCamera;
    public int width = 1920;
    public int height = 1080;
    public int frameRate = 30;

    private bool isRecording = false;
    private RenderTexture renderTexture;
    private Texture2D frameTexture;

    private string folderPath;
    private int frameCount = 0;

    private void OnEnable()
    {
        StartRecording();
    }

    private void OnDisable()
    {
        StopRecording();
    }

    // =========================
    // START RECORDING
    // =========================
    public void StartRecording()
    {
        if (isRecording) return;

        folderPath = Path.Combine(Application.persistentDataPath, "Recording_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        Directory.CreateDirectory(folderPath);

        renderTexture = new RenderTexture(width, height, 24);
        recordCamera.targetTexture = renderTexture;

        frameTexture = new Texture2D(width, height, TextureFormat.RGB24, false);

        frameCount = 0;
        isRecording = true;

        StartCoroutine(CaptureFrames());

        Debug.Log("[Recorder] Started → " + folderPath);
    }

    // =========================
    // STOP RECORDING
    // =========================
    public void StopRecording()
    {
        if (!isRecording) return;

        isRecording = false;

        recordCamera.targetTexture = null;
        RenderTexture.active = null;

        Debug.Log("[Recorder] Stopped. Frames saved to: " + folderPath);
    }

    // =========================
    // CAPTURE LOOP
    // =========================
    private IEnumerator CaptureFrames()
    {
        while (isRecording)
        {
            yield return new WaitForEndOfFrame();

            RenderTexture.active = renderTexture;
            frameTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            frameTexture.Apply();

            byte[] bytes = frameTexture.EncodeToPNG();
            string fileName = Path.Combine(folderPath, $"frame_{frameCount:D05}.png");

            File.WriteAllBytes(fileName, bytes);

            frameCount++;
        }
    }
}