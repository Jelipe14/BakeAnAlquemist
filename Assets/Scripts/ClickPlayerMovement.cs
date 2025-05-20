using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask groundLayer; // Asignar en el Inspector
    [SerializeField] private LayerMask npcLayer;

    private Vector2 target;
    private Camera cam;
    private bool shouldMove = false;
    
    private bool IsClickOnNPC(Vector2 mousePos)
    {
        return Physics2D.Raycast(mousePos, Vector2.zero,0.1f,npcLayer).collider != null;
    }
    void Start()
    {
        cam = Camera.main;
        target = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Click sobre UI, Ignorar movimiento");
                return;
            }
            Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (IsClickOnNPC(mouseWorldPos))
            {
                Debug.Log("Npc Click");
                return;
            }
            
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0.1f, groundLayer);

            if (hit.collider != null)
            {
                Debug.Log("Raycast hit: " + hit.collider.name + " in layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
                target = mouseWorldPos;
                shouldMove = true;
            }
            else
            {
                Debug.Log("Raycast missed.");
                shouldMove = false;
            }
        }

        if (shouldMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                shouldMove = false;
            }
        }
    }
}