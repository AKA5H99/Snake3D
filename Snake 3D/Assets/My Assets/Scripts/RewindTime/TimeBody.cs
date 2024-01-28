using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	public float recordTime = 2f;
	[HideInInspector]

	private GButtons GButtonsScript;

	List<PointInTime> pointsInTime;

	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();

		GButtonsScript = FindObjectOfType<GButtons>();
	}

	void FixedUpdate ()
	{
		if (GButtonsScript.isRewinding)
			Rewind();

		else if (GButtonsScript.CanNotRecrord) { }

		else
			Record();
	}

	void Rewind ()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		} else
		{
			GButtonsScript.isRewinding = false;
		}
		
	}

	void Record ()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}
}
