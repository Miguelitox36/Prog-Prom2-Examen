using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private LayerMask interactionLayerMask = -1;
    private Camera playerCamera;
    private Player player;

    void Start()
    {        
        player = GetComponent<Player>();
        playerCamera = Camera.main;

        if (playerCamera == null)
        {
            playerCamera = FindObjectOfType<Camera>();
        }

        if (player == null)
        {
            Debug.LogError("PlayerInteraction: No se encontró el componente Player");
        }
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        if (player == null) return;
        
        if (playerCamera != null)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange, interactionLayerMask))
            {
                Debug.Log($"Raycast hit: {hit.collider.name}");
               
                InteractableComponent interactable = hit.collider.GetComponent<InteractableComponent>();
                if (interactable != null)
                {
                    Debug.Log($"Found InteractableComponent on {hit.collider.name}");
                    interactable.Interact(player);
                    return;
                }
                               
                IInteractable directInteractable = hit.collider.GetComponent<IInteractable>();
                if (directInteractable != null)
                {
                    Debug.Log($"Found IInteractable directly on {hit.collider.name}");
                    directInteractable.Interact(player);
                    return;
                }

                Debug.Log($"No interactable component found on {hit.collider.name}");
            }
        }
                
        TryInteractNearby();
    }

    void TryInteractNearby()
    {        
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, interactionRange, interactionLayerMask);

        foreach (Collider col in nearbyColliders)
        {
            if (col.gameObject == gameObject) continue; 
            
            InteractableComponent interactable = col.GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                Debug.Log($"Found nearby InteractableComponent on {col.name}");
                interactable.Interact(player);
                return;
            }
            
            IInteractable directInteractable = col.GetComponent<IInteractable>();
            if (directInteractable != null)
            {
                Debug.Log($"Found nearby IInteractable directly on {col.name}");
                directInteractable.Interact(player);
                return;
            }
        }

        Debug.Log("No interactable objects found nearby");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}