using System;
using System.Globalization;
using System.Windows.Forms;

namespace Examen_Cafeteria_SuNombre
{
    public partial class frmPedidos : Form
    {
        private decimal subtotalAcumulado = 0m;
        private readonly CultureInfo rdCulture = CultureInfo.GetCultureInfo("es-DO");

        public frmPedidos()
        {
            InitializeComponent();
        }

        private string FormatearRD(decimal valor)
        {
            return "RD$ " + valor.ToString("N2", rdCulture);
        }

        private string FormatearNumero(decimal valor)
        {
            return valor.ToString("N2", rdCulture);
        }

        private string FormatearEncabezado()
        {
            return "Cant".PadRight(6) +
                   "Producto".PadRight(24) +
                   "Precio".PadLeft(10) +
                   "Total".PadLeft(12);
        }

        private string FormatearLineaPedido(int cantidad, string productoConTamano, decimal precioUnitario, decimal totalLinea)
        {
            string colCant = cantidad.ToString().PadRight(6);
            string colProd = productoConTamano.Length > 24
                ? productoConTamano.Substring(0, 24)
                : productoConTamano.PadRight(24);
            string colPrecio = FormatearNumero(precioUnitario).PadLeft(10);
            string colTotal = FormatearNumero(totalLinea).PadLeft(12);

            return colCant + colProd + colPrecio + colTotal;
        }

        private void ReiniciarControlesProducto()
        {
            textBox2.Clear();            // Producto
            textBox3.Clear();            // Precio
            numericUpDown1.Value = 1;    // Cantidad (1 a 20)
            radioButton1.Checked = true; // Normal por defecto
            checkBox2.Checked = false;   // Porción extra
            textBox2.Focus();
        }

        private void ActualizarValoresTotales(decimal subtotal, decimal descuento, decimal empaque, decimal itbis, decimal total)
        {
            label12.Text = FormatearRD(subtotal);
            label13.Text = FormatearRD(descuento);
            label14.Text = FormatearRD(empaque);
            label15.Text = FormatearRD(itbis);
            label16.Text = FormatearRD(total);
        }

        private void InicializarFormulario()
        {
            textBox1.Clear();              // Cliente
            dateTimePicker1.Value = DateTime.Today;
            checkBox1.Checked = false;     // Cliente frecuente
            radioButton3.Checked = true;   // Local por defecto

            listBox1.Items.Clear();
            label6.Text = FormatearEncabezado();

            subtotalAcumulado = 0m;
            ActualizarValoresTotales(0m, 0m, 0m, 0m, 0m);
            ReiniciarControlesProducto();
            textBox1.Focus();
        }

        // Evento Load del formulario
        private void frmPedidos_Load(object sender, EventArgs e)
        {
            InicializarFormulario();
        }

        // Botón Agregar al pedido (button1)
        private void button1_Click(object sender, EventArgs e)
        {
            string nombreProducto = textBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                MessageBox.Show("Debe ingresar el nombre del producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (!decimal.TryParse(textBox3.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precioDigitado) &&
                !decimal.TryParse(textBox3.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out precioDigitado))
            {
                MessageBox.Show("Ingrese un precio válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

            if (precioDigitado <= 0m)
            {
                MessageBox.Show("El precio debe ser mayor que cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

            // Regla 1: +25 si es tamaño grande, +30 si lleva porción extra
            decimal precioUnitario = precioDigitado;
            string tamanoTexto = "Normal";

            if (radioButton2.Checked)
            {
                precioUnitario += 25m;
                tamanoTexto = "Grande";
            }

            if (checkBox2.Checked)
            {
                precioUnitario += 30m;
            }

            // Regla 2: total de línea = precio unitario * cantidad
            int cantidad = (int)numericUpDown1.Value;
            decimal totalLinea = precioUnitario * cantidad;

            string productoFormateado = $"{nombreProducto} ({tamanoTexto})";
            string linea = FormatearLineaPedido(cantidad, productoFormateado, precioUnitario, totalLinea);

            listBox1.Items.Add(linea);

            subtotalAcumulado += totalLinea;
            label12.Text = FormatearRD(subtotalAcumulado);

            ReiniciarControlesProducto();
        }

        // Botón Calcular total (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No hay productos en el pedido para calcular.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal subtotal = subtotalAcumulado;

            // Regla 3: 10% del subtotal si el cliente es frecuente
            decimal descuento = checkBox1.Checked ? subtotal * 0.10m : 0m;

            // Regla 4: RD$20 una sola vez por pedido si es "Para llevar"
            decimal empaque = radioButton4.Checked ? 20m : 0m;

            // Regla 5: base imponible = subtotal - descuento + empaque
            decimal baseImponible = subtotal - descuento + empaque;

            // Regla 6: ITBIS = 18% de la base imponible; Total = base imponible + ITBIS
            decimal itbis = baseImponible * 0.18m;
            decimal total = baseImponible + itbis;

            ActualizarValoresTotales(subtotal, descuento, empaque, itbis, total);
        }

        // Botón Nuevo pedido / Limpiar (button3)
        private void button3_Click(object sender, EventArgs e)
        {
            InicializarFormulario();
        }

        // Botón Salir (button4)
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}