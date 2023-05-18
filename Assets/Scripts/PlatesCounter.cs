using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{
 public event EventHandler OnPlateSpawned;
 public event EventHandler OnPlateRemoved;
 [SerializeField] private KitchenObjectSO plateKitchenSO;
 private float spawnPlateTimer;
 private float plateTimerMax = 4f;


 private int platesSpawnedAmount;
 private int platesSpawnedAmountMax = 4;

 private void Update()
 {
  spawnPlateTimer += Time.deltaTime;
  if (spawnPlateTimer > plateTimerMax)
  {
   spawnPlateTimer = 0;

   if (KitchenGameManger.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax)
   {
    platesSpawnedAmount++;

    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
   }
  }
 }

 public override void Interact(Player player)
 {
  if (!player.HasKitchenObject())
  {
   if (platesSpawnedAmount > 0)
   {
    platesSpawnedAmount--;

    KitchenObject.SpawnKitchenObject(plateKitchenSO, player);

    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
   }
  }
 }
}
