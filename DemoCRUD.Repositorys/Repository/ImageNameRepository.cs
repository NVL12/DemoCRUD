using DemoCRUD.Entity;
using DemoCRUD.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace DemoCRUD.Repository.Repository
{
    public class ImageNameRepository : IImageNameRepository
    {
        private readonly IConfiguration _config;

        public ImageNameRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Server=localhost;Database=WebBDS;Trusted_Connection=true;");
                //return new SqlConnection(_config.GetValue<string>("Data:ConnectionString"));
                //return new SqlConnection(_config.GetConnectionString("Data:ConnectionString")); 
            }
        }

        public bool AddImageName(ImageNameOfHouse imageNameOfHouse, int idImage)
        {
            using (IDbConnection cnn = Connection)
            {
                string query = "insert into ImageNameOfHouse (IdImage, ImageName) values (@idimage, @imagename)";
                cnn.Open();
                try
                {
                    cnn.Execute(query, new { idimage = idImage, imagename = imageNameOfHouse.ImageName });
                    cnn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<string> GetImageByIdImage(int idImage)
        {
            List<string> ListImageName = new List<string>();
            using (IDbConnection cnn = Connection)
            {
                string query = "Select ImageName from ImageNameOfHouse where idImage = @idimage";
                cnn.Open();
                var result = cnn.Query<string>(query, new { idimage = idImage });
                foreach (string item in result)
                {
                    ListImageName.Add(item);
                }
            }
            return ListImageName;
        }

        public string GetFirstImageNameByIdImage(int idImage)
        {
            using (IDbConnection cnn = Connection)
            {
                string query = "select top 1 ImageName from ImageNameOfHouse where IdImage = @idimage";
                cnn.Open();
                //return cnn.Query<string>(query, new { idimage = idImage }).SingleOrDefault();
                return cnn.QuerySingleOrDefault<string>(query, new { idimage = idImage });
            }
        }
    }
}
