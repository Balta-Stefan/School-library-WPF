using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class ClearLoansFiltersCommand : ICommand
    {
        private LoansPanelViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.clearFilters();
        }

        public ClearLoansFiltersCommand(LoansPanelViewModel viewModel) => this.viewModel = viewModel;
    }
}
