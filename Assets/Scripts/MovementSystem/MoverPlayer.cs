using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoverPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 200f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MoveForward();   
        
        if(Input.GetMouseButtonUp(0))
            Stop();
    }

    private void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void MoveForward()
    {
        _rigidbody.AddForce(transform.right * _speed, ForceMode.Force);
    }
}
