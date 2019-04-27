using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouxAcademy.Data;
using RouxAcademy.Models;

namespace RouxAcademy.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly StudentDataContext _context;
        public StudentController(StudentDataContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Get Index Page
        /// </summary>
        /// <returns> Index Page With throwing new list of Cource Grade model </returns>
        public IActionResult Index()
        {
            return View(new List<UniGrad>());
        }

        #region Add Grade

        /// <summary>
        /// Get Add Grade View 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddGrade()
        {
            return View();
        }

        /// <summary>
        /// Post Add grade
        /// </summary>
        /// <param name="model"></param>
        /// <returns> return model and add it to database </returns>
        [HttpPost]
        public async Task<IActionResult> AddGrade(UniGrad model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            model.CreatedDate = DateTime.Now;

            await _context.uniGrads.AddAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(StudentController.Index), "Student");
        }


        #endregion


        #region Classifiction
        /// <summary>
        /// Get Classifiction
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Classifications()
        {
            var classifications = new List<string>()
            {
                "Freshman",
                "Sophomore",
                "Junior",
                "Senior"
            };
            return View(classifications);
        }
        #endregion
    }
}