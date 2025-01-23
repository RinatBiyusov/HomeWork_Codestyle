using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shoter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private Bullet _prefabBullet;

    private Coroutine _coroutineShooting;
    private WaitForSeconds _sleep;

    private void Start()
    {
        _sleep = new WaitForSeconds(_timeWaitShooting);
        _coroutineShooting = StartCoroutine(ShootingWorker());
    }

    private void OnDisable()
    {
        if (_coroutineShooting != null)
            StopCoroutine(_coroutineShooting);
    }

    private IEnumerator ShootingWorker()
    {
        bool isWork = enabled;

        while (true)
        {
            var direction = (_prefabBullet.transform.position - transform.position).normalized;
            Bullet newBullet = Instantiate(_prefabBullet, transform.position + direction, Quaternion.identity);

            newBullet.transform.up = direction;
            newBullet.Init(direction * _speed);

            yield return _sleep;

            isWork = enabled;
        }
    }
}