using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;

namespace SPIL.IGUConfigurator
{
    public class DatabaseService
    {
        private readonly string _connectionString;
        private readonly string _connectionString2;

        public DatabaseService(string connectionString, string connectionString2)
        {
            _connectionString = connectionString;
            _connectionString2 = connectionString2;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public IDbConnection CreateConnection2()
        {
            return new SqlConnection(_connectionString2);
        }

        // Get Finished Product Services from database
        public List<string> GetFinishedServices()
        {
            try
            {
                using (var connection = CreateConnection2())
                {
                    connection.Open();
                    return connection.Query<string>("SELECT Description_1 FROM stkitem WHERE ProdCatID=28").AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching finished services: {ex.Message} for comboboxes", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>();
            }
        }

        // Get Types from database
        public List<string> GetTypes()
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return connection.Query<string>("SELECT TypeDescription FROM spilStkTypes WHERE ShowSO=1").AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching types: {ex.Message} for comboboxes", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>();
            }
        }

        // Get paneldata from database
        public List<(string Code, string Description)> GetPanelData(int thickness)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();

                    // Define the query with parameterized SQL
                    string query = thickness > 0
                        ? "SELECT Code, Description_1 FROM stkitem WHERE ProdCatID = 22 AND ufIIThickness = @Thickness"
                        : "SELECT Code, Description_1 FROM stkitem WHERE ProdCatID = 22";

                    // Execute the query and return the results
                    return connection.Query<(string Code, string Description)>(query, new { Thickness = thickness }).AsList();
                }
            }
            catch (Exception ex)
            {
                // Log the error (replace with your logging mechanism)
                Console.WriteLine($"Error fetching panel data: {ex.Message}");
                return new List<(string Code, string Description)>();
            }
        }


        // Get spacers from database
        public List<(string Code, string Description)> GetSpacer(int spacerThickness)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();

                    // Define the query with parameterized SQL
                    string query = spacerThickness > 0
                        ? "SELECT Code, Description_1 FROM stkitem WHERE ProdCatID = 22 AND ufIIThickness = @Thickness"
                        : "SELECT Code, Description_1 FROM stkitem WHERE ProdCatID = 22";

                    // Execute the query and return the results
                    return connection.Query<(string Code, string Description)>(query, new { Thickness = spacerThickness }).AsList();
                }
            }
            catch (Exception ex)
            {
                // Log the error (replace with your logging mechanism)
                Console.WriteLine($"Error fetching spacer data: {ex.Message}");
                return new List<(string Code, string Description)>();
            }
        }



        // Get Gas from database
        public List<string> GetGas()
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return connection.Query<string>("SELECT Description_1 FROM stkitem WHERE ProdCatID=25").AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching gas: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>();
            }
        }


        // Get Sealant from database
        public List<string> GetSealant()
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return connection.Query<string>("SELECT Description_1 FROM stkitem WHERE ProdCatID=24").AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching sealant: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>();
            }
        }


        // Get Edge Services from database
        public List<string> GetEdgeServices()
        {
            try
            {
                using (var connection = CreateConnection2())
                {
                    connection.Open();
                    return connection.Query<string>("SELECT Description_1 FROM stkitem WHERE ProdCatID=28").AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching edge services: {ex.Message} for comboboxes", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>();
            }
        }

    }
}