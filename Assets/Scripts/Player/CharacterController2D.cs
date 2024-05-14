using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float m_JumpForce = 400f;									
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	
	[SerializeField] private bool m_AirControl = false;							
	[SerializeField] private LayerMask m_WhatIsGround;							
	[SerializeField] private Transform m_GroundCheck;							
	[SerializeField] private Transform m_CeilingCheck;	
	[SerializeField] private TrailRenderer tr;						
				

	const float k_GroundedRadius = .2f; 
	private bool m_Grounded;            
	const float k_CeilingRadius = .2f; 
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  
	private Vector3 m_Velocity = Vector3.zero;

	private bool doubleJump;

	
	public bool canDash = false;
	private bool isDashing;
	private float dashingPower = 300f;
	private float dashingTime = 0.25f;
	private float dashingCooldown = 2f;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	
	private void Start()
	{

	}
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
		{
			StartCoroutine(Dash());
		}
		
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		if (isDashing)
		{
			return;
		}
	}

	//JUMP, MOVE
	public void Move(float move, bool jump)
	{
		if (m_Grounded || m_AirControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			
			if (move > 0 && !m_FacingRight)
			{
				
				Flip();
			}
			
			else if (move < 0 && m_FacingRight)
			{
				
				Flip();
			}
		}
		//JUMP
		if (m_Grounded && jump)
    	{
        	m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f); 
        	m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			
    	}
		//DOUBLE JUMP
    	else if (!m_Grounded && jump && !doubleJump)
    	{
        	doubleJump = true;
        	m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f); 
        	m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			if (move > 0 && !m_FacingRight)
			{
				
				Flip();
				m_Rigidbody2D.AddForce(new Vector2(m_JumpForce * 1.5f, 0f));
			}
			
			else if (move < 0 && m_FacingRight)
			{
				
				Flip();
				m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce * 1.5f, 0f));
			}
			
    	}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
    	if (collision.gameObject.CompareTag("Ground"))
    	{
        	doubleJump = false;
    	}
	}

	

	//TURNING
	private void Flip()
	{
		
		m_FacingRight = !m_FacingRight;

		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//Dashing
	public IEnumerator Dash()
	{
		canDash = false;
		isDashing = true;
		float originalGravity = rb.gravityScale;
		rb.gravityScale = 0f;
		rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
		tr.emitting = true;
		yield return new WaitForSeconds(dashingTime);
		tr.emitting = false;
		rb.gravityScale = originalGravity;
		isDashing = false;
		yield return new WaitForSeconds(dashingCooldown);
		canDash = true; 
	}
}
