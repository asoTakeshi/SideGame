using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;    //�V�[���̐؂�ւ��ɕK�v

public class ChangeScene : MonoBehaviour
{
    public string sceneName;  //�ǂݍ��ރV�[��
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// �V�[����ǂݍ���
    /// </summary>
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
