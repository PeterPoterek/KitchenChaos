using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
 [SerializeField] private Button resumeButton;
 [SerializeField] private Button mainMenuButton;
 [SerializeField] private Button optionsButton;

 private void Awake()
 {
  resumeButton.onClick.AddListener(() =>
  {
   KitchenGameManger.Instance.TogglePauseGame();
  });
  mainMenuButton.onClick.AddListener(() =>
  {
   Loader.Load(Loader.Scene.MainMenuScene);
  });
  optionsButton.onClick.AddListener(() =>
  {
   Hide();
   OptionsUI.Instance.Show(Show);
  });
 }
 private void Start()
 {
  KitchenGameManger.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
  KitchenGameManger.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

  Hide();
 }
 private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
 {
  Show();
 }
 private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
 {
  Hide();
 }
 public void Show()
 {
  gameObject.SetActive(true);

  resumeButton.Select();
 }

 public void Hide()
 {
  gameObject.SetActive(false);
 }
}
