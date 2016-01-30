using UnityEngine;
using System.Collections;

public class TerroDeath : StateMachineBehaviour
{

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {

        animator.SetInteger("Death", Random.Range(0, 3));

        Debug.Log("enjmin anim machine");

    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("exit anim machine");

    }
}
