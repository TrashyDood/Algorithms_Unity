using UnityEngine;

public class Enemy : PoolableComponent<Enemy>
{
    [SerializeField]
    Transform _target;
    [SerializeField]
    float _speed = 5;

    float _timer;

    public override void OnFetched(ObjectPool<Enemy> pool)
    {
        base.OnFetched(pool);
        _timer = 5;
    }

    private void FixedUpdate()
    {
        if (_timer <= 0)
        {
            ReturnToPool();
            _timer = 5;
        }
        else
            _timer -= Time.fixedDeltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);
    }
}
