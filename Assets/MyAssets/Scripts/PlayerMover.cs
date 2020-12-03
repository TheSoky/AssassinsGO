using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

	public Vector3 destination;
	public bool isMoving = false;
	public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

	public float moveSpeed = 1.5f;
	public float iTweenDelay = 0.0f;

	private void Start()
	{

	}



	public void MoveLeft()
	{
		Vector3 newPosition = transform.position + new Vector3(-2.0f, 0.0f, 0.0f);
		Move(newPosition, 0.0f);
	}

	public void MoveRight()
	{
		Vector3 newPosition = transform.position + new Vector3(2.0f, 0.0f, 0.0f);
		Move(newPosition, 0.0f);
	}

	public void MoveForward()
	{
		Vector3 newPosition = transform.position + new Vector3(0.0f, 0.0f, 2.0f);
		Move(newPosition, 0.0f);
	}

	public void MoveBackward()
	{
		Vector3 newPosition = transform.position + new Vector3(0.0f, 0.0f, -2.0f);
		Move(newPosition, 0.0f);
	}

	public void Move(Vector3 destinationPos, float delayTime = 0.25f)
	{
		StartCoroutine(MoveRoutine(destinationPos, delayTime));
	}

	private IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
	{
		isMoving = true;
		destination = destinationPos;
		yield return new WaitForSeconds(delayTime);

		iTween.MoveTo(gameObject, iTween.Hash(
				"x", destinationPos.x,
				"y", destinationPos.y,
				"z", destinationPos.z,
				"delay", iTweenDelay,
				"easetype", easeType,
				"speed", moveSpeed
			));

		while(Vector3.Distance(destinationPos, transform.position) > 0.01f)
		{
			yield return null;
		}

		iTween.Stop(gameObject);
		transform.position = destinationPos;
		isMoving = false;
	}



}
