using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    // Start is called before the first frame update
    private Actions Gameplay;
    private Actions.GameplayActions Input;

    private void OnEnable()
    {
        Gameplay.Enable();
    }

    private void OnDisable()
    {
        Gameplay.Disable();
    }

    void Awake()
    {
        Gameplay = new Actions();
        Input = Gameplay.Gameplay;

    }

    void Start()
    {
        Input.Press.started += ctx => PressScreen(ctx);
    }

    // Update is called once per frame

    private void PressScreen(InputAction.CallbackContext context)
    {
        Debug.Log("Press Context");
        Debug.Log("Position At" + Input.Position.ReadValue<Vector2>());
    }
}
