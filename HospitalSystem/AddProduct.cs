using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            DataAccess _DataAccess = new DataAccess();

            foreach (Details Category in _DataAccess.RetreiveAllCategoriesFromDatabase())
            {
                ProductCategoryComboBox.Items.Add(Category.Name);
            }
        }

        private void UploadPictureButton_Click(object sender, EventArgs e)
        {


            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select Image file..";
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "Media Files|*.jpg;*.png;*.gif;*.bmp;*.jpeg|All Files|*.*";

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {

                ProductPictureBox.Load(ofd.FileName);
            }
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            DataAccess _DataAccess = new DataAccess();




            MemoryStream ms = new MemoryStream();


            ProductPictureBox.Image.Save(ms, ProductPictureBox.Image.RawFormat);


            byte[] ProductPicture = ms.GetBuffer();


            ms.Close();




            if (_DataAccess.AddNewProductToDatabase(ProductNameBox.Text, Convert.ToDecimal(ProductPriceBox.Text), _DataAccess.ReturnCategoryID(ProductCategoryComboBox.SelectedItem.ToString()), ProductDescriptionRBox.Text, ProductPicture))
            {
                MessageBox.Show("Product Added");
            }
            else MessageBox.Show("Product Not Added");
        }
    }
}
