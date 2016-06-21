using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int resultViewLength = 20;
        private bool isDotUsed;
        private string[] actionStack;
        private string lastOperation;
        private string lastValue;
        private bool isRes;

        public MainWindow()
        {
            this.InitializeComponent();
            this.InitializeCalculator();

            this.BtnClear.Click += delegate
             {
                 this.InitializeCalculator();
             };
            this.BtnSqrt.Click += this.FunctionButtonClick;

            this.BtnDot.Click += this.NumberButtonClick;
            this.Btn0.Click += this.NumberButtonClick;
            this.Btn1.Click += this.NumberButtonClick;
            this.Btn2.Click += this.NumberButtonClick;
            this.Btn3.Click += this.NumberButtonClick;
            this.Btn4.Click += this.NumberButtonClick;
            this.Btn5.Click += this.NumberButtonClick;
            this.Btn6.Click += this.NumberButtonClick;
            this.Btn7.Click += this.NumberButtonClick;
            this.Btn8.Click += this.NumberButtonClick;
            this.Btn9.Click += this.NumberButtonClick;


            this.BtnAdd.Click += this.FunctionButtonClick;
            this.BtnSubtract.Click += this.FunctionButtonClick;
            this.BtnMultiply.Click += this.FunctionButtonClick;
            this.BtnDivide.Click += this.FunctionButtonClick;

            this.BtnResult.Click += this.ResultButtonClick;
        }

        private void InitializeCalculator()
        {
            this.ResetCalculator();
        }

        private void ResetCalculator()
        {
            this.StackResultView.Text = string.Empty;
            this.ResultView.Text = "0";
            this.isDotUsed = false;
            this.lastOperation = string.Empty;
            this.isRes = true;
            this.actionStack = new string[3];
        }


        private void SetResultViewValue(string value)
        {
            if (!String.IsNullOrEmpty(value) && this.ResultView.Text.Length < resultViewLength)
            {
                if (this.ResultView.Text == "0" && value == "0")
                {
                    return;
                }
                else if (this.ResultView.Text == "0" && value == "." && this.isDotUsed == false)
                {
                    this.ResultView.Text += value;
                    this.isDotUsed = true;
                }
                else if (this.ResultView.Text == "0")
                {
                    this.ResultView.Text = value;
                }
                else if (value == ".")
                {
                    if (this.ResultView.Text.Length < resultViewLength - 1 && this.isDotUsed == false)
                    {
                        this.ResultView.Text += value;
                        this.isDotUsed = true;
                    }
                }
                else
                {
                    this.ResultView.Text += value;
                }
            }
        }

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            string button = (sender as Button).Tag as string;

            if (!string.IsNullOrEmpty(this.actionStack[1]))
            {
                switch (this.actionStack[1])
                {
                    case "ButtonAdd":
                    case "ButtonSubtrack":
                    case "ButtonMultiply":
                    case "ButtonDivide":
                        // TODO: finalize
                        if (this.isRes)
                        {
                            this.ResultView.Text = "0";
                            this.isRes = false;
                        }
                        goto Numbers;
                }
            }

            Numbers:
            switch (button)
            {
                case "ButtonDot":
                    this.SetResultViewValue(".");
                    break;
                case "Button0":
                    this.SetResultViewValue("0");
                    break;
                case "Button1":
                    this.SetResultViewValue("1");
                    break;
                case "Button2":
                    this.SetResultViewValue("2");
                    break;
                case "Button3":
                    this.SetResultViewValue("3");
                    break;
                case "Button4":
                    this.SetResultViewValue("4");
                    break;
                case "Button5":
                    this.SetResultViewValue("5");
                    break;
                case "Button6":
                    this.SetResultViewValue("6");
                    break;
                case "Button7":
                    this.SetResultViewValue("7");
                    break;
                case "Button8":
                    this.SetResultViewValue("8");
                    break;
                case "Button9":
                    this.SetResultViewValue("9");
                    break;
            }
        }

        // TODO finalize
        private void FunctionButtonClick(object sender, RoutedEventArgs e)
        {
            string button = (sender as Button).Tag as string;

            this.actionStack[0] = this.ResultView.Text;
            if (button == "ButtonSqrt")
            {
                this.actionStack[1] = button;
                if (!string.IsNullOrEmpty(this.StackResultView.Text))
                {
                    this.StackResultView.Text = (this.StackResultView.Text[0] == ((sender as Button).Content as string)[0])
                        ? string.Format("{0}({1})", (sender as Button).Content, this.StackResultView.Text)
                        : string.Format("{0}({1})", (sender as Button).Content, this.actionStack[0]);
                }
                else
                {
                    this.StackResultView.Text = string.Format("{0}({1})", (sender as Button).Content, this.actionStack[0]);
                }
                this.ResultView.Text = Calc.Sqrt(this.actionStack[0]).ToString();
            }
            else
            {
                switch (button)
                {
                    case "ButtonAdd":
                        this.actionStack[1] = button;
                        this.lastOperation = button;
                        break;
                    case "ButtonSubtrack":
                        this.actionStack[1] = button;
                        this.lastOperation = button;
                        break;
                    case "ButtonMultiply":
                        this.actionStack[1] = button;
                        this.lastOperation = button;
                        break;
                    case "ButtonDivide":
                        this.actionStack[1] = button;
                        this.lastOperation = button;
                        break;
                }
                this.isRes = true;
                this.StackResultView.Text = string.Format("{0} {1}", this.actionStack[0], (sender as Button).Content);
            }
        }

        private void ResultButtonClick(object sender, RoutedEventArgs e)
        {
            string button = (sender as Button).Tag as string;

            switch (this.lastOperation)
            {
                case "ButtonAdd":
                case "ButtonSubtrack":
                case "ButtonMultiply":
                case "ButtonDivide":
                    if (this.isRes)
                    {
                        this.lastValue = this.ResultView.Text;
                        this.actionStack[2] = this.lastValue;
                        break;
                    }
                    this.actionStack[2] = this.ResultView.Text;
                    break;
            }

            string result = null;
            switch (this.actionStack[1])
            {
                case "ButtonAdd":
                    {
                        result = Calc.Add(this.actionStack[0], this.actionStack[2]).ToString();
                        this.lastOperation = this.actionStack[1];
                        this.actionStack[0] = result;
                        this.isRes = true;
                        break;
                    }
                case "ButtonSubtrack":
                    {
                        result = Calc.Subtrack(this.actionStack[0], this.actionStack[2]).ToString();
                        this.lastOperation = this.actionStack[1];
                        this.actionStack[0] = result;
                        break;
                    }
                case "ButtonMultiply":
                    {
                        result = Calc.Multiply(this.actionStack[0], this.actionStack[2]).ToString();
                        this.lastOperation = this.actionStack[1];
                        this.actionStack[0] = result;
                        break;
                    }
                case "ButtonDivide":
                    {
                        result = Calc.Divide(this.actionStack[0], this.actionStack[2]).ToString();
                        this.lastOperation = this.actionStack[1];
                        this.actionStack[0] = result;
                        break;
                    }
            }
            this.ResultView.Text = result;
            this.StackResultView.Text = string.Empty;
        }
    }
}
