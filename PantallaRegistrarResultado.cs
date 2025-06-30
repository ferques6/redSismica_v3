using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RedSismicaWinForms
{
    public partial class PantRegistrarResultado : Form
    {
        private GestorRegistrarResultado gestor;
        private List<EventoSismico> eventos;
        private EventoSismico eventoSeleccionado;

        // Controles visuales
        private ListBox listBoxEventos;
        private Button btnSeleccionar;
        private TextBox txtDetalles;
        private ListBox listBoxSeries;
        private ComboBox comboAccion;
        private Button btnRegistrar;
        private Label lblTitulo, lblSubEventos, lblDetalles, lblAccion, lblSeries, lblMensaje;

        public PantRegistrarResultado(GestorRegistrarResultado gestor)
        {
            this.gestor = gestor;
            InicializarControles();
            seleccionarRegistroResultadoRevisionManual(); // Llamo al flujo principal de la pantalla
        }

        private void InicializarControles()
        {
            this.Text = "Registro de revisi�n manual de eventos s�smicos";
            this.MinimumSize = new Size(1000, 550); // Tama�o m�nimo recomendado
            this.Size = new Size(1000, 550);
            this.BackColor = Color.WhiteSmoke;

            Font fuenteTitulo = new Font("Segoe UI", 16, FontStyle.Bold);
            Font fuenteNormal = new Font("Segoe UI", 11);

            // T�tulo principal
            lblTitulo = new Label()
            {
                Text = "Registrar resultado de revisi�n manual",
                Font = fuenteTitulo,
                Left = 20,
                Top = 10,
                Width = 700,
                ForeColor = Color.DarkSlateBlue,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Subt�tulo eventos
            lblSubEventos = new Label()
            {
                Text = "Eventos s�smicos auto detectados",
                Font = fuenteNormal,
                Left = 20,
                Top = 60,
                Width = 350,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            listBoxEventos = new ListBox()
            {
                Left = 20,
                Top = 90,
                Width = 370,
                Height = 140,
                Font = fuenteNormal,
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            btnSeleccionar = new Button()
            {
                Left = 410,
                Top = 90,
                Width = 140,
                Height = 38,
                Text = "Seleccionar",
                Font = fuenteNormal,
                BackColor = Color.FromArgb(100, 149, 237),
                ForeColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnSeleccionar.FlatStyle = FlatStyle.Flat;
            btnSeleccionar.FlatAppearance.BorderSize = 0;
            btnSeleccionar.Cursor = Cursors.Hand;
            btnSeleccionar.Click += btnSeleccionar_Click;

            // Detalles del evento
            lblDetalles = new Label()
            {
                Text = "Detalle del evento seleccionado",
                Font = fuenteNormal,
                Left = 20,
                Top = 240,
                Width = 400,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            txtDetalles = new TextBox()
            {
                Left = 20,
                Top = 270,
                Width = 530,
                Height = 90,
                Multiline = true,
                ReadOnly = true,
                Font = fuenteNormal,
                BackColor = Color.Gainsboro,
                ScrollBars = ScrollBars.Vertical,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Series temporales
            lblSeries = new Label()
            {
                Text = "Series temporales asociadas",
                Font = fuenteNormal,
                Left = 570,
                Top = 60,
                Width = 260,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            listBoxSeries = new ListBox()
            {
                Left = 570,
                Top = 90,
                Width = 320,
                Height = 270,
                Font = fuenteNormal,
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };

            // Acci�n
            lblAccion = new Label()
            {
                Text = "Acci�n a realizar sobre el evento:",
                Font = fuenteNormal,
                Left = 20,
                Top = 375,
                Width = 260,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };

            comboAccion = new ComboBox()
            {
                Left = 20,
                Top = 400,
                Width = 230,
                Font = fuenteNormal,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            comboAccion.Items.AddRange(new string[] { "Confirmar", "Rechazar", "Solicitar revisi�n a experto" });
            comboAccion.SelectedIndex = 0;

            btnRegistrar = new Button()
            {
                Left = 270,
                Top = 400,
                Width = 170,
                Height = 38,
                Text = "Registrar resultado",
                Font = fuenteNormal,
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.Cursor = Cursors.Hand;
            btnRegistrar.Click += btnRegistrar_Click;

            // Mensaje de feedback
            lblMensaje = new Label()
            {
                Left = 20,
                Top = 450,
                Width = 880,
                Height = 28,
                Font = fuenteNormal,
                ForeColor = Color.DarkGreen,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            // Agrego los controles al formulario
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblSubEventos);
            this.Controls.Add(listBoxEventos);
            this.Controls.Add(btnSeleccionar);
            this.Controls.Add(lblDetalles);
            this.Controls.Add(txtDetalles);
            this.Controls.Add(lblSeries);
            this.Controls.Add(listBoxSeries);
            this.Controls.Add(lblAccion);
            this.Controls.Add(comboAccion);
            this.Controls.Add(btnRegistrar);
            this.Controls.Add(lblMensaje);
        }

        // ...El resto de los m�todos de tu clase quedan igual...
        // === M�todos requeridos por el diagrama de clases/secuencia ===

        public void habilitarVentana()
        {
            this.Enabled = true;
        }

        public void mostrarEventosOrdenados(List<EventoSismico> eventos)
        {
            listBoxEventos.Items.Clear();
            foreach (var ev in eventos)
                listBoxEventos.Items.Add(ev.getResumenEvento());
        }

        public int pedirSeleccionEvento()
        {
            return listBoxEventos.SelectedIndex;
        }

        public void mostrarDatosSismicos(EventoSismico evento)
        {
            txtDetalles.Text = evento.getDatosEventoSismico();

            listBoxSeries.Items.Clear();
            foreach (var st in evento.getSerieTemporal())
            {
                listBoxSeries.Items.Add(
                    $"Estaci�n: {st.obtenerCodigoEstacion()} | Sism�grafo: {st.obtenerNombreSismografo()}"
                );
            }
        }

        public void mostrarOpcionModificarDatosEvSismico()
        {
            MessageBox.Show("�Desea modificar los datos del evento s�smico?", "Modificar datos", MessageBoxButtons.YesNo);
        }

        public string pedirAccionConEvento()
        {
            return comboAccion.SelectedItem?.ToString();
        }

        public void tomarAccionConEvento()
        {
            string accion = pedirAccionConEvento();
            gestor.tomarAccionConEvento(accion);
            lblMensaje.Text = "Evento actualizado correctamente.";
            lblMensaje.ForeColor = Color.DarkGreen;
            CargarEventos();
            comboAccion.SelectedIndex = 0;
            eventoSeleccionado = null;
        }

        public void tomarOpcionModificarDatosEvSismico()
        {
            MessageBox.Show("Funcionalidad de modificar datos pendiente de implementaci�n.", "Info");
        }

        public void tomarOpcionVisualizarSismografo()
        {
            if (eventoSeleccionado == null)
            {
                MessageBox.Show("Seleccion� un evento primero.", "Advertencia");
                return;
            }
            var series = eventoSeleccionado.getSerieTemporal();
            string msg = "";
            foreach (var st in series)
            {
                msg += $"Estaci�n: {st.obtenerCodigoEstacion()} - Sism�grafo: {st.obtenerNombreSismografo()}\n";
            }
            MessageBox.Show(msg, "Sism�grafos del evento seleccionado");
        }

        public void tomarSeleccionEvento()
        {
            int indice = pedirSeleccionEvento();
            if (indice < 0)
            {
                lblMensaje.Text = "Seleccion� un evento s�smico para ver detalles.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            eventoSeleccionado = eventos[indice];
            gestor.tomarSeleccionEvento(eventoSeleccionado);
            mostrarDatosSismicos(eventoSeleccionado);
            lblMensaje.Text = "";
        }

        public void seleccionarRegistroResultadoRevisionManual()
        {
            this.habilitarVentana();
            eventos = gestor.obtenerEventosSismicosAutoDetectados();
            mostrarEventosOrdenados(eventos);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            tomarSeleccionEvento();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (eventoSeleccionado == null)
            {
                lblMensaje.Text = "Seleccion� un evento y mostr� detalles primero.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            if (string.IsNullOrEmpty(pedirAccionConEvento()))
            {
                lblMensaje.Text = "Seleccion� una acci�n.";
                lblMensaje.ForeColor = Color.IndianRed;
                return;
            }
            tomarAccionConEvento();
        }

        private void CargarEventos()
        {
            eventos = gestor.obtenerEventosSismicosAutoDetectados();
            mostrarEventosOrdenados(eventos);
            txtDetalles.Text = "";
            listBoxSeries.Items.Clear();
            lblMensaje.Text = "";
        }
    }
}
