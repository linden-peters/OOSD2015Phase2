/*
 * Author: Geetha Muniswamy
 * Date: July 17, 2015
 * Usage: Form to add, modify, delete productsupplier information.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelData;

namespace CMP248WorkShop2_Team6
{
    public partial class frmProductSupplierMaintenance : Form
    {
        public frmProductSupplierMaintenance()
        {
            InitializeComponent();
        }

        List<Product> products = null;
        List<Supplier> suppliers = null;
        int productId;
        int supplierId;
        int productSupplierId;

        //Fill the product and suppliers details while page loads
        private void frmProductSupplierMaintenance_Load(object sender, EventArgs e)
        {
            lblProductSupplierInfo.Text = "";
            try
            {
                //get records for products and suppliers 
                products = ProductDB.GetAllProducts();
                suppliers = SupplierDB.GetAllSuppliers();

                //display records in products and supplier grid
                productDataGridView.DataSource = products;
                supplierDataGridView.DataSource = suppliers;

                productDataGridView.Columns[0].Width = 80;
                productDataGridView.Columns[1].Width = 150;

                supplierDataGridView.Columns[0].Width = 80;
                supplierDataGridView.Columns[1].Width = 150;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        //Get the productid on selecting any product record
        private void productDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int currentRow = productDataGridView.CurrentRow.Index;
            productId = Convert.ToInt32(productDataGridView.Rows[currentRow].Cells[0].Value);
            lblSelectedProduct.Text = "Selected Product Id is " + productId.ToString();
        }

        //Get supplierId on selecting any supplier record
        private void supplierDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int currentRow = supplierDataGridView.CurrentRow.Index;
            supplierId = Convert.ToInt32(supplierDataGridView.Rows[currentRow].Cells[0].Value);
            lblSelectedSupplier.Text = "Selected Supplier Id is " + supplierId.ToString();
        }      

        //Method to add product and supplier to the productsupplier table
        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<ProductSupplier> productSupplier = null;
            try
            {
                if (!(productId < 0 && supplierId < 0))
                {
                   // int pscount = ProductSupplierDB.CheckProductSupplier(productId, supplierId);
                     productSupplier = ProductSupplierDB.GetProductSupplierbyProdIdSupId(productId, supplierId);
                    if (productSupplier.Count > 0)
                    {
                        lblProductSupplierInfo.Text = "The selected Product and Suppliers already exists.";
                    }
                    else
                    {
                        lblProductSupplierInfo.Text = "";
                        bool addStatus = ProductSupplierDB.AddProductSupplier(productId, supplierId);
                        if (addStatus)
                        {
                            productSupplier = ProductSupplierDB.GetProductSupplierbyProdIdSupId(productId, supplierId);
                            foreach (ProductSupplier ps in productSupplier)
                            {
                                //productSupplierDataGridView.Rows.Clear();
                                productSupplierDataGridView.Rows.Add(ps.ProductSupplierId, ps.ProductId, ps.SupplierId);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        //Method to delete the product supplier record
        private void btnDelete_Click(object sender, EventArgs e)
        {
           
            int currentRow = productSupplierDataGridView.CurrentRow.Index;
            if (currentRow < 0) return;
            productSupplierId = Convert.ToInt32(productSupplierDataGridView.Rows[currentRow].Cells[0].Value);

            DialogResult formResult = MessageBox.Show("Are you sure you want to delete the record for the ProductSupplier?\n" + productSupplierId,
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Prompt user to avoid accidental deletion.
            if (formResult == DialogResult.Yes)
            {

                try
                {
                    bool deleteStatus = ProductSupplierDB.DeleteProductSupplier(productSupplierId);
                    if (deleteStatus)
                        productSupplierDataGridView.Rows.RemoveAt(currentRow);         
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        //Method to modify the product supplier details
        private void btnModify_Click(object sender, EventArgs e)
        {   
            //Gives Row Index
            //int currentRow = productSupplierDataGridView.CurrentCell.RowIndex;
            int currentRow = productSupplierDataGridView.CurrentRow.Index;

            //Get old ProductSupplier details
            ProductSupplier oldProductSupplier = new ProductSupplier();
            oldProductSupplier.ProductSupplierId = Convert.ToInt32(productSupplierDataGridView.Rows[currentRow].Cells[0].Value);
            oldProductSupplier.ProductId = Convert.ToInt32(productSupplierDataGridView.Rows[currentRow].Cells[1].Value);
            oldProductSupplier.SupplierId = Convert.ToInt32(productSupplierDataGridView.Rows[currentRow].Cells[2].Value);

            //Get new Product supplier details
            int selectProduct = productDataGridView.CurrentRow.Index;
            int selectSupplier = supplierDataGridView.CurrentRow.Index;
            ProductSupplier productSupplier = new ProductSupplier();
            productSupplier.ProductSupplierId = oldProductSupplier.ProductSupplierId;
            productSupplier.ProductId = Convert.ToInt32(productDataGridView.Rows[selectProduct].Cells[0].Value);
            productSupplier.SupplierId = Convert.ToInt32(supplierDataGridView.Rows[selectSupplier].Cells[0].Value);

            try
            {
                //Check the database already having the product supplier relationship
                List<ProductSupplier> productSuppliersize =
                    ProductSupplierDB.GetProductSupplierbyProdIdSupId(productSupplier.ProductId, productSupplier.SupplierId);
                if (productSuppliersize.Count > 0)
                {
                    lblProductSupplierInfo.Text = "The selected Product and Suppliers already exists.";
                }
                else    // if product supplier relation is not present in database
                {
                    lblProductSupplierInfo.Text = "";
                    bool updateStatus = ProductSupplierDB.UpdateProductSupplier(oldProductSupplier, productSupplier);
                    if (updateStatus)
                    {
                        productSupplierDataGridView.Rows[currentRow].Cells[1].Value = productSupplier.ProductId;
                        productSupplierDataGridView.Rows[currentRow].Cells[2].Value = productSupplier.SupplierId;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        
        //Method to retrieve the product supplier details based on productsupplierId
        private void btnGetProductSupplier_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtProductSupplierId) &&
                Validator.IsInt32(txtProductSupplierId) &&
                Validator.IsPositiveOverZero(txtProductSupplierId))
            {
                int productSupplierId = Convert.ToInt32(txtProductSupplierId.Text);
                List<ProductSupplier> productSupplier = ProductSupplierDB.GetProductSupplierByProductSupplierId(productSupplierId);
                if (productSupplier.Count <= 0)
                {
                    lblProductSupplierInfo.Text = "No records found for the productSupplier " + productSupplierId;
                }
                else
                {
                    lblProductSupplierInfo.Text = "";
                    foreach (ProductSupplier ps in productSupplier)
                    {
                        productSupplierDataGridView.Rows.Clear();
                        productSupplierDataGridView.Rows.Add(ps.ProductSupplierId, ps.ProductId, ps.SupplierId);
                    }
                }
            }
        }

        //Get product supplier records based on productId and supplierId
        private void btnGetProdSuppByProdIdSupId_Click(object sender, EventArgs e)
        {

            if (Validator.IsPresent(txtProductId) && Validator.IsInt32(txtProductId) && Validator.IsPositive(txtProductId) &&
                Validator.IsPresent(txtSupplierId) && Validator.IsInt32(txtSupplierId) && Validator.IsPositive(txtSupplierId))
            {
                int productId = Convert.ToInt32(txtProductId.Text);
                int supplierId = Convert.ToInt32(txtSupplierId.Text);
                List<ProductSupplier> productSupplier = ProductSupplierDB.GetProductSupplierbyProdIdSupId(productId, supplierId);
                if (productSupplier.Count <= 0)
                {
                    lblProductSupplierInfo.Text = "No records found for the productId " + productId + " and SupplierId " + supplierId;
                }
                else
                {
                    lblProductSupplierInfo.Text = "";
                    foreach (ProductSupplier ps in productSupplier)
                    {
                        productSupplierDataGridView.Rows.Clear();   //clear rows in data grid view
                        productSupplierDataGridView.Rows.Add(ps.ProductSupplierId, ps.ProductId, ps.SupplierId);
                    }
                }
            }
        }

        //Method to clear the product supplier records displayed in grid view
        private void btnClearProductSupplier_Click(object sender, EventArgs e)
        {
            productSupplierDataGridView.Rows.Clear();
        }

        //Close the form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }                    
    }
}
