using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToDestination : MonoBehaviour
{
  public Transform destination;

  private NavMeshAgent navMeshAgent;
  private Animator Animator;

  void Awake() 
  {
    navMeshAgent = GetComponent<NavMeshAgent>();
    Animator = GetComponent<Animator>();
    
    navMeshAgent.SetDestination(destination.position);
  }

  void Update() 
  {
    float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
    Animator.SetFloat("speed", speed);
  }
  
}
