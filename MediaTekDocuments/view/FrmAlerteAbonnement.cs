using System;
using System.Collections.Generic;
using MediaTekDocuments.controller;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.model;


namespace MediaTekDocuments.view
{
    public partial class FrmAlerteAbonnement : Form
    {
        private readonly BindingSource bdgAbonnement = new BindingSource();
        private readonly List<AbonnementFin> lesabonnements = new List<AbonnementFin>();

        /// <summary>
        /// Constructeur de classe
        /// </summary>
        public FrmAlerteAbonnement(FrmMediatekController controller)
        {
            InitializeComponent();
            ;
            lesabonnements = controller.GetAbonnementFin();
            bdgAbonnement.DataSource = lesabonnements;
            dgvAbonnements.DataSource = bdgAbonnement;
            dgvAbonnements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAbonnements.Columns["dateFinAbonnement"].DisplayIndex = 1;
            dgvAbonnements.Columns[0].HeaderCell.Value = "Date de fin d'abonnement";
            dgvAbonnements.Columns[1].HeaderCell.Value = "Identitifiant";
            dgvAbonnements.Columns[2].HeaderCell.Value = "Titre de la Revue";
            dgvAbonnements.Focus();
        }
        

    private void buttoncontinuer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
