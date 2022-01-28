using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(PlayerSpriteRenderer))]

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
     
    public static int axeX = 0;
    public static int axeY = 1;

    [SerializeField] public float speed;
    [SerializeField] public float xMaxSpeed;                       // limitation de la vitesse en courant
    //public MaxSpeed xMaxSpeed;
    [SerializeField] public float yMaxSpeed;                       // limitation de la vitesse vertical pour limiter la hauteur de saut                              
    [SerializeField] public float jumpForce;
    

    public float jumpForceSave;
    public float speedSave;
    public float yMaxSpeedSave;

    //private PlayerAnimations animations;
    private new Rigidbody2D rigidbody2D;

    private IPlayerAnim animator;
    private PlayerSpriteRenderer spriteRenderer;

    public float direction;
    public bool canSwim;
    public bool canJump;

    public float GravityScale { set { rigidbody2D.gravityScale = value; }}

   void Awake()
    {                                                            // initialisation des composants
         rigidbody2D    = GetComponent<Rigidbody2D>();           // comportement physique de mon player
         animator       = GetComponent<IPlayerAnim>();           // lien sur etat/anim du sprite deu playere
         spriteRenderer = GetComponent<PlayerSpriteRenderer>();  // orientation du sprite player(flip droite/gauche)

         canJump = true;
         canSwim = false;


    }

    // callback pour les event
    public void OnMovePerformed(InputAction.CallbackContext obj)      // OnMovePerformed-> player non statique direction !=(different) 0
    {
        /* qd la touche < ou > est appuyée la direction devient non null                */
        /* si la  direction est plus grande que 0, le sprite en forme normal (pas FLip) */
        /* sinon on l'inverse (le sprite est Flippé)                                    */

        direction = obj.ReadValue<float>();
        spriteRenderer.Flip(PlayerSpriteRenderer.axeX, direction);
    }

    public void OnMoveCanceled(InputAction.CallbackContext obj)       // MoveOnCanceled->(touche de deplacement relaché, player devient static : direction =0
    {
         /* si il ne nage pas, il cours                                                */
         /* qd le player est à 0 on arrete l'animation running (running passe à false) */

         direction = 0;
         if (!canSwim)                                                                           // "!" = not                        
         {
            animator.SetRun(false);
         }
   }

    public void OnJumpPerformed(InputAction.CallbackContext obj)      // inputAction. Jump
    {
        /* si j'ai le droit de sauter                                                   */
        /*    j'ajoute l'impulsion de saut                                              */
        /*    je declenche l'anim jump                                                  */
        /*    je notifie que je suis en mode saut et que j'ai pas le droit de re-sauter */
        /* sinon si je suis en mode swim                                                */
        /*    je peux toujour donner une impulsion vers le haut                         */
        /*    j'ajoute l'impulsion de saut  
         *    */

        if (canJump)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);               // x = 0 , y = jumpForce _ ForcMode(type de force)            
            animator.SetJump(true);                                                  // passe en mode saut                                          
            canJump = false;                                                                    // empeche de re-sauter                                        
        }
        else if (canSwim)                                                                       // peux multiple jump seulement si on est dans l'eau
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void UpdateSpeed(int axe)
    {

      /* Selon l'axe X (horizontale) : 
      * recup de la vitesse du player : rigidbody2D.velocity.x (>0 a droite <0 a gauche)                          
      * on check que abs( rigidbody2D.velocity.x ) est inferieur a vitesse limite que l'on impose                  
      * si vitesse inferieur a limite                                                                              
      *     on ajoute a la vitesse                                                                                 
      * sinon on fait rien
      * Selon l'axe Y (vertical:
      *  On fait la meme chose pour la vitesse verticale afin de limiter le saut
      *  sinon on fait rien
      */


      if ((axe == axeX) && ( Mathf.Abs(rigidbody2D.velocity.x) < xMaxSpeed))
      {
         // je suis sur l'horizontale et je n'ai pas atteint la vitesse max alors j'accelere
         rigidbody2D.AddForce(new Vector2(speed * direction, 0));
      }
      else if ((axe == axeY) && (Mathf.Abs(rigidbody2D.velocity.y) < yMaxSpeed))
      {
         // je suis sur la verticale ( saut ou chute) et je n'ai pas atteint la vitesse max player accelere ou ralenti
         rigidbody2D.AddForce(new Vector2(0, speed));
      }

    }

}
