using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>(); // Доступ к другим компонентам, присоединенным к этому же объекту.
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Реакция на нажатие кнопки мыши.
        { 
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0); // Середина экрана — это половина его ширины и высоты.
            Ray ray = _camera.ScreenPointToRay(point); // Создание в этой точке луча методом ScreenPointToRay().
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Испущенный луч заполняет информацией  переменную, на которую имеется ссылка.Загружаем координаты точки, в которую попал луч.
                StartCoroutine(SphereIndicator(hit.point)); // Запуск сопрограммы в ответ на попадание.
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos) // Сопрограммы пользуются функциями IEnumerator.
    { 
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1); // Ключевое слово yield указывает сопрограмме,  когда следует остановиться.
        Destroy(sphere); // Удаляем этот GameObject и очищаем память.
    }
}
