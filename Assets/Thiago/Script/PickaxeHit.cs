using UnityEngine;

public class PickaxeHit : MonoBehaviour
{
    public Camera cam;
    public float range = 4f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                Ore ore = hit.collider.GetComponent<Ore>();

                if (ore != null)
                {
                    ore.HitOre();
                }
            }
        }
    }
}