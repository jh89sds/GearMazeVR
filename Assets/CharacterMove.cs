using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class CharacterMove : MonoBehaviour {
    ForwardStatus forwardStatus = ForwardStatus.NONE;
    int moveCount = 0;
	float moveSize = 0.06f;

	[SerializeField] private VRInput m_VRInput;

    enum ForwardStatus
    {
        FRONT, BACK, RIGHT, LEFT, NONE
    }
		
	private void HandleClick(){
		forwardStatus = ForwardStatus.FRONT;
	}

	// Use this for initialization
	void Start () {
		m_VRInput.OnClick += HandleClick;
    }
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetUp(OVRInput.RawButton.DpadRight))
        {
            forwardStatus = ForwardStatus.FRONT;
        }

        if (OVRInput.GetUp(OVRInput.RawButton.DpadLeft))
        {
            forwardStatus = ForwardStatus.BACK;
        }

        if (OVRInput.GetUp(OVRInput.RawButton.DpadUp))
        {
            forwardStatus = ForwardStatus.RIGHT;
        }

        if (OVRInput.GetUp(OVRInput.RawButton.DpadDown))
        {
            forwardStatus = ForwardStatus.LEFT;
        }

        if(forwardStatus == ForwardStatus.FRONT)
        {
			transform.Translate(0, 0, moveSize);
            moveCount++;
			moveSize += 0.01f;
        }
        if (forwardStatus == ForwardStatus.BACK)
        {
			transform.Translate(0, 0, -0.25f);
            moveCount++;
        }
        if (forwardStatus == ForwardStatus.RIGHT)
        {
            transform.Translate(-0.25f, 0, 0);
            moveCount++;
        }
        if (forwardStatus == ForwardStatus.LEFT)
        {
            transform.Translate(0.25f, 0, 0);
            moveCount++;
        }

        if(moveCount >= 35)
        {
            statusDefault();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(forwardStatus == ForwardStatus.FRONT)
            transform.Translate(0, 0, -5.0f);
        if (forwardStatus == ForwardStatus.BACK)
            transform.Translate(0, 0, 5.0f);
        if (forwardStatus == ForwardStatus.RIGHT)
            transform.Translate(5.0f, 0, 0);
        if (forwardStatus == ForwardStatus.FRONT)
            transform.Translate(-5.0f, 0, 0);
    }

    void statusDefault()
    {
        forwardStatus = ForwardStatus.NONE;
        moveCount = 0;
		moveSize = 0.06f;
    }
}
