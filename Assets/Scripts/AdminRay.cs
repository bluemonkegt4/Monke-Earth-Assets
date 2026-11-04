using UnityEngine;

public class AdminRay : MonoBehaviour
{
    public LineRenderer line;
    public float range = 50f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        line.SetPosition(0, transform.position);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            line.SetPosition(1, hit.point);

            if (Input.GetMouseButtonDown(0)) // replace with controller trigger later
            {
                PlayerTarget target = hit.collider.GetComponent<PlayerTarget>();
                if (target != null)
                {
                    target.OnAdminSelected();
                }
            }
        }
        else
        {
            line.SetPosition(1, transform.position + transform.forward * range);
        }
    }
}
