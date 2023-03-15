using System;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
    public partial class FrmAuthentification : Form
    {
        private readonly FrmAuthentificationController controller; 

        /// <summary>
        /// Constructeur de classe
        /// </summary>
        public FrmAuthentification()
        {
            InitializeComponent();
            this.controller = new FrmAuthentificationController();
            
        }

        /// <summary>
        /// Bouton qui véréfie le service est la personne connecter et affiche la fenetre principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnseconnecter_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            string utilisateur = txtboxutilisateur.Text;
            string pwd = txtboxmdp.Text;
           
            if (!txtboxutilisateur.Text.Equals("") && !txtboxmdp.Text.Equals(""))
            {
                    if (!controller.GetAuthentification(utilisateur, pwd))
                    {
                        MessageBox.Show("Authentification incorrecte", "Alerte");
                        txtboxmdp.Text = "";
                        txtboxutilisateur.Focus();
                    }
                    else
                    {
                        if(Service.Libelle != "Culture") 
                        {

                            this.Hide();
                            FrmMediatek frmMediatek = new FrmMediatek();
                            frmMediatek.ShowDialog();
                            

                        }
                        else
                        {
                        MessageBox.Show("Vous ne pouvez pas vous connecter, vous n'avez pas accès à cette application");
                        }
                    }

            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires");

            }
        }
        /// <summary>
        /// Bouton qui vide la les textbox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnannuler_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            txtboxutilisateur.Text = "";
            txtboxmdp.Text = "";
            txtboxutilisateur.Focus();
            
        }
    }
}
