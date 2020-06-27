using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalcWPF
{
    public partial class MainWindow : Window
    {
        string leftop = ""; // Левый операнд
        string operation = ""; // Знак операции
        string rightop = ""; // Правый операнд

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
            if (textBlock.Text.StartsWith("0") && leftop != "0") textBlock.Text = "";
            // Добавляем его в текстовое поле
            textBlock.Text += s;
            int num;
            // Пытаемся преобразовать его в число
            bool result = Int32.TryParse(s, out num);
            // Если текст - это число
            if (result == true)
            {
                // Если операция не задана
                if (operation == "")
                {
                    // Добавляем к левому операнду
                    leftop += s;
                }
                else
                {
                    // Иначе к правому операнду
                    rightop += s;
                }
            }
            // Если было введено не число
            else
            {
                // Если равно, то выводим результат операции
                if (s == "=")
                {
                    Update_RightOp();
                    textBlock.Text += rightop;
                    operation = "";

                }
                // Очищаем поле и переменные
                else if (s == "CLEAR")
                {
                    leftop = "";
                    rightop = "";
                    operation = "";
                    textBlock.Text = "0";
                }
                else if (s == "DEL")
                {
                    if (operation == "" && !leftop.Equals(""))
                    {
                        leftop = leftop.Remove(leftop.Length - 1);
                        textBlock.Text = leftop;
                    }
                    else if (!rightop.Equals(""))
                    {
                        rightop = rightop.Replace(rightop.Last(), ' ');
                        textBlock.Text = leftop + operation + rightop;
                    }
                    if (leftop.Equals("") || leftop.Equals(""))
                    {
                        textBlock.Text = "0";
                    }
                }
                else if (s == "+/-")
                {
                    try
                    {
                        if (operation == "")
                        {
                            if (leftop[0] != '-') leftop = leftop.Insert(0, "-");
                            else leftop = leftop.Replace("-", " ");
                            textBlock.Text = leftop;
                        }
                        else
                        {
                            if (rightop[0] != '-') rightop = rightop.Insert(0, "-");
                            else rightop = rightop.Replace("-", " ");
                            textBlock.Text = leftop + operation + '(' + rightop + ')';
                        }
                    }
                    catch
                    {
                        leftop = rightop = "";
                        textBlock.Text = "0";
                        MessageBox.Show("Для операции '+/-' cначала вводится число", "Ошибка!");
                    }
                }
                else if (s == "1/x")
                {
                    try
                    {
                        if (leftop != "0")
                        {
                            double number = double.Parse(leftop);
                            if (number != 0)
                            {
                                leftop = Math.Pow(number, -1).ToString();
                                textBlock.Text = leftop;
                            }
                        }
                        else
                        {
                            leftop = rightop = "";
                            textBlock.Text = "0";
                            MessageBox.Show("На ноль делить нельзя!");
                        }
                    }
                    catch
                    {
                        leftop = rightop = "";
                        textBlock.Text = "0";
                        MessageBox.Show("Введите число!");
                    }
                }
                else if (s == "n!")
                {
                    try
                    {
                        double number = double.Parse(leftop);
                        int digit = 1;
                        for (int i = 2; i <= number; i++) digit *= i;
                        if (digit < int.MaxValue)
                            leftop = digit.ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        textBlock.Text = "0";
                        MessageBox.Show("Для операции 'n!' cначала вводится число", "Ошибка!");
                    }
                }
                else if (s == "√x")
                {
                    try
                    {
                        double n = double.Parse(leftop);
                        if (n >= 0)
                        {
                            leftop = Math.Sqrt(n).ToString();
                            textBlock.Text = leftop;
                        }
                        else
                        {
                            leftop = rightop = "";
                            textBlock.Text = "0";
                            MessageBox.Show("Число не является положительным", "Ошибка!");
                        }
                    }
                    catch
                    {
                        textBlock.Text = "0";
                        MessageBox.Show("Для операции '√x' cначала вводится число", "Ошибка!");

                    }
                }
                else if (s == "x^2")
                {
                    try
                    {
                        if (rightop == "")
                        {
                            int number = int.Parse(leftop);
                            int digit = number * number;
                            if (digit < int.MaxValue)
                                leftop = digit.ToString();
                            textBlock.Text = leftop;
                        }
                        else
                        {
                            int number = int.Parse(rightop);
                            int digit = number * number;
                            if (digit < int.MaxValue)
                                rightop = digit.ToString();
                            textBlock.Text = rightop;
                        }
                    }
                    catch
                    {
                        textBlock.Text = "0";
                        MessageBox.Show("Для операции 'x^2' cначала вводится число", "Ошибка!");

                    }
                }
                else if (s == "sin")
                {
                    try
                    {
                        double number = double.Parse(leftop);
                        leftop = Math.Sin(number).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            textBlock.Text = "0";
                            MessageBox.Show("Для операции 'sin' cначала вводится число", "Ошибка!");
                        }
                    }
                }
                else if (s == "cos")
                {
                    try
                    {
                        double number = double.Parse(leftop);
                        leftop = Math.Cos(number).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            textBlock.Text = "0";
                            MessageBox.Show("Для операции 'cos' cначала вводится число", "Ошибка!");
                        }
                    }
                }
                else if (s == "tg")
                {
                    try
                    {
                        double number = double.Parse(leftop);
                        leftop = Math.Tan(number).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            textBlock.Text = "0";
                            MessageBox.Show("Для операции 'tg' cначала вводится число", "Ошибка!");
                        }
                    }
                }
                else if (s == "e")
                {
                    if (leftop == "")
                    {
                        leftop = Math.Exp(1).ToString();
                        textBlock.Text = leftop;
                    }
                    else
                    {
                        rightop = Math.Exp(1).ToString();
                        textBlock.Text = rightop;
                    }
                }
                else if (s == "e^x")
                {
                    try
                    {
                        double number = double.Parse(leftop);
                        leftop = Math.Exp(number).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            textBlock.Text = "0";
                            MessageBox.Show("Для операции 'e^x' cначала введите степень", "Ошибка!");
                        }
                    }
                }
                else if (s == "π")
                {
                    leftop = Math.PI.ToString();
                    textBlock.Text = leftop;
                }
                // Получаем операцию
                else
                {
                    // Если правый операнд уже имеется, то присваиваем его значение левому
                    // операнду, а правый операнд очищаем
                    if (rightop != "")
                    {
                        Update_RightOp();
                        leftop = rightop;
                        rightop = "";
                    }
                    operation = s;
                }
            }
        }
        // Обновляем значение правого операнда
        private void Update_RightOp()
        {
            try
            {
                double num1 = Double.Parse(leftop);
                double num2 = Double.Parse(rightop);
                // И выполняем операцию
                switch (operation)
                {
                    case "+":
                        rightop = (num1 + num2).ToString();
                        break;
                    case "-":
                        rightop = (num1 - num2).ToString();
                        break;
                    case "*":
                        rightop = (num1 * num2).ToString();
                        break;
                    case "/":
                        if (num2 != 0 && rightop[0] != '0')
                            rightop = (num1 / num2).ToString();
                        else
                        {
                            leftop = operation = rightop = "";
                            textBlock.Text = "0";
                            MessageBox.Show("Некорректный ввод", "Ошибка!");
                        }
                        break;
                    case "MOD":
                        if (num2 != 0)
                            rightop = (num1 % num2).ToString();
                        break;
                    case "DIV":
                        if (num2 != 0)
                            rightop = Math.Truncate((double)num1 / num2).ToString();
                        break;
                    case "x^y":
                        rightop = Math.Pow(num1, num2).ToString();
                        break;
                }
            }
            catch
            {
                leftop = operation = rightop = "";
                textBlock.Text = "0";
                MessageBox.Show("Некорректный ввод", "Ошибка!");
            }
        }
    }
}

