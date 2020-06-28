using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net.Http;

namespace CalcLibrary
{
    public static class Calc
    {
        public delegate T SingleOperationDelegate<T>(T x);
        public delegate T DoubleOperationDelegate<T>(T x, T y);
        public static string DoOperation(string s) // Возвращает ответ в виде строки
        {
            try
            {
                string[] operands = GetOperands(s);
                string operation = GetOperation(s)[0];
                string answer = "";
                if (operation == "reverse" || operation == "Sqrt" || operation == "division" || operation == "expX" ||
                operation == "sin" || operation == "Cos" || operation == "tg" ||operation == "Sqr" || operation == "n!")
                    answer = SingleOperation[operation](double.Parse(operands[0])).ToString();
                else if (operation == "π" || operation == "e")
                    answer = Print(s);
                else
                    answer = DoubleOperation[operation](double.Parse(operands[0]), double.Parse(operands[1])).ToString();
                return answer;
            }
            catch
            {
                string answer = "Ошибка";
                return answer;
            }
        }

        static double Factorial(double x)
        {
            if (x == 0) return 1; 
            else return x * Factorial(x - 1);  
        }
        public static string Print(string s)
        {
            string answer = "";
            if (s == "π") answer = Math.PI.ToString();
            if (s == "e") answer = Math.E.ToString();
            return answer;
        }
        public static string[] GetOperands(string s)
        {
            Regex rgx = new Regex(@"(-\d+[.,]?\d*)|(\d*[.,]?\d*)");
            MatchCollection mc = rgx.Matches(s);
            List<string> matches = new List<string>();
            foreach (Match m in mc)
            {
                if (m.Value.Length > 0)
                    matches.Add(m.Value);
            }
            return matches.ToArray();
        }
        // Словарь операций с одним операндом
        public static Dictionary<string, SingleOperationDelegate<double>> SingleOperation =
            new Dictionary<string, SingleOperationDelegate<double>>
            {
                { "reverse", (x) => -x  },
                { "sin", (x) => Math.Sin(x) },
                { "Cos", (x) => Math.Cos(x) },
                { "tg", (x) => Math.Tan(x) },
                { "Sqr", (x) => Math.Pow(x,2) },
                { "expX", (x) => Math.Pow(x,Math.E) },
                { "!", (x) => Factorial(x) },
                { "Sqrt", (x) => Math.Sqrt(x) },
                { "division", (x) => 1/x },
            };
        // Словарь операций с двумя операндами
        public static Dictionary<string, DoubleOperationDelegate<double>> DoubleOperation =
            new Dictionary<string, DoubleOperationDelegate<double>>
            {
                { "+", (x, y) => x + y },
                { "-", (x, y) => x - y },
                { "*", (x, y) => x * y },
                { "/", (x, y) => x / y },
                { "x^y", (x,y) => Math.Pow(x,y) },
                { "MOD", (x,y) => x % y },
                { "DIV", (x,y) => (int)x / (int)y },
            };
        
        public static string[] GetOperation(string s)
        {
            Regex reg = new Regex(@"(-\d+[.,]?\d*)|(\d*[.,]?\d*)");
            MatchCollection matches = reg.Matches(s);
            List<string> mat = new List<string>();
            foreach (Match m in matches)
                if (m.Value.Length > 0)
                    mat.Add(m.Value);
            string[] a = s.Split(mat.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (a.Length == 0)
                a = new string[] { "+" };
            return a;
        }
    }
}
