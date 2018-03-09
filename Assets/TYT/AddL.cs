using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddL : MonoBehaviour
{

    public Slider LiSlide;//力度条
    public GameObject GameoverText;//游戏结束
    public Text FenText;//得分UI显示Text
    public float li;//力度
    int Score = 0;//得分
    GameObject NextGameObject;//下一个要跳上的物体
    public GameObject[] PrefabsGameObjects;//所有要跳物体的类型数组
    GameObject PrefabsGameObject;//实例化物体的模块

  public  List<GameObject> GabGameObjects;//所有要跳上去的物体
    private int ind = 0;//要跳的物体个数参数

    private Vector3 CubPosition = new Vector3(0, 0, 0);//原点
    private bool firstdown = false;//是否第一次按下退出按键
    private float oldtime = 0f;
    void Awake()
    {
        GameoverText.SetActive(false);

        for (int i = 0; i < 100; i++)//生成100个要跳的块
        {
            PrefabsGameObject = PrefabsGameObjects[(int)Random.Range(0, 5)];

            CubPosition = new Vector3(CubPosition.x + Random.Range(-1f, 1f), CubPosition.y, CubPosition.z + Random.Range(2f, 3f));
            GameObject Cub = Instantiate(PrefabsGameObject, CubPosition, Quaternion.identity, GameObject.Find("GameObject").transform);
            GabGameObjects.Add(Cub);

        }
    }

    void Start()
    {

        NextGameObject = GabGameObjects[0];

    }

    void Update()
    {

        DoubleButtonQuit();//双击返回退出程序

        if (Input.GetMouseButton(0))
        {
            if (li < 500f)
            {
                li += 2f;

                LiSlide.value = li / 500f;
            }


        }
        if (Input.GetMouseButtonUp(0))
        {
            AddLi();

            ind += 1;
            NextGameObject = GabGameObjects[ind];

            Invoke("UpdateCamera", 1f);//更新相机位置
            li = 100f;
            LiSlide.value = 0.2f;

            // ind++;
        }
    }

    void UpdateCamera()//跳完相机移动
    {
        Camera.main.transform.position = gameObject.transform.position + new Vector3(0, 2f, -2f);
    }
    void AddLi()
    {
        Vector3 v3Vector3 = (NextGameObject.transform.position - gameObject.transform.position + new Vector3(0, 3f, 0)).normalized;
        gameObject.GetComponent<Rigidbody>().AddForce(v3Vector3 * li);
        Debug.Log("li+" + li);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plan")
        {


            GameoverText.SetActive(true);
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "1")
        {

            Score += 1;

            FenText.text = "得分:  " + Score.ToString();
        }
        if (collision.gameObject.tag == "2")
        {
            Score += 2;
            FenText.text = "得分:  " + Score.ToString();
        }
        if (collision.gameObject.tag == "3")
        {
            Score += 3;
            FenText.text = "得分:  " + Score.ToString();
        }
        if (collision.gameObject.tag == "4")
        {
            Score += 4;
            FenText.text = "得分:  " + Score.ToString();
        }
        if (collision.gameObject.tag == "5")
        {

            Score += 10;
            FenText.text = "得分:  " + Score.ToString();
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);

    }




    void DoubleButtonQuit()
    {



        //    nowTime = DateTime.Now;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            firstdown = true;


        }

        if (firstdown)
        {
            oldtime += 0.15f;
        }
        if (oldtime > 2f)
        {
            firstdown = false;
            oldtime = 0f;
        }


        if (oldtime > 0.2f)
        {
            if (Input.GetKeyDown(KeyCode.Escape) & firstdown)
            {
                Application.Quit();
                firstdown = false;
                oldtime = 0f;
            }
        }
    }
}
