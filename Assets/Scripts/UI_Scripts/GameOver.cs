using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;



namespace UI_Scripts
{
    /// <summary>
    /// Makes the Game-Over screen functional via initializing buttons to respective
    /// actions (e.g. quit button quits game).
    /// </summary>
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

        /// <summary>
        /// Used to initialized game over's UI elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="element"></param>
        public void UIInit(PanelRenderer sender, VisualElement element)
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

        /// <summary>
        /// Used only to make the title of game-over menu changed and
        /// call method that initialized UI elements. 
        /// </summary>
        /// <remarks> Called only when the player won game.</remarks>
        private void onPlayerWon()
        {
            playerWon = true;
            panelRenderer.RegisterUIReloadCallback(UIInit);
        }
        
        /// <summary>
        /// Quits game's level and return to main menu.
        /// </summary>
        /// <param name="e"></param>
        public void Quit(ClickEvent e) => Application.Quit(); 
        

        /// <summary>
        /// Restarts game after player completes game.
        /// </summary>
        /// <param name="e"></param>
        public void Continue(ClickEvent e) => SceneManager.LoadScene("GameScene");
    }
    
}
