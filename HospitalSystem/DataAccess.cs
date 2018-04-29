﻿using System;
using System . Collections;
using System . Data . SqlClient;
using System . IO;
using System . Windows . Forms;

namespace HospitalSystem
{
    class DataAccess
    {
        string ConnectionString;

        public DataAccess ( )
        {
            ConnectionString = System . Configuration . ConfigurationManager . ConnectionStrings [ "HospitalDBConnectionString" ] . ConnectionString;
        }

        public bool AddNewCategoryToDatabase ( string CategoryName , string CategoryDescription , PictureBox CategoryPictureBox )
        {
            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                connection . Open ( );
                SqlTransaction sqlTran = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command . Transaction = sqlTran;
                MemoryStream ms = new MemoryStream();
                CategoryPictureBox . Image . Save ( ms , CategoryPictureBox . Image . RawFormat );
                byte[] CategoryPicture = ms.GetBuffer();
                ms . Close ( );

                try
                {
                    command . Parameters . AddWithValue ( "@CategoryName" , CategoryName );
                    command . Parameters . AddWithValue ( "@CategoryDescription" , CategoryDescription );
                    command . Parameters . AddWithValue ( "@CategoryPicture" , CategoryPicture );
                    command . CommandText =
                        "Insert into Categories (CategoryName, CategoryDescription, CategoryPicture) values (@CategoryName, @CategoryDescription, @CategoryPicture)";
                    command . ExecuteNonQuery ( );
                    sqlTran . Commit ( );
                    connection . Close ( );
                    return true;
                }

                catch ( Exception ee )
                {
                    connection . Close ( );
                    return false;
                }
            }
        }

