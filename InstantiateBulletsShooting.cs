using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] private float _multiplier;
    [SerializeField] private Transform _objectToShoot;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private Rigidbody _prefabBullet;

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
            var vector3direction = (_objectToShoot.position - transform.position).normalized;
            Rigidbody newBullet = Instantiate(_prefabBullet, transform.position + vector3direction, Quaternion.identity);

            newBullet.transform.up = vector3direction;
            newBullet.velocity = vector3direction * _multiplier;

            yield return _sleep;

            isWork = enabled;
        }
    }
}