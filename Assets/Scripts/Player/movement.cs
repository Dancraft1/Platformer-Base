﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	public float jumpHeight;
	public float movementSpeed;

	bool isGrounded;
	void Update()
	{
		/*prevY = currY;
		currY = transform.position.y;
		yDelta = currY-prevY;*/
		if(Input.GetButtonDown("Jump") && isGrounded)
			StartCoroutine(Jump());
	}

	IEnumerator Jump()
	{
		yield return new WaitForFixedUpdate();
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
	}
	Ray2D[] rays = new Ray2D[2]; 
	void FixedUpdate()
	{

		GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, GetComponent<Rigidbody2D>().velocity.y);

		Collider2D col = GetComponent<Collider2D>();

		rays[0] = new Ray2D(new Vector2(col.bounds.min.x, transform.position.y), Vector2.down);
		rays[1] = new Ray2D(new Vector2(col.bounds.max.x, transform.position.y), Vector2.down);
		
		isGrounded = false;
		foreach(Ray2D ray in rays)
		{
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, col.bounds.extents.y+0.05f, 127);
			
			if(hit.collider != null)
			{
				isGrounded = true;
			}
		}
	}
	
}
