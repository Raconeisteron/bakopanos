using System.Windows.Forms;

namespace Bakopanos.Framework.Composite
{
    public static class WorkspaceExtensions
    {
        public static void ShowView<T>(this Control workspace, T control)
            where T : Control, IPart, IViewInfo
        {
            if (workspace.GetType() == typeof (Panel))
            {
                ShowView((Panel) workspace, control);
            }
            else if (workspace.GetType() == typeof (TabControl))
            {
                ShowView((TabControl) workspace, control);
            }
        }

        public static void ShowView<T>(this Panel workspace, T control)
            where T : Control, IPart, IViewInfo
        {
            control.Dock = DockStyle.Fill;

            workspace.Controls.Add(control);

            control.Run();
        }

        public static void ShowView<T>(this TabControl workspace, T control)
            where T : Control, IPart, IViewInfo
        {
            control.Dock = DockStyle.Fill;

            var page = new TabPage();
            page.Controls.Add(control);
            page.Text = control.Caption;

            workspace.TabPages.Add(page);

            control.Run();
        }
    }
}