using UnityEngine;                                                                                                                                              
using UnityEngine.InputSystem;                        //rajoute les inputS. (pakage manager)                                                                              
public class PlayerMgr : MonoBehaviour                                                
{

    /* declaration des composants unity gerant le comportement du player */                 // composants unity
    private Controls            controls;                                                        // gestionnaire de actions utilisées pour gerer les touches                                                 
    private IPlayerAnim         animator;
    private PlayerMovement      mvtCtrl;                                                          // gestion de transition des etats de l'animation    


   void Awake()
   {
      // initialisation des composants
        animator       = GetComponent<IPlayerAnim>();
        mvtCtrl        = GetComponent<PlayerMovement>();
    }

    private void OnEnable()                                                                            // Event au déclenchement du player 
    {
        /* initialisation input action ; abonnement aux evenements generer par le clavier */
        /* associations (binding)                                                         */

        controls = new Controls();                                                                                  

        controls.Main.Jump.performed   += mvtCtrl.OnJumpPerformed;                                                                                           
        controls.Main.MoveLR.performed += mvtCtrl.OnMovePerformed;
        controls.Main.MoveLR.canceled  += mvtCtrl.OnMoveCanceled;

        controls.Enable();
    }

   private void FixedUpdate()
   {
      /* event declenché a chaque changement de frame du sprite, 
       * on va s'en servir pour gerer la limite de vitesse 
       * on delegue a playerMovment object
      */

      mvtCtrl.UpdateSpeed(PlayerMovement.axeX);
      mvtCtrl.UpdateSpeed(PlayerMovement.axeY);
   }


   private void OnCollisionEnter2D(Collision2D col)                                             // Evenement déclancher à la collision
    {
      /* on peux de nouveau sauter quand on touche un collider     */
      /* On arrête l'anim "Jumping"                                */
      /* selon la direct° si elle est diff de 0 on cours donc anim */
      /* -> mode "Running"= true                                   */
      /* sinon                                                     */
      /* -> mode standing ,"Running" = False                       */

        mvtCtrl.canJump = true;                                                                                          
        animator.SetJump(false);
        animator.SetRun(mvtCtrl.direction != 0);
                                                                  
    }


    private void OnTriggerEnter2D(Collider2D obj)                                               // On a défini une zone ( Plouf_Square ) avec un trigger activée par l'entrée du player   
    {                                                                                           // ( Plouf_Square )est defini avec un tag="pool"
        /* Si son tag est "pool" on est sur le bon collider on va sauter et nager            */
        /*   je sauve mes valeurs pour pouvoir les reutiliser lors de a transitions inverses */

        if (obj.tag.Equals("pool"))                                                                                                                    
        {
            mvtCtrl.canJump = true;
            mvtCtrl.canSwim = true;

         // on sauve les constantes physiques hors de l'eau                                                                                         
            mvtCtrl.jumpForceSave = mvtCtrl.jumpForce;
            mvtCtrl.speedSave     = mvtCtrl.speed;
            mvtCtrl.yMaxSpeedSave = mvtCtrl.yMaxSpeed;

         // on modifie le comportement physique du rigidbody pour simuler la nage
            mvtCtrl.GravityScale = 30.0f;
            mvtCtrl.jumpForce = 150;
            mvtCtrl.speed = 40; 
            mvtCtrl.yMaxSpeed = 150;
        }

    }

    private void OnTriggerExit2D(Collider2D obj)                                                 // on sort de la zone trigger ( Plouf_Square )
    {
        /* on check le tag pour etre sur de sortir de l'eau */
        /* on rebascule sur les valeurs physiques hors de l'eau */

        if (obj.tag.Equals("pool"))                                                                                                                    
        {
           
            mvtCtrl.canJump = false;
            mvtCtrl.canSwim = false;

            mvtCtrl.GravityScale = 100.0f;
            mvtCtrl.jumpForce = mvtCtrl.jumpForceSave;
            mvtCtrl.speed     = mvtCtrl.speedSave;
            mvtCtrl.yMaxSpeed = mvtCtrl.yMaxSpeedSave;

        }
    }
}