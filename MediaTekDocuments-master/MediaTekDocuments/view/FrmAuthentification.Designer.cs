
namespace MediaTekDocuments.view
{
    partial class FrmAuthentification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnseconnecter = new System.Windows.Forms.Button();
            this.txtboxutilisateur = new System.Windows.Forms.TextBox();
            this.txtboxmdp = new System.Windows.Forms.TextBox();
            this.btnannuler = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nom d\'utilisateur : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mot de passe : ";
            // 
            // btnseconnecter
            // 
            this.btnseconnecter.Location = new System.Drawing.Point(8, 129);
            this.btnseconnecter.Margin = new System.Windows.Forms.Padding(2);
            this.btnseconnecter.Name = "btnseconnecter";
            this.btnseconnecter.Size = new System.Drawing.Size(262, 34);
            this.btnseconnecter.TabIndex = 3;
            this.btnseconnecter.Text = "Se connecter";
            this.btnseconnecter.UseVisualStyleBackColor = true;
            this.btnseconnecter.Click += new System.EventHandler(this.btnseconnecter_Click);
            // 
            // txtboxutilisateur
            // 
            this.txtboxutilisateur.Location = new System.Drawing.Point(125, 55);
            this.txtboxutilisateur.Margin = new System.Windows.Forms.Padding(2);
            this.txtboxutilisateur.Name = "txtboxutilisateur";
            this.txtboxutilisateur.Size = new System.Drawing.Size(337, 20);
            this.txtboxutilisateur.TabIndex = 4;
            // 
            // txtboxmdp
            // 
            this.txtboxmdp.Location = new System.Drawing.Point(125, 94);
            this.txtboxmdp.Margin = new System.Windows.Forms.Padding(2);
            this.txtboxmdp.Name = "txtboxmdp";
            this.txtboxmdp.PasswordChar = '*';
            this.txtboxmdp.Size = new System.Drawing.Size(337, 20);
            this.txtboxmdp.TabIndex = 5;
            // 
            // btnannuler
            // 
            this.btnannuler.Location = new System.Drawing.Point(291, 129);
            this.btnannuler.Margin = new System.Windows.Forms.Padding(2);
            this.btnannuler.Name = "btnannuler";
            this.btnannuler.Size = new System.Drawing.Size(237, 34);
            this.btnannuler.TabIndex = 6;
            this.btnannuler.Text = "Annuler";
            this.btnannuler.UseVisualStyleBackColor = true;
            this.btnannuler.Click += new System.EventHandler(this.btnannuler_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(242, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Login";
            // 
            // FrmAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 171);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnannuler);
            this.Controls.Add(this.txtboxmdp);
            this.Controls.Add(this.txtboxutilisateur);
            this.Controls.Add(this.btnseconnecter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmAuthentification";
            this.Text = "FrmAuthentification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnseconnecter;
        private System.Windows.Forms.TextBox txtboxutilisateur;
        private System.Windows.Forms.TextBox txtboxmdp;
        private System.Windows.Forms.Button btnannuler;
        private System.Windows.Forms.Label label1;
    }
}