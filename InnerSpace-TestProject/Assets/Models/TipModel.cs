using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class TipModel
    {
        private List<string> _tips;
        public List<string> Tips { get { return _tips; } set { _tips = value; } }
    }
}