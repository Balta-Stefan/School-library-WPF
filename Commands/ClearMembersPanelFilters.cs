using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class ClearMembersPanelFilters : ICommand
    {
        private MembersPanelViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.clearFilters();
        }

        public ClearMembersPanelFilters(MembersPanelViewModel viewModel) => this.viewModel = viewModel;
    }
}
