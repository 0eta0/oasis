using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour {
    [SerializeField]
    private GameObject[] enemies;

    private Camera _mainCamera;
    private int spawnNumMax;
    private int oldTime;

    private Vector3 getScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = _mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }


    // Use this for initialization
    void Start()
    {
        float position_x;
        float position_y;
        double object_range;
        double space;
        int flag;

        GameObject obj1 = GameObject.Find("Main Camera");
        _mainCamera = obj1.GetComponent<Camera>();


        // 壁、障害物プレハブをGameObject型で取得
        GameObject obj2 = (GameObject)Resources.Load("Wall_TB");
        GameObject obj3 = (GameObject)Resources.Load("Wall_S");
        GameObject obj4 = (GameObject)Resources.Load("Block");
        //GameObject obj5 = (GameObject)Resources.Load("Player");

        //プレハブをもとにインスタンス生成
        Instantiate(obj2, new Vector3(0.0f, getScreenTopLeft().y, 0.0f), Quaternion.identity);
        Instantiate(obj2, new Vector3(0.0f, getScreenBottomRight().y, 0.0f), Quaternion.identity);
        Instantiate(obj3, new Vector3(getScreenTopLeft().x, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(obj3, new Vector3(getScreenBottomRight().x, 0.0f, 0.0f), Quaternion.identity);

//        Debug.Log(getScreenTopLeft().x + " , " + getScreenTopLeft().y);
//        Debug.Log(getScreenBottomRight().x + " , " + getScreenBottomRight().y);

        int bairitsu = 3;
        object_range = getScreenBottomRight().x / bairitsu;  //障害物を生成させる範囲
        space = getScreenTopLeft().y / bairitsu; //壁に障害物を生成させないため

        flag = 1;
        for (double i = getScreenTopLeft().x + space; i <= getScreenBottomRight().x - space; i += object_range)
        {
            for (double j = getScreenBottomRight().y + space; j <= getScreenTopLeft().y - space; j += object_range)
            {
                position_x = Random.Range((float)i, (float)(i + object_range));
                position_y = Random.Range((float)j, (float)(j + object_range));
                if (flag == 1)
                {
                    Instantiate(obj4, new Vector3(position_x, position_y, 0.0f), Quaternion.identity);
                    flag = 0;
                }
                else
                {
                    Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(position_x, position_y, 0.0f), Quaternion.identity);
                    spawnNumMax++;
                    flag = 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {

        SpawnEnemies();

    }

    private void SpawnEnemies()
    {
        if (oldTime == (int)Timer.GetCurrentTime())
            return;

        if ((int)Timer.GetCurrentTime() == 40 || (int)Timer.GetCurrentTime() == 20)
        {
            oldTime = (int)Timer.GetCurrentTime();
            int spawnNumber = 0;
            if (GameObject.FindGameObjectWithTag("Enemy") != null)
                spawnNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;

            spawnNumber = spawnNumMax - spawnNumber;

            float position_x;
            float position_y;
            double object_range;
            double space;

            int bairitsu = 3;
            object_range = getScreenBottomRight().x / bairitsu;  //障害物を生成させる範囲
            space = getScreenTopLeft().y / bairitsu; //壁に障害物を生成させないため

            for (double i = getScreenTopLeft().x + space; i <= getScreenBottomRight().x - space; i += object_range)
            {
                for (double j = getScreenBottomRight().y + space; j <= getScreenTopLeft().y - space; j += object_range)
                {
                    position_x = Random.Range((float)i, (float)(i + object_range));
                    position_y = Random.Range((float)j, (float)(j + object_range));

                    Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(position_x, position_y, 0.0f), Quaternion.identity);
                }
            }
        }
    }
}
