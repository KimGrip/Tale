using UnityEngine;
using System.Collections;
public enum PlayerState
{
    Idle, Running, Jumping, Climbing, Aiming, 
};
public class scr_playerController : MonoBehaviour {

    public float m_MovementSpeed;
    public float m_RotationSpeed;
    public bool m_Grounded;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
