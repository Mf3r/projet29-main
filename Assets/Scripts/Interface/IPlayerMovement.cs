using UnityEngine.InputSystem;

internal interface IPlayerMovement
{   //Interfaces concernant les InputAction

    void OnMovePerformed(InputAction.CallbackContext obj);
   void OnMoveCanceled(InputAction.CallbackContext obj);
   void OnJumpPerformed(InputAction.CallbackContext obj);

   void UpdateSpeed(int axe);

   float GravityScale { set; }
}