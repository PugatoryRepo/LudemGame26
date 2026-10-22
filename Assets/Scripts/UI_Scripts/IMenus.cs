using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Scripts
{
    public interface IMenus
    {
        public abstract void Quit(ClickEvent e);

        public abstract void Continue(ClickEvent e);
    }
}
