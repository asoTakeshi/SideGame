using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     //UIを使う

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;       //画像を持つGameObject
    public Sprite gameOverSpr;         // GameOver画像
    public Sprite gameCleraSpr;        // GameClera画像
    public GameObject panel;           //　パネル
    public GameObject restartButton;   // RESTARTボタン
    public GameObject nextButton;      // ネクストボタン 

    Image titleImage;                  // 画像を表示しているImageコンポーネント

    
    void Start()
    {
        //画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        //ボタンを非表示にする
        panel.SetActive(false);
        
    }

    
    void Update()
    {
        if (PlayerController.gameStste == "gameclear")
        {
            //ゲームクリア
            mainImage.SetActive(true);         //画像を表示
            panel.SetActive(true);             //ボタンを表示
            //RESTARTボタンを無効にする
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameCleraSpr;     //画像を設定する
            PlayerController.gameStste = "gameend";
        }
        else if (PlayerController.gameStste == "gameover")
        {
            //ゲームオーバー
            mainImage.SetActive(true);         //画像を表示
            panel.SetActive(true);             //ボタンを表示
            //NEXTボタンを無効にする
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;     //画像を設定する
            PlayerController.gameStste = "gameend";
        }
        else if(PlayerController.gameStste == "playing")
        {
            //ゲーム中
        }
    }
    
    /// <summary>
    /// 画像を非表示にする
    /// </summary>
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
