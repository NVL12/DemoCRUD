using DemoCRUD.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCRUD.Repository.Interfaces
{
    public interface IHouseRepository
    {
        void AddHouse(House house);
        int GetMaxId();
        List<House> GetAllHouse();
        House GetHouseById(int id);
        void DeleteById(int id);
        void UpdateHouse(House house);
    }
}
