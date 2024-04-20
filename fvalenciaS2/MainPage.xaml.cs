
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace fvalenciaS2
{
    public partial class MainPage : ContentPage
    {
        string estudiante, estado, fecha;
        double nota1, examen1, nota2, examen2, notaparcial1, notaparcial2, notafinal;
        public MainPage()
        {
            InitializeComponent();
        }

        public void Main(string[] args)
        {
            double resultado1, resultado2, resultado3;
            string resultado4;
            ObtenerResultados(out resultado1, out resultado2, out resultado3, out resultado4);
        }

        public void ObtenerResultados(out double resultado1, out double resultado2, out double resultado3, out string resultado4)
        {

            nota1 = Convert.ToInt32(TxtNotaSeguimiento1.Text) * 0.3;
            examen1 = Convert.ToInt32(TxtExamen1.Text) * 0.2;
            notaparcial1 = nota1 + examen1;

            nota2 = Convert.ToInt32(TxtNotaSeguimiento2.Text) * 0.3;
            examen2 = Convert.ToInt32(TxtExamen2.Text) * 0.2;
            notaparcial2 = nota2 + examen2;

            notafinal = notaparcial1 + notaparcial2;

            resultado1 = Math.Round(notaparcial1, 2);
            resultado2 = Math.Round(notaparcial2, 2);
            resultado3 = Math.Round(notafinal, 2);

            if (resultado3 >= 7)
            {
                resultado4 = "Aprobado";
            }
            else if (resultado3 >= 5 && resultado3 <= 6.9)
            {
                resultado4 = "Complementario";
            }
            else resultado4 = "REPROBADO";
        }


        private async void CalcularBtn_Clicked(object sender, EventArgs e)
        {
            estudiante = PEstudiantes.SelectedItem?.ToString();

            if (estudiante == null)
            {
                DisplayAlert("Error", "Por favor, selecciona un estudiante", "Aceptar");
            }
            else
            {
                fecha = dpFecha.Date.ToString("MM/dd/yyyy");
                double resultado1, resultado2, resultado3;

                ObtenerResultados(out resultado1, out resultado2, out resultado3, out estado);

                await DisplayAlert("Calificaciones", "Nombre: " + estudiante + "\n" +
                 "Fecha: " + fecha + "\n" +
                 "Nota Parcial 1:   " + resultado1 + "\n" +
                 "Nota Parcial 2:   " + resultado2 + "\n" +
                 "Nota Final:   " + resultado3 + "\n" +
                 "Estado:   " + estado,
                 "Cerrar");
            }
        }

        private void LimpiarCajasDeTexto(object sender, EventArgs e)
        {
            TxtExamen1.Text = string.Empty;
            TxtExamen2.Text = string.Empty;
            TxtNotaSeguimiento1.Text = string.Empty;
            TxtNotaSeguimiento2.Text = string.Empty;
            PEstudiantes.SelectedIndex = -1;
            dpFecha.Date = new DateTime(2023, 01, 01);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumericRange((Entry)sender, 0, 10);
        }

        void ValidateNumericRange(Entry entry, double minValue, double maxValue)
        {
            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (!double.TryParse(entry.Text, out double number))
                {
                    entry.Text = string.Empty;
                }
                else
                {
                    if (number < minValue || number > maxValue)
                    {
                        entry.Text = string.Empty;
                    }
                }
            }
        }

    }
}
