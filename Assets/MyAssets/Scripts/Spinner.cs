using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    public float rotateSpeed = 20.0f;

	private void Start()
	{
		iTween.RotateBy(gameObject, iTween.Hash(
				"y", 360.0f,
				"looptype", iTween.LoopType.loop,
				"speed", rotateSpeed,
				"eassetype", iTween.EaseType.linear
			));
	}

}
