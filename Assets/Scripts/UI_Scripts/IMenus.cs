using UnityEngine.UIElements;

namespace UI_Scripts
{
    /// <summary>
    /// The interface for pause, gameover screen, etc.
    /// </summary>
    /// <remarks>
    /// IMenus scripts requires UI Toolkit (UIT) as noted the PanelRender
    /// parameter in <c>UIInit</c> method.
    /// </remarks>
    public interface IMenus
    {

        /// <summary>
        /// Abstract method that allows derived classes to initialize UIT buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="element"></param>
        public abstract void UIInit(PanelRenderer sender, VisualElement element);
        
        /// <summary>
        /// Abstract method to get rid of menu (depends on the derived class' implementation
        /// such as quitting application/game in <c>GameOver</c> method).
        /// </summary>
        /// <param name="e"></param>
        public abstract void Quit(ClickEvent e);

        /// <summary>
        /// Abstract method to "continue" game (depends on derived class' implementation).
        /// </summary>
        /// <param name="e"></param>
        public abstract void Continue(ClickEvent e);
    }
}
