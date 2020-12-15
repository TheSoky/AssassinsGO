using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{
	private BoxCollider boxCollider;

	private void Awake()
	{
		boxCollider = GetComponent<BoxCollider>();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
		Gizmos.DrawCube(transform.position, new Vector3(1.0f, 1.0f, 1.0f));
	}

}
