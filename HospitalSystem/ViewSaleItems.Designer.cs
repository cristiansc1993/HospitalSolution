﻿namespace HospitalSystem
{
    partial class ViewSaleItems
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
            this.SalesGroupBox = new System.Windows.Forms.GroupBox();
            this.SaleItemsGridView = new System.Windows.Forms.DataGridView();
            this.ProductNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductQuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductTotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaleItemsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SalesGroupBox
            // 
            this.SalesGroupBox.Controls.Add(this.SaleItemsGridView);
            this.SalesGroupBox.Font = new System.Drawing.Font("Proxima Nova Cn Rg", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalesGroupBox.ForeColor = System.Drawing.Color.Black;
            this.SalesGroupBox.Location = new System.Drawing.Point(14, 18);
            this.SalesGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SalesGroupBox.Name = "SalesGroupBox";
            this.SalesGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SalesGroupBox.Size = new System.Drawing.Size(688, 509);
            this.SalesGroupBox.TabIndex = 2;
            this.SalesGroupBox.TabStop = false;
            this.SalesGroupBox.Text = "Sales Items";
            // 
            // SaleItemsGridView
            // 
            this.SaleItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SaleItemsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.SaleItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SaleItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductNameColumn,
            this.ProductPriceColumn,
            this.ProductQuantityColumn,
            this.ProductTotalColumn});
            this.SaleItemsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaleItemsGridView.Location = new System.Drawing.Point(3, 20);
            this.SaleItemsGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SaleItemsGridView.Name = "SaleItemsGridView";
            this.SaleItemsGridView.Size = new System.Drawing.Size(682, 485);
            this.SaleItemsGridView.TabIndex = 0;
            // 
            // ProductNameColumn
            // 
            this.ProductNameColumn.HeaderText = "Name";
            this.ProductNameColumn.Name = "ProductNameColumn";
            // 
            // ProductPriceColumn
            // 
            this.ProductPriceColumn.HeaderText = "Price";
            this.ProductPriceColumn.Name = "ProductPriceColumn";
            // 
            // ProductQuantityColumn
            // 
            this.ProductQuantityColumn.HeaderText = "Quantity";
            this.ProductQuantityColumn.Name = "ProductQuantityColumn";
            // 
            // ProductTotalColumn
            // 
            this.ProductTotalColumn.HeaderText = "Total Price";
            this.ProductTotalColumn.Name = "ProductTotalColumn";
            this.ProductTotalColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ViewSaleItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(716, 544);
            this.Controls.Add(this.SalesGroupBox);
            this.Font = new System.Drawing.Font("Brandon Grotesque Regular", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ViewSaleItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ViewSaleItems";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Load += new System.EventHandler(this.ViewSaleItems_Load);
            this.SalesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SaleItemsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SalesGroupBox;
        private System.Windows.Forms.DataGridView SaleItemsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductQuantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductTotalColumn;
    }
}