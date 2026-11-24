using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;



namespace UI_Scripts
{
    public class GameOver : MonoBehaviour, IMenus
    {

        [Space(10)]
        [SerializeField] private PanelRenderer panelRenderer;
        
        private Button retryButton;
        private Button quitButton;
        private Label title;

        private bool playerWon;

        void Awake()
        {
            panelRenderer = GetComponent<PanelRenderer>();
        }
        
        void OnEnable()
        {
            panelRenderer.RegisterUIReloadCallback(UIInit);
            ManagerScripts.GameManager.OnGameWon += onPlayerWon;
        }

        void OnDisable()
        {
            panelRenderer.UnregisterUIReloadCallback(UIInit);

            if (playerWon)
            {
                playerWon = false;
            }
            
            ManagerScripts.GameManager.OnGameWon -= onPlayerWon;
        }

        private void UIInit(PanelRenderer sender, VisualElement element)
        {
            retryButton = element.Query<Button>("RetryButton");
            quitButton = element.Query<Button>("QuitButton");
            title = element.Query<Label>("Title");

            if (playerWon)
            {
                title.text = "You Won! Retry?";
            }
            
            retryButton.RegisterCallback<ClickEvent>(Continue);
            quitButton.RegisterCallback<ClickEvent>(Quit);
        }

        private void onPlayerWon()
        {
            playerWon = true;
            panelRenderer.RegisterUIReloadCallback(UIInit);
        }
        
        public void Quit(ClickEvent e) => Application.Quit(); 
        

        public void Continue(ClickEvent e) => SceneManager.LoadScene("GameScene");
    }
    
}
