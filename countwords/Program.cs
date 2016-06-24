using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace countwords
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Get the file's text.
            string txt = File.ReadAllText(args[0]);

            // Use regular expressions to replace characters
            // that are not letters or numbers with spaces.
            Regex reg_exp_remove = new Regex("[^a-zA-Z0-9-]");
            txt = reg_exp_remove.Replace(txt, " ");

            // Split the text into words.
            string[] words = txt.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            // Use LINQ to get the unique words.
            var word_query =
                (from string word in words
                 orderby word
                 select word).Distinct();

            string exp="";
            // Filter out matches
            if (args.Count() > 1)
            {
                 for (int i = 1; i <= args.Count() -1; i++)
                {
                    if (exp.Length > 0) { exp += "|"; }
                    exp +=args[i] ;
                }
            }
            else
            {
                exp = ".";
            }
            Regex reg_exp_find = new Regex(exp);
            var matches_query = (from string match in word_query
                                 where reg_exp_find.IsMatch(match)
                                 select match
                                 );
            // Display the result.
            string[] result = matches_query.ToArray();
            Console.WriteLine(result.ToString());
            Console.WriteLine(result.Length + " words");
        }
    }
}