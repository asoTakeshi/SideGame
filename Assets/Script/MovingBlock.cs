using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;            //X�ړ�����
    public float moveY = 0.0f;            //Y�ړ�����
    public float times = 0.0f;            //����
    public float weight = 0.0f;            //��~����
    public bool isMoveWhenOn = false;      //��������ɓ����t���O
    public bool isCanMove = true;          //�����t���O
    float perDX;                           //1�t���[����X�ړ��l
    float perDY;                           //1�t���[����Y�ړ��l
    Vector3 defPos;                        //�����ʒu
    bool isReverse = false;                //���]�t���O

    void Start()
    {
        //�����ʒu
        defPos = transform.position;
        //1�t���[���̈ړ����Ԏ擾
        float timestep = Time.fixedDeltaTime;
        //1�t���[����X�ړ��l
        perDX = moveX / (1.0f / timestep * times);
        //1�t���[����X�ړ��l
        perDY = moveY / (1.0f / timestep * times);

        if (isMoveWhenOn)
        {
            //��������ɓ����̂ōŏ��͓������Ȃ�
            isCanMove = false;
        }


    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isCanMove)
        {
            //�ړ���
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if (isReverse)
            {
                //�t�����ړ���
                //�ړ��ʂ��v���X�ňړ��ʒu�������ʒu��菬����
                //�܂��́A�ړ��ʂŃ}�C�i�X�ňړ��ʒu�������ʒu���傫��
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    //�ړ��ʂ��{��
                    endX = true;   //X�����̈ړ��I��
                }
                if ((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true;   //Y�����̈ړ��I��
                }
                //�����ړ�������
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));
            }
            else
            {
                //�������ړ���
                //�ړ��ʂ��v���X�ňʒu�������{�ړ��������傫��
                //�܂��́A�ړ��ʂŃ}�C�i�X�ňʒu�������{�ړ�������菬����
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true;       //X�����̈ړ��I��
                }
                if ((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true;   //Y�����̈ړ��I��
                }
                //�����ړ�������
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
            }
            if (endX && endY)
            {
                //�ړ��I��
                if (isReverse)
                {
                    //�������ړ��ɖ߂�O�ɏ����ʒu�ɖ߂��A�������Ȃ��ƈʒu������邽��
                    transform.position = defPos;
                }
                isReverse = !isReverse;     //�t���O�𔽓]������
                isCanMove = false;          //�ړ��t���O�����낷
                if (isMoveWhenOn == false)
                {
                    //��������ɓ����t���OOFF
                    Invoke("Move", weight);    //�ړ��t���O�𗧂Ă�x�����s
                }
            }
        }
    }

    /// <summary>
    /// �ړ��t���O�𗧂Ă�
    /// </summary>
    public void Move()
    {
        isCanMove = true;
    }
    
    /// <summary>
    /// �ړ��t���O�����낷
    /// </summary>
    public void Stop()
    {
        isCanMove = false;
    }

    /// <summary>
    /// �ڐG�J�n
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //�ڐG�����̂��v���C���[�Ȃ�ړ����̎q�ɂ���
            col.transform.SetParent(transform);
            col.transform.localScale = Vector3.one;

            if (isMoveWhenOn)
            {
                //��������ɓ����t���OON
                isCanMove = true;      //�ړ��t���O�𗧂Ă�
            }
        }
    }


    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //�ڐG�����̂��v���C���[�Ȃ�ړ����̎q����O��
            col.transform.SetParent(null);
        }
    }

}
