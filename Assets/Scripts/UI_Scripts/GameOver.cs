using System;
using ManagerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;


namespace UI_Scripts
{
    public class GameOver : MonoBehaviour, IMenus
    {

        [SerializeField] private PanelRenderer panelRenderer;
        
        private Button retryButton;
        private Button quitButton;
        private Label title;

        void Awake()
        {
            panelRenderer = GetComponent<PanelRenderer>();
        }
        
        void OnEnable()
        {
            panelRenderer.RegisterUIReloadCallback(UIInit);
        }

        void OnDisable()
        {
            panelRenderer.UnregisterUIReloadCallback(UIInit);
            
        }

        private void UIInit(PanelRenderer sender, VisualElement element)
        {
            retryButton = element.Query<Button>("RetryButton");
            quitButton = element.Query<Button>("QuitButton");
            title = element.Query<Label>("Title");
           
            
            retryButton.RegisterCallback<ClickEvent>(Continue);
            quitButton.RegisterCallback<ClickEvent>(Quit);
        }

        private void setTitle()
        {
            title.text = "You won! Retry again?";
        }
        
        public void Quit(ClickEvent e) => Application.Quit(); 
        

        public void Continue(ClickEvent e) => SceneManager.LoadScene("GameScene");
    }
    
}
