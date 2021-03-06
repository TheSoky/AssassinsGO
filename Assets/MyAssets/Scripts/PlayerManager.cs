﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerDeath))]
public class PlayerManager : TurnManager
{

	public PlayerMover playerMover;
	public PlayerInput playerInput;
	public UnityEvent deathEvent;

	private Board m_board;

	protected override void Awake()
	{
		base.Awake();
		playerMover = GetComponent<PlayerMover>();
		playerInput = GetComponent<PlayerInput>();
		m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
		playerInput.InputEnabled = true;
	}

	private void Update()
    {
        if(playerMover.isMoving || m_gameManager.CurrentTurn != Turn.Player)
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

	public void Die()
	{
		if(deathEvent != null)
		{
			deathEvent.Invoke();
		}
	}

	public override void FinishTurn()
	{
		CaptureEnemies();
		base.FinishTurn();
	}

	private void CaptureEnemies()
	{
		if(m_board != null)
		{
			List<EnemyManager> enemies = m_board.FindEnemiesAt(m_board.PlayerNode);

			if(enemies.Count != 0)
			{

				foreach(EnemyManager enemy in enemies)
				{
					if(enemy != null)
					{
						enemy.Die();
					}
				}
			}
		}
	}

}
