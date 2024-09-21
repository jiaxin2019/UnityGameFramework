using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform mapController;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private FloatingJoystick joystick;

    public float speed = 1f;

    private void Start()
    {
    }

    private void Update()
    {
        var position = mapController.position;
        Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        if (direction.x < 0)
        {
            playerTrans.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }
        else if (direction.x > 0)
        {
            playerTrans.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        position += -direction * speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -11, 11);
        position.z = Mathf.Clamp(position.z, -13, 10);
        mapController.position = position;
    }
}