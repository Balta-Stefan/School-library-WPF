﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_library.Models;


namespace School_library.ViewModels
{
    public class BookConditionViewModel : ViewModelBase
    {
        private BookCondition condition;

        public BookCondition BookCondition
        {
            get { return condition; }
        }
        public int ConditionId
        {
            get { return condition.ConditionId; }
        }
        public string Condition
        {
            get { return condition.Condition; }
            set
            {
                condition.Condition = value;
                OnPropertyChange("Condition");
            }
        }

        public  ICollection<BookCopy> BookCopies { get { return condition.BookCopies; } }

        public BookConditionViewModel(BookCondition condition)
        {
            this.condition = condition;
        }

        public override string ToString()
        {
            return condition.ToString();
        }
    }
}