        public ArrayList RetreiveAllCategoriesFromDatabase ( )
        {
            ArrayList CategoriesList = new ArrayList();

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ID, CategoryName, CategoryPicture FROM Categories;", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    {
                        int CategoryID = reader.GetInt32 ( 0 );
                        string CategoryName = reader.GetString ( 1 );
                        byte[] CategoryPicture = ( byte[] ) reader[2];
                        CategoriesList . Add ( new Details ( ) { ID = CategoryID , Name = CategoryName , Picture = CategoryPicture } );
                    }
                }

                reader . Close ( );
                return CategoriesList;
            }
        }

        public bool AddNewProductToDatabase ( string ProductName , decimal ProductPrice , int ProductCategoryID , string ProductDescription , byte [ ] ProductPicture )
        {
            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                connection . Open ( );
                SqlTransaction sqlTran = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command . Transaction = sqlTran;

                try
                {
                    command . Parameters . AddWithValue ( "@ProductName" , ProductName );
                    command . Parameters . AddWithValue ( "@ProductPrice" , ProductPrice );
                    command . Parameters . AddWithValue ( "@ProductCategoryID" , ProductCategoryID );
                    command . Parameters . AddWithValue ( "@ProductDescription" , ProductDescription );
                    command . Parameters . AddWithValue ( "@ProductImage" , ProductPicture );
                    command . CommandText =
                        "Insert into Products (ProductName, ProductPrice, ProductCategoryID, ProductDescription, ProductImage) values (@ProductName, @ProductPrice, @ProductCategoryID, @ProductDescription, @ProductImage)";
                    command . ExecuteNonQuery ( );
                    sqlTran . Commit ( );
                    connection . Close ( );
                    return true;
                }

                catch ( Exception ee )
                {
                    connection . Close ( );
                    return false;
                }
            }
        }

        public ArrayList RetreiveAllProducts ( )
        {
            ArrayList ProductsList = new ArrayList();

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ID, ProductName, ProductPrice, ProductCategoryID, ProductDescription, ProductImage FROM Products;", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    {
                        int ProductID = reader.GetInt32 ( 0 );
                        string ProductName = reader.GetString ( 1 );
                        decimal ProductPrice = reader.GetDecimal ( 2 );
                        int ProductCategoryID = reader.GetInt32 ( 3 );
                        string ProductCategoryName = ReturnCategoryName ( ProductCategoryID );
                        string ProductDescription = reader.GetString ( 4 );
                        byte[] ProductPicture = ( byte[] ) reader[5];
                        ProductsList . Add ( new Details ( ) { Name = ProductName , Price = ProductPrice , Category = ProductCategoryName , Description = ProductDescription , Picture = ProductPicture , ID = ProductID } );
                    }
                }

                reader . Close ( );
                return ProductsList;
            }
        }

        public ArrayList RetreiveProductsFromCategory ( int CategoryID )
        {
            ArrayList ProductsList = new ArrayList();

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ID, ProductName, ProductPrice, ProductDescription, ProductImage FROM Products where ProductCategoryID = '" + CategoryID + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    {
                        int ProductID = reader.GetInt32 ( 0 );
                        string ProductName = reader.GetString ( 1 );
                        decimal ProductPrice = reader.GetDecimal ( 2 );
                        string ProductDescription = reader.GetString ( 3 );
                        byte[] ProductPicture = ( byte[] ) reader[4];
                        ProductsList . Add ( new Details ( ) { ID = ProductID , Name = ProductName , Price = ProductPrice , Picture = ProductPicture , Description = ProductDescription } );
                    }
                }

                reader . Close ( );
                return ProductsList;
            }
        }

        public int ReturnCategoryID ( string CategoryName )
        {
            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ID FROM Categories where CategoryName = '" + CategoryName + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();
                int CategoryID = 0;

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    { CategoryID = reader . GetInt32 ( 0 ); }
                }

                reader . Close ( );
                return CategoryID;
            }
        }

        public string ReturnCategoryName ( int CategoryID )
        {
            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT CategoryName FROM Categories where ID = '" + CategoryID + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();
                string CategoryName = string.Empty;

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    { CategoryName = reader . GetString ( 0 ); }
                }

                reader . Close ( );
                return CategoryName;
            }
        }

        public Details RetreiveProductDetails ( int ProductID )
        {
            Details ProductDetails = new Details();

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ProductName, ProductPrice, ProductCategoryID, ProductDescription, ProductImage FROM Products where ID = '" + ProductID + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    {
                        ProductDetails . Name = reader . GetString ( 0 );
                        ProductDetails . Price = reader . GetDecimal ( 1 );
                        int ProductCategoryID = reader.GetInt32 ( 2 );
                        ProductDetails . Category = ReturnCategoryName ( ProductCategoryID );
                        ProductDetails . Description = reader . GetString ( 3 );
                        ProductDetails . Picture = ( byte [ ] ) reader [ 4 ];
                    }
                }

                reader . Close ( );
                return ProductDetails;
            }
        }

        public bool RecordASale ( ArrayList ProductsList , DateTime SaleTime , int DoctorID , decimal CashGiven , decimal TotalBill , decimal CashReturn )
        {
            int SaleID = ReturnSaleID();

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                connection . Open ( );
                SqlTransaction sqlTran = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command . Transaction = sqlTran;

                try
                {
                    command . Parameters . AddWithValue ( "@SaleTime" , SaleTime );
                    command . Parameters . AddWithValue ( "@DoctorID" , DoctorID );
                    command . Parameters . AddWithValue ( "@CashGiven" , CashGiven );
                    command . Parameters . AddWithValue ( "@TotalBill" , TotalBill );
                    command . Parameters . AddWithValue ( "@CashReturn" , CashReturn );
                    command . CommandText =
                        "Insert into Facturas (SaleTime, DoctorID, CashGiven, TotalBill, CashReturn) values (@SaleTime, @DoctorID, @CashGiven, @TotalBill, @CashReturn)";
                    command . ExecuteNonQuery ( );

                    foreach ( Details ProductDetail in ProductsList )
                    {
                        command . CommandText =
                            "Insert into SaleItems (ProductName, ProductPrice, ProductQuantity, ProductTotal, SaleID) values ('" + ProductDetail . Name + "', '" + ProductDetail . Price + "', '" + ProductDetail . Quantity + "', '" +
                            ProductDetail . Total + "', '" + SaleID + "')";
                        command . ExecuteNonQuery ( );
                    }

                    sqlTran . Commit ( );
                    connection . Close ( );
                    return true;
                }

                catch ( Exception ee )
                {
                    connection . Close ( );
                    return false;
                }
            }
        }

        public int ReturnSaleID ( )
        {
            int SaleID = 1;

            try
            {
                using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
                {
                    SqlCommand command = new SqlCommand ( "SELECT MAX(ID) FROM Facturas;", connection );
                    connection . Open ( );
                    SqlDataReader reader = command.ExecuteReader();

                    if ( reader . HasRows )
                    {
                        while ( reader . Read ( ) )
                        { SaleID = reader . GetInt32 ( 0 ); }
                    }

                    reader . Close ( );
                    SaleID = SaleID + 1;
                    return SaleID;
                }
            }

            catch ( Exception )
            {
                return SaleID;
            }
        }

        public bool DeleteProduct ( int ProductID )
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
                {
                    SqlCommand command = new SqlCommand ( "Delete from Products where ID = '" + ProductID + "'", connection );
                    connection . Open ( );

                    if ( command . ExecuteNonQuery ( ) > 0 )
                    {
                        connection . Close ( );
                        return true;
                    }

                    else
                    { return false; }
                }
            }

            catch ( Exception )
            {
                return false;
            }
        }

        public bool UpdateProduct ( int ProductID , string ProductName , decimal ProductPrice , int ProductCategoryID , string ProductDescription , byte [ ] ProductPicture )
        {
            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                connection . Open ( );
                SqlTransaction sqlTran = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command . Transaction = sqlTran;

                try
                {
                    command . Parameters . AddWithValue ( "@ProductID" , ProductID );
                    command . Parameters . AddWithValue ( "@ProductName" , ProductName );
                    command . Parameters . AddWithValue ( "@ProductPrice" , ProductPrice );
                    command . Parameters . AddWithValue ( "@ProductCategoryID" , ProductCategoryID );
                    command . Parameters . AddWithValue ( "@ProductDescription" , ProductDescription );
                    command . Parameters . AddWithValue ( "@ProductImage" , ProductPicture );
                    command . CommandText =
                        "Update Products set ProductName = @ProductName, ProductPrice = @ProductPrice, ProductCategoryID = @ProductCategoryID, ProductDescription = @ProductDescription, ProductImage=@ProductImage where ID = @ProductID";
                    command . ExecuteNonQuery ( );
                    sqlTran . Commit ( );
                    connection . Close ( );
                    return true;
                }

                catch ( Exception ee )
                {
                    connection . Close ( );
                    return false;
                }
            }
        }

        public ArrayList RetreiveAllFacturas ( )
        {
            ArrayList FacturasList = new ArrayList();

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ID, SaleTime, DoctorID, TotalBill FROM Facturas;", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    {
                        int SaleID = reader.GetInt32 ( 0 );
                        DateTime SaleTime = reader.GetDateTime ( 1 );
                        int DoctorID = reader.GetInt32 ( 2 );
                        string DoctorName = ReturnUserName ( DoctorID );
                        decimal TotalBill = reader.GetDecimal ( 3 );
                        FacturasList . Add ( new Details ( ) { SaleID = SaleID , SaleTime = SaleTime , Name = DoctorName , Total = TotalBill } );
                    }
                }

                reader . Close ( );
                return FacturasList;
            }
        }

        public string ReturnUserName ( int UserID )
        {
            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT Name FROM Users where ID = '" + UserID + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();
                string UserName = string.Empty;

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    { UserName = reader . GetString ( 0 ); }
                }

                reader . Close ( );
                return UserName;
            }
        }

        public int ReturnUserID ( string UserEmail )
        {
            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ID FROM Users where Email = '" + UserEmail + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();
                int UserID = 0;

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    { UserID = reader . GetInt32 ( 0 ); }
                }

                reader . Close ( );
                return UserID;
            }
        }

        public bool ConfirmUser ( string UserEmail , string UserPassword )
        {
            string PasswordFromDatabase = string.Empty;

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT Password FROM Users where Email = '" + UserEmail + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    { PasswordFromDatabase = reader . GetString ( 0 ); }
                }

                reader . Close ( );
            }

            if ( SimpleHash . VerifyHash ( UserPassword , PasswordFromDatabase ) )
            { return true; }

            else
            { return false; }
        }

        public bool RegisterUser ( string UserName , string UserEmail , string UserPassword , int UserRole )
        {
            UserPassword = SimpleHash . ComputeHash ( UserPassword );

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "Insert into Users (Name, Role, Email, Password) values ('" + UserName + "','" + UserRole + "','" + UserEmail + "','" + UserPassword + "');", connection );
                connection . Open ( );
                int RowsAffected = command.ExecuteNonQuery();

                if ( RowsAffected > 0 )
                { return true; }

                else
                { return false; }
            }
        }

        public ArrayList RetreiveSaleItems ( int SaleID )
        {
            ArrayList ProductsList = new ArrayList();

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                SqlCommand command = new SqlCommand ( "SELECT ProductName, ProductPrice, ProductQuantity, ProductTotal FROM SaleItems where SaleID = '" + SaleID + "';", connection );
                connection . Open ( );
                SqlDataReader reader = command.ExecuteReader();

                if ( reader . HasRows )
                {
                    while ( reader . Read ( ) )
                    {
                        string ProductName = reader.GetString ( 0 );
                        decimal ProductPrice = reader.GetDecimal ( 1 );
                        int ProductQuantity = reader.GetInt32 ( 2 );
                        decimal ProductTotal = reader.GetDecimal ( 3 );
                        ProductsList . Add ( new Details ( ) { Name = ProductName , Price = ProductPrice , Quantity = ProductQuantity , Total = ProductTotal } );
                    }
                }

                reader . Close ( );
                return ProductsList;
            }
        }
    }
}
