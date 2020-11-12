using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))] //данный компонент является обязательным
[AddComponentMenu("Control Script/FPS Input")] //Это строчка позволяет в юнити отображать скрипт FPS Input в выпадающем списке выбора скриптов
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;

    private CharacterController _charController;

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>(); //Доступ к другим компонентам, присоединенным к этому же объекту.
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed; 
        float deltaZ = Input.GetAxis("Vertical") * speed;
        //  transform.Translate(deltaX, 0, deltaZ);

        //Независимая от скорости работы компьютера скорость перемещений * Time.deltaTime
        //transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); //Ограничим движение по диагонали той же скоростью, что и движение параллельно осям.
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement); //Преобразуем вектор движения от локальных к глобальным координатам.
        _charController.Move(movement);  //Заставим этот вектор перемещать компонент CharacterController.

    }
}
