using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    [SerializeField] private float _speedModifier;

    private Vector3 _playerInput;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerInput = new Vector3(Input.GetButtonDown("Horizontal"), 0, Input.GetButtonDown("Vertical"));
    }

    void FixedUpdate(){

    }
}
