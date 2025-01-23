using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Transform[] _allTargets;
    [SerializeField] private Transform _allPlacespoint;
    [SerializeField] private float _speed;

    private int _index;

    private void Init()
    {
        _allTargets = new Transform[_allPlacespoint.childCount];

        for (int i = 0; i < _allPlacespoint.childCount; i++)
        {
            _allTargets[i] = _allPlacespoint.GetChild(i);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Transform target = _allTargets[_index];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        if ((transform.position - target.position).sqrMagnitude == 0)
            SelectNextTarget();
    }

    private void SelectNextTarget()
    {
        _index = ++_index % _allTargets.Length;

        Vector3 nextPoint = _allTargets[_index].transform.position;
        transform.forward = nextPoint - transform.position;
    }
}