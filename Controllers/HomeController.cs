using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandWordGen.Models;
using Microsoft.AspNetCore.Http;

namespace RandWordGen.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("num") == null)
            {
                HttpContext.Session.SetInt32("num", 1);
            }
            else
            {
                int count = HttpContext.Session.GetInt32("num").GetValueOrDefault();
                HttpContext.Session.SetInt32("num", count += 1);
            }
            ViewBag.Num = HttpContext.Session.GetInt32("num");
            var pass = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var randPass = new char[24];
            Random rand = new Random();
            for (int i = 0; i < randPass.Length; i++)
            {
                if ( i%6 == 0)
                {
                    randPass[0] = ' ';
                    randPass[i] = '-';
                }
                else
                {
                    randPass[i] = pass[rand.Next(pass.Length)];
                }
            }
            var passcode = new String(randPass);
            HttpContext.Session.SetString("word", passcode);
            ViewBag.Word = HttpContext.Session.GetString("word");
            return View();

        }

        [HttpGet("generate")]
        public IActionResult Generate()
        {
            return Redirect("/");
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
