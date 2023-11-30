using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Presentation_Layer.Helpers;
using Presentation_Layer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<IActionResult> Index(string NameValue)
        {
            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(NameValue))
            {    
                Employees=await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                Employees = _unitOfWork.EmployeeRepository.Search(NameValue);
            }
            ViewBag.Message = TempData["Message"];
            var MappedEmployees = _mapper.Map<IEnumerable< Employee>,IEnumerable< EmployeeViewModel>>(Employees);
            return View(MappedEmployees);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartementRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
               employeeVM.ImageName= DocumentSetting.UploadFile(employeeVM.Image, "Images");

                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmp);
                var Res = await _unitOfWork.CompleteAsync();
                if (Res>0)
                {
                    TempData["Message"] = "Employee Successfully Created :)";
                return RedirectToAction(nameof(Index));
                }
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? Id,string ViewName="Details")
        {
            if (Id is null)
            {
                return BadRequest();
            }
           var Employee=await _unitOfWork.EmployeeRepository.GetByIdAsync(Id.Value);
            ViewBag.Departments =await _unitOfWork.DepartementRepository.GetAllAsync();
            if (Employee is null)
            {
                return NotFound();
            }
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(ViewName, MappedEmployee);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            ViewBag.Departments =await _unitOfWork.DepartementRepository.GetAllAsync();
            return await Details(Id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int?id,EmployeeViewModel employeeVM)
        {
            if (employeeVM.Id!=id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                 try
                 {
                    employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image,"Images");
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    _unitOfWork.EmployeeRepository.Update(MappedEmp);
                    await _unitOfWork.CompleteAsync();
                     return RedirectToAction(nameof(Index));
                 }
                 catch (Exception ex)
                 { 
                     ModelState.AddModelError("",ex.Message);
                 }
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            ViewBag.Departments =await _unitOfWork.DepartementRepository.GetAllAsync();
            return await Details(Id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (employeeVM.Id != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(MappedEmp);
                   var  res=await _unitOfWork.CompleteAsync();
                    if (res>0)
                    {
                        DocumentSetting.DeleteFile("Images", employeeVM.ImageName);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employeeVM);
        }
    }
}
