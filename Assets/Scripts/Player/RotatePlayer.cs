using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    //настройка сеансы 
    public float SenX = 5, SensY = 10;
    //на сколько поворачивать камеру по X и по Y
    float moveY, moveX;
    //флаги движения по осям 
    public bool RootX = true, //разрешаем или запрещаем перемещение по оси X 
    RootY = true; //разрешаем или запрещаем перемещение по оси X
    public bool TestY = true,  //включаем ограничение поворота камеры вдоль оси Y
    TestX = false; //включение ограничения поворота камеры вдоль оси X
    public Vector2 MinMax_Y = new Vector2(-40, 40),    //ограничение по оси Y
    MinMax_X = new Vector2(-360, 360);  //ограничение по оси X
    Camera MyPawnBody; //контроллер игрока для вращения камерой


    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.tag.Equals("MainPlayer"))
        {
            MyPawnBody = GetComponent<Camera>();
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;

        if (angle > 360)
        angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
    void Update()
    {
        if (transform.parent.tag.Equals("MainPlayer"))
        {
            //получаем угол на который мышь улетела от центра экрана по Y
            if (RootY)  moveY -= Input.GetAxis("Mouse Y") * SensY;
            //ограничиваем угол поворота камеры чтобы она не вращалась под ноги 
            if(TestY)   moveY = ClampAngle(moveY, MinMax_Y.x, MinMax_Y.y); 
            //получаем угол на который мышь улетела от центра экрана по Х
            if (RootX)  moveX += Input.GetAxis("Mouse X") * SenX;
            //ограничиваем угол поворота камеры чтобы она не вращалась по оси X
            if (TestX)  moveX = ClampAngle(moveX, MinMax_X.x, MinMax_X.y); 
            //поворачиваем тело персонажа по осям 
            MyPawnBody.transform.rotation = Quaternion.Euler(moveY, moveX, 0);

            transform.parent.transform.rotation = Quaternion.Euler(0, moveX, 0);
        }
    }
}
