using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerEvents : MonoBehaviour
{
   public int playersJoined = 0;
   public PlayerInputManager playerInputManager;
   private GameManager gameManager;
   private void Start()
   {
      playerInputManager = GetComponent<PlayerInputManager>();
      
       gameManager  = FindObjectOfType<GameManager>();
   }

   void OnPlayerJoined()
   {
      playersJoined++;
      DisableJoining();
      
      if (playersJoined >= 2) // When player two joins, and should a third or more players join (after death potentially), sort them
      {
         SortPlayers();
      }
   }

   public void EnableJoining()
   {
      playerInputManager.EnableJoining();
   }
   public void DisableJoining()
   {
      playerInputManager.DisableJoining();
   }
   private void SortPlayers()
   {
      var players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
      players[0].Set(0);
      players[1].Set(1);
      gameManager.PlayersJoined();
   }
}
