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
           //ServiceReferenceAddGestorWs.AddGestorWsSoap wsProxy = new ServiceReferenceAddGestorWs.AddGestorWsSoap();
            ServiceReferenceAddGestorWsAsync.AddGestorWsSoap = new ServiceReferenceAddGestorWsAsync.AddGestorWsSoap();

            string emailAdmin = textBoxEmailAdmin.Text;
            string passwordAdmin = textBoxPasswordAdmin.Text;

            string emailGestor = textBoxEmailGestor.Text;
            string passwordGestor = textBoxPasswordGestor.Text;

            bool resultado = wsProxy.RegistarGestorBD();
        }
    }
}
