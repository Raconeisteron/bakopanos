namespace DeadDevsSociety.UnityDependencyInjection.Gui
{
    partial class ProductsView
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
            this.components = new System.ComponentModel.Container();
            this.buttonGetProducts = new System.Windows.Forms.Button();
            this.listBoxProducts = new System.Windows.Forms.ListBox();
            this.productModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.productModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGetProducts
            // 
            this.buttonGetProducts.Location = new System.Drawing.Point(12, 12);
            this.buttonGetProducts.Name = "buttonGetProducts";
            this.buttonGetProducts.Size = new System.Drawing.Size(120, 23);
            this.buttonGetProducts.TabIndex = 0;
            this.buttonGetProducts.Text = "GetProducts";
            this.buttonGetProducts.UseVisualStyleBackColor = true;
            // 
            // listBoxProducts
            // 
            this.listBoxProducts.DataSource = this.productModelBindingSource;
            this.listBoxProducts.DisplayMember = "DisplayName";
            this.listBoxProducts.FormattingEnabled = true;
            this.listBoxProducts.Location = new System.Drawing.Point(12, 41);
            this.listBoxProducts.Name = "listBoxProducts";
            this.listBoxProducts.Size = new System.Drawing.Size(185, 160);
            this.listBoxProducts.TabIndex = 1;
            this.listBoxProducts.ValueMember = "Product";
            // 
            // productModelBindingSource
            // 
            this.productModelBindingSource.DataSource = typeof(DeadDevsSociety.UnityDependencyInjection.Gui.ProductModel);
            // 
            // ProductsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 321);
            this.Controls.Add(this.listBoxProducts);
            this.Controls.Add(this.buttonGetProducts);
            this.Name = "ProductsView";
            this.Text = "ProductsView";
            ((System.ComponentModel.ISupportInitialize)(this.productModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGetProducts;
        private System.Windows.Forms.ListBox listBoxProducts;
        private System.Windows.Forms.BindingSource productModelBindingSource;
    }
}

