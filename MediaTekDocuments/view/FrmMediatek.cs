using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        internal FrmMediatek()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
            if (Service.Libelle == "Prêt")
            {
                
                tabonglet.TabPages.Remove(tabcommandelivres);
                tabonglet.TabPages.Remove(tabCommandeDvd);
                tabonglet.TabPages.Remove(tabCommandeRevue);
            }
        }
       
        /// Dès l'ouverture de l'application la vue d'alerte de fin d'abonnements s'ouvre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMediatek_Shown(object sender, EventArgs e)
        {
            if (Service.Libelle != "Prêt")
            {
                FrmAlerteAbonnement alerteFinAbonnements = new FrmAlerteAbonnement(controller);
                alerteFinAbonnements.StartPosition = FormStartPosition.CenterParent;
                alerteFinAbonnements.ShowDialog();
            }
        }
       
        

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }
        #endregion

        #region Onglet Livres
        private readonly BindingSource bdgLivresListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">liste de livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }
        #endregion

        #region Onglet Dvd
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">liste de dvd</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }
        #endregion

        #region Onglet Revues
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues"></param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }
        #endregion

        #region Onglet Parutions
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des exemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">liste d'exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocument = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocument);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }
        #endregion

        #region Onglet CommandesLivres
        private readonly BindingSource bdgCommandeslivres = new BindingSource();
        private List<CommandesDocument> lescommandesdocument = new List<CommandesDocument>();
        private List<Suivi> lesSuivis = new List<Suivi>();

        /// <summary>
        /// Ouverture de l'onglet : récupère les livres et les suiviq.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabcommandelivres_Enter_1(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            lesSuivis = controller.GetAllSuivis();
            Console.WriteLine("******** nb suivis=" + lesSuivis.Count);

        }

        /// <summary>
        /// Remplit le dategrid des commandes avec la liste de livre reçue en paramètre
        /// </summary>
        /// <param name="lescommandesdocument">liste des commandes</param>
        private void RemplirCommandesLivresListe(List<CommandesDocument> lescommandesdocument)
        {
            if (lescommandesdocument != null)
            {
                bdgCommandeslivres.DataSource = lescommandesdocument;
                dgvCommandelivres.DataSource = bdgCommandeslivres;
                dgvCommandelivres.Columns["id"].Visible = true;
                dgvCommandelivres.Columns["idLivreDvd"].Visible = false;
                dgvCommandelivres.Columns["suivi"].Visible = false;
                dgvCommandelivres.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCommandelivres.Columns["dateCommande"].DisplayIndex = 0;
                dgvCommandelivres.Columns["montant"].DisplayIndex = 1;
                dgvCommandelivres.Columns[5].HeaderCell.Value = "Date";
                dgvCommandelivres.Columns[0].HeaderCell.Value = "Exemplaires";
                dgvCommandelivres.Columns[3].HeaderCell.Value = "Etape";

            }
            else
            {
                bdgCommandeslivres.DataSource = null;
            }
        }

        /// <summary>
        /// Permet de rechercher si un livre possede une commande
        /// </summary>
        private void bttnrecherche_Click(object sender, EventArgs e)
        {
            if (!txtboxdocu.Text.Equals(""))
            {
                Livre livre = lesLivres.Find(x => x.Id.Equals(txtboxdocu.Text));
                if (livre != null)
                {
                    AfficheCommandeLivreInfos(livre);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }

        }

        /// <summary>
        /// Si le numéro de livre est modifié, la zone  est vidée et inactive
        /// les informations du livre sont aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtboxdocu_TextChanged_1(object sender, EventArgs e)
        {
            txtboxauteur.Text = "";
            txtboxcollection.Text = "";
            txtboxcheminimage.Text = "";
            txtcode.Text = "";
            txtnumerodocu2.Text = "";
            txtboxgenre.Text = "";
            txtboxpublic.Text = "";
            txtboxrayon.Text = "";
            txtboxtitre.Text = "";
            pictureBoxdocu.Image = null;
            RemplirCommandesLivresListe(null);
            AccesGroupBoxdelacommande(false);

        }

        /// <summary>
        /// Affichage des informations de la commande sélectionnée et les livres
        /// </summary>
        /// <param name="livre">la revue</param>
        private void AfficheCommandeLivreInfos(Livre livre)
        {
            // informations des livres
            txtboxauteur.Text = livre.Auteur;
            txtboxcollection.Text = livre.Collection;
            txtboxcheminimage.Text = livre.Image;
            txtcode.Text = livre.Isbn;
            txtnumerodocu2.Text = livre.Id;
            txtboxgenre.Text = livre.Genre;
            txtboxpublic.Text = livre.Public;
            txtboxrayon.Text = livre.Rayon;
            txtboxtitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pictureBoxdocu.Image = Image.FromFile(image);
            }
            catch
            {
                pictureBoxdocu.Image = null;
            }
            // affiche la liste des commandes des livres
            AfficheReceptionCommandeLivre();
        }

        /// <summary>
        /// Récupère et affiche les commandes d'un livre 
        /// </summary>
        private void AfficheReceptionCommandeLivre()
        {
            string idDocument = txtboxdocu.Text;
            lescommandesdocument = controller.GetCommandesDocument(idDocument);
            RemplirCommandesLivresListe(lescommandesdocument);
            AccesGroupBoxdelacommande(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la commande d'un livre
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesGroupBoxdelacommande(bool acces)
        {
            grpboxcommande.Enabled = acces;
            groupBox1.Enabled = acces;
            pictureBoxdocu.Image = null;
            dateTimePickercommande.Value = DateTime.Now;
        }

   
        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandelivres_ColumnHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandelivres.Columns[e.ColumnIndex].HeaderText;
            List<CommandesDocument> sortedList = new List<CommandesDocument>();
            switch (titreColonne)
            {
                case "Date":
                    sortedList = lescommandesdocument.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = lescommandesdocument.OrderBy(o => o.Montant).ToList();
                    break;
                case "Exemplaires":
                    sortedList = lescommandesdocument.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Etape":
                    sortedList = lescommandesdocument.OrderBy(o => o.Libelle).ToList();
                    break;

            }

            RemplirCommandesLivresListe(sortedList);
        }

        /// <summary>
        /// Bouton permettant d'enregistrer une commande, de l'ajouter 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnenregistrer_Click(object sender, EventArgs e)
        {
            if (!txtboxcommandelivre.Text.Equals("") && !txtboxmontant.Text.Equals("") && !txtboxexemplaire.Text.Equals(""))
            {

                string id = txtboxcommandelivre.Text;
                DateTime dateCommande = dateTimePickercommande.Value;
                double montant = double.Parse(txtboxmontant.Text);
                int nbexemplaire = int.Parse(txtboxexemplaire.Text);
                string idLivreDvd = txtboxdocu.Text;
                int suivi = lesSuivis[0].Idetape;
                string libelle = lesSuivis[0].Libelle;


                Commande commande = new Commande(id, dateCommande, montant);
                CommandesDocument commandesDocument = new CommandesDocument(id, dateCommande, montant, nbexemplaire, idLivreDvd, suivi, libelle);
                if (controller.CreerCommandes(commande) && controller.CreerCommandeDocuments(id, nbexemplaire, idLivreDvd, suivi))
                {
                    AfficheReceptionCommandeLivre();
                }
                else
                {
                    MessageBox.Show("numéro de commande déjà existant", "Erreur");
                }

            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires", "Information");
            }
        }

        /// <summary>
        /// Bouton permettant de supprimer une commande de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnSupprimer_Click(object sender, EventArgs e)
        {
           
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandeslivres.Current;
            if (MessageBox.Show("Souhaitez-vous confirmer la supression?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (controller.SupprimerCommande(commandesDocument.Id))
                {
                    AfficheReceptionCommandeLivre();
                }
                else
                {
                    MessageBox.Show("Une erreur s'est produite.", "Erreur");
                }
            }
        }

        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Réglée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegler_Click(object sender, EventArgs e)
        {
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandeslivres.List[bdgCommandeslivres.Position];

            Suivi suivi = lesSuivis.Find(x => x.Libelle == "Réglée");
            List<Suivi> Suivis = new List<Suivi>() { suivi };
            if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Idetape);
                AfficheReceptionCommandeLivre();
            }

        }

        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Relancée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRelancer_Click(object sender, EventArgs e)
        {
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandeslivres.List[bdgCommandeslivres.Position];

            Suivi suivi = lesSuivis.Find(x => x.Libelle == "Relancée");
            List<Suivi> Suivis = new List<Suivi>() { suivi };

            if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Idetape);
                AfficheReceptionCommandeLivre();
            }

        }

        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Livrée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLivrer_Click(object sender, EventArgs e)
        {
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandeslivres.List[bdgCommandeslivres.Position];
            Suivi suivi = lesSuivis.Find(x => x.Libelle == "Livrée");
            List<Suivi> Suivis = new List<Suivi>() { suivi };

            if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Idetape);
                AfficheReceptionCommandeLivre();
            }
        }

        
        /// <summary>
        /// Active/Désactive les boutons de gestion de commande en fonction de l'état de suivi
        /// </summary>
        /// <param name="commandesDocument">CommandeDocument concernée</param>
        private void Modifieretapesuivi(CommandesDocument commandesDocument)
        {
            string suiviLibelle = commandesDocument.Libelle;
            switch (suiviLibelle)
            {
                case "En cours":

                case "Relancée":
                    buttonLivrer.Enabled = true;
                    buttonRegler.Enabled = true;
                    buttonRelancer.Enabled = false;
                    bttnSupprimer.Enabled = true;
                    break;
                case "Livrée":
                    buttonLivrer.Enabled = false;
                    buttonRegler.Enabled = false;
                    buttonRelancer.Enabled = false;
                    bttnSupprimer.Enabled = false;
                    break;
                case "Réglée":
                    buttonLivrer.Enabled = true;
                    buttonRegler.Enabled = false;
                    buttonRelancer.Enabled = false;
                    bttnSupprimer.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Permet d'appuyer sur les lignes du datagridview pour empecher les boutons en fonction de la ligne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandelivres_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvCommandelivres.CurrentCell != null)
            {
                CommandesDocument commandesDocument = (CommandesDocument)bdgCommandeslivres.List[bdgCommandeslivres.Position];
                Modifieretapesuivi(commandesDocument); 
            }
        }

        #endregion

        #region Onglet CommandesDvd
        private readonly BindingSource bdgCommandesDVD = new BindingSource();

        /// <summary>
        /// Ouverture de l'onglet : récupère les dvd et les suivis.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeDvd_Enter_1(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            lesSuivis = controller.GetAllSuivis();
           
        }

        /// <summary>
        /// Remplit le dategrid des commandes avec la liste de dvd  reçue en paramètre
        /// </summary>
        /// <param name="commandesdocument">liste des commandes</param>
        private void RemplirCommandesDvdListe(List<CommandesDocument> commandesdocument)
        {
            if (commandesdocument != null)
            {
                bdgCommandesDVD.DataSource = commandesdocument;
                dgvcommandedvd.DataSource = bdgCommandesDVD;
                dgvcommandedvd.Columns["id"].Visible = false;
                dgvcommandedvd.Columns["idLivreDvd"].Visible = false;
                dgvcommandedvd.Columns["suivi"].Visible = false;
                dgvcommandedvd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvcommandedvd.Columns["dateCommande"].DisplayIndex = 0;
                dgvcommandedvd.Columns["montant"].DisplayIndex = 1;
                dgvcommandedvd.Columns[5].HeaderCell.Value = "Date";
                dgvcommandedvd.Columns[0].HeaderCell.Value = "Exemplaires";
                dgvcommandedvd.Columns[3].HeaderCell.Value = "Etape";
            }
            else
            {
                bdgCommandesDVD.DataSource = null;
            }
        }

        /// <summary>
        /// Permet de rechercher si un dvd possede une commande
        /// </summary>
        private void bttnrechercherdvd_Click(object sender, EventArgs e)
        {
            if (!txtboxdocudvd.Text.Equals(""))
            {
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txtboxdocudvd.Text));
                if (dvd != null)
                {
                    AfficheCommandeDvdInfos(dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de dvd est modifié, la zone  est vidée et inactive
        /// les informations du livre sont aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void txtboxdocudvd_TextChanged_1(object sender, EventArgs e)
        {
            txtboxrealisateur.Text = "";
            txtBoxsynopsis.Text = "";
            txtboxchemindvd.Text = ""; 
            txtduree.Text = "";
            textBoxnumero.Text = "";
            txtboxgenre.Text = "";
            txtboxpublic.Text = "";
            txtboxrayondvd.Text = "";
            txtboxtitredvd.Text = "";
            pictureBoximagedvd.Image = null;
            RemplirCommandesDvdListe(null);
            AccesGroupBoxdelacommandedvd(false);
        }

        /// <summary>
        /// Affichage des informations de la commande sélectionnée et les dvd
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheCommandeDvdInfos(Dvd dvd)
        {
            txtboxrealisateur.Text = dvd.Realisateur;
            txtBoxsynopsis.Text = dvd.Synopsis;
            txtboxchemindvd.Text = dvd.Image;
            txtduree.Text = dvd.Duree.ToString();
            textBoxnumero.Text = dvd.Id;
            txtboxgenre.Text = dvd.Genre;
            txtboxpublic.Text = dvd.Public;
            txtboxrayondvd.Text = dvd.Rayon;
            txtboxtitredvd.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pictureBoximagedvd.Image = Image.FromFile(image);
            }
            catch
            {
                pictureBoximagedvd.Image = null;
            }
            // affiche la liste des commandes des dvd
            AfficheReceptionCommandeDvd();
        }

        /// <summary>
        /// Récupère et affiche les commandes d'un dvd 
        /// </summary>
        private void AfficheReceptionCommandeDvd()
        {
            string idDocument = txtboxdocudvd.Text;
            lescommandesdocument = controller.GetCommandesDocument(idDocument);
            RemplirCommandesDvdListe(lescommandesdocument);
            AccesGroupBoxdelacommandedvd(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la commande d'un dvd
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesGroupBoxdelacommandedvd(bool acces)
        {
            groupboxnouvellecommandedvd.Enabled = acces;
            groupboxgestiondvd.Enabled = acces;
            pictureBoximagedvd.Image = null;
            dateTimePickerdvd.Value = DateTime.Now;
       }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvcommandedvd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvcommandedvd.Columns[e.ColumnIndex].HeaderText;
            List<CommandesDocument> sortedList = new List<CommandesDocument>();
            switch (titreColonne)
            {
                case "Date":
                    sortedList = lescommandesdocument.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = lescommandesdocument.OrderBy(o => o.Montant).ToList();
                    break;
                case "Exemplaires":
                    sortedList = lescommandesdocument.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Etape":
                    sortedList = lescommandesdocument.OrderBy(o => o.Libelle).ToList();
                    break;
            }

            RemplirCommandesDvdListe(sortedList);
        }


        /// <summary>
        /// Bouton permettant de supprimer une commande de dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonsupprimerdvd_Click(object sender, EventArgs e)
        {
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandesDVD.Current;
            if (MessageBox.Show("Souhaitez-vous confirmer la supression?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (controller.SupprimerCommande(commandesDocument.Id))
                {
                    AfficheReceptionCommandeDvd();
                }
                else
                {
                    MessageBox.Show("Une erreur s'est produite.", "Erreur");
                }
            }

        }

        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Livrée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonlivrerdvd_Click(object sender, EventArgs e)
        {
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandesDVD.List[bdgCommandesDVD.Position];

            Suivi suivi = lesSuivis.Find(x => x.Libelle == "Livrée");
            List<Suivi> Suivis = new List<Suivi>() { suivi };

            if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Idetape);
                AfficheReceptionCommandeDvd();
            }
        }

        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Reglée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnreglerdvd_Click(object sender, EventArgs e)
        {
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandesDVD.List[bdgCommandesDVD.Position];

            Suivi suivi = lesSuivis.Find(x => x.Libelle == "Réglée");
            List<Suivi> Suivis = new List<Suivi>() { suivi };

            if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Idetape);
                AfficheReceptionCommandeDvd(); 
            }

        }

        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Relancée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonrelancerdvd_Click(object sender, EventArgs e)
        {
            CommandesDocument commandesDocument = (CommandesDocument)bdgCommandesDVD.List[bdgCommandesDVD.Position];

            Suivi suivi = lesSuivis.Find(x => x.Libelle == "Relancée");
            List<Suivi> Suivis = new List<Suivi>() { suivi };

            if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Idetape);
                AfficheReceptionCommandeDvd();
            }
        }

        /// <summary>
        /// Bouton permettant d'enregistrer une commande de dvd, de l'ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonenregisterdvd_Click(object sender, EventArgs e)
        {
            if (!txtboxcommandedvd.Text.Equals("") && !txtboxmontantdvd.Text.Equals("") && !txtboxexemplairedvd.Text.Equals(""))
            {

                string id = txtboxcommandedvd.Text;
                DateTime dateCommande = dateTimePickerdvd.Value;
                double montant = double.Parse(txtboxmontantdvd.Text);
                int nbexemplaire = int.Parse(txtboxexemplairedvd.Text);
                string idLivreDvd = txtboxdocudvd.Text;
                int suivi = lesSuivis[0].Idetape;
                string libelle = lesSuivis[0].Libelle;


                Commande commande = new Commande(id, dateCommande, montant);
                CommandesDocument commandesDocument = new CommandesDocument(id, dateCommande, montant, nbexemplaire, idLivreDvd, suivi, libelle);
                if (controller.CreerCommandes(commande) && controller.CreerCommandeDocuments(id, nbexemplaire, idLivreDvd, suivi))
                {
                    AfficheReceptionCommandeDvd();
                }
                else
                {
                    MessageBox.Show("numéro de commande déjà existant", "Erreur");
                }

            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires", "Information");
            }

        }

        /// <summary>
        /// Active/Désactive les boutons de gestion de commande en fonction de l'état de suivi
        /// </summary>
        /// <param name="commandesDocument">CommandeDocument concernée</param>
        private void Modifieretapesuividvd(CommandesDocument commandesDocument)
        {
            string suiviLibelle = commandesDocument.Libelle;
            switch (suiviLibelle)
            {
                case "En cours":

                case "Relancée":
                    buttonlivrerdvd.Enabled = true;
                    bttnreglerdvd.Enabled = true;
                    buttonrelancerdvd.Enabled = false;
                    buttonsupprimerdvd.Enabled = true;
                    break;
                case "Livrée":
                    buttonlivrerdvd.Enabled = false;
                    bttnreglerdvd.Enabled = false;
                    buttonrelancerdvd.Enabled = false;
                    buttonsupprimerdvd.Enabled = false;
                    break;
                case "Réglée":
                    buttonlivrerdvd.Enabled = true;
                    bttnreglerdvd.Enabled = false;
                    buttonrelancerdvd.Enabled = false;
                    buttonsupprimerdvd.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Permet d'appuyer sur les lignes du datagridview pour empecher les boutons en fonction de la ligne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvcommandedvd_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                if (dgvcommandedvd.CurrentCell != null)
                {
                    CommandesDocument commandesDocument = (CommandesDocument)bdgCommandesDVD.List[bdgCommandesDVD.Position];
                    Modifieretapesuividvd(commandesDocument);
                }
        }
        #endregion

        #region Onglet CommandesRevues

        private readonly BindingSource bdgCommandesRevue = new BindingSource();
        private List<Abonnement> lescommandesabonnements = new List<Abonnement>();
        
        /// <summary>
        /// Récupere les revues quand l'onglet est ouvert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
          

        }

        /// <summary>
        /// Vide les champs et empeche d'avoir acces au groupbox de gestion de revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtboxdocurevue_TextChanged(object sender, EventArgs e)
        {
            txtboxperiodiciterevu.Text = "";
            txtboxcheminimagerevue.Text = "";
            txtboxdelairevu.Text = "";
            txtboxnumeroderevu2.Text = "";
            txtboxgenrerevu.Text = "";
            txtboxpublicrevue.Text = "";
            txtboxrayonrevu.Text = "";
            txtboxtitrerevue.Text = "";
            pictureBoxrevu.Image = null;
            RemplirCommandesRevueListe(null);
            AccesGroupBoxdelacommandederevue(false);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la commande d'une revue
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces"></param>
        private void AccesGroupBoxdelacommandederevue(bool acces)
        {
                groupboxajoutrevu.Enabled = acces;
                pictureBoxrevu.Image = null;
                dateTimePickerrevue.Value = DateTime.Now;
        }

        /// <summary>
        /// Bouton permettant de chercher un numero de revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttnrechercherevue_Click(object sender, EventArgs e)
        {
            if (!txtboxdocurevue.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txtboxdocurevue.Text));
                if (revue != null)
                {
                    AfficheCommandeRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }

        }

        /// <summary>
        /// Remplit le dategrid des commandes avec la liste de revue reçue en paramètre
        /// </summary>
        /// <param name="lescommandesabonnements">liste des commandes</param>
        private void RemplirCommandesRevueListe(List<Abonnement> lescommandesabonnements)
        {
            if (lescommandesabonnements != null)
            {
                bdgCommandesRevue.DataSource = lescommandesabonnements;
                dgvCommandesRevues.DataSource = bdgCommandesRevue;
                dgvCommandesRevues.Columns["id"].Visible = false;
                dgvCommandesRevues.Columns["idRevue"].Visible = false;
                dgvCommandesRevues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCommandesRevues.Columns["dateCommande"].DisplayIndex = 0;
                dgvCommandesRevues.Columns["montant"].DisplayIndex = 1;
                dgvCommandesRevues.Columns[3].HeaderCell.Value = "Date de la commande";
                dgvCommandesRevues.Columns[0].HeaderCell.Value = "Date de fin d'abonnement";

            }
            else
            {
                bdgCommandesRevue.DataSource = null;
            }
        }


     

        /// <summary>
        /// Affichage des informations de la commande sélectionnée et les revues
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheCommandeRevueInfos(Revue revue)
        {
            txtboxperiodiciterevu.Text = revue.Periodicite;
            txtboxcheminimagerevue.Text = revue.Image;
            txtboxdelairevu.Text = revue.DelaiMiseADispo.ToString();
            txtboxnumeroderevu2.Text = revue.Id;
            txtboxgenrerevu.Text = revue.Genre;
            txtboxpublicrevue.Text = revue.Public;
            txtboxrayonrevu.Text = revue.Rayon;
            txtboxtitrerevue.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pictureBoxrevu.Image = Image.FromFile(image);
            }
            catch
            {
                pictureBoxrevu.Image = null;
            }
            // affiche la liste des commandes des revues
            AfficheReceptionCommandeRevue();
        }

        /// <summary>
        /// Récupère et affiche les commandes d'une revue 
        /// </summary>
        private void AfficheReceptionCommandeRevue()
        {
            string idDocument = txtboxdocurevue.Text;
            lescommandesabonnements = controller.GetAbonnement(idDocument);
            Console.WriteLine("******** lescommandesabonnements =" + lescommandesabonnements.Count );
            RemplirCommandesRevueListe(lescommandesabonnements);
            AccesGroupBoxdelacommandederevue(true);
        }

        /// <summary>
        /// Bouton permettant d'enregistrer une commande de revue, de l'ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonenregistrer_Click(object sender, EventArgs e)
        {
            if (!txtboxcommandenumerorevue.Text.Equals("") && !txtboxmontantrevue.Text.Equals("") )
            {

                string id = txtboxcommandenumerorevue.Text;
                DateTime dateCommande = dateTimePickerrevue.Value;
                double montant = double.Parse(txtboxmontantrevue.Text);
                DateTime dateFinAbonnement = dateTimePickerfin.Value;
                string idRevue = txtboxdocurevue.Text;


                Commande commande = new Commande(id, dateCommande, montant);
                Abonnement abonnement = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);
                if (controller.CreerCommandes(commande) && controller.CreerCommandesRevue(id,dateFinAbonnement,idRevue))
                {
                    AfficheReceptionCommandeRevue();
                }
                else
                {
                    MessageBox.Show("numéro de commande déjà existant", "Erreur");
                }

            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires", "Information");
            }

        }

        /// <summary>
        /// Tri sur colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandesRevues_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandesRevues.Columns[e.ColumnIndex].HeaderText;
            List<Abonnement> sortedList = new List<Abonnement>();
            switch (titreColonne)
            {
                case "Date de la commande":
                    sortedList = lescommandesabonnements.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = lescommandesabonnements.OrderBy(o => o.Montant).ToList();
                    break;
                case "Date de fin d'abonnement":
                    sortedList = lescommandesabonnements.OrderBy(o => o.DateFinAbonnement).Reverse().ToList();
                    break;
            }
            RemplirCommandesRevueListe(sortedList);
        }

       

        /// <summary>
        /// Bouton qui vérifie qu'aucun exemplaire est relié à un abonnement et permet ensuite de le supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {

            Abonnement abonnement = (Abonnement)bdgCommandesRevue.Current;
            if (MessageBox.Show("Souhaitez-vous confirmer la supression?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
               
                if (controller.Exemplairesverifie(abonnement))
                {
                    if (controller.SupprimerCommande(abonnement.Id))
                    {
                        AfficheReceptionCommandeRevue();
                    }
                    else
                    {
                        MessageBox.Show("Une erreur s'est produite.", "Erreur");
                    }
                }
                else
                {
                    MessageBox.Show("Cet abonnement est lié à des exemplaires.", "Attention");
                }
            }
        }
        #endregion

        
    }
}