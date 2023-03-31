using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Westcoast.web.Data;
using Westcoast.web.Interface;
using Westcoast.web.Models;
using Westcoast.web.ViewModels;

namespace Westcoast.web.Controllers
{
    [Route("UserAdmin")]
    public class UserAdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
            var result = await _unitOfWork.UserRepository.ListAllAsync();

            var model = result.Select(c => new UserListViewModel{
                UserID = c.UserID,
                FirstName = c.FirstName,
                UserTitle = c.UserTitle,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                StreetAdress = c.StreetAdress,
                PostalCode = c.PostalCode
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
            var user = new UserPostViewModel();
            return View("Create", user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserPostViewModel user)
        {
            try
            {
                if(!ModelState.IsValid) return View("Create", user);


                var exists = await _unitOfWork.UserRepository.FindByUserIDAsync(user.UserID);

            if(exists is not null)
            {
                var error = new ErrorModel
                {
                    ErrorTitle ="Ett fel har inträffats",
                    ErrorMessage =$"Namnet {user.FirstName} finns redan"
                };

                return View("_Error", error);
            }
            var userToAdd = new User{

                UserID = user.UserID,
                FirstName = user.FirstName,
                UserTitle = user.UserTitle,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StreetAdress = user.StreetAdress,
                PostalCode = user.PostalCode,
            };

            
            if(await _unitOfWork.UserRepository.AddAsync(userToAdd)){
                if(await _unitOfWork.Complete())
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            
            var saveError = new ErrorModel
            {
                ErrorTitle ="Ett fel har inträffat",
                ErrorMessage =$"Det inträffades ett fel när Användar id {user.UserID} skulle sparas"
                };
                return View("_Error", saveError);
            }
            catch (Exception ex)
            {
                
                    var error = new ErrorModel
                    {
                    ErrorTitle ="Ett fel har inträffat när vi hämtar användare för redigering",
                    ErrorMessage =ex.Message
                    };
                return View("_Error", error);
            }
        }

        [HttpGet("edit/{userID}")]
        public async Task<IActionResult> Edit(int userID)
        {
            try
            {
            var result = await _unitOfWork.UserRepository.FindByUserIDAsync(userID);

            if (result is null){
                var error = new ErrorModel
            {
                ErrorTitle ="Ett fel inträffat",
                ErrorMessage =$"Vi kan inte hitta kursen med ID {userID}"
            };
            return View("_Error", error);
            }
            var model = new UserUpdateViewModel{
                UserID = result.UserID,
                FirstName = result.FirstName,
                UserTitle = result.UserTitle,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                StreetAdress = result.StreetAdress,
                PostalCode = result.PostalCode
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

        [HttpPost("edit/{userID}")]
        public async Task<IActionResult> Edit(int userID, UserUpdateViewModel user)
        {
            try
            {
                if(!ModelState.IsValid) return View("Edit", user);

                var userToUpdate = await _unitOfWork.UserRepository.FindByUserIDAsync(userID);

            if(userToUpdate is null) return RedirectToAction(nameof(Index));

            userToUpdate.UserTitle = user.UserTitle;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.UserID = user.UserID;
            userToUpdate.Email = user.Email;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.StreetAdress = user.StreetAdress;
            userToUpdate.PostalCode = user.PostalCode;
            

            if(await _unitOfWork.UserRepository.UpdateAsync(userToUpdate)){
                if(await _unitOfWork.Complete()){
                    return RedirectToAction(nameof(Index));
                }
            }

            var error = new ErrorModel
                    {
                    ErrorTitle ="Ett fel har inträffat när vi skulle spara kursen",
                    ErrorMessage = $"Ett fel inträffade när vi skulle updatera användarenmen användar id {userToUpdate.UserID}"
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

        [Route("delete/{userID}")]
        public async Task<IActionResult> Delete(int userID)
        {
            try
            {
                var userToDelete = await _unitOfWork.UserRepository.FindByUserIDAsync(userID);

                if(userToDelete is null) return RedirectToAction(nameof(Index));
            
                if(await _unitOfWork.UserRepository.DeleteAsync(userToDelete))
                {
                    if(await _unitOfWork.Complete()){
                        return RedirectToAction(nameof(Index));
                    }
                }
                
                var error = new ErrorModel
                    {
                    ErrorTitle ="Ett fel har inträffat när vi skulle spara kursen",
                    ErrorMessage = $"Ett fel inträffade när vi skulle updatera användarenmen användar id {userToDelete.UserID}"
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
                return View("_Error", error);;
            }
            
        }
    }
}