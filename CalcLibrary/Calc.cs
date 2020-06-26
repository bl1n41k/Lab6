using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;


namespace CalcLibrary
{
    public static class Calc
    {
        public delegate T OperationDelegate<T>(T x, T y);
        public static string DoOperation(string s) // Возвращает ответ в виде строки
        {
            string[] operands;
            string operation;
            operands = GetOperands(s);
            operation = GetOperation(s);
            s = DoubleOperation[operation](double.Parse(operands[0]), double.Parse(operands[1])).ToString();
            return s;
        }

        public static string[] GetOperands(string s) // Получение операции из исходной строки 
        {
            Regex rgx = new Regex(@"(?<=\d)[\+\-\*\/]");
            MatchCollection mc = rgx.Matches(s);
            List<string> lm = new List<string>();
            foreach (Match m in mc)
            {
                lm.Add(m.Value);
            }
            return s.Split(lm.ToArray(), StringSplitOptions.None);
        }
        // Словарь операций
        public static Dictionary<string, OperationDelegate<double>> DoubleOperation =
            new Dictionary<string, OperationDelegate<double>>
        {
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y },
        };
        
        public static string GetOperation(string s)// Возвращает найденную операцию 
        {
            Regex rgx = new Regex(@"[^\d-\(\),]+");
            MatchCollection mc = rgx.Matches(s);
            List<string> lm = new List<string>();
            foreach (Match m in mc)
            {
                lm.Add(m.Value);
                s = m.Value;
            }
            return s;
        }
    }
}
