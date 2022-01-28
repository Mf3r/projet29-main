using System.Collections;
using System.Collections.Generic;
using UnityEngine;


internal class PlayerSpriteRenderer : MonoBehaviour, IPlayerSpriteRender
{
   public const int axeX = 0;
   public const int axeY = 1;


   private SpriteRenderer playerRenderer;

   void Awake()
   {
      playerRenderer = GetComponent<SpriteRenderer>();
   }

   public void Flip(int axe, float direction) 
   {

        //Flip concernant les Sprites
      if (axe == axeX)                                       //axe X
      {

         if (direction > 0)
         {
            playerRenderer.flipX = false;
         }
         else
         {
            playerRenderer.flipX = true;
         }
      }
      else if (axe == axeY)                                 //axe Y
      {
         if (direction > 0)
         {
            playerRenderer.flipY = false;
         }
         else
         {
            playerRenderer.flipY = true;
         }
      }
   }

}