using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace EjercicioBd4o
{
    public partial class Form1 : Form
    {
        //IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
        //Pelicula unapeli = new Pelicula();
        //DVD copiadvd = new DVD();

        public Form1()
        {
            InitializeComponent();
            this.tabControl1.TabPages.Remove(this.tabPage2);//Oculta la pestaña DVD
            //this.tabControl1.TabPages.Remove(this.tabPage3);//Oculta la pestaña Alquiler
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabPage1);
            this.tabControl1.TabPages.Remove(this.tabPage2);
            this.tabControl1.TabPages.Remove(this.tabPage3);
            this.tabControl1.TabPages.Add(this.tabPage1);//Muestra la pestaña Cliente
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabPage1);
            this.tabControl1.TabPages.Remove(this.tabPage2);
            this.tabControl1.TabPages.Remove(this.tabPage3);
            this.tabControl1.TabPages.Add(this.tabPage2);//Muestra la pestaña Cita
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabPage1);
            this.tabControl1.TabPages.Remove(this.tabPage2);
            this.tabControl1.TabPages.Remove(this.tabPage3);
            this.tabControl1.TabPages.Add(this.tabPage3);//Muestra la pestaña VentanaOrden
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.ShowDialog();
        }
        private void ClearClienteFields()
        {
            TxtId_Cp.Clear();
            txtNombrep.Clear();
            txtCorreop.Clear();
            txtTelefonop.Clear();
            txtDireccionp.Clear();
        }
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtId_Cp.Text) &&
                !string.IsNullOrWhiteSpace(txtNombrep.Text) &&
                !string.IsNullOrWhiteSpace(txtCorreop.Text) &&
                !string.IsNullOrWhiteSpace(txtTelefonop.Text) &&
                !string.IsNullOrWhiteSpace(txtDireccionp.Text))
            {
                using (IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap"))
                {
                    // Verificar si el Id_C ya existe
                    IList<Cliente> resultados = BD.Query<Cliente>(c => c.Id_C == TxtId_Cp.Text);
                    if (resultados.Count > 0)
                    {
                        MessageBox.Show("Error: El ID ya existe. Introduzca uno diferente.");
                        return; // Sale del método si ya existe el ID
                    }

                    Cliente nuevoCliente = new Cliente
                    {
                        Id_C = TxtId_Cp.Text,
                        Nombre = txtNombrep.Text,
                        Correo = txtCorreop.Text,
                        Telefono = txtTelefonop.Text,
                        Direccion = txtDireccionp.Text
                    };

                    try
                    {
                        BD.Store(nuevoCliente);
                        BD.Commit();
                        MessageBox.Show("Éxito!!! Se guardó el cliente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }
                }

                ClearClienteFields();
            }
            else
            {
                MessageBox.Show("Hay campos vacíos.");
            }
        }



        private void button1_Click_1(object sender, EventArgs e)//modificar para cliente
        {
            if (TxtId_Cp.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string Id_C = TxtId_Cp.Text;
                Cliente c = new Cliente();
                c.Id_C = TxtId_Cp.Text;
                IList<Cliente> resultados = BD.Query<Cliente>(a => a.Id_C == Id_C);
                if (resultados.Count > 0)
                {
                    foreach (Cliente a in resultados)
                    {
                        TxtId_Cp.Text = a.Id_C;
                        txtNombrep.Text = a.Nombre;
                        txtCorreop.Text = a.Correo;
                        txtTelefonop.Text = a.Telefono;
                        txtDireccionp.Text = a.Direccion;
                        btnGuardarp.Visible=true;
                        btncancelm.Visible = true;
                    }
                }
                else
                    MessageBox.Show("No Existe");
                //TxtId_Cp.Clear();
                BD.Close();

            }
            else
                MessageBox.Show("Ingrese la clave");
        }

        private void btnGuardarp_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            Cliente cli = new Cliente();
            cli.Id_C = TxtId_Cp.Text;
            IList<Cliente> resultados = BD.Query<Cliente>(x => x.Id_C == TxtId_Cp.Text);
            if (resultados.Count > 0)
            {
                IObjectSet result = BD.QueryByExample(cli);
                Cliente v = (Cliente)result.Next();
                //vie.Clave = TxtClavep.Text;
                v.Nombre = txtNombrep.Text;
                v.Correo = txtCorreop.Text;
                v.Telefono = txtTelefonop.Text;
                v.Direccion = txtDireccionp.Text;
                BD.Store(v);
                BD.Commit();
                MessageBox.Show("Exito!!! Se Modifico");
                TxtId_Cp.Clear();
                txtNombrep.Clear();
                txtCorreop.Clear();
                txtTelefonop.Clear();
                txtDireccionp.Clear();
            }
            else
                MessageBox.Show("Error!!!  No se Modifico");
            BD.Close();
            TxtId_Cp.Clear();
            txtNombrep.Clear();
            txtCorreop.Clear();
            txtTelefonop.Clear();
            txtDireccionp.Clear();
            btnGuardarp.Visible = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (TxtId_Cp.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string nom = TxtId_Cp.Text;
                try
                {
                    IList<Cliente> consulta = BD.Query<Cliente>(z => z.Id_C == nom);
                    foreach (Cliente item in consulta)
                    {
                        BD.Delete(item);
                        MessageBox.Show("Registro eliminado");
                        TxtId_Cp.Clear();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    MessageBox.Show("No hay registros que coincidan");
                }
                finally
                {
                    BD.Close();
                }
            }
            else
                MessageBox.Show("Inserta la clave");
        }

        private void btnConsultaG_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            try
            {
                IList<Cliente> consulta = BD.Query<Cliente>();
                MessageBox.Show("Cantidad de registros encontrados: " + consulta.Count);

                // Limpiar filas y columnas antes de agregar nuevos datos
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // Definir columnas si es necesario
                if (consulta.Count > 0)
                {
                    // Crear columnas solo si no existen
                    if (dataGridView1.Columns.Count == 0)
                    {
                        dataGridView1.Columns.Add("Id_C", "ID");
                        dataGridView1.Columns.Add("Nombre", "Nombre");
                        dataGridView1.Columns.Add("Correo", "Correo");
                        dataGridView1.Columns.Add("Telefono", "Teléfono");
                        dataGridView1.Columns.Add("Direccion", "Dirección");
                    }

                    // Agregar los registros al DataGridView
                    foreach (var cliente in consulta)
                    {
                        dataGridView1.Rows.Add(cliente.Id_C, cliente.Nombre, cliente.Correo, cliente.Telefono, cliente.Direccion);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                BD.Close();
            }
        }



        private void btnBuscarp_Click(object sender, EventArgs e)
        {
            if (txtbuscar.Text != string.Empty)
            {
                IObjectContainer BD = Db4oEmbedded.OpenFile("SuperBD.yap");
                Cliente cl = new Cliente();
                cl.Id_C = txtbuscar.Text;
                IList<Cliente> resultados = BD.Query<Cliente>(x => x.Id_C == txtbuscar.Text);

                if (resultados.Count > 0)
                {

                    foreach (Cliente al in resultados)
                    {
                        MessageBox.Show("Id_C " + al.Id_C + "\n" + "Nombre " + al.Nombre + "\n" +
                            "Correo " + al.Correo + "\n"+ "Telefono: "+al.Telefono + "\n" +
                            "Direccion " + al.Direccion);
                    }

                }
                else
                    MessageBox.Show("No existe!! Inserta otra clave");
                BD.Close();
                txtbuscar.Clear();//es el txt que esta en el boton de buscar
            }
            else
                MessageBox.Show("Por favor ingrese la clave");
        }

        /// ///////////////INICIAN LOS BOTONES DE CITA

        private void btnagregarD_Click(object sender, EventArgs e)
        {
            if (txtId_Ct.Text != string.Empty && txtFecha.Text != string.Empty && txtHora.Text != string.Empty && txtNo_Orden.Text != string.Empty && txtId_Ce.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");

                try
                {
                    // Verificar si ya existe un Id_Ct igual
                    var existingCita = BD.Query<Cita>(c => c.Id_Ct == txtId_Ct.Text).FirstOrDefault();

                    if (existingCita != null)
                    {
                        MessageBox.Show("El Id_Ct ya existe. Por favor, use uno diferente.");
                        return;
                    }

                    // Si no existe, continuar creando el nuevo objeto Cita
                    Cita mo = new Cita
                    {
                        Id_Ct = txtId_Ct.Text,
                        Fecha = txtFecha.Text,
                        Hora = txtHora.Text,
                        No_Orden = txtNo_Orden.Text,
                        Id_C = txtId_Ce.Text

                    };

                    BD.Store(mo);
                    BD.Commit();
                    MessageBox.Show("ÉXITO!!! Se guardó");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}");
                }
                finally
                {
                    BD.Close();
                }

                // Limpiar campos
                txtId_Ct.Clear();
                txtFecha.Clear();
                txtHora.Clear();
                txtNo_Orden.Clear();
                txtId_Ce.Clear();
            }
            else
            {
                MessageBox.Show("Hay campos vacíos");
            }
        }


        private void btnModificarD_Click(object sender, EventArgs e)
        {
            if (txtId_Ct.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string Id_Ct = txtId_Ct.Text;
                Cita p = new Cita();
                p.Id_Ct = txtId_Ct.Text;
                IList<Cita> resultados = BD.Query<Cita>(a => a.Id_Ct == txtId_Ct.Text);
                if (resultados.Count > 0)
                {
                    foreach (Cita a in resultados)
                    {
                        txtId_Ct.Text = a.Id_Ct;
                        txtFecha.Text = a.Fecha;
                        txtHora.Text = a.Hora;
                        txtNo_Orden.Text = a.No_Orden;
                        txtId_Ce.Text=a.Id_Ct;
                        btnGuardarD.Visible=true;
                        btncanceld.Visible = true;
                    }
                }
                else
                    MessageBox.Show("No Existe");
                //TxtClavep.Clear();
                BD.Close();
            }
            else
                MessageBox.Show("Ingrese el Codigo");
        }

        private void btnGuardarD_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            Cita dv = new Cita();
            dv.Id_Ct = txtId_Ct.Text;
            IList<Cita> resultados = BD.Query<Cita>(x => x.Id_Ct == txtId_Ct.Text);
            if (resultados.Count > 0)
            {
                IObjectSet result = BD.QueryByExample(dv);
                Cita vie = (Cita)result.Next();
                vie.Id_Ct = txtId_Ct.Text;
                vie.Fecha = txtFecha.Text;
                vie.Hora = txtHora.Text;
                vie.No_Orden = txtNo_Orden.Text;
                vie.Id_C= txtId_Ce.Text;
                BD.Store(vie);
                BD.Commit();
                MessageBox.Show("Exito!!! Se Modifico");
                txtId_Ct.Clear();
                txtFecha.Clear();
                txtHora.Clear();
                txtNo_Orden.Clear();
                txtId_Ce.Clear();
            }
            else
                MessageBox.Show("Error!!!  No se Modifico");
            BD.Close();
            txtId_Ct.Clear();
            txtFecha.Clear();
            txtHora.Clear();
            txtNo_Orden.Clear();
            txtId_Ce.Clear();
            btnGuardarD.Visible = false;
        }

        private void btneliminarD_Click(object sender, EventArgs e)
        {
            if (txtId_Ct.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string cod = txtId_Ct.Text;
                try
                {
                    IList<Cita> consulta = BD.Query<Cita>(z => z.Id_Ct == cod);
                    foreach (Cita item in consulta)
                    {
                        BD.Delete(item);
                        MessageBox.Show("EL Registro se elimino");
                        txtId_Ct.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    MessageBox.Show("No hay registros que coincidan");
                }
                finally
                {
                    BD.Close();
                }
            }
            else
                MessageBox.Show("Inserta el Codigo");
        }

        private void btnConsultaD_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            try
            {
                IList<Cita> consulta = BD.Query<Cita>();
                MessageBox.Show("Cantidad de registros encontrados: " + consulta.Count);

                // Limpiar filas y columnas antes de agregar nuevos datos
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                // Definir columnas si es necesario
                if (consulta.Count > 0)
                {
                    // Crear columnas solo si no existen
                    if (dataGridView2.Columns.Count == 0)
                    {
                        dataGridView2.Columns.Add("Id_Ct", "ID Cita");
                        dataGridView2.Columns.Add("Fecha", "Fecha");
                        dataGridView2.Columns.Add("Hora", "Hora");
                        dataGridView2.Columns.Add("No_Orden", "Número de Orden");
                        dataGridView2.Columns.Add("Id_C", "ID Cliente");
                    }

                    // Agregar los registros al DataGridView
                    foreach (var cita in consulta)
                    {
                        dataGridView2.Rows.Add(cita.Id_Ct, cita.Fecha, cita.Hora, cita.No_Orden,cita.Id_C);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                BD.Close();
            }
        }


        private void btnbuscarD_Click(object sender, EventArgs e)
        {
            if (txtId_Ct1.Text != string.Empty)
            {
                IObjectContainer BD = Db4oEmbedded.OpenFile("SuperBD.yap");
                Cita ve = new Cita();
                ve.Id_Ct = txtId_Ct1.Text;
                IList<Cita> resultados = BD.Query<Cita>(x => x.Id_Ct == txtId_Ct1.Text);

                if (resultados.Count > 0)
                {

                    foreach (Cita al in resultados)
                    {
                        MessageBox.Show("Id_Ct " + al.Id_Ct + "\n" + "Fecha " + al.Fecha + "\n" +
                            "Hora" + al.Hora + "\n" + "No_Orden" + al.No_Orden +"/n" + "Id_C" + al.Id_C);
                    }

                }
                else
                    MessageBox.Show("No existe!! Inserta otro codigo");
                BD.Close();
                txtId_Ct1.Clear();
            }
            else
                MessageBox.Show("Por favor ingrese el codigo");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btncancelm.Visible = false;
            btnGuardarp.Visible = false;
            txtCorreop.Clear();
            TxtId_Cp.Clear();
            txtCorreop.Clear();
            txtNombrep.Clear();
            txtTelefonop.Clear();
        }

        private void btncanceld_Click(object sender, EventArgs e)
        {
            btncanceld.Visible = false;
            btnGuardarD.Visible = false;
            txtId_Ct1.Clear();
            txtHora.Clear();
            txtFecha.Clear();
            txtTelefonop.Clear();
            txtId_Ce.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
        /// ///////////////INICIAN LOS BOTONES DE VENTANA ORDEN
        private void BtnAgregarVentana_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId_VO.Text) && !string.IsNullOrEmpty(txtTipo_Ventana.Text) &&
                !string.IsNullOrEmpty(txtLargo_v.Text) && !string.IsNullOrEmpty(txtAncho_v.Text) &&
                !string.IsNullOrEmpty(txtCosto.Text))
            {
                using (IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap"))
                {
                    // Verificar si ya existe un Id_VO igual
                    var existingVentana = BD.Query<VentanaOrden>(v => v.Id_VO == txtId_VO.Text).FirstOrDefault();

                    if (existingVentana != null)
                    {
                        MessageBox.Show("El Id_VO ya existe. Por favor, use uno diferente.");
                        return;
                    }

                    // Si no existe, crear el nuevo objeto VentanaOrden
                    VentanaOrden ventana = new VentanaOrden
                    {
                        Id_VO = txtId_VO.Text,
                        Tipo_Ventana = txtTipo_Ventana.Text,
                        Largo_v = txtLargo_v.Text,
                        Ancho_v = txtAncho_v.Text,
                        Costo = txtCosto.Text
                    };

                    BD.Store(ventana);
                    BD.Commit();
                    MessageBox.Show("Éxito! Ventana guardada.");
                }

                // Limpiar campos
                txtId_VO.Clear();
                txtTipo_Ventana.Clear();
                txtLargo_v.Clear();
                txtAncho_v.Clear();
                txtCosto.Clear();
            }
            else
            {
                MessageBox.Show("Hay campos vacíos.");
            }
        }


        private void BtnModificarVentana_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId_VO.Text))
            {
                using (IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap"))
                {
                    IList<VentanaOrden> resultados = BD.Query<VentanaOrden>(v => v.Id_VO == txtId_VO.Text);
                    if (resultados.Count > 0)
                    {
                        VentanaOrden ventana = resultados[0];
                        txtId_VO.Text = ventana.Id_VO;
                        txtTipo_Ventana.Text = ventana.Tipo_Ventana;
                        txtLargo_v.Text = ventana.Largo_v;
                        txtAncho_v.Text = ventana.Ancho_v;
                        txtCosto.Text = ventana.Costo;

                        button6.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No existe.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese el ID de la ventana.");
            }
        }

        private void BtnEliminarVentana_ClickV(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId_VO.Text))
            {
                using (IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap"))
                {
                    IList<VentanaOrden> resultados = BD.Query<VentanaOrden>(v => v.Id_VO == txtId_VO.Text);
                    if (resultados.Count > 0)
                    {
                        foreach (var ventana in resultados)
                        {
                            BD.Delete(ventana);
                        }
                        BD.Commit();
                        MessageBox.Show("Registro eliminado.");
                    }
                    else
                    {
                        MessageBox.Show("No hay registros que coincidan.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Inserta el ID de la ventana.");
            }
        }

        private void BtnGuardarVentana(object sender, EventArgs e)
        {
            using (IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap"))
            {
                IList<VentanaOrden> resultados = BD.Query<VentanaOrden>(v => v.Id_VO == txtId_VO.Text);
                if (resultados.Count > 0)
                {
                    VentanaOrden ventana = resultados[0];
                    ventana.Tipo_Ventana = txtTipo_Ventana.Text;
                    ventana.Largo_v = txtLargo_v.Text;
                    ventana.Ancho_v = txtAncho_v.Text;
                    ventana.Costo = txtCosto.Text;

                    BD.Store(ventana);
                    BD.Commit();
                    MessageBox.Show("Éxito! Ventana modificada.");
                }
                else
                {
                    MessageBox.Show("Error! No se modificó.");
                }
            }
            button6.Visible = false;
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtId_VO.Clear();
            txtTipo_Ventana.Clear();
            txtLargo_v.Clear();
            txtAncho_v.Clear();
            txtCosto.Clear();
        }

        private void BtnBuscarVentana_Click(object sender, EventArgs e)
        {
            if (txtId_VO2.Text != string.Empty)
            {

                IObjectContainer BD = Db4oEmbedded.OpenFile("SuperBD.yap");
                VentanaOrden o = new VentanaOrden();
                o.Id_VO = txtId_VO2.Text;
                IList<VentanaOrden> resultados = BD.Query<VentanaOrden>(v => v.Id_VO == txtId_VO2.Text);
                if (resultados.Count > 0)
                {
                    foreach (VentanaOrden al in resultados)
                    {
                        MessageBox.Show("Id_VO " + al.Id_VO + "\n" + "Tipo_Ventana " + al.Tipo_Ventana + "\n" +
                    "Largo_v" + al.Largo_v + "\n" + "Ancho_v" + al.Ancho_v + "\n" + "Costo" + al.Costo);
                    }
                }
                else
                    MessageBox.Show("No existe!! Inserta otro codigo");
                BD.Close();
                txtId_VO2.Clear();
            }
            else
                MessageBox.Show("Por favor ingrese el codigo");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtId_Ct1_TextChanged(object sender, EventArgs e)
        {

        }


        private void BtnConsultaVentanas_Click(object sender, EventArgs e)
        {
            using (IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap"))
            {
                IList<VentanaOrden> consulta = BD.Query<VentanaOrden>();
                MessageBox.Show("Cantidad de registros encontrados: " + consulta.Count);

                // Limpiar filas y columnas antes de agregar nuevos datos
                dataGridView3.Rows.Clear();
                dataGridView3.Columns.Clear();

                if (consulta.Count > 0)
                {
                    if (dataGridView3.Columns.Count == 0)
                    {
                        dataGridView3.Columns.Add("Id_VO", "ID Ventana");
                        dataGridView3.Columns.Add("Tipo_Ventana", "Tipo");
                        dataGridView3.Columns.Add("Largo_v", "Largo");
                        dataGridView3.Columns.Add("Ancho_v", "Ancho");
                        dataGridView3.Columns.Add("Costo", "Costo");
                    }

                    foreach (var ventana in consulta)
                    {
                        dataGridView3.Rows.Add(ventana.Id_VO, ventana.Tipo_Ventana, ventana.Largo_v, ventana.Ancho_v, ventana.Costo);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros.");
                }
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}