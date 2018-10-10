using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayCamera : MonoBehaviour {

    public Transform Mount = null;
    public float SpeedUpdate = 5.0f;

	void LateUpdate () 
    {
        Vector3 mountPos = new Vector3(Mount.position.x, Mount.position.y, -10.0f);
        transform.position = Vector3.Lerp(transform.position, mountPos, Time.deltaTime * SpeedUpdate);
	}
}
