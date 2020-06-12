using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context context;

        public HomeController(Context DBContext)
        {
            context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = context.Leagues
                .Where(l => l.Sport.Contains("Baseball"));
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
          ViewBag.One = context.Leagues.Where(l => l.Name.Contains("Women")).ToList();
          ViewBag.Two = context.Leagues.Where(l => l.Name.Contains("Hockey")).ToList();
          ViewBag.Three = context.Leagues.Where(l => !l.Name.Contains("Football")).ToList();
          ViewBag.Four = context.Leagues.Where(l => l.Name.Contains("Conference")).ToList();
          ViewBag.Five = context.Leagues.Where(l => l.Name.Contains("Altantic")).ToList();   
          ViewBag.Six = context.Teams.Where(t => t.Location == "Dallas").ToList();
          ViewBag.Seven = context.Teams.Where(t => t.TeamName == "Raptors").ToList();
          ViewBag.Eight = context.Teams.Where(t => t.Location.Contains("City")).ToList();      
          ViewBag.Nine = context.Teams.Where(t => t.TeamName[0] == 'T').ToList();
          ViewBag.Ten = context.Teams.OrderBy(t => t.Location).ToList();
          ViewBag.Eleven = context.Teams.OrderByDescending(t => t.TeamName).ToList();
          ViewBag.Twelve  = context.Players.Where(p => p.LastName == "Cooper").ToList(); 
          ViewBag.Thirteen  = context.Players.Where(p => p.FirstName == "Joshua").ToList(); 
          ViewBag.Fourteen  = context.Players.Where(p => p.LastName == "Cooper" && p.FirstName != "Joshua").ToList();
          ViewBag.fifteen  = context.Players.Where(p => p.FirstName == "Alexander" || p.FirstName == "Wyatt").ToList();
          return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}