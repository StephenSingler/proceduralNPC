using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 1f);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            {
                NPCDialogue npc = hit.collider.GetComponent<NPCDialogue>();
                if (npc != null)
                {
                    npc.Interact();
                }
            }
        }
    }
}