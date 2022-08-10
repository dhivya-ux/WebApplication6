using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ProductController : Controller
    {
        String connectionstring = @"Data Source = DESKTOP-ERUKNF9\DHIVYALEARN; Initial Catalog = Customer_Registration; Integrated Security=True";
       [HttpGet]
        public ActionResult Index()

        {
            DataTable dtblProcut = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Product", sqlcon);
                sqlDa.Fill(dtblProcut);
            }
                return View(dtblProcut);
        }

       [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }


        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                
                sqlcon.Open();
                string query = "INSERT INTO Product VALUES (@ProductName,@Price,@Count)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlcmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlcmd.ExecuteNonQuery();
               
            }
           
          
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
       
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "SELECT * FROM Product Where ProductID = @ProductID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.ProductName = dtblProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(productModel);

            }
            else
                return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "UPDATE Product SET ProductName=@ProductName,Price=@Price,Count=@Count where ProductID=@ProductID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                sqlcmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlcmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "DELETE from Product where ProductID=@ProductID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ProductID", id);
                
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        
    }
}
