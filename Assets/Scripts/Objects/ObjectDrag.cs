using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public float interactionDistance = 2f;
    private bool isDragging = false;
    private Transform draggedObject;
    private Vector3 offset;

    void Update()
    {
        // Raycast to detect objects in proximity
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryStartDrag();
        }

        if (isDragging)
        {
            UpdateDrag();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            EndDrag();
        }
    }

    void TryStartDrag()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Draggable"))
            {
                StartDrag(hit.transform);
            }
        }
    }

    void StartDrag(Transform obj)
    {
        isDragging = true;
        draggedObject = obj;
        offset = obj.position - GetMouseWorldPos();
    }

    void UpdateDrag()
    {
        if (draggedObject != null)
        {
            draggedObject.position = GetMouseWorldPos() + offset;
        }
    }

    void EndDrag()
    {
        isDragging = false;
        draggedObject = null;
    }

    Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}