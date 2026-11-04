using UnityEngine;

public class WaterVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GorillaSwimming swimmer = other.GetComponentInParent<GorillaSwimming>();

        if (swimmer != null)
        {
            swimmer.inWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GorillaSwimming swimmer = other.GetComponentInParent<GorillaSwimming>();

        if (swimmer != null)
        {
            swimmer.inWater = false;
        }
    }
}