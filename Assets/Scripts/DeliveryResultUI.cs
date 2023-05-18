using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryResultUI : MonoBehaviour
{
 private const string POPUP = "Popup";

 [SerializeField] private Image backgroundImage;
 [SerializeField] private Image iconImage;
 [SerializeField] private TextMeshProUGUI messageText;

 [SerializeField] private Color sucessColor;
 [SerializeField] private Color failedColor;

 [SerializeField] Sprite sucessSprite;
 [SerializeField] Sprite failedSprite;

 private Animator animator;
 private void Awake()
 {
  animator = GetComponent<Animator>();
 }

 private void Start()
 {
  DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeSuccess;
  DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

  gameObject.SetActive(false);
 }

 private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
 {
  gameObject.SetActive(true);
  animator.SetTrigger(POPUP);
  backgroundImage.color = sucessColor;
  iconImage.sprite = sucessSprite;
  messageText.text = "DELIVERY\nSUCCESS";
 }
 private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
 {
  gameObject.SetActive(true);
  animator.SetTrigger(POPUP);
  backgroundImage.color = failedColor;
  iconImage.sprite = failedSprite;
  messageText.text = "DELIVERY\nFAILED";

 }
}
