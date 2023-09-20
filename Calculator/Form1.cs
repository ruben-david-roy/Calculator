using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double totalSum = 0;
        string inputvalue;
        bool operationCondition = false;
        int counter = 0;
        double lastInputNumber;
        double beforeLastInputNumber;
        string number;
        bool a = true;
        ///
        int num_of_input_operation = 0;
        double firstNum;
        double total;// used for sum of first "x (inputOperation) y = total

        public Form1()
        {
            InitializeComponent();
        }
        Point lastPoint;
        //1. x (input operation) y = z for the first time -> onother operation without equals// calculate the current sum without "Btn_Equals_OnClick" method
        private double Calculation(string inputvalue, double firstNum, double secondNum)
        {
            double sum = 0;
            switch (inputvalue)
            {
                case "+":
                    sum = firstNum + secondNum;
                    break;

                case "-":
                    sum = firstNum - secondNum;
                    break;

                case "x":
                    sum = firstNum * secondNum;
                    break;

                case "/":
                    sum = firstNum / secondNum;
                    break;

                case "√":
                    textBox_OutPutValue.Text = Math.Sqrt(totalSum).ToString();
                    operationCondition = false;
                    break;

                default:
                    Lbl_1.Text = "Invalid argument!";
                    break;
            }
            return sum;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            a = true;
            if (textBox_OutPutValue.Text == "0" || operationCondition)
            {
                textBox_OutPutValue.Clear();
            }

            operationCondition = false;

            if (((Button)sender).Text == ".")
            {
                if (!textBox_OutPutValue.Text.Contains("."))
                {
                    textBox_OutPutValue.Text += ((Button)sender).Text;
                }
            }
            else
            {
                if (counter != 0)
                {
                    textBox_OutPutValue.Text = ((Button)sender).Text;
                    counter = 0;
                }
                else textBox_OutPutValue.Text += ((Button)sender).Text;
            }

            lastInputNumber = double.Parse(textBox_OutPutValue.Text);

            number = ((Button)sender).Text;
            ///1.
            if (num_of_input_operation == 0)
            {
                firstNum = double.Parse(textBox_OutPutValue.Text);
            }
            if (num_of_input_operation >= 1)// the condition is ">=1" in order to calculate the following operation/operations without the method "Btn_Equals_OnClick" !
            {
                total = Calculation(inputvalue, firstNum, lastInputNumber);
            }
        }


        private void Btn_Clear(object sender, EventArgs e)
        {
            textBox_OutPutValue.Font = new Font("Nirmala UI", 24, FontStyle.Bold);
            Lbl_1.Font = new Font("Nirmala UI", 12, FontStyle.Bold);
            textBox_OutPutValue.Text = "0";
            Lbl_1.Text = null;
            counter = 0;
            a = true;
            inputvalue = null;
            beforeLastInputNumber = 0;///
            num_of_input_operation = 0;///
            total = 0;
            //
            number = null;
            operationCondition = false;
            firstNum = 0;
        }

        private void Operation_Click(object sender, EventArgs e)
        {
            a = true;
            counter = 0;
            inputvalue = ((Button)sender).Text;//input operation
            totalSum = double.Parse(textBox_OutPutValue.Text);// current value
            operationCondition = true;
            //1. check for "x + y * z must give t."
            if (num_of_input_operation >= 1)
            {
                textBox_OutPutValue.Text = total.ToString();
                firstNum = total;
                totalSum = total;// stores the current sum for  "Btn_Equals_OnClick" method !
            }

            if (((Button)sender).Text != "√")
            {
                Lbl_1.Text = $"{textBox_OutPutValue.Text} {inputvalue} ";
            }
            else // √
            {
                Lbl_1.Text = $"{inputvalue}({totalSum.ToString()})";
            }

            num_of_input_operation++;
            beforeLastInputNumber = double.Parse(textBox_OutPutValue.Text);// for example: x + y... ,  beforeLastInputNumber = x

            if (textBox_OutPutValue.Text.Length >= 14)
            {
                textBox_OutPutValue.Font = new Font("Nirmala UI", 17, FontStyle.Bold);
                Lbl_1.Font = new Font("Nirmala UI", 9, FontStyle.Bold);
            }
        }


        private void Btn_Equals_OnClick(object sender, EventArgs e)
        {
            if ((textBox_OutPutValue.Text != "0" && a && (!operationCondition || (operationCondition && inputvalue == "√") || (operationCondition && num_of_input_operation > 0))) || (textBox_OutPutValue.Text == "0" && a && !operationCondition /*&& counter != 0*/))
            {

                if (operationCondition)
                {
                    if (num_of_input_operation > 0)
                    {
                        Lbl_1.Text += $"{textBox_OutPutValue.Text} =";
                        operationCondition = false;
                    }
                    else Lbl_1.Text += " =";
                }
                else
                {
                    if (counter == 0)
                    {
                        Lbl_1.Text += $"{textBox_OutPutValue.Text} =";
                    }
                    else
                    {
                        if (inputvalue == "√")
                        {
                            Lbl_1.Text = $"{inputvalue}({totalSum.ToString()}) =";
                        }
                        else
                        {
                            Lbl_1.Text = $"{textBox_OutPutValue.Text} {inputvalue} {lastInputNumber} =";
                        }
                    }
                }

                switch (inputvalue)
                {
                    case "+":
                        if (counter == 0)
                        {
                            lastInputNumber = double.Parse(textBox_OutPutValue.Text);//1
                            textBox_OutPutValue.Text = (totalSum + double.Parse(textBox_OutPutValue.Text)).ToString();
                        }
                        else
                        {
                            textBox_OutPutValue.Text = (lastInputNumber + double.Parse(textBox_OutPutValue.Text)).ToString();
                        }
                        a = true;
                        break;

                    case "-":
                        if (counter == 0)
                        {
                            lastInputNumber = double.Parse(textBox_OutPutValue.Text);//1
                            textBox_OutPutValue.Text = (totalSum - double.Parse(textBox_OutPutValue.Text)).ToString();
                        }
                        else
                        {
                            textBox_OutPutValue.Text = (double.Parse(textBox_OutPutValue.Text) - lastInputNumber).ToString();
                        }
                        a = true;
                        break;

                    case "x":
                        if (counter == 0)
                        {
                            lastInputNumber = double.Parse(textBox_OutPutValue.Text);//1
                            textBox_OutPutValue.Text = (totalSum * double.Parse(textBox_OutPutValue.Text)).ToString();
                        }
                        else
                        {
                            textBox_OutPutValue.Text = (double.Parse(textBox_OutPutValue.Text) * lastInputNumber).ToString();
                        }
                        a = true;
                        break;

                    case "/":
                        if (counter == 0)
                        {
                            if (!operationCondition && num_of_input_operation > 0)
                            {
                                lastInputNumber = double.Parse(textBox_OutPutValue.Text);
                                textBox_OutPutValue.Text = (totalSum / double.Parse(textBox_OutPutValue.Text)).ToString();
                                Console.WriteLine("123456789Hello World");
                            }
                        }
                        else
                        {
                            textBox_OutPutValue.Text = (double.Parse(textBox_OutPutValue.Text) / lastInputNumber).ToString();
                        }
                        a = true;
                        break;

                    case "√":
                        textBox_OutPutValue.Text = Math.Sqrt(totalSum).ToString();
                        operationCondition = false;
                        break;

                    default:
                        Lbl_1.Text = "Invalid argument!";
                        break;
                }
                counter++;
            }
            else
            {
                if (inputvalue != "√")
                {
                    if (number == null || inputvalue == null)//looks for case: 0 -> =
                    {
                        Lbl_1.Text = "0 =";
                    }
                    else
                    {
                        Lbl_1.Text = $"{totalSum.ToString()} {inputvalue} 0 =";
                    }
                }
                else // √
                {
                    Lbl_1.Text = $"{inputvalue}({totalSum.ToString()}) =";
                }

                textBox_OutPutValue.Text = "0";
                if (counter != 0) a = false;
            }

            if (textBox_OutPutValue.Text.Length >= 14)
            {
                textBox_OutPutValue.Font = new Font("Nirmala UI", 17, FontStyle.Bold);
                Lbl_1.Font = new Font("Nirmala UI", 9, FontStyle.Bold);
            }
            num_of_input_operation = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
