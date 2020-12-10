using UnityEngine;
using UnityEngine.AI;
public class MouseScript : MonoBehaviour
{
    [SerializeField] private GameObject _onClickObj;
    private ParticleSystem _onClickEffect;
    private PlayerControls _input;
    private NavMeshAgent _navMesh;
    private Vector3 _movePoint;   
    private void Awake()
    {
        _input = new PlayerControls();
        _input.Player.Click.performed += context => OnClick();
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
    private void Start()
    {
        _onClickEffect = _onClickObj.GetComponentInChildren<ParticleSystem>();
        _navMesh = GetComponent<NavMeshAgent>();
    }
    private void LateUpdate()
    {
        
    }
    private void OnClick()
    {      
        Ray camRay = Camera.main.ScreenPointToRay(_input.Player.Clickposition.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit))
        {
            if (hit.collider.gameObject.layer == 0)
            {
                _onClickObj.transform.position = hit.point;
                _onClickEffect.Play();
                _movePoint = hit.point;
                _navMesh.SetDestination(GetCharacterPositionOnNavMesh(_movePoint));
            }                     
        }
    }
    public Vector3 GetCharacterPositionOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        bool positionFound = NavMesh.SamplePosition(position, out hit, 50, NavMesh.AllAreas);

        if (!positionFound)
            Debug.LogWarning("No valid position found !");

        if (positionFound == true)
        {
            return hit.position;
        }
        else
        {
            return position;
        }        
    }
    private void RayCaster()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.collider.gameObject.GetComponent<DoorScript>() == true)
            {
                _navMesh.ResetPath();                
            }
        }
    }
}