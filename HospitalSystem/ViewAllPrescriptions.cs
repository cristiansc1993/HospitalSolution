using System;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class ViewAllPrescriptions : Form
    {
        public ViewAllPrescriptions()
        {
            InitializeComponent();
        }

        private void ViewAllSales_Load(object sender, EventArgs e)
        {
            DataAccess _DataAccess = new DataAccess();

            foreach (Details SaleDetails in _DataAccess.RetreiveAllFacturas())
            {
                SalesGridView.Rows.Add(SaleDetails.SaleID, SaleDetails.SaleTime, SaleDetails.Name, SaleDetails.Total, "View Products");
            }
        }

        private void SalesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (SalesGridView.Columns[e.ColumnIndex].Name == "ProductsColumn")
                {
                    int SaleID = Convert.ToInt32(SalesGridView.Rows[e.RowIndex].Cells["SaleIDColumn"].Value);

                    ViewSaleItems _ViewSaleItems = new ViewSaleItems(SaleID);

                    _ViewSaleItems.ShowDialog();
                }
            }
        }
    }
}
