using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f;      //�����������m����
    public bool isDelete = false;    //������ɍ폜����t���O

    bool isFell = false;            //�����t���O
    float fadeTime = 0.5f;          //�t�F�[�h�A�E�g����
    
    void Start()
    {
        //Rigidbody2D�̋������~�߂�
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        
    }

    
    void Update()
    {
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");  //�v���C���[��T��
        if (player != null)
        {
            //�v���C���[�Ƃ̋������v��
            float d = Vector2.Distance(
                transform.position,player.transform.position);
            if(length >= d)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if(rb.bodyType == RigidbodyType2D.Static)
                {
                    // Rigidbody2D�̕����������J�n
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if (isFell)
        {
            //��������
            //�����x��ύX���ăt�F�[�h�A�E�g�����
            fadeTime -= Time.deltaTime;                      //�O�t���[���̍����b�}�C�i�X
            Color col = GetComponent<SpriteRenderer>().color;    //�J���[�����o��
            col.a = fadeTime;     //�����x��ύX
            GetComponent<SpriteRenderer>().color = col;      //�J���[���Đݒ�
            if (fadeTime <= 0.0f)
            {
                //0�ȉ�(����)�ɂȂ��������
                Destroy(gameObject);
            }


        }
    }
    /// <summary>
    /// �ڐG�J�n
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true;     //�����t���O�I��
        }
    }
}
