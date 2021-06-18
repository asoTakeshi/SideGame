using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     //UI���g��

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;       //�摜������GameObject
    public Sprite gameOverSpr;         // GameOver�摜
    public Sprite gameCleraSpr;        // GameClera�摜
    public GameObject panel;           //�@�p�l��
    public GameObject restartButton;   // RESTART�{�^��
    public GameObject nextButton;      // �l�N�X�g�{�^�� 

    Image titleImage;                  // �摜��\�����Ă���Image�R���|�[�l���g

    
    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);
        //�{�^�����\���ɂ���
        panel.SetActive(false);
        
    }

    
    void Update()
    {
        if (PlayerController.gameStste == "gameclear")
        {
            //�Q�[���N���A
            mainImage.SetActive(true);         //�摜��\��
            panel.SetActive(true);             //�{�^����\��
            //RESTART�{�^���𖳌��ɂ���
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameCleraSpr;     //�摜��ݒ肷��
            PlayerController.gameStste = "gameend";
        }
        else if (PlayerController.gameStste == "gameover")
        {
            //�Q�[���I�[�o�[
            mainImage.SetActive(true);         //�摜��\��
            panel.SetActive(true);             //�{�^����\��
            //NEXT�{�^���𖳌��ɂ���
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;     //�摜��ݒ肷��
            PlayerController.gameStste = "gameend";
        }
        else if(PlayerController.gameStste == "playing")
        {
            //�Q�[����
        }
    }
    
    /// <summary>
    /// �摜���\���ɂ���
    /// </summary>
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
