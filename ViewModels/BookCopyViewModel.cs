using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_library.Models;

namespace School_library.ViewModels
{
    public class BookCopyViewModel : ViewModelBase
    {
        private BookCopy copy;

        public BookCopy BookCopy
        {
            get { return copy; }
        }

        public DateTime? DeliveredAt
        {
            get { return copy.DeliveredAt; }
            set
            {
                copy.DeliveredAt = value;
                OnPropertyChange("DeliveredAt");
            }
        }
        public bool Available 
        {
            get
            {
                if (copy.Available == 0)
                    return false;
                return true;
            }
            set
            {
                if (value == true)
                    copy.Available = 1;
                else
                    copy.Available = 0;
                OnPropertyChange("Available");
            }
        }

        public virtual Book Book
        {
            get { return copy.Book; }
            set
            {
                copy.Book = value;
                OnPropertyChange("Book");
            }
        }
        public virtual BookCondition Condition
        {
            get { return copy.Condition; }
            set
            {
                copy.Condition = value;
                OnPropertyChange("Condition");
            }
        }
        public virtual ICollection<Loan> Loans { get { return copy.Loans; } }


        public BookCopyViewModel(BookCopy copy)
        {
            this.copy = copy;
        }

        public override string ToString()
        {
            return copy.ToString();
        }
    }
}
