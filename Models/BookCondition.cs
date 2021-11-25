using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class BookCondition
    {
        public int conditionID { get; private set; }
        public string condition { get; private set; }

        public BookCondition(int conditionID, string condition)
        {
            this.conditionID = conditionID;
            this.condition = condition;
        }

        public override bool Equals(object? obj)
        {
            return obj is BookCondition conditions &&
                   conditionID == conditions.conditionID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(conditionID);
        }
    }
}
