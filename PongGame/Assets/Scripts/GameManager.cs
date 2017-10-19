using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //单例模式，GameManager只有一个实例
    private static GameManager _instance;
    public static  GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    //public static GameManager instance()
    //{
    //    get
    //    { }

    //    }
    //}

    private BoxCollider2D rightWall;
    private BoxCollider2D leftWall;
    private BoxCollider2D downWall;
    private BoxCollider2D upWall;

    private Transform Ball;

    public Transform leftPlayer;
    public Transform rightPlayer;

    private Button ResetButton;
    private int leftScore=0;
    private int rightScore=0;

    //要引入UI库才能使用Text类型
    private Text leftScoreText;
    private Text rightScoreText;
    private Text countText;
    private void Awake()
    {
        _instance = this;
    }
    void Start () {
        rightWall = gameObject.transform.Find("rightWall").GetComponent<BoxCollider2D>();
        leftWall = gameObject.transform.Find("leftWall").GetComponent<BoxCollider2D>();
        downWall = gameObject.transform.Find("downWall").GetComponent<BoxCollider2D>();
        upWall = gameObject.transform.Find("upWall").GetComponent<BoxCollider2D>();

        Ball = GameObject.Find("Ball").GetComponent<Transform>();
        ResetWall();
        ResetPlayer();

        leftScoreText = GameObject.Find("ScoreLeft").GetComponent<Text>();
        rightScoreText = GameObject.Find("ScoreRight").GetComponent<Text>();
        countText = GameObject.Find("CountNumber").GetComponent<Text>();
        countText.gameObject.SetActive(false);
    }
    void Update () {
		
	}
    void ResetWall()
    {
        //这里需要将屏幕坐标转换成世界坐标，屏幕坐标是以屏幕左下角的点为原点
        //Vector2 upWallPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height));
        //upWallPos += new Vector2(0, 0.5f);
        //upWall.transform.position = upWallPos;
        //float width = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x * 2;
        //这里控制的是BoxCollider的大小，所以修改size属性
        //upWall.size = new Vector2(width, 1);

        //简化代码
        Vector2 tempPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        upWall.transform.position = new Vector2(0, tempPos.y+0.5f);
        upWall.size = new Vector2(tempPos.x * 2, 1);
        downWall.transform.position = new Vector2(0, -(tempPos.y + 0.5f));
        downWall.size = new Vector2(tempPos.x * 2, 1);
        leftWall.transform.position = new Vector2(-(tempPos.x + 0.5f), 0);
        leftWall.size = new Vector2(1, tempPos.y*2);
        rightWall.transform.position = new Vector2(tempPos.x + 0.5f, 0);
        rightWall.size = new Vector2(1, tempPos.y * 2);
    }
    void ResetPlayer()
    {
        //由于摄像机的z轴坐标为-10，所以在使用ScreenToWorldPoint时会使得被赋值的二维物体的z轴也被赋值成-10，所以造成游戏物体被背景遮住
        leftPlayer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(40, Screen.height / 2, 10));
        rightPlayer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 40, Screen.height / 2, 10));
    } 
    public void ChangeScore(string wallName)
    {
        if (wallName == "rightWall")
        {
            leftScore++;

        }
        else if (wallName == "leftWall")
        {
            rightScore++;
        }
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }
    public void Reset()
    {
        leftScore = 0;
        rightScore = 0;
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();       
        countText.gameObject.SetActive(true);
        Ball.gameObject.SetActive(false);
        int countNumber = 3;
        StartCoroutine("CountDown", countNumber);
        //发送消息启动BallController中的Reset函数   

    }
    IEnumerator CountDown(int countNum)
    {
        while(countNum>-1)
        {
            yield return new WaitForSeconds(1);
            countNum--;
            countText.text = countNum.ToString();
            if(countNum==0)
            {
                countText.text = "GO!";                
            }
        }
        countNum = 3;
        countText.gameObject.SetActive(false);
        Ball.gameObject.SetActive(true);
        countText.text = countNum.ToString();
        GameObject.Find("Ball").SendMessage("Reset");
    }
}
