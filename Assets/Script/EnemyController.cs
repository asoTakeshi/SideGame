using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;             //�ړ����x
    public string directino = "left";      //����right or left
    public float range = 0.0f;             //�������͈�
    Vector3 defPos;                        //�����ʒu
    Rigidbody2D rb;
    void Start()
    {
        // ���x���X�V
        //Rigidbody���擾
        rb = GetComponent<Rigidbody2D>();
        if (directino == "right")
        {
            transform.localScale = new Vector2(-1, 1);  //�����̕ύX
        }
        //�����ʒu
        defPos = transform.position;

    }

    void Update()
    {
        if(range > 0.0f)
        {
            if(transform.position.x < defPos.x - (range / 2))
            {
                directino = "right";
                transform.localScale = new Vector2(-1, 1);  //�����̕ύX
            }
            if (transform.position.x > defPos.x + (range / 2))
            {
                directino = "left";
                transform.localScale = new Vector2(1, 1);  //�����̕ύX
            }
        }
    }
    private void FixedUpdate()
    {
        //���x���X�V
        //Rigidbody���擾
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (directino == "right")
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    /// <summary>
    /// �ڐG
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(directino == "right")
        {
            directino = "left";
            transform.localScale = new Vector2(1, 1);   //�����̕ύX
        }
        else
        {
            directino = "right";
            transform.localScale = new Vector2(-1, 1);   //�����̕ύX
        }
    }
}
