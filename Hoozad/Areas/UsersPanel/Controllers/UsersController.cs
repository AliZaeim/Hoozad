using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Entities.User;
using Core.Services.Interfaces;
using Core.DTOs.Admin;
using Core.Convertors;
using DataLayer.Entities.Supplementary;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    public class UsersController : Controller
    {
        
        private readonly IUserService _userService;
        private readonly ISuppService _suppService;

        public UsersController(IUserService userService, ISuppService suppService)
        {
            _userService = userService;
            _suppService = suppService;            
        }

        // GET: UsersPanel/Users
        public async Task<IActionResult> Index()
        {

            return View(await _userService.GetUsersAsync());
        }

        // GET: UsersPanel/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _userService.GetUsersAsync() == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UsersPanel/Users/Create
        public async Task<IActionResult> Create()
        {
            UserVM userVM = new()
            {
                States = await _suppService.GetStatesAsync(),

            };
            return View(userVM);
        }

        // POST: UsersPanel/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            if (ModelState.IsValid)
            {

                User? userCell = await _userService.GetUserByCellphone(userVM.Cellphone!);
                if (userCell != null)
                {
                    ModelState.AddModelError("Cellphone", "شماره تلفن همراه تکراری است !");
                    userVM.States = await _suppService.GetStatesAsync();
                    if (userVM.StateId != null)
                    {
                        userVM.Counties = await _suppService.GetCountiesOfState(userVM.StateId.Value);
                    }
                    return View(userVM);
                }
                User? userName = await _userService.GetUserByUsernameAsync(userVM.UserName!);
                if (userName != null)
                {
                    ModelState.AddModelError("UserName", "نام کاربری تکراری است !");
                    userVM.States = await _suppService.GetStatesAsync();
                    if (userVM.StateId != null)
                    {
                        userVM.Counties = await _suppService.GetCountiesOfState(userVM.StateId.Value);
                    }
                    return View(userVM);
                }
                User user = new()
                {
                    Name = userVM.Name,
                    Family = userVM.Family,
                    Cellphone = userVM.Cellphone,
                    UserName = userVM.UserName,
                    IsActive = userVM.IsActive,
                    CountyId = userVM.CountyId,
                    Address = userVM.Address,
                    PostalCode = userVM.PostalCode,
                    Password = userVM.Password,
                    Email = userVM.Email,
                    BirthDate = userVM.BirthDate?.ToMiladiWithoutTime(),

                };
                if (string.IsNullOrEmpty(userVM.UserName))
                {
                    user.UserName = userVM.Cellphone;
                }
                _userService.CreateUser(user);
                await _userService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            userVM.States = await _suppService.GetStatesAsync();
            if (userVM.StateId != null)
            {
                userVM.Counties = await _suppService.GetCountiesOfState(userVM.StateId.Value);
            }

            return View(userVM);
        }
        public async Task<IActionResult> GetCounties(int sId)
        {
            List<County> counties = await _suppService.GetCountiesOfState(sId);
            return PartialView(counties);
        }
        // GET: UsersPanel/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _userService.GetUsersAsync() == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            UserVM userVM = new()
            {
                Name = user.Name,
                Family = user.Family,
                BirthDate = user.BirthDate.ToShamsiN(),
                IsActive = user.IsActive,
                Cellphone = user.Cellphone,
                Password = user.Password,
                UserName = user.UserName,
                CountyId = user.CountyId,
                Address = user.Address,
                PostalCode = user.PostalCode,
                Email = user.Email,
                Id = id.Value
            };
            if (user == null)
            {
                return NotFound();
            }
            userVM.States = await _suppService.GetStatesAsync();
            if (user.CountyId != null)
            {
                userVM.Counties = await _suppService.GetCountiesOfState(user.County!.StateId);
                userVM.StateId = user.County.StateId;
            }

            return View(userVM);
        }

        // POST: UsersPanel/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserVM userVM)
        {
            if (id != userVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User? userCell = await _userService.GetUserByCellphone(userVM.Cellphone!);
                    if (userCell != null)
                    {
                        if (userCell.Id != userVM.Id)
                        {
                            ModelState.AddModelError("Cellphone", "شماره تلفن همراه تکراری است !");
                            userVM.States = await _suppService.GetStatesAsync();
                            if (userVM.StateId != null)
                            {
                                userVM.Counties = await _suppService.GetCountiesOfState(userVM.StateId.Value);
                            }
                            return View(userVM);
                        }

                    }
                    User? userName = await _userService.GetUserByUsernameAsync(userVM.UserName!);
                    if (userName != null)
                    {
                        if (userName.Id != userVM.Id)
                        {
                            ModelState.AddModelError("UserName", "نام کاربری تکراری است !");
                            userVM.States = await _suppService.GetStatesAsync();
                            if (userVM.StateId != null)
                            {
                                userVM.Counties = await _suppService.GetCountiesOfState(userVM.StateId.Value);
                            }
                            return View(userVM);
                        }

                    }

                    User user = await _userService.GetUserByIdAsync(userVM.Id);
                    if (user != null)
                    {
                        user.Name = userVM.Name;
                        user.Family = userVM.Family;
                        user.Cellphone = userVM.Cellphone;
                        user.UserName = userVM.UserName;
                        user.IsActive = userVM.IsActive;
                        user.CountyId = userVM.CountyId;
                        user.Address = userVM.Address;
                        user.PostalCode = userVM.PostalCode;
                        user.Password = userVM.Password;
                        user.Email = userVM.Email;
                        user.BirthDate = userVM.BirthDate?.ToMiladiWithoutTime();
                        _userService.UpdateUser(user);
                        await _userService.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            userVM.States = await _suppService.GetStatesAsync();
            if (userVM.StateId != null)
            {
                userVM.Counties = await _suppService.GetCountiesOfState(userVM.StateId!.Value);
            }
            return View(userVM);
        }

        // GET: UsersPanel/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _userService.GetUsersAsync() == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersPanel/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _userService.GetUsersAsync() == null)
            {
                return Problem("Entity set 'MyContext.Users'  is null.");
            }
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
            {
                _userService.DeleteUser(id);
                await _userService.SaveChangesAsync();
            }            
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            List<User> users = _userService.GetUsersAsync().Result;
            return users.Any(u => u.Id == id);
        }
    }
}
