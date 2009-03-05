using System.Collections.Generic;
using System.Windows.Forms;
using Bakopanos.Framework.Composite;
using Microsoft.Practices.Unity;

namespace Bakopanos.WinClient.ProductsModule.Views
{
    public partial class TestView : UserControl, IView<ITestPresenter>
    {
        private readonly Dictionary<string, Control> _Placeholders =
            new Dictionary<string, Control>();

        private ITestPresenter _presenter;

        public TestView()
        {
            InitializeComponent();
        }

        #region IView<ITestPresenter> Members

        [Dependency]
        public ITestPresenter Presenter
        {
            get { return _presenter; }
            set { _presenter = value; }
        }


        public void Run()
        {
            _presenter.Run();
        }

        public string Caption { get; set; }

        public Dictionary<string, Control> Placeholders
        {
            get { return _Placeholders; }
        }

        #endregion
    }
}