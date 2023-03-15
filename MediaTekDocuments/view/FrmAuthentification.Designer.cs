
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
            this.label2.Location = new System.Drawing.Point(44, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nom d\'utilisateur : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mot de passe : ";
            // 
            // btnseconnecter
            // 
            this.btnseconnecter.Location = new System.Drawing.Point(12, 198);
            this.btnseconnecter.Name = "btnseconnecter";
            this.btnseconnecter.Size = new System.Drawing.Size(393, 52);
            this.btnseconnecter.TabIndex = 3;
            this.btnseconnecter.Text = "Se connecter";
            this.btnseconnecter.UseVisualStyleBackColor = true;
            this.btnseconnecter.Click += new System.EventHandler(this.btnseconnecter_Click);
            // 
            // txtboxutilisateur
            // 
            this.txtboxutilisateur.Location = new System.Drawing.Point(188, 85);
            this.txtboxutilisateur.Name = "txtboxutilisateur";
            this.txtboxutilisateur.Size = new System.Drawing.Size(504, 26);
            this.txtboxutilisateur.TabIndex = 4;
            // 
            // txtboxmdp
            // 
            this.txtboxmdp.Location = new System.Drawing.Point(188, 145);
            this.txtboxmdp.Name = "txtboxmdp";
            this.txtboxmdp.PasswordChar = '*';
            this.txtboxmdp.Size = new System.Drawing.Size(504, 26);
            this.txtboxmdp.TabIndex = 5;
            // 
            // btnannuler
            // 
            this.btnannuler.Location = new System.Drawing.Point(436, 198);
            this.btnannuler.Name = "btnannuler";
            this.btnannuler.Size = new System.Drawing.Size(356, 52);
            this.btnannuler.TabIndex = 6;
            this.btnannuler.Text = "Annuler";
            this.btnannuler.UseVisualStyleBackColor = true;
            this.btnannuler.Click += new System.EventHandler(this.btnannuler_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(363, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Login";
            // 
            // FrmAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 263);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnannuler);
            this.Controls.Add(this.txtboxmdp);
            this.Controls.Add(this.txtboxutilisateur);
            this.Controls.Add(this.btnseconnecter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
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