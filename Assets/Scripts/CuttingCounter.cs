using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgress
{
 public static event EventHandler OnAnyCut;
 new public static void ResetStaticData()
 {
  OnAnyCut = null;
 }
 public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

 public event EventHandler OnCut;
 [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

 private int cuttingProgress;
 public override void Interact(Player player)
 {
  if (!HasKitchenObject())
  {
   if (player.HasKitchenObject())
   {
    if (HasRecpieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
    {
     player.GetKitchenObject().SetKitchenObjectParent(this);
     cuttingProgress = 0;
     CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

     OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
     {
      progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
     });
    }
   }
   else
   {

   }
  }
  else
  {
   if (player.HasKitchenObject())
   {
    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
    {
     if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
     {
      GetKitchenObject().DestorySelf();
     }
    }
   }
   else
   {
    GetKitchenObject().SetKitchenObjectParent(player);
   }
  }
 }

 private bool HasRecpieWithInput(KitchenObjectSO inputKitchenObjectSO)
 {
  CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
  return cuttingRecipeSO != null;

 }

 public override void InteractAlternate(Player player)
 {
  if (HasKitchenObject() && HasRecpieWithInput(GetKitchenObject().GetKitchenObjectSO()))
  {
   cuttingProgress++;

   OnCut?.Invoke(this, EventArgs.Empty);
   OnAnyCut?.Invoke(this, EventArgs.Empty);

   CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

   OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
   {
    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
   });


   if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
   {
    KitchenObjectSO outputkitchenObjectSO =
    GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

    GetKitchenObject().DestorySelf();

    KitchenObject.SpawnKitchenObject(outputkitchenObjectSO, this);
   }
  }
 }

 private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
 {
  CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

  if (cuttingRecipeSO != null)
  {
   return cuttingRecipeSO.output;
  }
  else
  {
   return null;
  }
 }

 private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
 {
  foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
  {
   if (cuttingRecipeSO.input == inputKitchenObjectSO)
   {
    return cuttingRecipeSO;
   }
  }
  return null;
 }
}
