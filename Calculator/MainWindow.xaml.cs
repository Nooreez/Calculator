using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Calculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string opera = string.Empty;
        private string currentNumber = string.Empty;
        private string l = string.Empty;
        private string r = string.Empty;
        private int nA=0,nB=0,nS=0; // Number of rows
        private int mA=0,mB=0,mS=0; // Number of columns
        private double EXP = 0.0000000000000001;
        private int idOfResult = 1;
        public string CurrentNumber
        {
            get { return currentNumber; }
            set
            {
                if (currentNumber != value)
                {
                    currentNumber = value;
                    NotifyPropertyChanged(nameof(CurrentNumber));
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            DefaultMatrices();
        }
        private void DefaultMatrices()
        {
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeAMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeBMatrix('+', '.');
            changeSMatrix('+', '.');
            changeSMatrix('+', '.');
            changeSMatrix('+', '.');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeAMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeBMatrix('.', '+');
            changeSMatrix('.', '+');
            changeSMatrix('.', '+');
            changeSMatrix('.', '+');
        }
        public void OperationsB(object sender, RoutedEventArgs e)
        {
            StackPanel resultPane = new StackPanel();
            resultPane.Orientation = Orientation.Vertical;
            StackPanel finalResult = new StackPanel();
            finalResult.HorizontalAlignment = HorizontalAlignment.Stretch;
            finalResult.Height = 100;
            finalResult.Margin = new Thickness(15);
            TextBlock theNew = new TextBlock();
            theNew.Text = (idOfResult++).ToString() + ". ";
            finalResult.Children.Add(theNew);
            StackPanel MatrixA = ShowMatrixB;
            TextBox[,] data = new TextBox[nB, mB];
            Button button = (Button)sender;
            string operations = button.Name;
            double[,] dataA = new double[nB, mB];
            int i = 0;
            bool ok = true;
            foreach (StackPanel row in MatrixA.Children)
            {
                if (!ok) break;
                foreach (TextBox col in row.Children)
                {
                    dataA[i / mA, i % mA] = new double();
                    if (col.Text.Contains(".")) col.Text.Replace('.', ',');
                    if (!double.TryParse(col.Text, out double result))
                    {
                        MessageBox.Show("There is a problem value in a: " + (i / mA + 1).ToString() + " row | " + (i % mA + 1).ToString() + " column");
                        ok = false;
                        break;
                    }
                    dataA[i / mA, i % mA] = double.Parse(col.Text);
                    i++;
                }
            }
            if ((operations.StartsWith("Det") || operations.StartsWith("Inv")) && (nB != mB))
            {
                MessageBox.Show("Number of rows must be same as number of columns", "FIX IT");
            }
            else
            {
                if (operations.StartsWith("Tra"))
                {
                    double[,] dataB = new double[mA, nA];

                    for (i = 0; i < nB; ++i)
                    {
                        for (int j = 0; j < mB; ++j)
                        {
                            dataB[j, i] = dataA[i, j];
                        }
                    }
                    int temp = nA;
                    nA = mA;
                    mA = temp;
                    MatrixA.Children.Clear();
                    for (i = 0; i < nB; ++i)
                    {
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        for (int j = 0; j < mB; ++j)
                        {
                            TextBox tb = new TextBox();
                            tb.Width = 25;
                            tb.Height = 25;
                            tb.Margin = new Thickness(4);
                            tb.Text = dataB[i, j].ToString();
                            stackPanel.Children.Add(tb);
                        }
                        MatrixA.Children.Add(stackPanel);
                    }
                }
                else if (operations.StartsWith("Mul"))
                {
                    TextBox num = (TextBox)FindName("MultiplyNumA");
                    num.Text.Replace('.', ',');
                    double n = double.Parse(num.Text);
                    //                MessageBox.Show(n.ToString());
                    double[,] dataB = new double[nB, mB];
                    for (i = 0; i < nB; ++i)
                    {
                        for (int j = 0; j < mB; ++j)
                        {
                            dataB[i, j] = dataA[i, j] * n;
                        }
                    }

                    i = 0;
                    foreach (StackPanel row in MatrixA.Children)
                    {
                        foreach (TextBox col in row.Children)
                        {
                            col.Text = dataB[i / mB, i % mB].ToString();
                            i++;
                        }
                    }
                }
                else if (operations.StartsWith("Div"))
                {
                    TextBox num = (TextBox)FindName("DivideNumB");
                    num.Text.Replace('.', ',');
                    double n = double.Parse(num.Text);
                    //                MessageBox.Show(n.ToString());
                    double[,] dataB = new double[nB, mB];
                    for (i = 0; i < nB; ++i)
                    {
                        for (int j = 0; j < mB; ++j)
                        {
                            dataB[i, j] = dataA[i, j] / n;
                        }
                    }

                    i = 0;
                    foreach (StackPanel row in MatrixA.Children)
                    {
                        foreach (TextBox col in row.Children)
                        {
                            col.Text = dataB[i / mB, i % mB].ToString();
                            i++;
                        }
                    }
                }
                else if (operations.StartsWith("Rai"))
                {
                    double[,] dataB = dataA;
                    TextBox num = (TextBox)FindName("RaiseByB");
                    if (int.TryParse(num.Text, out int result))
                    {
                        ok = false;
                        MessageBox.Show("the number must be integer");
                    }
                    else
                    {
                        int n = int.Parse(num.Text);

                        n--;
                        while (n-- > 0)
                        {
                            double[,] sample = new double[nB, mB];
                            for (i = 0; i < nB; ++i)
                            {
                                for (int j = 0; j < mB; ++j)
                                {
                                    double sum = 0;
                                    for (int k = 0; k < nB; ++k)
                                    {
                                        sum += dataB[i, k] * dataA[k, j];
                                    }
                                    sample[i, j] = sum;
                                }
                            }
                            dataB = sample;
                        }
                        i = 0;
                        foreach (StackPanel row in MatrixA.Children)
                        {
                            foreach (TextBox col in row.Children)
                            {
                                col.Text = dataB[i / mB, i % mB].ToString();
                                i++;
                            }
                        }
                    }
                }
                else if (operations.StartsWith("Row"))
                {
                    for (int k = 0; k < mB; ++k)
                    {
                        if (dataA[k, k] == 0)
                        {
                            for (i = k + 1; i < nB; ++i)
                            {
                                if (dataA[i, k] != 0)
                                {
                                    //swap rows
                                    double x;
                                    for (int j = 0; j < mB; ++j)
                                    {
                                        x = dataA[k, j];
                                        dataA[k, j] = dataA[i, j];
                                        dataA[i, j] = x;
                                    }
                                    break;
                                }
                            }
                        }
                        for (i = k + 1; i < nB; ++i)
                        {
                            for (int j = k + 1; j < mB; ++j)
                            {
                                //                            MessageBox.Show((dataA[i, k] / dataA[k, k]).ToString(),"SALma");
                                dataA[i, j] = dataA[i, j] - (dataA[i, k] / dataA[k, k]) * dataA[k, j];
                            }
                            dataA[i, k] = 0;
                        }
                    }
                    string res = "";
                    for (i = 0; i < nB; ++i)
                    {
                        for (int j = 0; j < mB; ++j)
                        {
                            res += dataA[i, j].ToString() + " ";
                        }
                        res += '\n';
                    }
                    MessageBox.Show(res, "Row Echelon Form");
                }
                else if (operations.StartsWith("Ran"))
                {
                    int res = 0;
                    for (int k = 0; k < mB; ++k)
                    {
                        if (dataA[k, k] == 0)
                        {
                            for (i = k + 1; i < nB; ++i)
                            {
                                if (dataA[i, k] != 0)
                                {
                                    //swap rows
                                    double x;
                                    string row1 = "", row2 = "";
                                    for (int j = 0; j < mB; ++j)
                                    {
                                        x = dataA[k, j];
                                        dataA[k, j] = dataA[i, j];
                                        dataA[i, j] = x;
                                        row1 += dataA[k, j].ToString() + " ";
                                        row2 += dataA[i, j].ToString() + " ";
                                    }
                                    break;
                                }
                            }
                        }
                        for (i = k + 1; i < nB; ++i)
                        {
                            for (int j = k + 1; j < mB; ++j)
                            {
                                //                            MessageBox.Show((dataA[i, k] / dataA[k, k]).ToString(),"SALma");
                                dataA[i, j] = dataA[i, j] - (dataA[i, k] / dataA[k, k]) * dataA[k, j];
                            }
                            dataA[i, k] = 0;
                        }
                    }
                    for (i = 0; i < nB; i++)
                    {
                        bool okRank = false;
                        for (int j = 0; j < mB; ++j)
                        {
                            if (dataA[i, j] != 0) okRank = true;
                        }
                        if (okRank) res++;
                    }
                    MessageBox.Show("The rank of the following Matrix is " + res.ToString(), "Rank of MatrixB");
                }
                else if (operations.StartsWith("Det"))
                {
                    for (int k = 0; k < mB; ++k)
                    {
                        if (dataA[k, k] == 0)
                        {
                            for (i = k + 1; i < nB; ++i)
                            {
                                if (dataA[i, k] != 0)
                                {
                                    //swap rows
                                    double x;
                                    for (int j = 0; j < mB; ++j)
                                    {
                                        x = dataA[k, j];
                                        dataA[k, j] = dataA[i, j];
                                        dataA[i, j] = x;
                                    }
                                }
                            }
                        }
                        for (i = k + 1; i < nB; ++i)
                        {
                            for (int j = k + 1; j < mB; ++j)
                            {
                                dataA[i, j] = dataA[i, j] - (dataA[i, k] / dataA[k, k]) * dataA[k, j];
                            }
                            dataA[i, k] = 0;
                        }
                    }
                    double det = -1;
                    for (i = 0; i < nB; ++i)
                    {
                        det = det * dataA[i, i];
                    }
                    MessageBox.Show("The determinant of the following Matrix is " + det.ToString(), "Determinant");
                }
                else if (operations.StartsWith("Inv"))
                {
                    if (nB != mB)
                    {
                        MessageBox.Show("Number of rows must be same as number of columns", "FIX IT");
                    }
                    else
                    {

                        double[,] dataB = new double[nB, mB];
                        for (i = 0; i < nB; ++i)
                        {
                            for (int j = 0; j < mB; ++j)
                            {
                                dataB[i, j] = 0;
                            }
                            dataB[i, i] = 1;
                        }
                        for (int k = 0; k < nB; ++k)
                        {
                            if (dataA[k, k] == 0)
                            {
                                for (i = k + 1; i < nB; ++i)
                                {
                                    if (dataA[i, k] != 0)
                                    {
                                        //swap rows
                                        double x;
                                        for (int j = 0; j < mB; ++j)
                                        {
                                            x = dataA[k, j];
                                            dataA[k, j] = dataA[i, j];
                                            dataA[i, j] = x;
                                        }
                                        for (int j = 0; j < mB; ++j)
                                        {
                                            x = dataB[k, j];
                                            dataB[k, j] = dataB[i, j];
                                            dataB[i, j] = x;
                                        }
                                        break;
                                    }
                                }
                            }
                            for (i = k + 1; i < nB; ++i)
                            {
                                double coef = (dataA[i, k] / dataA[k, k]);
                                for (int j = k; j < mA; ++j)
                                {
                                    //                            MessageBox.Show((dataA[i, k] / dataA[k, k]).ToString(),"SALma");
                                    dataA[i, j] = dataA[i, j] - coef * dataA[k, j];
                                    dataB[i, j] = dataB[i, j] - coef * dataB[k, j];
                                }
                            }
                        }

                        double det = -1;
                        for (i = 0; i < nB && ok; ++i)
                        {
                            det = det * dataA[i, i];
                        }
                        if (det == 0)
                        {
                            MessageBox.Show("Cannot convert, because determinant is equal to 0", "Inverse Imposible");
                            ok = false;
                        }
                        if (ok)
                        {
                            for (int k = 0; k < nB; ++k)
                            {
                                if (dataA[k, k] != 1)
                                {
                                    for (int j = 0; j < mB; ++j)
                                    {
                                        if (j != k)
                                        {
                                            dataA[k, j] /= dataA[k, k];
                                        }
                                        if (dataB[k, j] != 0)
                                            dataB[k, j] /= dataA[k, k];
                                    }
                                    dataA[k, k] = 1;
                                }
                            }
                            for (i = 0; i < nA; ++i)
                            {
                                for (int j = 0; j < mB; ++j)
                                {
                                    if (dataA[i, j] != 0 && j != i)
                                    {
                                        double coef = dataA[i, j] / dataA[j, j];
                                        for (int k = 0; k < nB; k++)
                                        {
                                            dataB[i, k] = dataB[i, k] - coef * dataB[j, k];
                                            dataA[i, k] = dataA[i, k] - coef * dataA[j, k];
                                        }
                                    }
                                }
                            }
                            string s = "";
                            for (i = 0; i < nA; ++i)
                            {
                                for (int j = 0; j < mA; ++j)
                                {
                                    s += dataB[i, j].ToString() + " ";
                                }
                                s += '\n';
                            }
                            MessageBox.Show(s, "InverseA");
                        }
                    }
                }
            }
        }
        public void OperationsA(object sender, RoutedEventArgs e)
        {
            StackPanel resultPane = new StackPanel();
            resultPane.Orientation = Orientation.Vertical;
            StackPanel finalResult = new StackPanel();
            finalResult.Orientation = Orientation.Horizontal;
            finalResult.Margin = new Thickness(45);
            finalResult.Background = new SolidColorBrush(Colors.White);
            TextBlock theNew = new TextBlock();
            theNew.Text = (idOfResult++).ToString() + ". ";
            theNew.FontSize = 35;
            finalResult.Children.Add(theNew);
            StackPanel MatrixA = ShowMatrixA;
            TextBox[,] data = new TextBox[nA, mA];
            bool ok = true;
            Button button = (Button)sender;
            string operations = button.Name;
            double[,] dataA = new double[nA, mA]; 
            int i = 0;
            foreach (StackPanel row in MatrixA.Children)
            {
                if (!ok) break;
                foreach (TextBox col in row.Children)
                {
                    dataA[i / mA, i % mA] = new double();
                    col.Text=col.Text.Replace('.', ',');
                    if (!double.TryParse(col.Text, out double result))
                    {
                        ok = false;
                        col.BorderBrush = Brushes.Red;
                        break;
                    }
                    col.BorderBrush = SystemColors.ControlDarkBrush;
                    dataA[i / mA, i % mA] = double.Parse(col.Text);
                    i++;
                }
            }
            if (!ok)
            {

            }
            else if((operations.StartsWith("Det") || operations.StartsWith("Inv") || operations.StartsWith("Raise")) && (nA != mA))
            { 
                    MessageBox.Show("Number of rows must be same as number of columns", "FIX IT");
            }
            else
            {
                foreach (StackPanel row in MatrixA.Children)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    foreach (TextBox col in row.Children)
                    {
                        TextBox tB = new TextBox();
                        tB.Width = 40;
                        tB.Height = 40;
                        tB.Text = col.Text;
                        tB.Margin = new Thickness(10);
                        tB.TextAlignment = TextAlignment.Center;
                        tB.IsEnabled = false;
                        stackPanel.Children.Add(tB);
                        i++;
                    }
                    resultPane.Children.Add(stackPanel);
                }
                finalResult.Children.Add(resultPane);
                if (operations.StartsWith("Tra"))
                {

                    double[,] dataB = new double[mA, nA];

                    for (i = 0; i < nA; ++i)
                    {
                        for (int j = 0; j < mA; ++j)
                        {
                            dataB[j, i] = dataA[i, j];
                        }
                    }
                    int temp = nA;
                    nA = mA;
                    mA = temp;
                    MatrixA.Children.Clear();
                    for (i = 0; i < nA; ++i)
                    {
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        for (int j = 0; j < mA; ++j)
                        {
                            TextBox tb = new TextBox();
                            tb.Width = 40;
                            tb.Height = 40;
                            tb.Margin = new Thickness(10);
                            tb.TextAlignment = TextAlignment.Center;
                            tb.Text = dataB[i, j].ToString();
                            stackPanel.Children.Add(tb);
                        }
                        MatrixA.Children.Add(stackPanel);
                    }
                }
                else if (operations.StartsWith("Mult"))
                {
                    
                    TextBox num = (TextBox)FindName("MultiplyNumA");
                    num.Text.Replace('.', ',');
                    double n = double.Parse(num.Text);
                    //                MessageBox.Show(n.ToString());
                    double[,] dataB = new double[nA, mA];
                    TextBlock textBlock = new TextBlock();
                    textBlock.Margin = new Thickness(20, 20 + nA / 2 * 60, 20, 0);
                    textBlock.Text = " multiplied by " + num.Text + " is ";
                    finalResult.Children.Add(textBlock);
                    for (i = 0; i < nA; ++i)
                    {
                        for (int j = 0; j < mA; ++j)
                        {
                            dataB[i, j] = dataA[i, j] * n;
                        }
                    }

                    i = 0;
                    StackPanel second = new StackPanel();
                    foreach (StackPanel row in MatrixA.Children)
                    {
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        foreach (TextBox col in row.Children)
                        {
                            col.Text = dataB[i / mA, i % mA].ToString("F2");
                            TextBox tB = new TextBox();
                            tB.Width = 40;
                            tB.Height = 40;
                            tB.Text = col.Text;
                            tB.Margin = new Thickness(10);
                            tB.TextAlignment= TextAlignment.Center;
                            tB.IsEnabled = false;
                            stackPanel.Children.Add(tB);
                            i++;
                        }
                        second.Children.Add(stackPanel);
                    }
                    finalResult.Children.Add(second);
                    ShowResults.Children.Insert(0, finalResult);
                }
                else if (operations.StartsWith("Div"))
                {
                    
                    TextBox num = (TextBox)FindName("DivideNumA");
                    num.Text.Replace('.', ',');
                    double n = double.Parse(num.Text);
                    //                MessageBox.Show(n.ToString());
                    double[,] dataB = new double[nA, mA];
                    TextBlock textBlock = new TextBlock();
                    textBlock.Margin = new Thickness(20, 10 + nA / 2 * 60, 20, 0);
                    textBlock.Text = " divided by " + num.Text + " is ";
                    finalResult.Children.Add(textBlock);
                    for (i = 0; i < nA; ++i)
                    {
                        for (int j = 0; j < mA; ++j)
                        {
                            dataB[i, j] = dataA[i, j] / n;
                        }
                    }

                    i = 0;
                    StackPanel second = new StackPanel();
                    foreach (StackPanel row in MatrixA.Children)
                    {
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        foreach (TextBox col in row.Children)
                        {
                            col.Text = dataB[i / mA, i % mA].ToString("F2");
                            TextBox tB = new TextBox();
                            tB.Width = 40;
                            tB.Height = 40;
                            tB.Text = col.Text;
                            tB.Margin = new Thickness(10);
                            tB.TextAlignment = TextAlignment.Center;
                            tB.IsEnabled = false;
                            stackPanel.Children.Add(tB);
                            i++;
                        }
                        second.Children.Add(stackPanel);
                    }
                    finalResult.Children.Add(second);
                    ShowResults.Children.Insert(0, finalResult);
                }
                else if (operations.StartsWith("Raise"))
                {
                    double[,] dataB = dataA;
                    TextBox num = (TextBox)FindName("RaiseByA");
                    if (!int.TryParse(num.Text, out int result))
                    {
                        ok = false;
                        MessageBox.Show(num.Text  + " the number must be integer");
                    }
                    else
                    {
                        int n = int.Parse(num.Text);
                        TextBlock textBlock = new TextBlock();
                        textBlock.Margin = new Thickness(20, 10 + nA / 2 * 60, 20, 0);
                        textBlock.Text = "in the power of " + num.Text + " is ";
                        finalResult.Children.Add(textBlock);
                        n--;
                        while (n-- > 0)
                        {
                            double[,] sample = new double[nA, mA];
                            for (i = 0; i < nA; ++i)
                            {
                                for (int j = 0; j < mA; ++j)
                                {
                                    double sum = 0;
                                    for (int k = 0; k < nA; ++k)
                                    {
                                        sum += dataB[i, k] * dataA[k, j];
                                    }
                                    sample[i, j] = sum;
                                }
                            }
                            dataB = sample;
                        }
                        i = 0;
                        StackPanel second = new StackPanel();
                        foreach (StackPanel row in MatrixA.Children)
                        {
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Orientation = Orientation.Horizontal;
                            foreach (TextBox col in row.Children)
                            {
                                col.Text = dataB[i / mA, i % mA].ToString("F2");
                                TextBox tB = new TextBox();
                                tB.Width = 40;
                                tB.Height = 40;
                                tB.Text = col.Text;
                                tB.Margin = new Thickness(10);
                                tB.TextAlignment = TextAlignment.Center;
                                tB.IsEnabled = false;
                                stackPanel.Children.Add(tB);
                                i++;
                            }
                            second.Children.Add(stackPanel);
                        }
                        finalResult.Children.Add(second);
                        ShowResults.Children.Insert(0,finalResult);
                    }
                }
                else if (operations.StartsWith("Row"))
                {
                   
                    TextBlock textBlock = new TextBlock();
                    textBlock.Margin = new Thickness(20, 10 + nA / 2 * 60, 20, 0);
                    textBlock.Text = " row echelon form of the Matrix is ";
                    finalResult.Children.Add(textBlock);
                    for (int k = 0; k < nA; ++k)
                    {
                        if (dataA[k, k] == 0)
                        {
                            for (i = k + 1; i < nA; ++i)
                            {
                                if (dataA[i, k] != 0)
                                {
                                    //swap rows
                                    double x;
                                    for (int j = 0; j < mA; ++j)
                                    {
                                        x = dataA[k, j];
                                        dataA[k, j] = dataA[i, j];
                                        dataA[i, j] = x;
                                    }
                                    break;
                                }
                            }
                        }
                        for (i = k + 1; i < nA; ++i)
                        {
                            for (int j = k + 1; j < mA; ++j)
                            {
                                //                            MessageBox.Show((dataA[i, k] / dataA[k, k]).ToString(),"SALma");
                                dataA[i, j] = dataA[i, j] - (dataA[i, k] / dataA[k, k]) * dataA[k, j];
                            }
                            dataA[i, k] = 0;
                        }
                    }
                    string res = "";
                    for (i = 0; i < nA; ++i)
                    {
                        for (int j = 0; j < mA; ++j)
                        {
                            res += dataA[i, j].ToString() + " ";
                        }
                        res += '\n';
                    }
                    i = 0;
                    StackPanel second = new StackPanel();
                    foreach (StackPanel row in MatrixA.Children)
                    {
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        foreach (TextBox col in row.Children)
                        {
                            col.Text = dataA[i / mA, i % mA].ToString("F2");
                            TextBox tB = new TextBox();
                            tB.Width = 40;
                            tB.Height = 40;
                            tB.Text = col.Text;
                            tB.Margin = new Thickness(10);
                            tB.TextAlignment = TextAlignment.Center;
                            tB.IsEnabled = false;
                            stackPanel.Children.Add(tB);
                            i++;
                        }
                        second.Children.Add(stackPanel);
                    }
                    finalResult.Children.Add(second);
                    ShowResults.Children.Insert(0, finalResult);
                    MessageBox.Show(res, "Row Echelon Form");
                }
                else if (operations.StartsWith("Rank"))
                {
                    int res = 0;
                    for (int k = 0; k < nA; ++k)
                    {
                        if (dataA[k, k] == 0)
                        {
                            for (i = k + 1; i < nA; ++i)
                            {
                                if (dataA[i, k] != 0)
                                {
                                    //swap rows
                                    double x;
                                    for (int j = 0; j < mA; ++j)
                                    {
                                        x = dataA[k, j];
                                        dataA[k, j] = dataA[i, j];
                                        dataA[i, j] = x;
                                    }
                                    break;
                                }
                            }
                        }
                        for (i = k + 1; i < nA; ++i)
                        {
                            for (int j = k + 1; j < mA; ++j)
                            {
                                //                            MessageBox.Show((dataA[i, k] / dataA[k, k]).ToString(),"SALma");
                                dataA[i, j] = dataA[i, j] - (dataA[i, k] / dataA[k, k]) * dataA[k, j];
                            }
                            dataA[i, k] = 0;
                        }
                    }
                    for (i = 0; i < nA; i++)
                    {
                        bool okRank = false;
                        for (int j = 0; j < mA; ++j)
                        {
                            if (dataA[i, j] != 0) okRank = true;
                        }
                        if (okRank) res++;
                    }
                    TextBlock textBlock = new TextBlock();
                    textBlock.Margin = new Thickness(20, 10 + nA / 2 * 60, 20, 0);
                    textBlock.Text = " rank of the Matrix is " + res.ToString();
                    finalResult.Children.Add(textBlock);
                    ShowResults.Children.Insert(0, finalResult);
                    MessageBox.Show("The rank of the following Matrix is " + res.ToString(), "Rank of MatrixA");
                }
                else if (operations.StartsWith("Det"))
                {
                    for (int k = 0; k < mA; ++k)
                    {
                        if (dataA[k, k] == 0)
                        {
                            for (i = k + 1; i < nA; ++i)
                            {
                                if (dataA[i, k] != 0)
                                {
                                    //swap rows
                                    double x;
                                    for (int j = 0; j < mA; ++j)
                                    {
                                        x = dataA[k, j];
                                        dataA[k, j] = dataA[i, j];
                                        dataA[i, j] = x;
                                    }
                                }
                            }
                        }
                        for (i = k + 1; i < nA; ++i)
                        {
                            for (int j = k + 1; j < mA; ++j)
                            {
                                //                            MessageBox.Show((dataA[i, k] / dataA[k, k]).ToString(),"SALma");
                                dataA[i, j] = dataA[i, j] - (dataA[i, k] / dataA[k, k]) * dataA[k, j];
                            }
                            dataA[i, k] = 0;
                        }
                    }
                    double det = -1;
                    for (i = 0; i < nA && ok; ++i)
                    {
                        det = det * dataA[i, i];
                    }
                    TextBlock textBlock = new TextBlock();
                    textBlock.Margin = new Thickness(20, 10 + nA / 2 * 60, 20, 0);
                    textBlock.Text = " determinant of the Matrix is " + det.ToString("F2");
                    finalResult.Children.Add(textBlock);
                    ShowResults.Children.Insert(0, finalResult);
                    MessageBox.Show("The determinant of the following Matrix is " + det.ToString("F2"), "DeterminantA");
                }
                else if (operations.StartsWith("Inv"))
                {
                    if (nA != mA)
                    {
                        MessageBox.Show("Number of rows must be same as number of columns", "FIX IT");
                    }
                    else
                    {

                        double[,] dataB = new double[nA, mA];
                        for (i = 0; i < nA; ++i)
                        {
                            for (int j = 0; j < mA; ++j)
                            {
                                dataB[i, j] = 0;
                            }
                            dataB[i, i] = 1;
                        }
                        for (int k = 0; k < nA; ++k)
                        {
                            if (dataA[k, k] == 0)
                            {
                                for (i = k + 1; i < nA && (dataA[k,k]==0); ++i)
                                {
                                    if (dataA[i, k] != 0)
                                    {
                                        //swap rows
                                        double x;
                                        for (int j = 0; j < mA; ++j)
                                        {
                                            x = dataA[k, j];
                                            dataA[k, j] = dataA[i, j];
                                            dataA[i, j] = x;
                                        }
                                        for (int j = 0; j < mA; ++j)
                                        {
                                            x = dataB[k, j];
                                            dataB[k, j] = dataB[i, j];
                                            dataB[i, j] = x;
                                        }
                                        break;
                                    }
                                }
                            }
                            for (i = k + 1; i < nA; ++i)
                            {
                                double coef = (dataA[i, k] / dataA[k, k]);
                                for (int j = k; j < mA; ++j)
                                {
                                    //                            MessageBox.Show((dataA[i, k] / dataA[k, k]).ToString(),"SALma");
                                    dataA[i, j] = dataA[i, j] - coef * dataA[k, j];
                                    dataB[i, j] = dataB[i, j] - coef * dataB[k, j];
                                }
                            }
                        }

                        double det = -1;
                        for (i = 0; i < nA && ok; ++i)
                        {
                            det = det * dataA[i, i];
                        }
                        if (det == 0)
                        {
                            MessageBox.Show("Cannot convert, because determinant is equal to 0", "Inverse Imposible");
                            ok = false;
                        }
                        if (ok)
                        {
                            for (int k = 0; k < nA; ++k)
                            {
                                if (dataA[k, k] != 1)
                                {
                                    for (int j = 0; j < mA; ++j)
                                    {
                                        if (j != k)
                                        {
                                            dataA[k, j] /= dataA[k, k];
                                        }
                                        if (dataB[k, j] != 0)
                                            dataB[k, j] /= dataA[k, k];
                                    }
                                    dataA[k, k] = 1;
                                }
                            }
                            for (i = 0; i < nA; ++i)
                            {
                                for (int j = 0; j < mA; ++j)
                                {
                                    if (dataA[i, j] != 0 && j != i)
                                    {
                                        double coef = dataA[i, j] / dataA[j, j];
                                        for (int k = 0; k < nA; k++)
                                        {
                                            dataB[i, k] = dataB[i, k] - coef * dataB[j, k];
                                            dataA[i, k] = dataA[i, k] - coef * dataA[j, k];
                                        }
                                    }
                                }
                            }

                            TextBlock textBlock = new TextBlock();
                            textBlock.Margin = new Thickness(20, 10 + nA / 2 * 60, 20, 0);
                            textBlock.Text = " inverse of the Matrix is " ;
                            finalResult.Children.Add(textBlock);
                            i = 0;
                            StackPanel second = new StackPanel();
                            foreach (StackPanel row in MatrixA.Children)
                            {
                                StackPanel stackPanel = new StackPanel();
                                stackPanel.Orientation = Orientation.Horizontal;
                                foreach (TextBox col in row.Children)
                                {
                                    col.Text = dataB[i / mA, i % mA].ToString();
                                    TextBox tB = new TextBox();
                                    tB.Width = 40;
                                    tB.Height = 40;
                                    tB.Text = col.Text;
                                    tB.Margin = new Thickness(10);
                                    tB.TextAlignment = TextAlignment.Center;
                                    tB.IsEnabled = false;
                                    stackPanel.Children.Add(tB);
                                    i++;
                                }
                                second.Children.Add(stackPanel);
                            }
                            finalResult.Children.Add(second);
                            ShowResults.Children.Insert(0, finalResult);
                            string s = "";
                            for (i = 0; i < nA; ++i)
                            {
                                for (int j = 0; j < mA; ++j)
                                {
                                    s += dataB[i, j].ToString("F2") + " ";
                                }
                                s += '\n';
                            }
                            MessageBox.Show(s, "InverseA");
                        }
                    }
                }         
            }
        }
        public void changeAMatrix(char row, char col)
        {

//            MessageBox.Show(row + " " + col, "Col");
            if (row == '+')
            {
                nA++;
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Name = "row" + (nA).ToString();
                for (int i = 0; i < mA; ++i)
                {
                    TextBox textBox = new TextBox();
                    textBox.Width = 30;
                    textBox.Height = 30;
                    textBox.Margin = new Thickness(4);
                    textBox.TextAlignment = TextAlignment.Center;
                    textBox.Name = "col" + (i + 1).ToString();
                    stackPanel.Children.Add(textBox);
                }
                ShowMatrixA.Children.Add(stackPanel);
//                MessageBox.Show(nB.ToString() + " " + col, "Col");

            }
            else if (row == '-')
            {
                ShowMatrixA.Children.RemoveAt(--nA);
            }
            else if (col == '+')
            {
                mA++;
                foreach (var child in ShowMatrixA.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Width = 30;
                        textBox.Height = 30;
                        textBox.Margin = new Thickness(4);
                        textBox.Name = "col" + (mA).ToString();
                        textBox.TextAlignment = TextAlignment.Center;
                        stackPanel.Children.Add(textBox);
                    }
                }

            }
            else
            {
                foreach (var child in ShowMatrixA.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        stackPanel.Children.RemoveAt(mA - 1);
                    }
                }
                mA--;
            }
        }
        private void ChangeASize(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name;
            if (buttonName.Equals("RowAddA"))
            {
                if (nA < 10)
                {
                    changeAMatrix('+', '.');
                }
            }
            else if (buttonName.Equals("RowRemoveA"))
            {
                if (nA > 0)
                {
                    changeAMatrix('-', '.');

                }
            }
            else if (buttonName.Equals("ColAddA"))
            {
                if (mA < 10)
                {
                    changeAMatrix('.', '+');
                }
            }
            else
            {
                if (mA > 0)
                {
                    changeAMatrix('.', '-');
                }
            }
        }
        public void changeBMatrix(char row, char col)
        {

//            MessageBox.Show(row + " " + col, "Col");
            if (row == '+')
            {
                nB++;
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Name = "row" + (nB).ToString();
                for(int i = 0; i < mB; ++i)
                {
                    TextBox textBox=new TextBox();
                    textBox.Width = 25;
                    textBox.Height = 25;
                    textBox.Margin = new Thickness(4);
                    textBox.Name = "col" + (i+1).ToString();
                    textBox.TextAlignment = TextAlignment.Center;
                    stackPanel.Children.Add(textBox);
                }
                ShowMatrixB.Children.Add(stackPanel);
//                MessageBox.Show(nB.ToString() + " " + col, "Col");

            }
            else if(row == '-')
            {
                ShowMatrixB.Children.RemoveAt(--nB);
            }
            else if(col == '+')
            {
                mB++;
                foreach (var child in ShowMatrixB.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Width = 25;
                        textBox.Height = 25;
                        textBox.Margin = new Thickness(4);
                        textBox.Name = "col" + (mB).ToString();
                        textBox.TextAlignment = TextAlignment.Center;

                        stackPanel.Children.Add(textBox);
                    }
                }

            }
            else
            {
                foreach(var child in ShowMatrixB.Children)
                {
                    if(child is StackPanel stackPanel)
                    {
                        stackPanel.Children.RemoveAt(mB-1);
                    }
                }
                mB--;
            }
        }
        private void ChangeBSize(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name;
            if (buttonName.Equals("RowAddB"))
            {
                if (nB < 10)
                {
                    changeBMatrix('+', '.');
                }
            }
            else if(buttonName.Equals("RowRemoveB"))
            {
                if(nB > 0)
                {
                    changeBMatrix('-', '.');

                }
            }
            else if (buttonName.Equals("ColAddB"))
            {
                if (mB < 10)
                {
                    changeBMatrix('.', '+');
                }
            }
            else
            {
                if(mB > 0)
                {
                    changeBMatrix('.', '-');
                }
            }
        }
        private void changeSMatrix(char row, char col)
        {

            //            MessageBox.Show(row + " " + col, "Col");
            if (row == '+')
            {
                dividerLine.Y2 += 40;
                nS++;
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Name = "row" + (nS).ToString();
                for (int i = 0; i < mS; ++i)
                {
                    StackPanel xx = new StackPanel();
                    xx.Orientation = Orientation.Horizontal;
                    TextBox textBox = new TextBox();
                    textBox.Width = 25;
                    textBox.Height = 25;
                    textBox.Margin = new Thickness(8);
                    textBox.Name = "col" + (i + 1).ToString();
                    textBox.TextAlignment = TextAlignment.Center;
                    if (i != 0)
                    {
                        TextBlock plus = new TextBlock();
                        plus.FontSize = 25;
                        plus.Text = "+";
                        xx.Children.Add(plus);
                    }
                    xx.Children.Add(textBox);
                    TextBlock textBlock = new TextBlock();
                    textBlock.TextAlignment= TextAlignment.Center;
                    Run run1 = new Run("x");
                    Run run2 = new Run((i+1).ToString());
                    run1.FontSize = 15;
                    run2.FontSize = 10;
                    textBlock.Margin = new Thickness(0, 15, 0, 0);
                    textBlock.Inlines.Add(run1);
                    textBlock.Inlines.Add(run2);
                    xx.Children.Add(textBlock);
                    stackPanel.Children.Add(xx);
                }
                TextBox tB=new TextBox();
                tB.Width = 25;
                tB.Height = 25;
                tB.Margin = new Thickness(8);
                ShowMatrixS.Children.Add(stackPanel);
                ListOfEq.Children.Add(tB);
                //                MessageBox.Show(nB.ToString() + " " + col, "Col");

            }
            else if (row == '-')
            {
                dividerLine.Y2 -= 40;
                ShowMatrixS.Children.RemoveAt(--nS);
                ListOfEq.Children.RemoveAt(nS);
            }
            else if (col == '+')
            {
                mS++;
                foreach (var child in ShowMatrixS.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        StackPanel xx = new StackPanel();
                        xx.Orientation = Orientation.Horizontal;
                        TextBox textBox = new TextBox();
                        textBox.TextAlignment= TextAlignment.Center;
                        textBox.Width = 25;
                        textBox.Height = 25;
                        textBox.Margin = new Thickness(8);
                        textBox.Name = "col" + (mS).ToString();
                        textBox.TextAlignment = TextAlignment.Center;
                        if (mS != 1)
                        {
                            TextBlock plus = new TextBlock();
                            plus.FontSize = 25;
                            plus.Text = "+";
                            xx.Children.Add(plus);
                        }
                        xx.Children.Add(textBox);
                        TextBlock textBlock = new TextBlock();
                        textBlock.TextAlignment = TextAlignment.Center;
                        Run run1 = new Run("x");
                        Run run2 = new Run(mS.ToString());
                        run1.FontSize = 15;
                        run2.FontSize = 10;
                        textBlock.Inlines.Add(run1);
                        textBlock.Inlines.Add(run2);
                        textBlock.Margin = new Thickness(0, 15, 0, 0);
                        xx.Children.Add(textBlock);
                        stackPanel.Children.Add(xx);
                    }
                }

            }
            else
            {
                foreach (var child in ShowMatrixS.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        stackPanel.Children.RemoveAt(mS - 1);
                    }
                }
                mS--;
            }
        }
        private void ChangeSSize(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name;
            if (buttonName.Equals("RowAddS"))
            {
                if (nS < 10)
                {
                    changeSMatrix('+', '.');
                }
            }
            else if (buttonName.Equals("RowRemoveS"))
            {
                if (nS > 0)
                {
                    changeSMatrix('-', '.');

                }
            }
            else if (buttonName.Equals("ColAddS"))
            {
                if (mS < 10)
                {
                    changeSMatrix('.', '+');
                }
            }
            else
            {
                if (mS > 0)
                {
                    changeSMatrix('.', '-');
                }
            }
        }
        private void GaussianMethod(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            double[,] data = new double[nS,mS+1];
            int i = 0;
            foreach (StackPanel row in ShowMatrixS.Children)
            {
                foreach (StackPanel column in row.Children)
                {
                    TextBox tB = column.Children[(i%mS==0?0:1)] as TextBox;
                    tB.Text = tB.Text.Replace('.',',');
                    if(!double.TryParse(tB.Text,out double res))
                    {
                        ok = false;
                        tB.BorderBrush = Brushes.Red;
                    }
                    else
                    {
                        data[i / mS, i % mS] = double.Parse(tB.Text);
                        tB.BorderBrush = SystemColors.ControlDarkBrush;
                    }
                    i++;
                }
            }
            i = 0;
            foreach (TextBox tB in ListOfEq.Children)
            {
                tB.Text = tB.Text.Replace('.', ',');
                if (double.TryParse(tB.Text, out double result))
                {
                    tB.BorderBrush = SystemColors.ControlDarkBrush;
                    data[i++, mS] = double.Parse(tB.Text);
                }
                else
                {
                    tB.BorderBrush = Brushes.Red;
                    ok = false;
                }
            }
            if (ok)
            {

                for (int k = 0; k < nS; ++k)
                {
                    if (data[k, k] == 0)
                    {
                        for (i = k + 1; i < nS; ++i)
                        {
                            if (data[i, k] != 0)
                            {
                                //swap rows
                                double x;
                                for (int j = 0; j < mS; ++j)
                                {
                                    x = data[k, j];
                                    data[k, j] = data[i, j];
                                    data[i, j] = x;
                                }
                                break;
                            }
                        }
                    }
                    for (i = k + 1; i < nS; ++i)
                    {
                        for (int j = k + 1; j <= mS; ++j)
                        {
                            data[i, j] = data[i, j] - (data[i, k] / data[k, k]) * data[k, j];
                        }
                        data[i, k] = 0;
                    }
                }
                //HERE 
                string s = "";
                for (i = 0; i < nS; ++i)
                {
                    for (int j = 0; j <= mS; ++j)
                    {
                        s += data[i, j].ToString() + " ";
                    }
                    s += '\n';
                }

                if (nS == mS)
                {
                    StackPanel finalResult = new StackPanel();
                    finalResult.Background = new SolidColorBrush(Colors.White);
                    StackPanel LES = new StackPanel();
                    finalResult.Margin = new Thickness(45);
                    finalResult.Orientation = Orientation.Horizontal;
                    TextBlock bracket = new TextBlock();
                    bracket.Text= "{";
                    bracket.FontSize = nS * 55;
                    bracket.Margin = new Thickness(0, 0, 0, 0);
                    LES.Margin = new Thickness(0,55,0,0);
                    TextBlock theNew = new TextBlock();
                    theNew.Text = (idOfResult++).ToString() + ". ";
                    theNew.FontSize = 35;
                    finalResult.Children.Add(theNew);
                    finalResult.Children.Add(bracket);
                    foreach (StackPanel row in ShowMatrixS.Children)
                    {
                        i = 0;
                        StackPanel roller = new StackPanel();
                        roller.Orientation = Orientation.Horizontal;
                        foreach (StackPanel column in row.Children)
                        {
                            StackPanel xx = new StackPanel();
                            xx.Orientation = Orientation.Horizontal;
                            TextBlock textBox = new TextBlock();
                            textBox.TextAlignment = TextAlignment.Center;
                            textBox.Margin = new Thickness(8,8,0,8);
                            textBox.Name = "col" + (mS).ToString();
                            textBox.FontSize = 25;
                            textBox.IsEnabled = false;
                            textBox.Text = ((TextBox)column.Children[(i == 0) ? 0 : 1]).Text.ToString();
//                            MessageBox.Show(textBox.Text);
                            if (i != 0)
                            {
                                TextBlock plus = new TextBlock();
                                plus.FontSize = 25;
                                plus.Text = "+";
                                plus.Margin = new Thickness(8);
                                xx.Children.Add(plus);
                            }
                            xx.Children.Add(textBox);
                            TextBlock textBlock = new TextBlock();
                            textBlock.TextAlignment = TextAlignment.Center;
                            Run run1 = new Run("x");
                            Run run2 = new Run((++i).ToString());
                            run1.FontSize = 15;
                            run2.FontSize = 10;
                            textBlock.Inlines.Add(run1);
                            textBlock.Inlines.Add(run2);
                            textBlock.Margin = new Thickness(0,25,0,0);
                            xx.Children.Add(textBlock);
                            roller.Children.Add(xx);
                        }
                        LES.Children.Add(roller);
                    }
                    finalResult.Children.Add(LES);
                    TextBlock equal = new TextBlock();
                    equal.FontSize = 55;
                    equal.Text = "=";
                    equal.Margin = new Thickness(10, 55+nS / 2 * 30.5, 0, 10);
                    finalResult.Children.Add(equal);
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(0,55,0,0);
                    foreach(TextBox eq in ListOfEq.Children)
                    {
                        TextBlock textBox = new TextBlock();
                        textBox.TextAlignment = TextAlignment.Center;
                        textBox.Margin = new Thickness(8, 8, 0, 8);
                        textBox.Name = "col" + (mS).ToString();
                        textBox.FontSize = 25;
                        textBox.Text = eq.Text;
                        textBox.IsEnabled = false;
                        stackPanel.Children.Add(textBox);
                    }
                    finalResult.Children.Add(stackPanel);
                    
                    double[] x = new double[nS];
                    StackPanel Xx = new StackPanel();
                    TextBlock Xa = new TextBlock();
                    TextBlock equal1 = new TextBlock();
                    Xa.FontSize = 65;
                    Xa.Text = "x";
                    Line line = new Line();
                    line.Y1 = 0;
                    line.X1 = 0;
                    line.X2 = 0;
                    line.Y2 = 55 * nS;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 2;
                    line.Margin = new Thickness(20, 55, 0, 0);
                    Xa.Margin = new Thickness(30, 45 + nS / 2 * 30.5, 0, 10);
                    equal1.FontSize = 55;
                    equal1.Text = "=";
                    equal1.Margin = new Thickness(10, 55 + nS / 2 * 30.5, 0, 10);
                    finalResult.Children.Add(line);
                    finalResult.Children.Add(Xa);
                    finalResult.Children.Add(equal1);
                    TextBlock leftBracket = new TextBlock();
                    leftBracket.Text = "(";
                    leftBracket.FontSize = nS * 55;
                    leftBracket.Margin = new Thickness(0, 0, 0, 0);
                    TextBlock rightBracket = new TextBlock();
                    rightBracket.Text = ")";
                    rightBracket.FontSize = nS * 55;
                    rightBracket.Margin = new Thickness(0, 0, 0, 0);
                    stackPanel.Margin = new Thickness(0, 55, 0, 0);
                    finalResult.Children.Add(leftBracket);
                    Xx.Margin = new Thickness(0, 55, 0, 0);
                    for (i = nS - 1; i >= 0; --i)
                    {
                        double sum = 0;
                        for (int j = i + 1; j < mS; ++j)
                        {
                            sum += data[i, j] * x[j];
                        }
                        x[i] = (data[i, mS] - sum) / data[i, i];
                    }
                    string res = "";
                    for (i = 0; i < nS; ++i)
                    {
                        TextBlock textBox = new TextBlock();
                        textBox.TextAlignment = TextAlignment.Center;
                        textBox.Margin = new Thickness(8, 8, 0, 8);
                        textBox.Name = "col" + (mS).ToString();
                        textBox.FontSize = 25;
                        textBox.Text = x[i].ToString();
                        Xx.Children.Add(textBox);
                        res += (i + 1).ToString() + ". " + x[i].ToString() + "\n";
                    }
                    finalResult.Children.Add(Xx);
                    finalResult.Children.Add(rightBracket);
                    ShowResults.Children.Insert(0,finalResult);
                    OpenResultPane();
                }
                else
                {
                    MessageBox.Show("Change size to quadratic");
                }
            }

        }
        public void OperationsOnMatrices(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String operation = button.Name;
            if (operation.StartsWith("C"))
            {
                var tempChildrenA = new List<UIElement>(ShowMatrixA.Children.Cast<UIElement>());
                var tempChildrenB = new List<UIElement>(ShowMatrixB.Children.Cast<UIElement>());

                ShowMatrixA.Children.Clear();
                ShowMatrixB.Children.Clear();

                foreach (var child in tempChildrenA)
                {
                    ShowMatrixB.Children.Add(child);
                }

                foreach (var child in tempChildrenB)
                {
                    ShowMatrixA.Children.Add(child);
                }

            }
            else
            {
                StackPanel result = new StackPanel();
                StackPanel first = new StackPanel();
                StackPanel second = new StackPanel();
                StackPanel third = new StackPanel();
                result.Orientation = Orientation.Horizontal;
                result.Margin = new Thickness(45);
                result.Background = new SolidColorBrush(Colors.White);
                TextBlock theNew = new TextBlock();
                theNew.Text = (idOfResult++).ToString() + ". ";
                theNew.FontSize = 35;
                result.Children.Add(theNew);
                foreach (StackPanel row in ShowMatrixA.Children)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    foreach (TextBox col in row.Children)
                    {
                        TextBox tB = new TextBox();
                        tB.Width = 40;
                        tB.Height = 40;
                        tB.Margin = new Thickness(10);
                        tB.IsEnabled = false;
                        tB.TextAlignment = TextAlignment.Center;
                        tB.Text = col.Text;
                        stackPanel.Children.Add(tB);
                    }
                    first.Children.Add(stackPanel);
                }
                result.Children.Add(first);
                TextBlock firstAndHalf = new TextBlock();
                TextBlock secondAndHalf = new TextBlock();
                firstAndHalf.Margin = new Thickness(30,10+nA/2*60,30,0);
                firstAndHalf.FontSize = 18;
                firstAndHalf.Text = (operation.StartsWith("A") ? "+" : (operation.StartsWith("M"))?"X":"-");
                result.Children.Add(firstAndHalf);
                secondAndHalf.Margin = new Thickness(30, 10 + nA / 2 * 60, 30, 0);
                secondAndHalf.FontSize = 18;
                secondAndHalf.Text = "=";
                foreach (StackPanel row in ShowMatrixB.Children)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    foreach (TextBox col in row.Children)
                    {
                        TextBox tB = new TextBox();
                        tB.Width = 40;
                        tB.Height = 40;
                        tB.Margin = new Thickness(10);
                        tB.IsEnabled = false;
                        tB.TextAlignment = TextAlignment.Center;
                        tB.Text = col.Text;
                        stackPanel.Children.Add(tB);
                    }
                    second.Children.Add(stackPanel);
                }
                result.Children.Add(second);
                result.Children.Add(secondAndHalf);
                StackPanel MatrixA = ShowMatrixA;
                StackPanel MatrixB = ShowMatrixB;
                TextBox[,] data = new TextBox[nA, mB];
                if ((nA != nB || mA != mB) && (operation.StartsWith("Add") || operation.StartsWith("Sub"))){
                    MessageBox.Show("Sizes must be the Same", "Invalid Input");
                }
                else
                {
                    if (operation.StartsWith("M"))
                    {
                        if (nA != mB)
                        {
                            MessageBox.Show("Rows number of A matrix must be equal with Column number on B", "Multiply Error");
                        }
                        else
                        {
                            double[,] dataA = new double[nA, mA];
                            double[,] dataB = new double[nB, mB];
                            int i = 0;
                            foreach (StackPanel row in MatrixA.Children)
                            {
                                foreach (TextBox col in row.Children)
                                {
                                    dataA[i / mA, i % mA] = new double();
                                    if (col.Text == "") col.Text = "0";
                                    if (col.Text.Contains(".")) col.Text.Replace('.', ',');
                                    dataA[i / mA, i % mA] = double.Parse(col.Text);
                                    i++;
                                }
                            }
                            i = 0;
                            foreach (StackPanel row in MatrixB.Children)
                            {
                                foreach (TextBox col in row.Children)
                                {
                                    dataB[i / mB, i % mB] = new double();
                                    if (col.Text == "") col.Text = "0";
                                    if (col.Text.Contains(".")) col.Text.Replace('.', ',');
                                    dataB[i / mB, i % mB] = double.Parse(col.Text);
                                    i++;
                                }
                            }
                            for (int row = 0; row < nA; ++row)
                            {
                                StackPanel stackPanel = new StackPanel();
                                stackPanel.Orientation = Orientation.Horizontal;
                                for (int col = 0; col < mB; ++col)
                                {
//                                    MessageBox.Show(row.ToString() + " " + col.ToString(), "asdsad");
                                    double sum = 0;
                                    for (int k = 0; k < nA; ++k)
                                    {
                                        sum += dataA[row, k] * dataB[k, col];
                                    }
                                    TextBox textBox = new TextBox();
                                    textBox.Width = 40;
                                    textBox.Height = 40;
                                    textBox.Margin = new Thickness(10);
                                    textBox.IsEnabled = false;
                                    textBox.TextAlignment = TextAlignment.Center;
                                    textBox.Text = sum.ToString("F2");
                                    stackPanel.Children.Add(textBox);
                                }
                                third.Children.Add((StackPanel)stackPanel);
                            }
                            result.Children.Add(third);
                            ShowResults.Children.Insert(0,result);
                            OpenResultPane();
                        }
                    }
                    else
                    {
                        int i = 0;
                        foreach (StackPanel row in MatrixA.Children)
                        {
                            foreach (TextBox col in row.Children)
                            {
                                data[i / mA, i % mA] = new TextBox();
                                data[i / mA, i % mA].Width = 25;
                                data[i / mA, i % mA].Height = 25;
                                data[i / mA, i % mA].Margin = new Thickness(4);
                                col.Text.Replace('.', ',');
                                if (col.Text == "") col.Text = "0";
                                data[i / mA, i % mA].Text = col.Text;
                                i++;
                            }
                        }
                        i = 0;
                        foreach (StackPanel row in MatrixB.Children)
                        {
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Orientation = Orientation.Horizontal;

                            foreach (TextBox col in row.Children)
                            {
                                TextBox tB = new TextBox();
                                tB.Width = 40;
                                tB.Height = 40;
                                tB.Margin = new Thickness(10);
                                tB.IsEnabled = false;
                                tB.TextAlignment = TextAlignment.Center;
                                if (col.Text == "") col.Text = "0";
                                col.Text.Replace('.', ',');
                                if (operation.StartsWith("Add"))
                                {
                                    data[i / mA, i % mA].Text = (double.Parse(data[i / mB, i % mB].Text) + double.Parse(col.Text)).ToString();

                                }
                                else
                                {
                                    data[i / mA, i % mA].Text = (double.Parse(data[i / mB, i % mB].Text) - double.Parse(col.Text)).ToString();
                                }
                                tB.Text = string.Format("{0:0.00}", double.Parse(data[i / mA, i % mA].Text)); ;
                                stackPanel.Children.Add(tB);
                                i++;
                            }
                            third.Children.Add(stackPanel);
                        }
                        result.Children.Add(third);
                        ShowResults.Children.Insert(0,result);
                        OpenResultPane();
                    }
                }

            }
        }//need changes
        private void OpenResultPane()
        {
            ClearAllPanels();

            ResultPanel.Visibility = Visibility.Visible;
            BackToMenu.Visibility = Visibility.Visible;
        }
        private void YourKeyDownHandler(object sender, KeyEventArgs e)
        {
            // Handle the key event for the button here
            if(Calc.Visibility == (Visibility.Visible))
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        CalculateResult();
                        break;
                    case Key.Escape:
                        ClearFull();
                        break;
                    case Key.D1:
                    case Key.NumPad1:
                        currentNumber += "1";
                        break;
                    case Key.D0:
                    case Key.NumPad0:
                        currentNumber += "0";
                        break;
                    case Key.D2:
                    case Key.NumPad2:
                        currentNumber += "2";
                        break;
                    case Key.D3:
                    case Key.NumPad3:
                        currentNumber += "3";
                        break;
                    case Key.D4:
                    case Key.NumPad4:
                        currentNumber += "4";
                        break;
                    case Key.D5:
                    case Key.NumPad5:
                        currentNumber += "5";
                        break;
                    case Key.D6:
                    case Key.NumPad6:
                        currentNumber += "6";
                        break;
                    case Key.D7:
                    case Key.NumPad7:
                        currentNumber += "7";
                        break;
                    case Key.D8:
                    case Key.NumPad8:
                        currentNumber += "8";
                        break;
                    case Key.D9:
                    case Key.NumPad9:
                        currentNumber += "9";
                        break;
                    case Key.C:
                        ClearInput();
                        break;
                    case Key.Back:
                        RemoveLast();
                        break;
                    case Key.Multiply:
                        HandleOperator("*");
                        break;
                    case Key.Divide:
                        HandleOperator("/");
                        break;
                    case Key.Add:
                        HandleOperator("+");
                        break;
                    case Key.Subtract:
                        HandleOperator("-");
                        break;
                    case Key.Decimal:
                        currentNumber += ',';
                        break;
                }
                NotifyPropertyChanged(nameof(CurrentNumber));
            }
            // Add more cases for other keys as needed
        }     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();

            if (double.TryParse(content, out double number))
            {
                CurrentNumber += content;
            }
            else if (content == ".")
            {
                if (!CurrentNumber.Contains(","))
                {
                    CurrentNumber += ",";
                }
            }
            else if (content == "C")
            {
                ClearInput();
            }
            else if (content == "=")
            {
                CalculateResult();
            }
            else
            {
                HandleOperator(content);
            }
        }
        private void HandleOperator(string op)
        {

            if (opera.Equals(string.Empty))
            {
                if (l.Length == 0)
                    l = currentNumber;
            }
            else
            {
                if(l.Length == 0)
                {

                }
                else
                {
                    if (r.Length == 0)
                    {
                        r = currentNumber;
                    }
                    if(r.Length == 0)
                    {
                        switch (opera)
                        {
                            case "/":
                                l = (double.Parse(l) / double.Parse(r)).ToString();
                                break;
                            case "*":
                                l = (double.Parse(l) * double.Parse(r)).ToString();
                                break;
                            case "+":
                                l = (double.Parse(l) + double.Parse(r)).ToString();
                                break;
                            case "-":
                                l = (double.Parse(l) - double.Parse(r)).ToString();
                                break;
                        }
                        r = string.Empty;

                    }
                }
            }
            currentNumber = string.Empty;
            opera = op;
        }
        private void CalculateResult()
        {
            string result = "";
            if(opera.Length == 0)
            {

            }
            else
            {
                if (l.Length == 0)
                {
                    l = currentNumber;
                }
                else
                {
                    if (r.Length == 0)
                    {
                        if (currentNumber.Length == 0) r = l;
                        else r = currentNumber;
                    }
                    switch (opera)
                    {
                        case "/":
                            result = (double.Parse(l) / double.Parse(r)).ToString();
                            break;
                        case "*":
                            result = (double.Parse(l) * double.Parse(r)).ToString();
                            break;
                        case "-":
                            result = (double.Parse(l) - double.Parse(r)).ToString();
                            break;
                        case "+":
                            result = (double.Parse(l) + double.Parse(r)).ToString();
                            break;
                    }
                }
                currentNumber = result.ToString();
                l = result.ToString(); r = string.Empty;
                opera = string.Empty;
                NotifyPropertyChanged(nameof(CurrentNumber));
            }
        }
        private void ClearInput()
        {
            // Clear the input and update the CurrentNumber
            // Similar to how you handle the Clear button in the OnButtonClick event handler
            // You need to implement this method according to your specific logic
            CurrentNumber = string.Empty;
        }
        private void ClearFull()
        {
            currentNumber= string.Empty;
            l = string.Empty;
            r = string.Empty;
            opera= string.Empty;
            NotifyPropertyChanged(nameof(CurrentNumber));
        }
        private void RemoveLast()
        {
            if(currentNumber.Length > 0)
                currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
        }
        private void ChangeCalc(object sender , RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string s = button.Name;
            ClearAllPanels();
            if (s.StartsWith("Matrix"))
            {
                MatrixCalc.Visibility = Visibility.Visible;
                MatrixA.Visibility = Visibility.Visible;;
                BackToMenu.Visibility = Visibility.Visible;
                GoToResult.Visibility = Visibility.Visible;
            }
            else if(s.StartsWith("Sy"))
            {
                SOE.Visibility = Visibility.Visible;
                BackToMenu.Visibility = Visibility.Visible;
                GoToResult.Visibility = Visibility.Visible;
            }
            else if (s.StartsWith("BackToM"))
            {
                MenuPanel.Visibility = Visibility.Visible;
                GoToResult.Visibility = Visibility.Visible;
            }
            else if (s.StartsWith("GoToB"))
            {
                MatrixCalc.Visibility = Visibility.Visible;
                MatrixB.Visibility = Visibility.Visible;
                GoToA.Visibility = Visibility.Visible;
                GoToResult.Visibility = Visibility.Visible;
            }
            else if (s.StartsWith("GoToA"))
            {
                MatrixCalc.Visibility = Visibility.Visible;
                MatrixA.Visibility = Visibility.Visible;
                BackToMenu.Visibility = Visibility.Visible;
                GoToResult.Visibility = Visibility.Visible;
            }
            else if (s.StartsWith("GoToRes"))
            {
                ResultPanel.Visibility = Visibility.Visible;
                BackToMenu.Visibility = Visibility.Visible;
                ClearResultPane.Visibility = Visibility.Visible;
            }
            else if(s.StartsWith("ClearResult"))
            {

                ShowResults.Children.Clear();
                MenuPanel.Visibility = Visibility.Visible;
                GoToResult.Visibility = Visibility.Visible;
                idOfResult = 1;
            }
            else
            {
                Calc.Visibility = Visibility.Visible;
                BackToMenu.Visibility = Visibility.Visible;
            }
        }
        private void ClearAllPanels()
        {
            MatrixCalc.Visibility = Visibility.Collapsed;
            SOE.Visibility = Visibility.Collapsed;
            Calc.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Collapsed;
            GoToA.Visibility = Visibility.Collapsed;
            BackToMenu.Visibility = Visibility.Collapsed;
            GoToResult.Visibility = Visibility.Collapsed;
            MatrixB.Visibility = Visibility.Collapsed;
            MatrixA.Visibility = Visibility.Collapsed;
            ResultPanel.Visibility = Visibility.Collapsed;
            ClearResultPane.Visibility = Visibility.Collapsed;
        }
    }
}