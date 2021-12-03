using School_library.Models;
using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace School_library.Commands
{
    public class OpenAddUserWindowCommand : ICommand
    {
        private MembersPanelViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.addMember();
        }

        public OpenAddUserWindowCommand(MembersPanelViewModel viewModel) 
        {
            this.viewModel = viewModel;
        }

    }
}
