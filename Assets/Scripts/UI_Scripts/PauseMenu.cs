using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace UI_Scripts
{
    /// <summary>
    /// The Pause Menu when the player presses esc button.
    /// </summary>
    [RequireComponent(typeof(PanelRenderer))]
    public class PauseMenu: MonoBehaviour, IMenus
    {
        
        [Space(10)]
        [SerializeField] private PanelRenderer panelRenderer;
        
        private Button continueButton;
        private Button quitButton;
        private Button restartButton;

        void Awake()
        {
           panelRenderer ??= GetComponent<PanelRenderer>();

           if (panelRenderer.enabled)
           {
               panelRenderer.enabled = false;
           }
           
        }

        void OnEnable()
        {
            PlayerPause.OnPause += pauseGame;
        }

        void OnDisable()
        {
            PlayerPause.OnPause -= pauseGame;
        }
        
        /// <summary>
        /// Pauses all activity in-game and calls <c>UIInit</c> method to
        /// make the pause menu functional.
        /// </summary>
        private void pauseGame()
        {
            Time.timeScale = 0f;
            panelRenderer.enabled = true;
            
            panelRenderer.RegisterUIReloadCallback(UIInit);
        }


        /// <summary>
        /// Initializes the Pause Menu's buttons.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="element"></param>
        public void UIInit(PanelRenderer renderer, VisualElement element)
        {
            continueButton = element.Query<Button>("Continue");
            quitButton = element.Query<Button>("Quit");
            restartButton = element.Query<Button>("Restart");


            continueButton.RegisterCallback<ClickEvent>(Continue);
            quitButton.RegisterCallback<ClickEvent>(Quit);
            restartButton.RegisterCallback<ClickEvent>(restart);
        }
        
        /// <summary>
        /// Quit's application/game when player clicks quit button.
        /// </summary>
        /// <param name="e"></param>
        public void Quit(ClickEvent e)
        {
            Time.timeScale = 1f;
            Application.Quit();
        }
        
        /// <summary>
        /// Restarts current game level when player clicks restart button.
        /// </summary>
        /// <param name="e"></param>
        private void restart(ClickEvent e)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Resumes game when player presses the continue button.
        /// </summary>
        /// <param name="e"></param>
        public void Continue(ClickEvent e)
        {
            Time.timeScale = 1f;
            panelRenderer.enabled = false;
        }
    }
}
