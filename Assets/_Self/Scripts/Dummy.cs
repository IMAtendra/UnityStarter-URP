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
	public float walkSpeed = 3f;
	public float sprintSpeed = 5f;
	private float _currentVelY;
	private readonly float transitionTime = 0.12f;

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
		Movement();
	}

	void OnGroundCheck()
	{
		// Show Distance from Ground
        Debug.DrawLine(start: transform.position,
                       end: transform.position + Vector3.down * groundDistance,
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
        
        // Add Character motion on Y Axis
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

	public void Movement()
	{
		// Inputs for Movement
		var inputX = Input.GetAxis("Horizontal");
		var inputZ = Input.GetAxis("Vertical");
		var moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
		var direction = new Vector3(inputX, 0, inputZ);

		// for Direction Rotation
		var targetAngle = Mathf.Atan2(y: direction.x, x: direction.z) * Mathf.Rad2Deg;
		var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelY, transitionTime);
		transform.rotation = Quaternion.Euler(x: 0, y: angle, z: 0);

		// Add Character motion on XZ Axis
		m_Controller.Move(motion: moveSpeed * Time.deltaTime * direction);

        // Add Animations For Movement
		m_Animator.SetFloat(name: "InputX", value: inputX);
		m_Animator.SetFloat(name: "InputZ", value: inputZ);
	}

	void OnTriggerEnter(Collider other)
	{
		ICollectible collectible = other.GetComponent<ICollectible>();
		if (other.gameObject.tag.Equals("Coin") || collectible != null)
		{
			collectible.Collect();
		}
	}

}
