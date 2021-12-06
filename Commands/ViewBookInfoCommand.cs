using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class ViewBookInfoCommand : ICommand
    {
        private BooksPanelViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (viewModel.SelectedBook == null)
                return false;
            else
                return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.OnBookSelect();
        }

        public void changeSelection()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public ViewBookInfoCommand(BooksPanelViewModel viewModel) => this.viewModel = viewModel;
    }
}
