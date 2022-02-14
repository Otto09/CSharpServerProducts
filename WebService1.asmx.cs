using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServerProducts
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        SqlConnection myCon = new SqlConnection();

        [WebMethod]
        public void AddProducts(string cod_prod, string name, int price, string availability,
            string best_seller)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            // SqlDataAdapter = contains a set of data commands and connection to db and
            // is used to exchange data between dataset and db
            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Products ORDER BY cod_prod", myCon);

            // SqlCommandBuilder can generate CRUD statements using the SelectCommand property
            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);

            // DataSet - data manipulation
            DataSet dsProducts = new DataSet();

            // using the dataAdapter and the Fill property, we will make a copy of the data from
            // db in dataSet
            productsAdapter.Fill(dsProducts, "Products");

            // a new row of type Products is created
            DataRow newRow = dsProducts.Tables["Products"].NewRow();

            // populate the newly created row
            newRow["cod_prod"] = cod_prod;
            newRow["name"] = name;
            newRow["price"] = price;
            newRow["availability"] = availability;
            newRow["best_seller"] = best_seller;

            // We add in the dataSet
            dsProducts.Tables["Products"].Rows.Add(newRow);

            // update with the new dataSet in db
            productsAdapter.Update(dsProducts, "Products");
            
            myCon.Close();
        }

        [WebMethod]
        public void AddClients(int id_cl, string cod_prod, string name, string address, string phone)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();
            
            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Clients ORDER BY Id_cl", myCon);
           
            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);

            // we will create a new dataSet
            DataSet dsClients = new DataSet();

            // using the dataAdapter and the Fill property, we will make a copy of the data from
            // db in dataSet
            productsAdapter.Fill(dsClients, "Clients");

            // a new line of type Clients is created
            DataRow newRow = dsClients.Tables["Clients"].NewRow();

            // populate the newly created row
            newRow["Id_cl"] = id_cl;
            newRow["cod_prod"] = cod_prod;
            newRow["name"] = name;
            newRow["address"] = address;
            newRow["phone"] = phone;

            // We add in the dataSet
            dsClients.Tables["Clients"].Rows.Add(newRow);

            // update with the new dataSet in db
            productsAdapter.Update(dsClients, "Clients");
            
            myCon.Close();
        }

        [WebMethod]
        public void AddComments(int id_cm, int id_cl, char rating, string comment)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();
            
            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Comments ORDER BY Id_cm", myCon);
           
            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);

            // we use a new dataset to make the addition
            DataSet dsComments = new DataSet();

            //we will make a copy of the data from db in dataSet
            productsAdapter.Fill(dsComments, "Comments");

            // a new row of type Comments is created
            DataRow newRow = dsComments.Tables["Comments"].NewRow();

            // populate the newly created row
            newRow["Id_cm"] = id_cm;
            newRow["Id_cl"] = id_cl;
            newRow["Rating"] = rating;
            newRow["Comment"] = comment;

            // We add in the dataSet
            dsComments.Tables["Comments"].Rows.Add(newRow);

            // update with the new dataSet in db
            productsAdapter.Update(dsComments, "Comments");
            
            myCon.Close();
        }

        [WebMethod]
        public void DeleteProducts(string cod_prod)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();
            
            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Products ORDER BY cod_prod", myCon);
            //we make querry at db

            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);
            
            DataSet dsDeleteProducts = new DataSet(); // we use the dataSet to manipulate the data

            productsAdapter.Fill(dsDeleteProducts, "Products"); // in dataSet we make a copy of the table

            DataColumn[] pk = new DataColumn[1]; // define an array of DataColumn objects with 1 element
             
            pk[0] = dsDeleteProducts.Tables["Products"].Columns["cod_prod"]; // on the first element
            // from the array we put the cod_prod column which is of type DataColumn
    
            dsDeleteProducts.Tables["Products"].PrimaryKey = pk; // set to dataSet as
            // cod_prod primary key that I defined earlier

            // we look for the Row corresponding to the cod_prod received on the method
            DataRow caut = null;
            while (caut == null)
            {
                caut = dsDeleteProducts.Tables["Products"].Rows.Find(cod_prod); // search for cod_prod
            }
            
            caut.Delete(); // delete Row found from dataSet

            productsAdapter.Update(dsDeleteProducts, "Products"); // update the table in db with the dataSet

            myCon.Close();
        }

        [WebMethod]
        public void DeleteClients(int id_cl)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();
            
            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Clients ORDER BY Id_cl", myCon);
           
            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);
            
            DataSet dsDeleteClients = new DataSet();
            
            productsAdapter.Fill(dsDeleteClients, "Clients"); // in dataSet we make a copy of the table

            DataColumn[] pk = new DataColumn[1]; // define an array of DataColumn objects with 1 element

            pk[0] = dsDeleteClients.Tables["Clients"].Columns["Id_cl"]; // on the first element of
            // array we put the Id_cl column which is of type DataColumn
 
            dsDeleteClients.Tables["Clients"].PrimaryKey = pk; // set to dataSet as
            // Id_cl primary key that I defined earlier

            // we look for the Row corresponding to the id_cl received on the method
            DataRow caut = null;
            while (caut == null)
            {
                caut = dsDeleteClients.Tables["Clients"].Rows.Find(id_cl); // search for id_cl
            }
            
            caut.Delete(); // delete Row found from dataSet
            
            productsAdapter.Update(dsDeleteClients, "Clients"); // update the table in db with the dataSet

            myCon.Close();
        }

        [WebMethod]
        public void DeleteComments(int id_cm)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Comments ORDER BY Id_cm", myCon);

            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);

            DataSet dsDeleteComments = new DataSet();

            productsAdapter.Fill(dsDeleteComments, "Comments"); // in dataSet we make a copy of the table

            DataColumn[] pk = new DataColumn[1]; // define an array of DataColumn objects with 1 element

            pk[0] = dsDeleteComments.Tables["Comments"].Columns["Id_cm"]; // on the first element of
            // array we put the Id_cm column which is of type DataColumn

            dsDeleteComments.Tables["Comments"].PrimaryKey = pk; // set to dataSet as
            // Id_cm primary key that I defined earlier

            // we look for the Row corresponding to the id_cm received on the method
            DataRow caut = null;
            while (caut == null)
            {
                caut = dsDeleteComments.Tables["Comments"].Rows.Find(id_cm); // search for id_cm
            }

            caut.Delete(); // delete Row found from dataSet

            productsAdapter.Update(dsDeleteComments, "Comments"); // update the table in db with the dataSet

            myCon.Close();
        }

        [WebMethod]
        public void UpdateClients(int id_cl, string name, string address, string phone)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Clients ORDER BY Id_cl", myCon);
           
            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);
            
            DataSet dsUpdateClients = new DataSet();
            
            productsAdapter.Fill(dsUpdateClients, "Clients"); // in dataSet we make a copy of the table
            
            DataColumn[] pk = new DataColumn[1]; // define an array of DataColumn objects with 1 element

            pk[0] = dsUpdateClients.Tables["Clients"].Columns["Id_cl"]; // on the first element of
            // array we put the Id_cl column which is of type DataColumn
            
            dsUpdateClients.Tables["Clients"].PrimaryKey = pk; // set to dataSet as
            // Id_cl primary key that I defined earlier

            // we look for the Row corresponding to the id_cl received on the method
            DataRow caut = null;
            while (caut == null)
            {
                caut = dsUpdateClients.Tables["Clients"].Rows.Find(id_cl); // search for id_cl
            }

            // populate the created row
            caut["name"] = name;
            caut["address"] = address;
            caut["phone"] = phone;

            // update the table in db with the new dataSet
            productsAdapter.Update(dsUpdateClients, "Clients");
            
            myCon.Close();
        }

        [WebMethod]
        public void UpdateProducts(string cod_prod, string name, int price, string availability, string best_seller)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Products ORDER BY cod_prod", myCon);
           
            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);
            
            DataSet dsUpdateProducts = new DataSet();
            
            productsAdapter.Fill(dsUpdateProducts, "Products"); // in dataSet we make a copy of the table

            DataColumn[] pk = new DataColumn[1]; // define an array of DataColumn objects with 1 element

            pk[0] = dsUpdateProducts.Tables["Products"].Columns["cod_prod"]; // on the first element of
            // array we put the cod_prod column which is of type DataColumn

            dsUpdateProducts.Tables["Products"].PrimaryKey = pk; // set to dataSet as
                                                                 // cod_prod primary key that I defined earlier

            // we look for the Row corresponding to the cod_prod received on the method
            DataRow caut = null;
            while (caut == null)
            {
                caut = dsUpdateProducts.Tables["Products"].Rows.Find(cod_prod); // search for cod_prod
            }

            // populate the created row
            caut["name"] = name;
            caut["price"] = price;
            caut["availability"] = availability;
            caut["best_seller"] = best_seller;

            // update the table in db with the new dataSet
            productsAdapter.Update(dsUpdateProducts, "Products");
            
            myCon.Close();
        }

        [WebMethod]
        public void UpdateComments(int id_cm, int id_cl, char rating, string comment)
        {
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IAP\PAC#\Exemple\ServerProducts\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            SqlDataAdapter productsAdapter = new SqlDataAdapter("SELECT * FROM Comments ORDER BY Id_cm", myCon);

            SqlCommandBuilder productsBuilder = new SqlCommandBuilder(productsAdapter);

            DataSet dsUpdateComments = new DataSet();

            productsAdapter.Fill(dsUpdateComments, "Comments"); // in dataSet we make a copy of the table

            DataColumn[] pk = new DataColumn[1]; // define an array of DataColumn objects with 1 element

            pk[0] = dsUpdateComments.Tables["Comments"].Columns["Id_cm"]; // on the first element of
            // array we put the Id_cm column which is of type DataColumn

            dsUpdateComments.Tables["Comments"].PrimaryKey = pk; // set to dataSet as
            // Id_cm primary key that I defined earlier

            // we look for the Row corresponding to the id_cm received on the method
            DataRow caut = null;
            while (caut == null)
            {
                caut = dsUpdateComments.Tables["Comments"].Rows.Find(id_cm); // search for id_cm
            }

            // populate the created row
            caut["Id_cl"] = id_cl;
            caut["Rating"] = rating;
            caut["Comment"] = comment;

            // update the table in db with the new dataSet
            productsAdapter.Update(dsUpdateComments, "Comments");

            myCon.Close();
        }
    }
}
