namespace Zlagoda_Net4._7._2.Admin
{
    partial class ShowCheck
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
            this.CardLabel = new System.Windows.Forms.Label();
            this.ListProducts = new System.Windows.Forms.ListView();
            this.UPC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalLabel = new System.Windows.Forms.Label();
            this.TotalCountLabel = new System.Windows.Forms.Label();
            this.ErrorProduct = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ErrorCheck = new System.Windows.Forms.Label();
            this.CardNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CardLabel
            // 
            this.CardLabel.AutoSize = true;
            this.CardLabel.Font = new System.Drawing.Font("Fixel Text Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CardLabel.Location = new System.Drawing.Point(54, 40);
            this.CardLabel.Name = "CardLabel";
            this.CardLabel.Size = new System.Drawing.Size(124, 19);
            this.CardLabel.TabIndex = 0;
            this.CardLabel.Text = "Card number";
            // 
            // ListProducts
            // 
            this.ListProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UPC,
            this.Number,
            this.Price});
            this.ListProducts.HideSelection = false;
            this.ListProducts.Location = new System.Drawing.Point(58, 89);
            this.ListProducts.Name = "ListProducts";
            this.ListProducts.Size = new System.Drawing.Size(417, 421);
            this.ListProducts.TabIndex = 2;
            this.ListProducts.UseCompatibleStateImageBehavior = false;
            this.ListProducts.View = System.Windows.Forms.View.Details;
            // 
            // UPC
            // 
            this.UPC.Text = "UPC";
            this.UPC.Width = 181;
            // 
            // Number
            // 
            this.Number.Text = "Number";
            this.Number.Width = 102;
            // 
            // Price
            // 
            this.Price.Text = "Price";
            this.Price.Width = 128;
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Font = new System.Drawing.Font("Fixel Text Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalLabel.Location = new System.Drawing.Point(54, 540);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(53, 19);
            this.TotalLabel.TabIndex = 3;
            this.TotalLabel.Text = "Total:";
            // 
            // TotalCountLabel
            // 
            this.TotalCountLabel.AutoSize = true;
            this.TotalCountLabel.Font = new System.Drawing.Font("Fixel Text Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalCountLabel.Location = new System.Drawing.Point(113, 540);
            this.TotalCountLabel.Name = "TotalCountLabel";
            this.TotalCountLabel.Size = new System.Drawing.Size(0, 19);
            this.TotalCountLabel.TabIndex = 4;
            // 
            // ErrorProduct
            // 
            this.ErrorProduct.AutoSize = true;
            this.ErrorProduct.Font = new System.Drawing.Font("Fixel Text Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorProduct.ForeColor = System.Drawing.Color.Coral;
            this.ErrorProduct.Location = new System.Drawing.Point(487, 180);
            this.ErrorProduct.Name = "ErrorProduct";
            this.ErrorProduct.Size = new System.Drawing.Size(0, 19);
            this.ErrorProduct.TabIndex = 9;
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Fixel Text Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseButton.Location = new System.Drawing.Point(58, 592);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(284, 31);
            this.CloseButton.TabIndex = 12;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CreateCheckButton_Click);
            // 
            // ErrorCheck
            // 
            this.ErrorCheck.AutoSize = true;
            this.ErrorCheck.Font = new System.Drawing.Font("Fixel Text Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorCheck.ForeColor = System.Drawing.Color.Coral;
            this.ErrorCheck.Location = new System.Drawing.Point(54, 570);
            this.ErrorCheck.Name = "ErrorCheck";
            this.ErrorCheck.Size = new System.Drawing.Size(0, 19);
            this.ErrorCheck.TabIndex = 13;
            // 
            // CardNumber
            // 
            this.CardNumber.AutoSize = true;
            this.CardNumber.Font = new System.Drawing.Font("Fixel Text Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CardNumber.Location = new System.Drawing.Point(184, 40);
            this.CardNumber.Name = "CardNumber";
            this.CardNumber.Size = new System.Drawing.Size(124, 19);
            this.CardNumber.TabIndex = 14;
            this.CardNumber.Text = "Card number";
            // 
            // ShowCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 646);
            this.Controls.Add(this.CardNumber);
            this.Controls.Add(this.ErrorCheck);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ErrorProduct);
            this.Controls.Add(this.TotalCountLabel);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.ListProducts);
            this.Controls.Add(this.CardLabel);
            this.Name = "ShowCheck";
            this.Text = "AddCheck";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CardLabel;
        private System.Windows.Forms.ListView ListProducts;
        private System.Windows.Forms.ColumnHeader UPC;
        private System.Windows.Forms.ColumnHeader Number;
        private System.Windows.Forms.ColumnHeader Price;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Label TotalCountLabel;
        private System.Windows.Forms.Label ErrorProduct;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label ErrorCheck;
        private System.Windows.Forms.Label CardNumber;
    }
}