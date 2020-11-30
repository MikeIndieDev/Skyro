using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movent")]
    [SerializeField]
    CharacterController _characterController;
    [SerializeField]
    float _speedMove;

    [Header("Rotation")]
    [SerializeField]
    Transform _camera;
    [SerializeField]
    float _turnSmooth;
    float _turSmoothVelocity;

    [Header("ChecGround")]
    [SerializeField]
    Transform _playerGround;
    [SerializeField]
    LayerMask _layerGound;
    bool _isGrounded;
    [SerializeField]
    float _distanceToGround;
    [SerializeField]
    float _gravity;
    Vector3 _velocityVertical;

    [Header("Jump")]
    [SerializeField]
    float _jumpHight;



    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_playerGround.position, _distanceToGround, _layerGound);

        if (_isGrounded)
        {
            _velocityVertical.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical") ;

        Vector3 direction = new Vector3(moveX,0f,moveZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float angleRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;//Calculamos el angulo con atan2 y lo convertimos a grados rad2deg ademas sumamos la rotacion de la camara, 
                                                                                                                //para desplazarnos donde se mueve la camara

            float angleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleRotation,  ref _turSmoothVelocity, _turnSmooth);//smoothDampAngle nos devuelve un movimiento suave, necesitamos crear una variable que haga referencia

            transform.rotation = Quaternion.Euler(0f, angleSmooth, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, angleRotation, 0f) * Vector3.forward;
            _characterController.Move(moveDirection.normalized * _speedMove * Time.deltaTime);
        }

        /******************************************* Salto***********************/
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocityVertical.y = Mathf.Sqrt(_jumpHight * -2 * _gravity);//Para calcular la fuerza de salto necesitamos la formula matematica de la raiz cuadrada de la altura *-2 * la gravedad
        }
        /**************************************SALTO*********************************/

        _velocityVertical.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocityVertical * Time.deltaTime);
         
        
    }
}
