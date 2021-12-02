using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace School_library.Models
{
    public class LanguageAndFlag
    {
        public string language { get; set; }
        public string flagPath { get; set; }

        public LanguageAndFlag(string language, string flag)
        {
            this.language = language;
            this.flagPath = flag;
        }
    }
}
