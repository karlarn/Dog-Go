using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using System.Collections.Generic;
using DogGo.Models;
using System.Security.Claims;
using System;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {

        private readonly IWalkerRepository _walkerRepo;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository,
            IOwnerRepository ownerRepository,
            IDogRepository dogRepository)
        {
            _walkerRepo = walkerRepository;
            _ownerRepository = ownerRepository;
            _dogRepo = dogRepository;
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
        // GET: WalkersController
        public ActionResult Index()
        {
            try
            {
                Owner activeUser = _ownerRepository.GetOwnerById(GetCurrentUserId());
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(activeUser.NeighborhoodId);

                return View(walkers);
            }
            catch(Exception)
            {
                List<Walker> allWalkers = _walkerRepo.GetAllWalkers();
                return View(allWalkers);
            }
            
            
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            walker = _walkerRepo.GetWalksByWalkerId(walker);
            foreach(Walk i in walker.Walks)
            {
                Dog dog = _dogRepo.GetDogById(i.DogId);
                i.DogOwnerName = dog.Owner.Name;
            }

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
