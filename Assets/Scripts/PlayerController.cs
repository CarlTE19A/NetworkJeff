using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour {

  [SerializeField]
  float movementSpeed = 3f; // Unity-enheter per sekund

  [SerializeField]
  float rotationSpeed = 200f; // Grader per sekund

  [SerializeField]
  GameObject bulletPrefab;

  [SerializeField]
  Transform bulletSpawnPoint;

  [SerializeField]
  float timeBetweenShots = 0.5f;
  
  [SerializeField]
  int hp = 100;
  int bulletPerShot = 5;
  float timeSinceLastShot = 0f;

  void Start()
  {
    if(isLocalPlayer)
    {
      GetComponent<Renderer>().material.color = Color.magenta;
    }
    if(!isLocalPlayer)
    {
      GetComponent<Renderer>().material.color = Color.yellow;
    }

  }
	void Update () 
  {
    if(!isLocalPlayer)
    {
      return;
    }

    float yRotation = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
    float zMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

    Vector3 rotationVector = new Vector3(0, yRotation, 0);
    Vector3 movementVector = new Vector3(0, 0, zMovement);

    transform.Rotate(rotationVector);
    transform.Translate(movementVector);

    timeSinceLastShot += Time.deltaTime;

    if (Input.GetAxisRaw("Fire1") > 0)
    {
      if (timeSinceLastShot > timeBetweenShots)
      {
        Fire();
        timeSinceLastShot = 0;
      }
    }

	}

[Command]
  void Fire()
  {
    for (var i = 0; i < bulletPerShot; i++)
    {
      GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
      Vector3 tempVector = new Vector3(
        Random.Range(-10,10),
        Random.Range(-10,10),
        0
      ) + 
      bulletSpawnPoint.rotation.eulerAngles;
      bullet.transform.rotation = Quaternion.Euler(tempVector);
      NetworkServer.Spawn(bullet);
    }
  }
  void OnCollisionEnter(Collision collision)
    {
        hp -= 10;
        Debug.Log(hp);
        if(hp <= 0)
        {
          Destroy(gameObject);
        }
    }
}