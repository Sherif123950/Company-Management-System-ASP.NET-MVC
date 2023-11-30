using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
	[Authorize]
	public class DepartementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DepartementController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var departement =await _unitOfWork.DepartementRepository.GetAllAsync();
            ViewBag.Message = TempData["Message"];
            var MappedDepartment = _mapper.Map<IEnumerable<Departement>, IEnumerable<DepartmentViewModel>>(departement);
            return View(MappedDepartment);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departementVM)
        {
            if (ModelState.IsValid)
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Departement>(departementVM);
               await _unitOfWork.DepartementRepository.AddAsync(MappedDept);
                var res =await _unitOfWork.CompleteAsync();
                if (res > 0)
                {
                    TempData["Message"] = "Department Successfully Created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departementVM);
        }
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest();

            var departement =await _unitOfWork.DepartementRepository.GetByIdAsync(id.Value);
            var MappedDepartment = _mapper.Map<Departement, DepartmentViewModel>(departement);

            if (departement is null)
                return NotFound();
            return View(ViewName, MappedDepartment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
            #region MyRegion
            //if (id is null)
            //{
            //    return BadRequest();
            //}
            //var departement = _departementRepository.GetById(id.Value);
            //if (departement is null)
            //{
            //    return NotFound();
            //}
            //return View(departement); 
            #endregion
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,DepartmentViewModel departementVM)
        {
            if (id!=departementVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDept = _mapper.Map<DepartmentViewModel, Departement>(departementVM);
                    _unitOfWork.DepartementRepository.Update(MappedDept);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departementVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
            #region MyRegion
            //if (id is null)
            //{
            //    return BadRequest();
            //}
            //var departememnt = _departementRepository.GetById(id.Value);
            //if (departememnt is null)
            //{
            //    return NotFound();
            //}
            //return View(departememnt); 
            #endregion
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, DepartmentViewModel departementVM)
        {
            if (id != departementVM.Id)
                return BadRequest();
            
            try
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Departement>(departementVM);

                _unitOfWork.DepartementRepository.Delete(MappedDept);
                 await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(departementVM);
            }
        }
    }
}
