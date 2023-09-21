using UnityEngine;
using UnityEngine.AI;

public class NavigationMovement : MonoBehaviour
{
    int layerIndex = 3;

    Vector3 nowMovePoint = Vector3.zero;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Touch();
    }

    void Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 movePoint = Raycasting(touch.position);
            nowMovePoint = movePoint;
            agent.SetDestination(movePoint);
        }
    }

    Vector3 Raycasting(Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);

        if (Physics.Raycast(ray, out RaycastHit hit) &&
            InspectRaycastObjectLayers(hit))
            return hit.point;
        return nowMovePoint;
    }

    bool InspectRaycastObjectLayers(RaycastHit hit)
    {
        return hit.transform.gameObject.layer == layerIndex;
    }
}
