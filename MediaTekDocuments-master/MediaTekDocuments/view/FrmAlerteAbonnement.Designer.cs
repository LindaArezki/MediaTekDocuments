
namespace MediaTekDocuments.view
{
    partial class FrmAlerteAbonnement
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAbonnements = new System.Windows.Forms.DataGridView();
            this.buttoncontinuer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbonnements)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(75, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = " Abonnements expirant dans 30 jours : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgvAbonnements
            // 
            this.dgvAbonnements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAbonnements.Location = new System.Drawing.Point(28, 77);
            this.dgvAbonnements.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvAbonnements.Name = "dgvAbonnements";
            this.dgvAbonnements.RowHeadersWidth = 62;
            this.dgvAbonnements.RowTemplate.Height = 28;
            this.dgvAbonnements.Size = new System.Drawing.Size(576, 257);
            this.dgvAbonnements.TabIndex = 1;
            // 
            // buttoncontinuer
            // 
            this.buttoncontinuer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncontinuer.Location = new System.Drawing.Point(29, 352);
            this.buttoncontinuer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttoncontinuer.Name = "buttoncontinuer";
            this.buttoncontinuer.Size = new System.Drawing.Size(563, 48);
            this.buttoncontinuer.TabIndex = 2;
            this.buttoncontinuer.Text = "Continuer";
            this.buttoncontinuer.UseVisualStyleBackColor = true;
            this.buttoncontinuer.Click += new System.EventHandler(this.buttoncontinuer_Click);
            // 
            // FrmAlerteAbonnement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 424);
            this.Controls.Add(this.buttoncontinuer);
            this.Controls.Add(this.dgvAbonnements);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmAlerteAbonnement";
            this.Text = "FrmAlerteAbonnement";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbonnements)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAbonnements;
        private System.Windows.Forms.Button buttoncontinuer;
    }
}