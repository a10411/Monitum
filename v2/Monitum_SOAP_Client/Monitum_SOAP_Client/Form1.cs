using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitum_SOAP_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ServiceReferenceAddGestorWs.AddGestorWsSoapClient wsProxy = new ServiceReferenceAddGestorWs.AddGestorWsSoapClient();

            string emailAdmin = textBoxEmailAdmin.Text;
            string passwordAdmin = textBoxPasswordAdmin.Text;

            string emailGestor = textBoxEmailGestor.Text;
            string passwordGestor = textBoxPasswordGestor.Text;

            bool resultado = wsProxy.RegistarGestorBD(emailGestor, passwordGestor, emailAdmin, passwordAdmin);
            if (resultado)
            {
                MessageBox.Show("Sucesso! Gestor adicionado.");
            } else
            {
                MessageBox.Show("Erro ao adicionar gestor.");
            }
        }
    }
}
