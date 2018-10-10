using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BotType 
{ 
    BotTypePlayer,
    BotTypeEnemy,
    BotTypeNeutral 
}

public enum InputControllType 
{
    InputControllType_Keyboard,
    InputControllType_Touch,
    InputControllType_Joystick,
    InputControllType_Mouse,
    InputControllType_AI
}

public abstract class Bot : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private BotType type;
    [SerializeField] private new string name = null;
    [SerializeField] private float runSpeed = 0.0f;
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private InputControllType inputType;
    [SerializeField] private Animator animator;
    [SerializeField] private float linerMoveInterpolation = 0.0f;

    protected Rigidbody2D rigidBody = null;

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public BotType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public float RunSpeed
    {
        get
        {
            return runSpeed;
        }

        set
        {
            runSpeed = value;
        }
    }

    public Vector2 MoveDirection
    {
        get
        {
            return moveDirection;
        }

        set
        {
            moveDirection = value;
        }
    }

    public InputControllType InputType
    {
        get
        {
            return inputType;
        }

        set
        {
            inputType = value;
        }
    }

    public Animator Animator
    {
        get
        {
            return animator;
        }

        set
        {
            animator = value;
        }
    }

    public float LinerMoveInterpolation
    {
        get
        {
            return linerMoveInterpolation;
        }

        set
        {
            linerMoveInterpolation = value;
        }
    }

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        BotControll();
        Move();
    }

    protected virtual void Move()
    {
        if(moveDirection.x != 0 || moveDirection.y != 0)
        {
            Animate(moveDirection);
        }
        else
        {
            animator.SetLayerWeight(1,0); 
        }

    }

    protected virtual void Animate(Vector2 direction)
    {
        animator.SetLayerWeight(1,1); 
    }

    protected virtual void BotControll()
    {
        switch (inputType)
        {
            case InputControllType.InputControllType_Keyboard:
                Input_Keyboard();
                break;
            case InputControllType.InputControllType_Mouse:
                input_Mouse();
                break;
            case InputControllType.InputControllType_Touch:
                input_Touch();
                break;

        }
    }

    protected virtual void Input_Keyboard()
    {
        
    }

    protected virtual void input_Mouse()
    {

    }

    protected virtual void input_Touch()
    {

    }

    protected virtual void input_GamePad()
    {

    }
}
