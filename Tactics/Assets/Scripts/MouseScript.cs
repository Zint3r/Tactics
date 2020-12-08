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
    private void Update()
    {
        _navMesh.SetDestination(_movePoint);
    }
    private void OnClick()
    {
        //RaycastHit hit;
        //Vector3 clickPosition = new Vector3(_input.Player.Clickposition.ReadValue<Vector2>().x, _input.Player.Clickposition.ReadValue<Vector2>().y, 0);
        ////Debug.Log(_input.Player.Clickposition.ReadValue<Vector2>());
        //Ray ray = Camera.main.ScreenPointToRay(clickPosition);
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Vector3 newPos = Camera.main.ScreenToWorldPoint(hit.point);
        //    _onClickObj.transform.position = newPos;
        //    _onClickEffect.Play();
        //    _movePoint = clickPosition;           
        //}



        Ray camRay = Camera.main.ScreenPointToRay(_input.Player.Clickposition.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit))
        {
            _onClickObj.transform.position = hit.point;
            _onClickEffect.Play();
            _movePoint = hit.point;
        }
    }
}