using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class BooksPanel_AddNewBookCommand : ICommand
    {
        private BooksPanelViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.openAddBookWindow();
        }

        public BooksPanel_AddNewBookCommand(BooksPanelViewModel viewModel) => this.viewModel = viewModel;
    }
}
