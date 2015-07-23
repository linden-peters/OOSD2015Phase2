/*
 * Author: Linden & Sunny
 * Date: July 07, 2015
 * Usage: Form to view/edit product information.
 */
using TravelData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMP248WorkShop2_Team6
{
    public partial class frmProductMaintenance : Form
    {
        // column counts 
        const int MAX_GRID_VIEW_COLUMN_COUNT = 2;
        int curRow = -1;
        public frmProductMaintenance()
        {
            InitializeComponent();
        }

        List<Product> products = null;
        
        // initialize Product Maintenance form
        private void frmProductMaintenance_Load(object sender, EventArgs e)
        {
            // set the DataGridView's columns
            supplierDataGridView.Columns.Add("Id", "Supplier ID");
            supplierDataGridView.Columns.Add("Name", "Supplier Name");

            try
            {
                // load the products table data 
                products = ProductDB.GetAllProducts();
                productDataGridView.DataSource = products;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }


            // set supplier info Gridview control format. 
            supplierDataGridView.ColumnCount = MAX_GRID_VIEW_COLUMN_COUNT;
            supplierDataGridView.Columns[0].ReadOnly = true;
            supplierDataGridView.Columns[1].ReadOnly = true;
            //supplierDataGridView.Columns[1].HeaderText = "Supplier Name";
            supplierDataGridView.Columns[0].Width = 130;
            supplierDataGridView.Columns[1].Width = 210;

            curRow = 0;
            productDataGridView.CurrentCell = productDataGridView.Rows[0].Cells[0];
            try
            {
                List<Supplier> suppliers = SupplierDB.GetSuppliersByProduct(Convert.ToInt32(productDataGridView.Rows[curRow].Cells[1].Value));
                //supplierDataGridView.DataSource = suppliers;
                foreach (Supplier s in suppliers)
                {
                    //string[] row1 = new string[] { id.ToString(), name };
                    int rowid = supplierDataGridView.Rows.Add(s.SupplierId.ToString(), s.SupName);
                    if (rowid % 2 == 1)
                    {
                        // set background color for specific rows
                        supplierDataGridView.Rows[rowid].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Add new Product to database.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //int gridCount = productDataGridView.Rows.Count;
            //for (int i = 0; i < gridCount; i++)
            //{
            //}
            //productDataGridView.Rows.Add(); // Doesn't work due to data binding
            if (Validator.IsPresent(txtNewName))
            {
                Product newProduct = new Product();
                newProduct.ProdName = txtNewName.Text;
                try
                {
                    ProductDB.AddProduct(newProduct);
                    txtNewName.Text = ""; // clear field
                    // reload the products table data (and related suppliers table)
                    products = ProductDB.GetAllProducts();
                    productDataGridView.DataSource = products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        // Modify existing Product in database.
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtNewName))
            {
                int index = productDataGridView.CurrentCell.RowIndex;
                if (index < 0) return;
                int prodId = Convert.ToInt32(productDataGridView.Rows[index].Cells[0].Value);
                string prodName = Convert.ToString(productDataGridView.Rows[index].Cells[1].Value);
                Product oldProduct = new Product();
                oldProduct.ProductId = prodId;
                oldProduct.ProdName = prodName;
                Product newProduct = new Product();
                newProduct.ProductId = prodId;
                newProduct.ProdName = txtNewName.Text;
                try
                {
                    ProductDB.UpdateProduct(oldProduct, newProduct);
                    txtNewName.Text = ""; // clear field
                    // reload the products table data (and related suppliers table)
                    products = ProductDB.GetAllProducts();
                    productDataGridView.DataSource = products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        // Delete Product and links to Supplier from database.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = productDataGridView.CurrentCell.RowIndex;
            if (index < 0) return;
            int prodId = Convert.ToInt32(productDataGridView.Rows[index].Cells[0].Value);
            string prodName = Convert.ToString(productDataGridView.Rows[index].Cells[1].Value);
            DialogResult formResult = MessageBox.Show("Are you sure you want to delete the following entry?\n" + prodId + "\n" + prodName,
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Prompt user to avoid accidental deletion.
            if (formResult == DialogResult.Yes)
            {
                try
                {
                    Product target = new Product();
                    target.ProductId = prodId;
                    target.ProdName = prodName;
                    ProductDB.DeleteProduct(target);
                    // reload the products table data (and related suppliers table)
                    products = ProductDB.GetAllProducts();
                    productDataGridView.DataSource = products;
                    //productDataGridView.Rows.RemoveAt(index);
                    /*
                    if (!ProductDB.DelProduct(product))
                    {
                        MessageBox.Show("Failed to delete matching record, it might have been changed/deleted.", "DB Error");
                        this.GetProduct(product.ProductCode);
                        if (product != null)
                            ShowProduct();
                        else
                            ResetForm();
                    }
                    else
                        ResetForm();
                    */
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        // user selection row changed.
        private void productDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // SELECTED ROW Changed. Update view of linked suppliers.
            if (productDataGridView.CurrentRow.Index != curRow)
            {
                curRow = productDataGridView.CurrentRow.Index;
                supplierDataGridView.Rows.Clear();

                int productid = Convert.ToInt32(productDataGridView.Rows[curRow].Cells[0].Value);
                lblInfo.Text = "The Suppliers for Product [" + (productDataGridView.Rows[curRow].Cells[1].Value.ToString()) + "] is:"; // update label for Suppliers view
                List<Supplier> suppliers = SupplierDB.GetSuppliersByProduct(productid);
                //supplierDataGridView.DataSource = suppliers;
                foreach (Supplier s in suppliers)
                {
                    //string[] row1 = new string[] { id.ToString(), name };
                    int rowid = supplierDataGridView.Rows.Add(s.SupplierId.ToString(), s.SupName);
                    if (rowid % 2 == 1)
                    {
                        // set background color for specific rows
                        supplierDataGridView.Rows[rowid].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
