using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
	Stationary,
	Patrol,
	Spinner
}

public class EnemyMover : Mover
{
	public Vector3 directionToMove = new Vector3(0.0f, 0.0f, Board.spacing);

	public MovementType movementType = MovementType.Stationary;


	public float standTime = 1.0f;

	protected override void Awake()
	{
		base.Awake();
		faceDestination = true;
	}

	protected override void Start()
	{
		base.Start();
	}

	public void MoveOneTurn()
	{
		switch(movementType)
		{
			case MovementType.Stationary:
				Stand();
				break;

			case MovementType.Patrol:
				Patrol();
				break;

			case MovementType.Spinner:
				Spin();
				break;

		}

	}

	private void Stand()
	{
		StartCoroutine(StandRoutine());
	}

	private IEnumerator StandRoutine()
	{
		yield return new WaitForSeconds(standTime);
		base.finishMovementEvent.Invoke();
	}

	private void Patrol()
	{
		StartCoroutine(PatrolRoutine());
	}

	private IEnumerator PatrolRoutine()
	{
		Vector3 startPos = new Vector3(m_currentNode.Coordinate.x, 0.0f, m_currentNode.Coordinate.y);

		Vector3 newDest = startPos + transform.TransformVector(directionToMove);

		Vector3 nextDest = startPos + transform.TransformVector(directionToMove * 2.0f);

		Move(newDest, 0.0f);

		while(isMoving)
		{
			yield return null;
		}

		if(m_board != null)
		{
			Node newDestNode = m_board.FindNodeAt(newDest);
			Node nextDestNode = m_board.FindNodeAt(nextDest);

			if(nextDestNode == null || !newDestNode.LinkedNodes.Contains(nextDestNode))
			{
				destination = startPos;
				FaceDestination();

				yield return new WaitForSeconds(rotateTime);
			}
		}

		base.finishMovementEvent.Invoke();
	}

	private void Spin()
	{
		StartCoroutine(SpinRoutine());
	}

	private IEnumerator SpinRoutine()
	{
		Vector3 localForward = new Vector3(0.0f, 0.0f, Board.spacing);
		destination = transform.TransformVector(localForward * -1.0f) + transform.position;

		FaceDestination();

		yield return new WaitForSeconds(rotateTime);

		base.finishMovementEvent.Invoke();
	}

}
