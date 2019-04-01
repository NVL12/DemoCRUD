using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoCRUD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace DemoCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _he;
        private readonly IHouseRepository _houseRepository;
        private readonly IImageNameRepository _imageNameRepository;
        public HomeController(IHostingEnvironment e, IImageNameRepository imageNameRepository, IHouseRepository houseRepository)
        {
            _he = e;
            _imageNameRepository = imageNameRepository;
            _houseRepository = houseRepository;
        }
        public IActionResult Index()
        {
            //because view Index has two controller references
            //from Update controller, transfer into a model wanna update
            //from Index controller, create empty model
            

            //House house = new House();
            //return View(house);

            List<House> ListNewsHouse = GetAllInfoHouse();
            ViewBag.List = ListNewsHouse;
            return View("ViewNewsList");
        }

        [HttpPost]
        public IActionResult Add()
        {
            House house = new House();
            return View("Index", house);
        }
        [HttpPost]
        public async Task<IActionResult> HandleInfo(House house, string cancel)
        {
            //when click button Cancel, references to ViewNewsList View
            if (cancel == "Cancel") {
                List<House> ListNewsHouse = GetAllInfoHouse();
                ViewBag.List = ListNewsHouse;
                return View("ViewNewsList");
            }
            //when click button Ok to submit
            //check validation fields
            if (ModelState.IsValid)
            {
                //Id = 0 => Create new Entity
                if (house.Id == 0)
                    _houseRepository.AddHouse(house);
                else
                    //Id != 0 => Update has Entity
                    _houseRepository.UpdateHouse(house);
                
                int idImage = _houseRepository.GetMaxId();
                //save image and get image name
                for (int i = 0; i < house.ListImageFile.Count; i++)
                {
                    IFormFile image = house.ListImageFile[i];
                    var filePath = Path.Combine(_he.WebRootPath, "images", Path.GetFileName(image.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                        stream.Flush();
                    }
                    ImageNameOfHouse imageNameOfHouse = new ImageNameOfHouse
                    {
                        ImageName = image.FileName
                    };

                    _imageNameRepository.AddImageName(imageNameOfHouse, idImage);
                }
                //show news list => references to ViewNewsList View
                List<House> ListNewsHouse = GetAllInfoHouse();
                ViewBag.List = ListNewsHouse;
                return View("ViewNewsList");
            }
            //if Validation Model fail => return Index View and notify invalid data
            else
            {
                House h = new House();
                return View("Index", h);
            }
        }

        //Show specific a news
        public IActionResult ViewInfo(int id)
        {
            House house = _houseRepository.GetHouseById(id);
            List<string> ListImageName = _imageNameRepository.GetImageByIdImage(id);
            ViewBag.ListImage = ListImageName;
            return View(house);
        }

        //Delete specific a news
        public IActionResult DeleteNews(int id)
        {
            //delete
            _houseRepository.DeleteById(id);
            //show list news again
            List<House> ListNewsHouse = GetAllInfoHouse();
            ViewBag.List = ListNewsHouse;
            return View("ViewNewsList");
        }

        //Update specific a news => references to Index View, attach a model wanna update
        public IActionResult UpdateNews(House house)
        {
            return View("Index", house);
        }

        //Get information all house
        public List<House> GetAllInfoHouse()
        {
            List<House> ListNewsHouse = new List<House>();
            ListNewsHouse = _houseRepository.GetAllHouse();
            //Get first image to assign Avatar of news
            foreach (House item in ListNewsHouse)
            {
                item.FirstImageName = _imageNameRepository.GetFirstImageNameByIdImage(item.Id);
            }
            return ListNewsHouse;
        }
    }
}
