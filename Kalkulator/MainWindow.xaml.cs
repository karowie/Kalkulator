using Microsoft.SqlServer.Server;
using System;
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

namespace Kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Globalization.CultureInfo EnglishCulture = new System.Globalization.CultureInfo("en-EN");
        // Lista przechowująca liczby do obliczeń
        private List<Double> liczby = new List<Double>();
        // Lista przechowująca znaki do obliczeń
        private List<char> dzialania = new List<char>();
        // Zwracany wynik
        private double wynik = 0;

        public MainWindow()
        {
            InitializeComponent();
            

        }

        // Funkcja dopisująca po wpisaniu
        private void buttonNumery_Click(object sender, RoutedEventArgs e)
        {
            LabelBlad.Content = "";
            var button = sender as Button;
            // Zabezpieczenie przed wpisaniem liczby typu 06 lub -06
            if ((textBoxEkran.Text.Equals("0")) || (textBoxEkran.Text.Equals("-0")))
            {
                if (!button.Content.ToString().Equals("0"))
                    textBoxEkran.Text = button.Content.ToString();
            }
            else
                textBoxEkran.Text += button.Content.ToString();            
        }

        private void buttonDzialania_Click(object sender, RoutedEventArgs e)
        {
            LabelBlad.Content = "";
            var button = sender as Button;

            //sprząta po starych działaniach
            if ((!(LabelWypisz.Text.ToString().Equals(""))) && (LabelWypisz.Text.ToString().Last() == '='))
                LabelWypisz.Text = "";

            if (!textBoxEkran.Text.Equals(""))
            {
                if ((!(LabelWypisz.Text.ToString().Equals("")))&&(LabelWypisz.Text.ToString().Last() == '/'))
                {
                    if (textBoxEkran.Text == "0")
                    {
                        textBoxEkran.Text = "";
                        LabelBlad.Content = "Nie można dzielić przez 0! Wprowadz dane raz jeszcze.";
                    }
                    else if ((button.Content.ToString().Equals("+")) && (textBoxEkran.Text.Equals("-")))
                    {
                        textBoxEkran.Text = "";
                    }
                    else if(!textBoxEkran.Text.Equals("-"))
                    {
                        if(textBoxEkran.Text.StartsWith("-"))
                        {
                            try
                            {
                                liczby.Add(Double.Parse(textBoxEkran.Text));
                                LabelWypisz.Text += "(" + liczby.Last() + ")" + button.Content.ToString();
                            }
                            catch
                            {
                                liczby.Add(1);
                                LabelWypisz.Text += 1 + button.Content.ToString();
                                LabelBlad.Content = "Błędne dane. Wstawiam 1!";
                            }
                            dzialania.Add(Convert.ToChar(button.Content.ToString()));
                            textBoxEkran.Text = "";
                        }
                        else
                        {
                            try
                            {
                                liczby.Add(Double.Parse(textBoxEkran.Text));
                                LabelWypisz.Text += liczby.Last() + button.Content.ToString();
                            }
                            catch
                            {
                                liczby.Add(1);
                                LabelWypisz.Text += 1 + button.Content.ToString();
                                LabelBlad.Content = "Błędne dane. Wstawiam 1!";

                            }
                            dzialania.Add(Convert.ToChar(button.Content.ToString()));
                            textBoxEkran.Text = "";
                        }
                        
                    }
                }
                else if ((button.Content.ToString().Equals("+")) && (textBoxEkran.Text.Equals("-")))
                    textBoxEkran.Text = "";
                else if (!textBoxEkran.Text.Equals("-"))
                {
                    if (textBoxEkran.Text.StartsWith("-"))
                    {
                        try
                        {
                            liczby.Add(Double.Parse(textBoxEkran.Text));
                            LabelWypisz.Text += "(" + liczby.Last() + ")" + button.Content.ToString();
                        }
                        catch
                        {
                            liczby.Add(1);
                            LabelWypisz.Text += 1 + button.Content.ToString();
                            textBoxEkran.Text = "";

                        }
                        dzialania.Add(Convert.ToChar(button.Content.ToString()));
                        textBoxEkran.Text = "";
                    }
                    else
                    {
                        try
                        {
                            liczby.Add(Double.Parse(textBoxEkran.Text));
                            LabelWypisz.Text += liczby.Last() + button.Content.ToString();
                        }
                        catch
                        {
                            liczby.Add(1);
                            LabelWypisz.Text += 1 + button.Content.ToString();
                            LabelBlad.Content = "Błędne dane. Wstawiam 1!";
                        }
                        
                        dzialania.Add(Convert.ToChar(button.Content.ToString()));
                        textBoxEkran.Text = "";
                    }
                }
            }
            else if(button.Content.ToString().Equals("-"))
                textBoxEkran.Text = "-"; 
        }

        //Funkcja działająca po wciśnięciu przecinka
        private void ButtonPrzecinek_Click(object sender, RoutedEventArgs e)
        {
            LabelBlad.Content = "";
            if (!textBoxEkran.Text.Equals(""))
            {
                if (!textBoxEkran.Text.Contains(","))
                {
                    if (!textBoxEkran.Text.Equals("-"))
                    {
                        textBoxEkran.Text += ",";
                    }
                    else //Gdy na początku jest -, zrób -0.
                    {
                        textBoxEkran.Text += "0,";
                    }
                }
            }
            else //Gdy puste pole zrób 0.
                textBoxEkran.Text += "0,";
        }

        //Usuwanie aktualnie wprowadzanej wartości
        private void ButtonC_Click(object sender, RoutedEventArgs e)
        {
            LabelBlad.Content = "";
            textBoxEkran.Text = "";
        }

        //Usuwanie całego działania
        private void ButtonCE_Click(object sender, RoutedEventArgs e)
        {
            LabelBlad.Content = "";
            textBoxEkran.Text = "";
            LabelWypisz.Text = "";
            liczby = new List<double>();
            dzialania = new List<char>();
        }

        //Usuwanie pojedynczego znak
        private void ButtonUsun_Click(object sender, RoutedEventArgs e)
        {
            LabelBlad.Content = "";
            if (!textBoxEkran.Text.Equals(""))
                textBoxEkran.Text=textBoxEkran.Text.Remove(textBoxEkran.Text.Length-1,1);
        }

        private void ButtonWynik_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Boolean przerwij = false;
            LabelBlad.Content = "";
            if(LabelWypisz.Text.Contains('='))
                LabelWypisz.Text = "";

            if ((textBoxEkran.Text.Equals(""))&&((LabelWypisz.Text.EndsWith("-"))|| (LabelWypisz.Text.EndsWith("+"))|| (LabelWypisz.Text.EndsWith("/")) || (LabelWypisz.Text.EndsWith("*"))))
            {
                LabelWypisz.Text = LabelWypisz.Text.Remove(LabelWypisz.Text.Length - 1);
                LabelWypisz.Text += button.Content.ToString();
                dzialania.RemoveAt(dzialania.Count-1);
            }

            if ((!textBoxEkran.Text.Equals("")))
            {
                try
                {
                    liczby.Add(Double.Parse(textBoxEkran.Text));
                    if (textBoxEkran.Text.StartsWith("-"))
                        LabelWypisz.Text += "(" + liczby.Last() + ")" + button.Content.ToString();
                    else
                        LabelWypisz.Text += liczby.Last() + button.Content.ToString();
                }
                catch
                {
                    liczby.Add(1);
                    LabelWypisz.Text += 1 + button.Content.ToString();
                    LabelBlad.Content = "Błędne dane. Wstawiam 1!";
                }

                textBoxEkran.Text = "";
            }
                if (!LabelWypisz.Text.ToString().Equals(""))
                {
                    wynik = liczby[0];
                    if (dzialania.Count > 0)
                    {
                        // bez kolejnosci dzialan
                        //for (int j = 0; j < dzialania.Count; j++)
                        //{
                        //    if (dzialania[j] == '+')
                        //    {
                        //        wynik += liczby[j+1];
                        //    }
                        //    else if (dzialania[j] == '-')
                        //    {
                        //        wynik = wynik - liczby[j+1];
                        //    }
                        //    else if (dzialania[j] == '/')
                        //    {
                        //        wynik = wynik / liczby[j+1];
                        //    }
                        //    else
                        //    {
                        //        wynik = wynik * liczby[j+1];
                        //    }
                        //}


                        //dzialania w kolejnosci wykonywania dzialan
                        for (int i = 0; i < dzialania.Count; i++)
                        {
                            if(dzialania[i] == '*')
                            {
                                liczby[i] = liczby[i] * liczby[i+1];
                                liczby.RemoveAt(i+1);
                                dzialania.RemoveAt(i);
                                i--;
                            }
                            else if(dzialania[i] == '/')
                            {
                                if(liczby[i + 1]==0)
                                {
                                    wynik = 0;
                                    LabelBlad.Content = "Wystąpiło dzielenie przez 0! Zwracam 0";
                                    przerwij = true;
                                    break;
                                }
                                else
                                {
                                    liczby[i] = liczby[i] / liczby[i + 1];
                                    liczby.RemoveAt(i + 1);
                                    dzialania.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                        if(!przerwij)
                        {
                            for (int i = 0; i < dzialania.Count; i++)
                            {
                                if (dzialania[i] == '+')
                                {
                                    liczby[i] = liczby[i] + liczby[i + 1];
                                    liczby.RemoveAt(i + 1);
                                    dzialania.RemoveAt(i);
                                    i--;
                                }
                                else if (dzialania[i] == '-')
                                {
                                    liczby[i] = liczby[i] - liczby[i + 1];
                                    liczby.RemoveAt(i + 1);
                                    dzialania.RemoveAt(i);
                                    i--;
                                }
                            }
                            wynik = liczby[0];
                        }  
                    }
                    textBoxEkran.Text = Convert.ToString(wynik);
                    liczby = new List<double>();
                    dzialania = new List<char>();
                }
            //}
            
        }

    }
}
