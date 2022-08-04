using UnityEngine;
using System.Collections.Generic;

public class Circle : MonoBehaviour
{
    public static bool _canMove = true;
    public static CollisionAction CollisionEvent;
    public delegate void CollisionAction(bool isWin);

    [SerializeField] private float _speed = 5.0f;

    private List<Vector3> _targets = new List<Vector3>();

    private void AddTarget()
    {
        var newTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newTarget.z = transform.position.z;
        _targets.Add(newTarget);
    }

    private void TryMove()
    {
        if (_targets.Count > 0 && _canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targets[0], _speed * Time.deltaTime);
            if (transform.position == _targets[0])
            {
                _targets.RemoveAt(0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var coin = collision.gameObject.GetComponent<Coin>();
        if (coin)
        {
            Destroy(collision.gameObject);
            CollisionEvent?.Invoke(true);
        }
        else if (collision.gameObject.GetComponent<Spike>())
        {
            CollisionEvent?.Invoke(false);
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AddTarget();
        }
        TryMove();
    }
}
