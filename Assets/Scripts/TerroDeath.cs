using UnityEngine;
using System.Collections;

public class TerroDeath : StateMachineBehaviour
{

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("Death", Random.Range(0, 3));
    }
}
