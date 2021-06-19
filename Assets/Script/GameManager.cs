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

    //+++���Ԑ����ǉ�+++
    public GameObject timeBar;        //���ԕ\���C���[�W
    public GameObject timeText;       //���ԃe�L�X�g
    TimeController timeCnt;           //TimeController

    Image titleImage;                  // �摜��\�����Ă���Image�R���|�[�l���g
    // +++ �X�R�A�ǉ� +++
    public GameObject scoreText;       �@//�X�R�A�e�L�X�g
    public static int totalScore;       //���v�X�R�A
    public int stageScore = 0;       �@//�X�e�[�W�X�R�A


    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);
        //�{�^�����\���ɂ���
        panel.SetActive(false);
        //TimeController
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if (timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);    //�������ԂȂ��Ȃ�B��
            }
        }
        UpdateScore();
        
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
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;      //���ԃJ�E���g��~
                //�����ɑ�����邱�Ƃŏ�����؂�̂Ă�
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;    //�c�莞�Ԃ��X�R�A�ɉ�����
            }
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore(); //�X�R�A�X�V

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
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;      //���ԃJ�E���g��~
            }

        }
        else if(PlayerController.gameStste == "playing")
        {
            //�Q�[����
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerContyoller���擾
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            //�^�C�����X�V����
            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0.0f)
                {
                    //�����ɑ�����邱�Ƃŏ�����؂�̂Ă�
                    int time = (int)timeCnt.displayTime;
                    //�^�C���X�V
                    timeText.GetComponent<Text>().text = time.ToString();
                    //�^�C���I�[�o�[
                    if (time == 0)
                    {
                        playerCnt.GameOver();       //�Q�[���I�[�o�[�ɂ���
                    }
                }    
            if (playerCnt.score != 0)
                {
                    stageScore += playerCnt.score;
                    playerCnt.score = 0;
                    UpdateScore();
                }   
            }
        }
    }
    
    /// <summary>
    /// �摜���\���ɂ���
    /// </summary>
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
