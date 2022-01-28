using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class PlayerAnimations : MonoBehaviour, IPlayerAnim

    //animations
{
    private Animator animator;
    private PlayerSpriteRenderer spriteRenderer;

    private void Awake()
    {
      animator = GetComponent<Animator>();
      spriteRenderer = GetComponent<PlayerSpriteRenderer>();
    }

   public void SetStand(bool isStanding)                         //Standind
   {
      animator.SetBool("Standing", isStanding);
   }

   public void SetJump(bool isJumping)                           //Jumping
   {
      animator.SetBool("Jumping", isJumping);
   }

   public void SetRun(bool isRunning)                            //Running
   {
      animator.SetBool("Running", isRunning);
   }

    public void OnMovePerformed(InputAction.CallbackContext obj)      // OnMovePerformed-> player non statique direction !=(different) 0
    {
        /* qd la touche < ou > est appuyée la direction devient non null                */
        /* si la  direction est plus grande que 0, le sprite en forme normal (pas FLip) */
        /* sinon on l'inverse (le sprite est Flippé)                                    */

        var direction = obj.ReadValue<float>();
        spriteRenderer.Flip(PlayerSpriteRenderer.axeX, direction);
    }
}
