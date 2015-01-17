using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TranslateJS.Core
{
    public static class JsMath
    {
        public static double floor(double value) { return Math.Floor(value); }
        public static double random() { return new Random().NextDouble(); }
    }
}
