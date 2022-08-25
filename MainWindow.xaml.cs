using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace ElAhorcado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //variables del juego
        private Char[] array;
        private string[] palabraTapada;
        private string letra;
        private int acierto = 0; //Chequea acierto. si no aciertas ninguna letra es 0. Si aciertas alguna es 1
        private readonly int fallosMax = 6; //Fallos maximos permitidos
        private int fallosCheq = 0; //fallos incrementales si falla letra
        private int numAciertos = 0; //numero de aciertos de letras
        private int numLetras = 0;// numero de letras a acertar



        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIntro_Click(object sender, RoutedEventArgs e)
        {
            grdPalabra.Visibility = Visibility.Visible;
            //grdJuego.Visibility = Visibility.Hidden;
            

        }

        private void btnIntroPalabra_Click(object sender, RoutedEventArgs e)
        {
            grdPalabra.Visibility = Visibility.Visible;
            acierto = 0;
            array = boxIntroPalabra.Text.ToCharArray();
            palabraTapada = new string[array.Length];
            txtLineas.Text = "";
            fallosCheq = 0;
            if (array.Length == 0)
            {
                MessageBox.Show("Primero introduce una palabra", "ATENCIÓN");
            }
            else
            {
                grdPalabra.Visibility = Visibility.Hidden;
                grdJuego.Visibility = Visibility.Visible;
                pintaLineas();
            }
        }

        private void btnChequea_Click(object sender, RoutedEventArgs e)
        {
            acierto = 0;
            if (boxIntroLetra.Text.ToString() == "")
            {
                MessageBox.Show("Introduce un letra primero!!", "ATENCIÓN");
            }
            else
            {
                letra = boxIntroLetra.Text.ToString();
                for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
                {
                    if (array[i].ToString().Equals(letra))
                    {
                        acierto = 1;
                        palabraTapada[i] = array[i].ToString();
                        pintaLineas();
                        numAciertos++;
                        SystemSounds.Asterisk.Play();
                        if (numAciertos == numLetras)
                        {
                            ganar();
                        }
                    }
                }
                if (acierto == 0)
                {
                    fallosCheq++;
                    letraFallada(fallosCheq, letra);
                    SystemSounds.Hand.Play();
                    if (fallosCheq >= fallosMax)
                    {
                        gameOver();
                    }
                }
            }
            boxIntroLetra.Text = "";
        }

        private void pintaLineas()
        {
            if (acierto == 0)
            {
                for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
                {
                    if (Char.IsLetter(array[i]))
                    //if (array[i].ToString() != " ")
                    {
                        palabraTapada[i] = " _ ";
                        numLetras++;
                    }
                    else if(Char.IsSeparator(array[i]))
                    {
                        palabraTapada[i] ="   ";
                    }
                    else
                    {
                        palabraTapada[i] = array[i].ToString();
                    }
                    txtLineas.Text += palabraTapada[i].ToString();
                }
            }
            else
            {
                txtLineas.Text = "";
                for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
                {

                    txtLineas.Text += palabraTapada[i].ToString();
                }

            }
        }

        private void letraFallada(int numFallo, string letraFallada)
        {
            switch (numFallo)
            {
                case 1:
                    boxFalloUno.Text = letraFallada;
                    imgAhorcado.Source = new BitmapImage(new Uri("Imagenes/dib2.png", UriKind.Relative));
                    break;
                case 2:
                    boxFalloDos.Text = letraFallada;
                    imgAhorcado.Source = new BitmapImage(new Uri("Imagenes/dib3.png", UriKind.Relative));
                    break;
                case 3:
                    boxFalloTres.Text = letraFallada;
                    imgAhorcado.Source = new BitmapImage(new Uri("Imagenes/dib4.png", UriKind.Relative));
                    break;
                case 4:
                    boxFalloCuatro.Text = letraFallada;
                    imgAhorcado.Source = new BitmapImage(new Uri("Imagenes/dib5.png", UriKind.Relative));
                    break;
                case 5:
                    boxFalloCinco.Text = letraFallada;
                    imgAhorcado.Source = new BitmapImage(new Uri("Imagenes/dib6.png", UriKind.Relative));
                    break;
                case 6:
                    boxFalloSeis.Text = letraFallada;
                    imgAhorcado.Source = new BitmapImage(new Uri("Imagenes/dib7.png", UriKind.Relative));
                    break;
                default:
                    txtRecibido.Text = "Algo falló";
                    break;
            }
        }

        private void btnRendirse_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Hand.Play();
            gameOver();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            //mostrar palabra buscada y reiniciar
            grdFinal.Visibility = Visibility.Hidden;
            grdJuego.Visibility = Visibility.Hidden;
            grdGanar.Visibility = Visibility.Hidden;
            grdOver.Visibility = Visibility.Hidden;
            grdPalabra.Visibility = Visibility.Visible;
            //Reinicio de Parametros 
            boxIntroPalabra.Text = "";
            imgAhorcado.Source = new BitmapImage( new Uri("Imagenes/dib1.png", UriKind.Relative));
            boxFalloUno.Text = "";
            boxFalloDos.Text = "";
            boxFalloTres.Text = "";
            boxFalloCuatro.Text = "";
            boxFalloCinco.Text = "";
            boxFalloSeis.Text = "";
            numAciertos = 0;
            numLetras = 0;
        }

        private void gameOver()
        {
            //mostrar palabra buscada, mostrar grid game over y esperar click en sus botones
            grdFinal.Visibility = Visibility.Visible;
            grdOver.Visibility = Visibility.Visible;
            for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
            {
                palabraTapada[i] = array[i].ToString();
            }
            txtLineas.Text = "";
            acierto = 1; //Para entrar en el if que pinta las letras y no rayas
            pintaLineas();

        }

        private void ganar()
        {
            //mostrar grid ganar y esperar click en sus botones
            grdFinal.Visibility = Visibility.Visible;
            grdGanar.Visibility = Visibility.Visible;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            //Cerrar programa
            this.Close();
        }
    }
}
