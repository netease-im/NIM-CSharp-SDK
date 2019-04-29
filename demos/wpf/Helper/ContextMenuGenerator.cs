using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo
{
    delegate void MenuItemAction(params object[] args);

    class MenuItemData
    {
        public string Text { get; set; }

        public MenuItemAction ItemAction { get; set; }

        public object[] ActionArgs { get; set; }
    }

    class ContextMenuGenerator
    {
        private static readonly Dictionary<Control,List<MenuItemData>> _controlMenuRecord = new Dictionary<Control, List<MenuItemData>>();
        MenuItem CreateMenuItem(string text,MenuItemAction action,params object[] args)
        {
            MenuItem item = new MenuItem(text);
            item.Click += (sender, e) =>
            {
                if (action != null)
                    action(args);
            };
            return item;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            
        }

        public ContextMenu CreateContextMenu(List<MenuItemData> items)
        {
            ContextMenu menu = new ContextMenu();
            foreach (var item in items)
            {
                var menuItem = CreateMenuItem(item.Text, item.ItemAction,item.ActionArgs);
                menu.MenuItems.Add(menuItem);
            }
            return menu;
        }

        void AddContextMenu(Control control, List<MenuItemData> items)
        {
            if (!_controlMenuRecord.ContainsKey(control))
            {
                _controlMenuRecord.Add(control, items);
                control.MouseUp += OnControlOnMouseUp;
                control.HandleDestroyed += Control_HandleDestroyed;
            }
        }

        private void Control_HandleDestroyed(object sender, EventArgs e)
        {
            var control = sender as Control;
            _controlMenuRecord.Remove(control);
            control.MouseUp -= OnControlOnMouseUp;
            control.HandleDestroyed -= Control_HandleDestroyed;
        }

        private void OnControlOnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
               
            }
        }

        void Test()
        {
            List<MenuItemData> datas = new List<MenuItemData>
            {
                new MenuItemData { Text = "", ItemAction = null, ActionArgs = null },
                new MenuItemData { Text = "", ItemAction = null, ActionArgs = null },
                new MenuItemData { Text = "", ItemAction = null, ActionArgs = null },
                new MenuItemData { Text = "", ItemAction = null, ActionArgs = null },
                new MenuItemData { Text = "", ItemAction = null, ActionArgs = null },
                new MenuItemData { Text = "", ItemAction = null, ActionArgs = null },
            };
        }
    }
}
