using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Dummy : MonoBehaviour
{

    #region RequireComponents
		private Animator m_Animator;
		private CharacterController m_Controller;
	#endregion

	#region GroundCheck Variables

        [Header ("GroundCheck")] [Space(10)]  
		[SerializeField] private bool isGrounded;
		[SerializeField] private LayerMask groundMask;
		[SerializeField] private Transform groundCheck;
		private readonly float groundDistance = 0.4f;
		private readonly float gravity = 9.8f;
	#endregion

    private Vector3 velocity;
	public float jumpHeight = 3f;

    void Start() => Init();

	void Init()
	{
        // Attach CharacterController
		if (m_Controller == null) m_Controller = GetComponentInChildren<CharacterController>();
        // Attach Animator
		if (m_Animator == null) m_Animator = GetComponentInChildren<Animator>();
        // Attach Transform
		if (groundCheck == null) groundCheck = transform.Find("GroundCheck");
        // Attach LayerMask
		groundMask = LayerMask.GetMask("Ground");
	}

	void Update()
	{
		OnGroundCheck();
		JumpInput();
	}

	void OnGroundCheck()
	{
        Debug.DrawLine(start: transform.position,
                       end: Vector3.down * groundDistance,
                       color: Color.black);

		// Checking if a Character is on the Ground
		isGrounded = Physics.CheckSphere(
            position: groundCheck.position,
            radius: groundDistance,
            layerMask: groundMask);

        // Add DownForce when Character is under Ground Distance
        if (isGrounded && velocity.y < groundDistance) velocity.y = -2f;
        
        // Add DownForce when Character is not under Ground Distance
        velocity.y -= gravity * Time.deltaTime;
        
        // Add Character motion in Y Axis
		m_Controller.Move(motion: velocity * Time.deltaTime);

        // Animation For isGrounded
		m_Animator.SetBool(name: "Grounded", value: isGrounded);
	}

	public void JumpInput()
	{
        // Input for Jump
		if (Input.GetKeyDown(KeyCode.Space))
		{
            // Add UpForce when Character is on the Ground
			if (isGrounded) velocity.y = Mathf.Sqrt(f: jumpHeight * gravity);
		}
	}
}
