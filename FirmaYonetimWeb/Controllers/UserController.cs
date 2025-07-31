using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Entities;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UserList()
        {
            var model = new FirmaPersonel
            {
                users = _userManager.Users.ToList(),
            };


            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]

        public IActionResult CreateUser()
        {
            var model = new FirmaPersonel
            {
                users = _userManager.Users.ToList(),
                createUser = new CreateUserModel()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel createUser)
        {
            if (ModelState.IsValid)
            {
                var newUser = new AppUser
                {
                    UserName = createUser.KullaniciAdi,
                    FirstName = createUser.Ad,
                    LastName = createUser.Soyad,
                    Email = createUser.Email,
                    PhoneNumber = createUser.PhoneNumber,
                    
                };
                var identityResult = _userManager.CreateAsync(newUser, createUser.Sifre).Result;

                if (createUser.IsAdmin)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                        await _roleManager.CreateAsync(new AppRole { Name = "Admin" });

                    await _userManager.AddToRoleAsync(newUser, "Admin");
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                        await _roleManager.CreateAsync(new AppRole { Name = "User" });

                    await _userManager.AddToRoleAsync(newUser, "User");
                }

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("UserList");

            }
            else
            {
                return RedirectToAction("UserList");

            }


        }

        public IActionResult UpdateUser()
        {
            var model = new FirmaPersonel
            {
                users = _userManager.Users.ToList(),
                updateUser = new UpdateUserModel()
            };
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
       
        public async Task<IActionResult> UpdateUser(UpdateUserModel updateUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == updateUser.Id);
                if (user != null)
                {
                 
                    user.UserName = updateUser.KullaniciAdi;
                    user.PhoneNumber = updateUser.PhoneNumber;
                    user.Email = updateUser.Email;

                    var updateResult = await _userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (updateUser.IsAdmin)
                        {
                            if (!roles.Contains("Admin"))
                            {
                                await _userManager.RemoveFromRolesAsync(user, roles);
                                await _userManager.AddToRoleAsync(user, "Admin");
                            }
                        }
                        else
                        {
                            if (roles.Contains("Admin")) 
                            {
                                await _userManager.RemoveFromRolesAsync(user, roles);
                                await _userManager.AddToRoleAsync(user, "User");
                            }
                        }

                        if (!string.IsNullOrEmpty(updateUser.Sifre))
                        {
                            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                            if (removePasswordResult.Succeeded)
                            {
                                var addResult = await _userManager.AddPasswordAsync(user, updateUser.Sifre);
                                if (!addResult.Succeeded)
                                {
                                    foreach (var error in addResult.Errors)
                                    {
                                        ModelState.AddModelError("", error.Description);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var error in updateResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            return RedirectToAction("UserList");
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser (int id)
        {
            var user = _userManager.Users.FirstOrDefault(m => m.Id == id);
            if (user != null)
            {
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
       

                return View();

        }





    }
}
