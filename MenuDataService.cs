using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MenuListModel;

namespace MenuListDataLayer
{
    public class MenuDataService
    {
        private string connectionString = "Data Source=DESKTOP-13QP2OT\\SQLEXPRESS;Initial Catalog=VanessaRestaurant;Integrated Security=True;";

        public List<Menu> GetMenus()
        {
            List<Menu> menus = new List<Menu>();
            string query = "SELECT * FROM Menus";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    menus.Add(new Menu
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Category = reader["Category"].ToString(),
                        Item = reader["Item"].ToString(),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")) 
                    });
                }
            }

            return menus;
        }

        public Menu GetMenu(string order)
        {
            Menu menu = null;
            string query = "SELECT * FROM Menus WHERE Item = @Item";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Item", order);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    menu = new Menu
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Category = reader["Category"].ToString(),
                        Item = reader["Item"].ToString(),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")) 
                    };
                }
            }

            return menu;
        }
    }
}

