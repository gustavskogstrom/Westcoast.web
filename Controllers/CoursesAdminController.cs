using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Westcoast.web.Data;
using Westcoast.web.Interface;
using Westcoast.web.Models;
using Westcoast.web.ViewModels;

namespace Westcoast.web.Controllers;

    [Route("CoursesAdmin")]
    public class CoursesAdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

    public CoursesAdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<IActionResult> Index()
        {
            try
            {
            var courses = await _unitOfWork.CourseRepository.ListAllAsync();

            var model = courses.Select(c => new CourseListViewModel{
                CourseID = c.CourseID,
                CourseName = c.CourseName,
                CourseTitle = c.CourseTitle,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                CourseLenght = c.CourseLenght
            }).ToList();
            
            return View("Index", model);    
            }
            catch (Exception ex)
            {
                
                var error = new ErrorModel
                {
                    ErrorTitle ="Ett fel har inträffat",
                    ErrorMessage =ex.Message
                };
                return View("_Error", error);
            }
            
        }

        [HttpGet("create")]
        public IActionResult create()
        {
            var course = new CoursePostViewModel();
            return View("Create", course);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CoursePostViewModel course)
        {
            try
            {
                if(!ModelState.IsValid) return View("Create", course);

            var exists = await _unitOfWork.CourseRepository.FindByCourseNameAsync(course.CourseName);

            if(exists is not null)

            {
                var error = new ErrorModel
                {
                    ErrorTitle ="Ett fel har inträffats",
                    ErrorMessage =$"Kursen {course.CourseName} finns redan"
                };

                return View("_Error", error);
            }
            var courseToAdd = new Course{

                CourseName = course.CourseName,
                CourseTitle = course.CourseTitle,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                CourseLenght = course.CourseLenght
            };


            if(await _unitOfWork.CourseRepository.AddAsync(courseToAdd)){
                if(await _unitOfWork.Complete())
                {
                    return RedirectToAction(nameof(Index));
                }
            }
                var saveError = new ErrorModel
                {
                    ErrorTitle ="Ett fel har inträffats",
                    ErrorMessage =$"Det inträffade ett fel med kursnamnet {course.CourseName} skulle sparas"
                };

                return View("_Error", saveError);
        
            }
            catch (Exception ex)
            {
                
                    var error = new ErrorModel
                    {
                    ErrorTitle ="Ett fel har inträffat när vi hämtar kurs för redigering",
                    ErrorMessage =ex.Message
                    };
                return View("_Error", error);
            }
        }

        [HttpGet("edit/{courseID}")]
        public async Task<IActionResult> Edit(int courseID)
        {
            try
            {
                
                var result = await _unitOfWork.CourseRepository.FindByCourseIDAsync(courseID);
                
                if(result is null){
                var error = new ErrorModel
            {
                ErrorTitle ="Ett fel inträffat",
                ErrorMessage =$"Vi kan inte hitta kursen med ID {courseID}"
            };
            return View("_Error", error);

            } 
            var model = new CourseUpdateViewModel{
                CourseID = result.CourseID,
                CourseName = result.CourseName,
                CourseTitle = result.CourseTitle,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                CourseLenght = result.CourseLenght
            };
            return View("Edit", model);

            
            }
            catch (Exception ex)
            {
                var error = new ErrorModel
                    {
                    ErrorTitle ="Ett fel har inträffat när vi skulle spara en kurs",
                    ErrorMessage =ex.Message
                };
                return View("_Error", error);
                
            }
        }

        [HttpPost("edit/{courseID}")]
        public async Task<IActionResult> Edit(int courseID, CourseUpdateViewModel course)
        {
            try
            {
                if(!ModelState.IsValid) return View("Edit", course);

                var courseToUpdate = await _unitOfWork.CourseRepository.FindByCourseIDAsync(courseID);
                
            if(courseToUpdate is null) return RedirectToAction(nameof(Index));

            courseToUpdate.CourseTitle = course.CourseTitle;
            courseToUpdate.CourseName = course.CourseName;
            courseToUpdate.StartDate = course.StartDate;
            courseToUpdate.EndDate = course.EndDate;
            courseToUpdate.CourseLenght = course.CourseLenght;
            courseToUpdate.CourseID = course.CourseID;
            
            if(await _unitOfWork.CourseRepository.UpdateAsync(courseToUpdate)){
                if(await _unitOfWork.Complete()){
                    return RedirectToAction(nameof(Index));
                }
            }
            
            
            var error = new ErrorModel
                    {
                    ErrorTitle ="Ett fel har inträffat när vi skulle spara kursen",
                    ErrorMessage = $"Ett fel inträffade när vi skulle updatera kursen med kurs id {courseToUpdate.CourseID}"
                };
                return View("_Error", error);
            }
            catch (Exception ex)
            {
                
            var error = new ErrorModel
                    {
                    ErrorTitle ="Ett fel har inträffat när vi skulle spara kursen",
                    ErrorMessage =ex.Message
                };
                return View("_Error", error);
            }
            
        }

        [Route("delete/{courseID}")]
        public async Task<IActionResult> Delete(int courseID)
        {
            try
            {
                var courseToDelete = await _unitOfWork.CourseRepository.FindByCourseIDAsync(courseID);

                if(courseToDelete is null) return RedirectToAction(nameof(Index));
            
                if(await _unitOfWork.CourseRepository.DeleteAsync(courseToDelete)){
                    if(await _unitOfWork.Complete()){
                        return RedirectToAction(nameof(Index));
                    }
                }

                var error = new ErrorModel
                {
                    ErrorTitle ="Ett fel har inträffat när kursen skulle raderas",
                    ErrorMessage =$"Ett fel inträffade när kursen blev registrerad med kurs id {courseToDelete.CourseID} skulle tas bort"
                };

                return View("_Error", error);
                }
                catch (Exception ex)
                {
                var error = new ErrorModel
                {
                    ErrorTitle ="Ett fel har inträffat när kursen skulle raderas",
                    ErrorMessage =ex.Message
                };
                return View("_Error", error);
            }
            
        }
    }
