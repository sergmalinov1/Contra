using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MyMouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public enum RotationAxes        //Объявляем структуру данных enum, которая будет сопоставлять имена с параметрами.
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 10.0f;
    public float sensitivityVert = 10.0f; // Объявляем переменные, задающие поворот в вертикальной плоскости.
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0; // Объявляем закрытую переменную для угла поворота по вертикали.

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) // Проверяем, существует ли этот компонент.
            body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)   // это поворот в горизонтальной плоскости 
        {
         //   transform.Rotate(0, sensitivityHor, 0);
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            //  Debug.Log($"333");
        }
        else if (axes == RotationAxes.MouseY)  // это поворот в вертикальной плоскости 
        {
            //Увеличиваем угол поворота по вертикали в соответствии с перемещениями указателя мыши.
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;


            //Фиксируем угол поворота по вертикали в диапазоне, заданном минимальным и максимальным значениями.
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);


            // Сохраняем одинаковый угол поворота вокруг оси Y(т.е. вращение в горизонтальной плоскости отсутствует).
            float rotationY = transform.localEulerAngles.y;


            //Создаем   новый вектор из сохраненных значений поворота
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else // это комбинированный поворот  Сюда поместим код для комбинированного вращения.
        {
             _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
             _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

             //Приращение угла поворота через значение delta
             float delta = Input.GetAxis("Mouse X") * sensitivityHor;

             //Значение delta — это величина изменения угла поворота.
             float rotationY = transform.localEulerAngles.y + delta; 

             transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
