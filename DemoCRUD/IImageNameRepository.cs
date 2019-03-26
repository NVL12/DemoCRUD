using DemoCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoCRUD
{
    public interface IImageNameRepository
    {
        bool AddImageName(ImageNameOfHouse imageNameOfHouse, int idImage);
        List<string> GetImageByIdImage(int idImage);
        string GetFirstImageNameByIdImage(int idImage);
    }
}
