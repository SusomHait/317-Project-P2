using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* This module contains the BuildPassword method, which is used
 * in the AddPasswordView page to generate passwords.
 * 
 * The functionality provided here is already complete.  DO NOT
 * edit this file!
 */

namespace CSC317PassManagerP2Starter.Modules
{
    public class PasswordGeneration
    {
        public static string BuildPassword(bool upper_letter, bool digit, string req_symbols, int min_length)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string opt_symbols = "!@#$%^&*()-+=[]{}|;:<>?/\\_";

            Random r = new Random();

            string pass = "";

            if (upper_letter)
            {
                int index = r.Next(0, alphabet.Length);
                pass += Char.ToUpper(alphabet[index]);
            }
            if (digit)
            {
                int index = r.Next(0, 10);
                pass += index.ToString();
            }
            if (req_symbols != null && req_symbols.Length > 0)
            {
                int index = r.Next(0, req_symbols.Length);
                pass += req_symbols[index];
            }

            int length = r.Next(min_length, 20);

            for (int i = 0; i < length; i++)
            {
                //(1 - 50): alpha, (51 - 80): digit, (81 - 100): symbol
                int type = r.Next(1, 101);

                if (type <= 50)
                {
                    int index = r.Next(0, alphabet.Length);
                    int upper = r.Next(0, 2);
                    if (upper == 1)
                        pass += Char.ToUpper(alphabet[index]);
                    else
                        pass += alphabet[index];
                }
                else if (type > 50 && type <= 80)
                {
                    pass += r.Next(0, 10).ToString();
                }
                else
                {
                    int index = r.Next(0, opt_symbols.Length);
                    pass += opt_symbols[index];
                }
            }

            pass = new string(pass.OrderBy(x => Guid.NewGuid()).ToArray());

            return pass;

        }
    }
}
