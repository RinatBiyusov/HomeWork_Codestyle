using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private Transform[] _allTargets;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private int _index;

    private void Init()
    {
        _allTargets = new Transform[_target.childCount];

        for (int i = 0; i < _target.childCount; i++)
        {
            _allTargets[i] = _target.GetChild(i);
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

        if (transform.position == target.position)
            SelectNextTarget();
    }

    private void SelectNextTarget()
    {
        _index = ++_index % _allTargets.Length;

        Vector3 thisPointVector = _allTargets[_index].transform.position;
        transform.forward = thisPointVector - transform.position;
    }
}