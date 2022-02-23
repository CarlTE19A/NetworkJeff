using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

  [SerializeField]
  float initialVelocity = 6;

  [SerializeField]
  float lifeTime = 4;

	void Start ()
  {
    Rigidbody rigidbody = GetComponent<Rigidbody>();
    rigidbody.velocity = transform.forward * initialVelocity;

    Destroy(this.gameObject, lifeTime);
    Physics.IgnoreLayerCollision(6 ,6 , true);
	}
  void Update()
  {
  }
  void OnCollisionEnter(Collision collision)
  {
    Destroy(this.gameObject);
  }
}
