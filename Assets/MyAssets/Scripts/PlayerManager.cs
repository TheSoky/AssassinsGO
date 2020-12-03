using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{

	public PlayerMover playerMover;
	public PlayerInput playerInput;

	private void Awake()
	{
		playerMover = GetComponent<PlayerMover>();
		playerInput = GetComponent<PlayerInput>();
		playerInput.InputEnabled = true;
	}

	private void Update()
    {
        if(playerMover.isMoving)
		{
			return;
		}

		playerInput.GetKeyInput();

		if(playerInput.V == 0.0f)
		{
			if(playerInput.H < 0.0f)
			{
				playerMover.MoveLeft();
			}
			else if (playerInput.H > 0.0f)
			{
				playerMover.MoveRight();
			}
		}
		else if(playerInput.H == 0.0f)
		{
			if(playerInput.V < 0.0f)
			{
				playerMover.MoveBackward();
			}
			else if(playerInput.V > 0.0f)
			{
				playerMover.MoveForward();
			}
		}

    }

}
