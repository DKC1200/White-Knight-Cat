using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProcessor : MonoBehaviour
{
    public PlayerInput playerInput;

    private InputAction walkInput;
    private InputAction jumpInput;
    private InputAction mouseLInput;

    [SerializeField] private string walkName;
    [SerializeField] private string jumpName;
    [SerializeField] private string mouseLName;

    private float walk;
    private float jump;
    private float mouseL;
    
    void Start()
    {
        walkInput = playerInput.actions[walkName];
        jumpInput = playerInput.actions[jumpName];
        mouseLInput = playerInput.actions[mouseLName];
    }

    // Update is called once per frame
    void Update()
    {
        walk = walkInput.ReadValue<float>();
        jump = jumpInput.ReadValue<float>();
        mouseL = mouseLInput.ReadValue<float>();
    }

    public float getWalk()
    {
        return walk;
    }

    public float getJump()
    {
        return jump;
    }

    public float getMouseL()
    {
        return mouseL;
    }
}
