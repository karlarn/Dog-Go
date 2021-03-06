using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using System.Collections.Generic;
using DogGo.Models;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DogGo.Controllers
{
    public class DogController : Controller
    {

        private readonly IDogRepository _dogRepo;

        public DogController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        // GET: DogController
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(dog);
            }
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            int currentUserId = GetCurrentUserId();
            Dog dog = _dogRepo.GetDogById(id);
            if(dog == null || dog.OwnerId != currentUserId)
            {
                return NotFound();
            }
            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(dog);
            }
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            int currentUserId = GetCurrentUserId();
            Dog dog = _dogRepo.GetDogById(id);
            if(dog.OwnerId == currentUserId)
            {
                return View(dog);
            }
            return NotFound();
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(dog.Id);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View();
            }
        }
    }
}
