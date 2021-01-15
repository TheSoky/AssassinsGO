using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Mover
{
	private PlayerCompass m_playerCompass;

	protected override void Awake()
	{
		base.Awake();
		m_playerCompass = Object.FindObjectOfType<PlayerCompass>().GetComponent<PlayerCompass>();
	}

	protected override void Start()
	{
		base.Start();
		UpdateBoard();
	}

	protected override IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
	{
		if(m_playerCompass != null)
		{
			m_playerCompass.ShowArrows(false);
		}

		yield return StartCoroutine(base.MoveRoutine(destinationPos, delayTime));

		UpdateBoard();

		if(m_playerCompass != null)
		{
			m_playerCompass.ShowArrows(true);
		}

		base.finishMovementEvent.Invoke();
	}

	private void UpdateBoard()
	{
		if(m_board != null)
		{
			m_board.UpdatePlayerNode();
		}
	}

}
