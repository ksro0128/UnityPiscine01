using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

	[SerializeField] private Transform[] points;
	[SerializeField] private float speed = 3f;
	[SerializeField] private int targetPoint = 0;
	[SerializeField] private float waitTime = 1f;
	private bool isWaiting = false;
	private Rigidbody rb;


	private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isWaiting)
        {
            if (Vector3.Distance(transform.position, points[targetPoint].position) > 0.1f)
            {
                Vector3 direction = (points[targetPoint].position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            }
            else
            {
                StartCoroutine(WaitAtPoint());
            }
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        targetPoint = (targetPoint + 1) % points.Length;
        isWaiting = false;
    }
}
