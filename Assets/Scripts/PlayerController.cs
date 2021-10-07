using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 30f;  //Player 걷는 속도
    [SerializeField]            
    float jumpForce = 400f; //Player 점프 강도

    [SerializeField]
    Transform foot;     //Player 발 위치
    [SerializeField]
    float radius = .2f; //땅 체크를 위한 overlap circle의 radius
    [SerializeField]
    LayerMask ground;   //Player가 인식하는 땅의 LayerMask

    Rigidbody2D _rigid;
    float _move = 0f;
    bool _jump = false;
    bool _canjump = true;

    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            _jump = true;
    }

    void FixedUpdate()
    {
        
        Move();
        if (_jump && _canjump)
            Jump();
        _jump = false;
        _canjump = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(foot.position, radius, ground);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                _canjump = true;
        }
    }

    void Move()
    {
        transform.position += new Vector3(_move, 0, 0) * walkSpeed * Time.deltaTime;
    }

    void Jump()
    {
        _canjump = false;
        _rigid.AddForce(new Vector2(0f, jumpForce));
    }

}
