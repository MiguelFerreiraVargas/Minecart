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
                Debug.Log("ACERTOU: " + hit.collider.name);

                Ore ore = hit.collider.GetComponentInParent<Ore>();

                if (ore != null)
                {
                    Debug.Log("TEM ORE");
                    ore.HitOre();
                }
                else
                {
                    Debug.Log("NÃO TEM ORE");
                }
            }
        }
    }
}