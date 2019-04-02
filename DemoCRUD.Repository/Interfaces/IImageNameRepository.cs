using DemoCRUD.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCRUD.Repository.Interfaces
{
    public interface IImageNameRepository
    {
        bool AddImageName(ImageNameOfHouse imageNameOfHouse, int idImage);
        List<string> GetImageByIdImage(int idImage);
        string GetFirstImageNameByIdImage(int idImage);
    }
}
