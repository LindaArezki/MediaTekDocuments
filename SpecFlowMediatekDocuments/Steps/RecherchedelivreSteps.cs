using MediaTekDocuments.model;
using MediaTekDocuments.view;
using NUnit.Framework;
using System;
using System.Windows.Forms;
using TechTalk.SpecFlow;

namespace SpecFlowMediatekDocuments.Steps
{
    [Binding]
    public class RecherchedelivredvdSteps
    {
        public readonly FrmMediatek frmMediatek = new FrmMediatek();

        private TabPage GetTabPage(string tabPage)
        {
            return (TabPage)frmMediatek.Controls["tabonglet"].Controls[tabPage];
        }

        [Given(@"je me positionne sur l onglet livre")]
        public void GivenJeMePositionneSurLOngletLivre()
        {
            TabControl tabonglet = (TabControl)frmMediatek.Controls["tabonglet"];
            frmMediatek.Visible = true;
            tabonglet.SelectedTab = GetTabPage("tabLivres");
        }

        [Given(@"je saisis le numero de livre '(.*)'")]
        public void GivenJeSaisisLeNumeroDeLivre(string valeur)
        {
            TextBox txbLivresNumRecherche = (TextBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["txbLivresNumRecherche"];
            txbLivresNumRecherche.Text = valeur;
        }


        [When(@"je saisis la partie du titre de livre '(.*)'")]
        public void WhenJeSaisisLaPartieDuTitreDeLivre(string valeur)
        {
            TextBox txbLivresTitreRecherche = (TextBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["txbLivresTitreRecherche"];
            txbLivresTitreRecherche.Text = valeur;
        }

        [When(@"je clique sur le bouton recherche livre")]
        public void WhenJeCliqueSurLeBoutonRechercheLivre()
        {
            Button btnLivresNumRecherche = (Button)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["btnLivresNumRecherche"];
            frmMediatek.Visible = true;
            btnLivresNumRecherche.PerformClick();
        }


        [When(@"je sélectionne le genre (.*)")]
        public void WhenJeSelectionneLeGenre(int valeur)
        {
            ComboBox cbxLivresGenres = (ComboBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["cbxLivresGenres"];
            cbxLivresGenres.SelectedIndex = valeur;
        }

        [When(@"je sélectionne le public (.*)")]
        public void WhenJeSelectionneLePublic(int valeur)
        {
            ComboBox cbxLivresPublics = (ComboBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["cbxLivresPublics"];
            cbxLivresPublics.SelectedIndex = valeur;
        }

        [When(@"je sélectionne le rayon (.*)")]
        public void WhenJeSelectionneLeRayon(int valeur)
        {
            ComboBox cbxLivresRayons = (ComboBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["cbxLivresRayons"];
            cbxLivresRayons.SelectedIndex = valeur;
        }

        [Then(@"le nombre de livres obtenu est de (.*)")]
        public void ThenLeNombreDeLivresObtenuEstDe(int nbAttendu)
        {
            DataGridView dgvLivresListe = (DataGridView)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["dgvLivresListe"];
            int nblivres = dgvLivresListe.Rows.Count;
            Assert.AreEqual(nbAttendu, nblivres);
        }

    }
}
