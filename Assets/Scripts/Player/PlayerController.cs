using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private PlayerInput playerControlls;
	public float moveSpeed = 6;
	private Vector3 InputAmmount;
	private Rigidbody rb;
	private Vector3 velocity;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		InputAmmount = playerControlls.InputPlayerMoveSpeed(transform);
		animator.SetBool("walk", InputAmmount.magnitude > 0);
		velocity = moveSpeed * InputAmmount;
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
}