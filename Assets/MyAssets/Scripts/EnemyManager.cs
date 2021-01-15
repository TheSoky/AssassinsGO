using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyManager : TurnManager
{

	public UnityEvent deathEvent;

	private EnemyMover m_enemyMover;
	private EnemySensor m_enemySensor;
	private EnemyAttack m_enemyAttack;
	private Board m_board;

	private bool m_isDead = false;
	public bool IsDead { get { return m_isDead; } }

	protected override void Awake()
	{
		base.Awake();

		m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();

		m_enemyAttack = GetComponent<EnemyAttack>();
		m_enemyMover = GetComponent<EnemyMover>();
		m_enemySensor = GetComponent<EnemySensor>();
	}

	public void PlayTurn()
	{
		if(m_isDead)
		{
			FinishTurn();
			return;
		}
		
		StartCoroutine(PlayTurnRoutine());
	}

	public void Die()
	{
		if(m_isDead)
		{
			return;
		}

		m_isDead = true;
		if(deathEvent != null)
		{
			deathEvent.Invoke();
		}
	}

	private IEnumerator PlayTurnRoutine()
	{
		if(m_gameManager != null)
		{
			if(!m_gameManager.IsGameOver)
			{
				m_enemySensor.UpdateSensor(m_enemyMover.CurrentNode);

				yield return new WaitForSeconds(0.0f);

				if(m_enemySensor.FoundPlayer)
				{
					m_gameManager.LoseLevel();

					Vector3 playerPosition = new Vector3(m_board.PlayerNode.Coordinate.x, 0.0f, m_board.PlayerNode.Coordinate.y);
					m_enemyMover.Move(playerPosition, 0.0f);

					while(m_enemyMover.isMoving)
					{
						yield return null;
					}

					m_enemyAttack.Attack();

				}
				else
				{
					m_enemyMover.MoveOneTurn();
				}
			}
		}
	}

}
