using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
 private const string NUMBER_POPUP = "NumberPopup";
 [SerializeField] private TextMeshProUGUI countDownText;
 private Animator animator;
 private int previousCountdownNumber;
 private void Awake()
 {
  animator = GetComponent<Animator>();
 }

 private void Start()
 {
  KitchenGameManger.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
  Hide();
 }

 private void Update()
 {
  int countDownNumber = Mathf.CeilToInt(KitchenGameManger.Instance.GetCountdownToStartTimer());
  countDownText.text = countDownNumber.ToString();

  if (previousCountdownNumber != countDownNumber)
  {
   previousCountdownNumber = countDownNumber;
   animator.SetTrigger(NUMBER_POPUP);
   SoundManager.Instance.PlayCountdownSound();
  }
 }

 private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
 {
  if (KitchenGameManger.Instance.IsCountDownToStartActive())
  {
   Show();
  }
  else
  {
   Hide();
  }
 }

 private void Show()
 {
  gameObject.SetActive(true);
 }
 private void Hide()
 {
  gameObject.SetActive(false);
 }
}
