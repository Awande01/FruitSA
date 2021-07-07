using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCVApp.Utility
{
    public class UtilityCheck
    {
        public static bool IsValidCode(string code)
        {
            if (code.Length > 6 || code.Length < 6)
                return false;
            var stringCharacter = code.Substring(0, 3);
            var stringNumeric = code.Substring(3, 3);
            if (stringCharacter.Any(x => char.IsDigit(x)) || stringNumeric.Any(x => char.IsLetter(x)))
                return false;
            return true;
        }
        
    }
}