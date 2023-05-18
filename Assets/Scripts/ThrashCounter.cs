using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrashCounter : BaseCounter
{
 public static event EventHandler OnAnyObjectTrashed;

 new public static void ResetStaticData()
 {
  OnAnyObjectTrashed = null;
 }
 public override void Interact(Player player)
 {
  if (player.HasKitchenObject())
  {
   player.GetKitchenObject().DestorySelf();

   OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
  }
 }
}