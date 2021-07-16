using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Roles()
        {
            List<IdentityRole> roles = _context.Roles.ToList();
            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(IdentityRole model)
        {
            var result = await _roleManager.CreateAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Roles", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new VmRole
            {
                Id = role.Id,
                RoleName = role.Name
            };



            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(VmRole model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(role);
        }





        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Users()
        {
            List<CustomUser> users = _context.CustomUsers.ToList();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(string id)
        {
            CustomUser user = _context.CustomUsers.Find(id);
            var userRole = _context.UserRoles.FirstOrDefault(r => r.UserId == id);
            List<SelectListItem> roles = new List<SelectListItem>();
            foreach (var item in _context.Roles.ToList())
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Value = item.Id;
                selectListItem.Text = item.Name;

                roles.Add(selectListItem);
            }

            user.Roles = roles;
            if (userRole != null)
            {
                user.RoleId = userRole.RoleId;
            }
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(CustomUser model)
        {
            if (ModelState.IsValid)
            {
                CustomUser customUser = _context.CustomUsers.Find(model.Id);
                customUser.Name = model.Name;
                customUser.Surname = model.Surname;
                customUser.Profision = model.Profision;
                customUser.Adress = model.Adress;
                customUser.About = model.About;
                customUser.IsVerify = model.IsVerify;

                var selectedRole = _context.Roles.Find(model.RoleId);
                if (selectedRole == null)
                {
                    return NotFound();
                }

                var oldRole = _context.UserRoles.FirstOrDefault(r => r.UserId == customUser.Id);
                if (oldRole != null)
                {
                    await _userManager.RemoveFromRoleAsync(customUser, _context.Roles.Find(oldRole.RoleId).Name);
                }
                await _userManager.AddToRoleAsync(customUser, selectedRole.Name);
                _context.SaveChanges();
                return RedirectToAction("Users");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Users", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(user);
        }






        public JsonResult JsonRoles()
        {
            List<IdentityRole> roles = _context.Roles.ToList();

            return Json(
                new
                {
                    Result = from obj in roles
                             select new
                             {
                                 obj.Id,
                                 obj.Name,
                             }
                }
                );
        }





        public async Task<JsonResult> AddRole(IdentityRole model)
        {
            
            IdentityRole rol = new IdentityRole();


            if (model.Name != null)
            {
                rol.Name = model.Name;
                await _roleManager.CreateAsync(model);
                return Json("1");
            }
            else
            {
                return Json("0");
            }

        }





        [HttpPost]
        public async Task<JsonResult> DeleteJson(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (id != null)
            {
                await _roleManager.DeleteAsync(role);
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }





        [HttpPost]
        public async Task<JsonResult> GuncelleJson(string id)
        {
            var roles = await _roleManager.FindByIdAsync(id);

            return Json(
                new
                {
                    Result = new
                    {
                        id,
                        roles.Name,
                    }
                }
                );
        }





        [HttpPost]
        public async Task<JsonResult> Guncelle(string id, IdentityRole model)
        {
            var role = await _roleManager.FindByIdAsync(id);
            //VmCar car = new VmCar();

            if (model.Name != null)
            {
                role.Name = model.Name;
                await _roleManager.UpdateAsync(role);

                return Json("1");
            }
            else
            {
                return Json("0");
            }

        }


    }

}
