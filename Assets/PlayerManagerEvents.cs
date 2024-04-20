using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerEvents : MonoBehaviour
{
   public int playersJoined = 0; 
   void OnPlayerJoined()
   {
      playersJoined++;
      if (playersJoined == 2)
      {
         SortPlayers();
      }
   }

   private void SortPlayers()
   {
      var players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
      players[0].Set(1);
      players[1].Set(2);
   }
}
