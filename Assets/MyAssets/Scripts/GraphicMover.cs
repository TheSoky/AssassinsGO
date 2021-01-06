using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GraphicMoverMode
{
    MoveTo,
    ScaleTo,
    MoveFrom
}
public class GraphicMover : MonoBehaviour
{

    public GraphicMoverMode mode;

    public Transform startXForm;
    public Transform endXForm;

    public float moveTime = 1.0f;
    public float delay = 0.0f;
    public iTween.LoopType loopType = iTween.LoopType.none;
    public iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

	private void Awake()
	{
		if(endXForm == null)
		{
            endXForm = new GameObject(gameObject.name + "XFormEnd").transform;

            endXForm.position = transform.position;
            endXForm.rotation = transform.rotation;
            endXForm.localScale = transform.localScale;
		}

        if(startXForm == null)
        {
            startXForm = new GameObject(gameObject.name + "XFormStart").transform;

            startXForm.position = transform.position;
            startXForm.rotation = transform.rotation;
            startXForm.localScale = transform.localScale;
        }
        Reset();
    }

	public void Reset()
	{
		switch(mode)
		{
			case GraphicMoverMode.MoveTo:
				if(startXForm != null)
				{
					transform.position = startXForm.position;
				}
				break;
			
            case GraphicMoverMode.ScaleTo:
				if(startXForm != null)
				{
					transform.localScale = startXForm.localScale;
				}
				break;
			
            case GraphicMoverMode.MoveFrom:
				if(startXForm != null)
				{
					transform.position = endXForm.position;
				}
				break;
			
            default:
				break;
		}
	}

    public void Move()
	{
		switch(mode)
		{
			case GraphicMoverMode.MoveTo:
				iTween.MoveTo(gameObject, iTween.Hash(
						"position", endXForm.position,
						"time", moveTime,
						"delay", delay,
						"easetype", easeType,
						"looptype", loopType
					));
				break;

			case GraphicMoverMode.ScaleTo:
				iTween.ScaleTo(gameObject, iTween.Hash(
						"scale", endXForm.localScale,
						"time", moveTime,
						"delay", delay,
						"easetype", easeType,
						"looptype", loopType
					));
				break;

			case GraphicMoverMode.MoveFrom:
				iTween.MoveFrom(gameObject, iTween.Hash(
						"position", startXForm.position,
						"time", moveTime,
						"delay", delay,
						"easetype", easeType,
						"looptype", loopType
					));
				break;

			default:
				break;
		}
	}

}
