﻿using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace BlogApp.Controllers
{
    public class UsersController:Controller

    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index","Posts");
            }
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> Register(RegisterViewModel model)
        {

            

            if (ModelState.IsValid)
            {
                var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email  );
                if(user == null)
                {
                    _userRepository.CreateUser(new User
                    {
                        UserName = model.UserName,
                        Name = model.Name,
                        Email= model.Email,
                        Password= model.Password,
                        Image= "p1.png"


                    });

                return RedirectToAction("Login");


                }
                else
                {
                    ModelState.AddModelError("","Username yada Email Kullanımda.");
                }


            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = _userRepository.Users.FirstOrDefault(x => x.Email==model.Email && x.Password==model.Password );
            
            if(isUser != null)
                {
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString())) ;
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                    if (isUser.Email == "apaydinyasar0@gmail.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
};
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                        );
                    return RedirectToAction("Index","Posts");

                }
              else
            {
                ModelState.AddModelError("","Kullanıcı Adı Veya Şifre Yanlış.");

            }
            
            }
          
            return View(model);

        }



        public IActionResult Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user = _userRepository
                .Users
                .Include(i => i.Posts)
                .Include(i => i.Comments)
                .ThenInclude(i => i.Post)
                .FirstOrDefault(i => i.UserName == username);
        if(user == null)
            {
                return NotFound();

            }
            return View(user);
        }


    }
}
