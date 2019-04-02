using DemoCRUD.Entity;
using DemoCRUD.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace DemoCRUD.Repository.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private readonly IConfiguration _config;

        public HouseRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                //return new SqlConnection(_config.GetValue<string>("Data:ConnectionString"));
                return new SqlConnection("Server=localhost;Database=WebBDS;Trusted_Connection=true;");
                //return new SqlConnection(_config.GetConnectionString("ConnectionString"));
                
            }
        }

        public void AddHouse(House house)
        {
            using (IDbConnection cnn = Connection)
            {
                string query = $"insert into House(Phone, Name, Address, Type, Description, Price, Acreage, Caption)" +
                    "values (@phone, @name, @address, @type, @description, @price, @acreage, @caption)";
                cnn.Open();

                cnn.Execute(query, new
                {
                    phone = house.Phone,
                    name = house.Name,
                    address = house.Address,
                    type = house.Type,
                    description = house.Description,
                    price = house.Price,
                    acreage = house.Acreage,
                    caption = house.Caption
                });
                cnn.Close();

            }
        }

        public int GetMaxId()
        {
            using (IDbConnection cnn = Connection)
            {
                string query = "select Max(Id) from House";
                cnn.Open();
                //return cnn.Query<int>(query).SingleOrDefault();
                return cnn.QuerySingleOrDefault<int>(query);
            }
        }

        public List<House> GetAllHouse()
        {
            using (IDbConnection cnn = Connection)
            {
                string query = "Select * from House";
                cnn.Open();
                return (cnn.Query<House>(query)).ToList<House>();
                //return cnn.Query<>
            }
        }

        public House GetHouseById(int id)
        {
            using (IDbConnection cnn = Connection)
            {
                string query = "Select * from House where id = @id";
                cnn.Open();
                //return cnn.Query<House>(query, new { id = id })
                return cnn.QuerySingleOrDefault<House>(query, new { id = id });
            }
        }

        public void DeleteById(int id)
        {
            using (IDbConnection cnn = Connection)
            {
                string query = "delete from House where id = @id";
                string query1 = "delete from ImageNameOfHouse where IdImage = @id";
                cnn.Open();
                cnn.Execute(query1, new { id = id });
                cnn.Execute(query, new { id = id });
                cnn.Close();
            }
        }

        public void UpdateHouse(House house)
        {
            using (IDbConnection cnn = Connection)
            {
                //string query = string.Format("Update House set Phone = {0}, Name = {1}, Address = {2}, " +
                //    "Type = {3}, Description = {4}, Price = {5}, Acreage = {6}, Caption = {7} where Id = {8}", 
                //    house.Phone, house.Name, house.Address, house.Type, house.Description, house.Price, 
                //    house.Acreage, house.Caption, house.Id);
                string query = "Update House set Phone = @phone, Name = @name, Address = address, " +
                    "Type = @type, Description = @description, Price = @price, Acreage = @acreage, " +
                    "Caption = @caption where Id = @id";
                cnn.Open();
                cnn.Execute(query, new
                {
                    phone = house.Phone,
                    name = house.Name,
                    address = house.Address,
                    type = house.Type,
                    description = house.Description,
                    price = house.Price,
                    acreage = house.Acreage,
                    caption = house.Caption,
                    id = house.Id
                });
                cnn.Close();
            }
        }
    }
}
