using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualPad : MonoBehaviour
{
    public float MaxLength = 70;         //�^�u�������ő勗��
    public bool is4DPad = false;         //�㉺���E�ɓ������t���O
    GameObject player;                   //���삷��v���C���[��GameObject
    Vector2 defPos;                      //�^�u�̏������W
    Vector2 downPos;                     //�^�b�`�ʒu

    void Start()
    {
        //�v���C���[�L�������擾
        player = GameObject.FindGameObjectWithTag("Player");
        //�^�u�̏������W
        defPos = GetComponent<RectTransform>().localPosition;
    }

    void Update()
    {
       
    }

    /// <summary>
    /// �_�E���C�x���g
    /// </summary>
    public void PadDown()
    {
        //�}�E�X�|�C���g�̃X�N���[�����W
        downPos = Input.mousePosition;
    }

    /// <summary>
    /// �h���b�O�C�x���g
    /// </summary>
    public void PadDrag()
    {
        //�}�E�X�|�C���g�̃X�N���[�����W
        Vector2 mousePosition = Input.mousePosition;
        //�V�����^�u�̈ʒu�����߂�
        Vector2 newTabPos = mousePosition - downPos;  //�}�E�X�_�E���ʒu����̈ړ�����
        if (is4DPad == false)
        {
            newTabPos.y = 0;   //���X�N���[���̏ꍇ��Y����0�ɂ���
        }
        //�ړ��x�N�g�����v�Z����
        Vector2 axis = newTabPos.normalized;    //�x�N�g���𐳎�������
        //2�_�̋��������߂�
        float len = Vector2.Distance(defPos, newTabPos);
        if (len > MaxLength)
        {
            //���E�����𒴂����̂Ō��E���W��ݒ肷��
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;
        }
        //�^�u���ړ�������
        GetComponent<RectTransform>().localPosition = newTabPos;
        //�v���C���[�L�������ړ�������
        PlayerController plcnt = player.GetComponent<PlayerController>();
        plcnt.SetAxis(axis.x, axis.y);
    }

    /// <summary>
    /// �A�b�v�C�x���g
    /// </summary>
    public void PedUp()
    {
        //�^�u�ʒu�̏�����
        GetComponent<RectTransform>().localPosition = defPos;
        //�v���C���[�L�������~������
        PlayerController plcnt = player.GetComponent<PlayerController>();
        plcnt.SetAxis(0, 0);
    }
}
