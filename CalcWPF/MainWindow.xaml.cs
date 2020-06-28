using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalcLibrary;

namespace CalcWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Добавляем обработчик для всех кнопок на гриде
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            // Получаем текст кнопки
            string s = (string)((Button)e.OriginalSource).Content;
            string answer = textBlock.Text;
            if (answer == "Ошибка") answer = "0";
            switch (s)
            {
                case "DEL":
                    {
                        if (answer.Length > 0)
                            answer = answer.Remove(answer.Length - 1);
                        if (answer.Length == 0) answer = "0";
                        break;
                    }
                case "CLEAR":
                    {
                        answer = "0";
                        break;
                    }
                case "=":
                    {
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "π":
                    {   if (answer != "0") answer = "0";
                        answer += Math.Round(Math.PI, 10);
                        break;
                    }
                case "e":
                    {
                        if (answer != "0") answer = "0";
                        answer += Math.Round(Math.E, 10);
                        break;
                    }
                case "+/-":
                    {
                        answer += "reverse";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "1/x":
                    {
                        answer += "division";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "n!":
                    {
                        answer += "factor";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "x^2": 
                    {
                        answer += "Sqr";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "e^x": 
                    {
                        answer += "expX";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "√x":
                    {
                        answer += "Sqrt";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "tg":
                    {
                        answer += "tg";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "cos":
                    {
                        answer += "Cos";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "sin":
                    {
                        answer += "sin";
                        answer = Calc.DoOperation(answer);
                        break;
                    }
                case "x^y": 
                    {
                        answer += "^";
                        break;
                    }
                default:
                    {
                        answer += s;
                        break;
                    }
            }
            if (answer.Length > 0 && answer[0] == '0')
            {
                if (answer.Length > 1 && answer[1] != ',')
                    answer = answer.Remove(0, 1);
            }
            textBlock.Text = answer;
        }
    }
}

