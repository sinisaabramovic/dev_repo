using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

// main Player class
public class PlayerBot : Bot {

    [SerializeField] InventoryManager character;
    [SerializeField] GameObject inventoryObject;
    protected override void Awake () {
        Type = BotType.BotTypePlayer;
        rigidBody = GetComponent<Rigidbody2D>();
        //MoveSpeed = 0.2f;
        MoveDirection = Vector2.zero;
        Animator = GetComponentInChildren<Animator>();
        Analytics.CustomEvent("Game started", new Dictionary<string, object>
        {
            {"Player Type",Type}
        });
	}

    // Update is called once per frame
    protected override void Update () {  
        if(inventoryObject != null && !inventoryObject.activeSelf)
            base.Update();
	}

    protected override void Move()
    {
        base.Move();
        rigidBody.velocity = new Vector2(Mathf.Lerp(0, MoveDirection.x * MoveSpeed, LinerMoveInterpolation),
                                         Mathf.Lerp(0, MoveDirection.y * MoveSpeed, LinerMoveInterpolation));

        Analytics.CustomEvent("Player move", new Dictionary<string, object>
        {
            {"Direction", MoveDirection},
            {"Speed", MoveSpeed},
            {"LinerMoveInterpolation", LinerMoveInterpolation}
        });
    }

    protected override void BotControll()
    {
        base.BotControll();
    }

    protected override void Input_Keyboard()
    {
        base.Input_Keyboard();
        MoveDirection = Vector2.zero;
        bool move_left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool move_right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool move_up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool move_down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (move_left)
        {
            MoveDirection += Vector2.left;
            return;
        }
        if (move_right)
        {
            MoveDirection += Vector2.right;
            return;
        }
        if (move_up)
        {
            MoveDirection += Vector2.up;
            return;
        }
        if (move_down)
        {
            MoveDirection += Vector2.down;
            return;
        }

        Analytics.CustomEvent("Player move input", new Dictionary<string, object>
        {
            {"move_left", move_left},
            {"move_right", move_right},
            {"move_up", move_up},
            {"move_down", move_down}
        });
    }

    protected override void Animate(Vector2 direction)
    {
        base.Animate(direction);
        Animator.SetFloat("x_dir", direction.x);
        Animator.SetFloat("y_dir", direction.y);


    }

}
