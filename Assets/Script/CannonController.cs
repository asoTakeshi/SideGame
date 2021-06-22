using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;             //����������Prefab�f�[�^
    public float delayTime = 3.0f;�@�@�@�@�@ //�x������
    public float fireSpeedX = -4.0f;         //���˃x�N�g��X
    public float fireSpeedY = 0.0f;          //���˃x�N�g��Y
    public float length = 8.0f;    

    GameObject player;                      //Player
    GameObject gateObj;                     //���ˌ�
    float passedTimes = 0;                  //�o�ߎ���


    void Start()
    {
        //���ˌ��I�u�W�F�N�g�擾
        Transform tr = transform.Find("gate");
        gateObj = tr.gameObject;
        //�v���C���[
        player = GameObject.FindGameObjectWithTag("Player");
        
        
    }

    
    void Update()
    {
        //���ˎ��Ԕ���
        passedTimes += Time.deltaTime;
        //�����`�F�b�N
        if (CheckLength(player.transform.position))
        {
            if (passedTimes > delayTime)
            {
                //����
                passedTimes = 0;
                //���ˈʒu
                Vector3 pos = new Vector3(gateObj.transform.position.x,
                                          gateObj.transform.position.y,
                                          transform.position.z);
                //prefab����GameObject�����
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                //���˕���
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, fireSpeedY);
                rb.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    /// <summary>
    /// �����`�F�b�N
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
