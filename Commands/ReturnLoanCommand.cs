using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class ReturnLoanCommand : ICommand
    {
        private LoansPanelViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            LoanViewModel tmp = (LoanViewModel)parameter;
            if (tmp.ReturnedToLibrarian != null)
                return false;
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter != null)
            {
                LoanViewModel tmp = (LoanViewModel)parameter;
                viewModel.returnLoan(tmp);
                if (tmp.ReturnedToLibrarian != null)
                    CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public ReturnLoanCommand(LoansPanelViewModel viewModel) => this.viewModel = viewModel;
    }
}
