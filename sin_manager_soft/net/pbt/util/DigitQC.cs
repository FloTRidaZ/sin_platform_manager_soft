using System;
using System.Text.RegularExpressions;

namespace sin_platform_soft_unit_tests.net.pbt.util
{
    public class DigitQC : IInputQC
    {
        public bool IsValid(string str)
        {
            if (str == null)
            {
                return false;
            }

            Regex regex = new Regex("^\\d+$");
            return regex.IsMatch(str);
        }
    }
}