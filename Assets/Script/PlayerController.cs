using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;                  //Rigidbody2D変数
    float axisH = 0.0f;              //入力
    public float speed = 3.0f;      // 移動速度  
    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地出来るレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ

    //アニメーション対応
    Animator animator;     //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";
    public static string gameStste = "playing";   //ゲームの状態
    public int score = 0;            // スコア
    bool isMoving = false;           //タッチスクリーン対応追加

    void Start()
    {
        //Rigidbody2D取得
        rb = GetComponent<Rigidbody2D>();
        //Animatorを取得
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        gameStste = "playing";   //ゲーム中にする
    }

    void Update()
    {
        if (gameStste != "playing")
        {
            return;
        };
        //移動
        if (isMoving == false)
        {
            //水平方向の入力をチェックする
            axisH = Input.GetAxisRaw("Horizontal");
        }
        //向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1);     //左右反転
        }
        //キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();  //ジャンプ
        }

    }

    private void FixedUpdate()
    {
        if (gameStste != "playing")
        {
            return;
        }
        //地上判定
        onGround = Physics2D.Linecast(transform.position,
                                      transform.position - (transform.up * 0.1f),
                                      groundLayer);
        if (onGround || axisH != 0)
        {
            //地面の上or速度が０でない
            //速度を変更する
            rb.velocity = new Vector2(speed * axisH, rb.velocity.y);
        }
        if (onGround && goJump)
        {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            Debug.Log("ジャンプ！");
            Vector2 jumpPw = new Vector2(0, jump);    //ジャンプさせるベクトルを作る
            rb.AddForce(jumpPw, ForceMode2D.Impulse);   //瞬間的な力を加える
            goJump = false;                           //ジャンプフラグを下ろす
        }
        if (onGround)
        {
            //地面の上
            if (axisH == 0)
            {
                nowAnime = stopAnime;    //停止中
            }
            else
            {
                nowAnime = moveAnime;    //移動
            }
        }
        else
        {
            //空中
            nowAnime = jumpAnime;
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);      //アニメーション再生
        }
    }
    /// <summary>
    /// ジャンプ
    /// </summary>
    public void Jump()
    {
        goJump = true;      //ジャンプフラグを立てる
        Debug.Log("ジャンプボタンを押した");
    }

    /// <summary>
    /// 接触
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Goal")
        {
            Goal();   //ゴール
        }
        else if(col.gameObject.tag == "Dead")
        {
            GameOver();    //ゲームオーバー
        }
        else if (col.gameObject.tag == "ScoreItem")
        {
            //スコアアイテム
            //ItenDataを取得
            ItemData item = col.gameObject.GetComponent<ItemData>();
            //スコアを取得
            score = item.value;

            //アイテム削除
            Destroy(col.gameObject);
        }
    }

    /// <summary>
    /// ゴール
    /// </summary>
    public void Goal()
    {
        Debug.Log("ゴール");
        animator.Play(goalAnime);
        gameStste = "gameclear";
        GameStop();       //ゲーム停止
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        animator.Play(deadAnime);
        gameStste = "gameover";
        GameStop();       //ゲーム停止
        //===================
        //ゲームオーバー演出
        //===================
        //プレイヤー当たりを消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        //プレイヤーを上に少し跳ね上げる演出
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }
    /// <summary>
    /// ゲーム停止
    /// </summary>

    void GameStop()
    {
        //Rigidbody2D取得
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //速度を0にして強制停止
        rb.velocity = new Vector2(0, 0);
    }

    /// <summary>
    /// タッチスクリーン対応追加
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void SetAxis(float h ,float v)
    {
        axisH = h;
        if(axisH == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
