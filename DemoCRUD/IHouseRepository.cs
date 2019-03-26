using DemoCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoCRUD
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
