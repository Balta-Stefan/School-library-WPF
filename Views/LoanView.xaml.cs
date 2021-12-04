using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace School_library.Views
{
    /// <summary>
    /// Interaction logic for LoanView.xaml
    /// </summary>
    public partial class LoanView : UserControl
    {
        public LoanView()
        {
            InitializeComponent();
            this.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.F:
                    LoanTabFilterExpander.IsExpanded = !LoanTabFilterExpander.IsExpanded;
                    break;
            }
        }
    }
}
