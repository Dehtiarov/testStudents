using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    public class Mark
    {
        private short _ozinka;
        private string _discipline;

        public string Discipline
        {
            get { return _discipline; }
            set { _discipline = value; }
        }

        public short Ozinka
        {
            get { return _ozinka; }
            set
            {
                if (value <= 100 && value > 0)
                    _ozinka = value;
            }
        }

    }
}
