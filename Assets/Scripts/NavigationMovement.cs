using UnityEngine;
using UnityEngine.AI;

public class NavigationMovement : MonoBehaviour
{
    public bool isMoving;

    public float Speed => agent.speed;

    Vector3 nowMovePoint = Vector3.zero;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        isMoving = true;        
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

        if (isMoving && Physics.Raycast(ray, out RaycastHit hit))
            return hit.point;
        return nowMovePoint;
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
}
