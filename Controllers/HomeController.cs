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
          ViewBag.One = context.Teams.Include(t => t.CurrLeague)
                                      .Where(t => t.CurrLeague.Name
                                      .Contains("Atlantic Soccer Conference"))
                                      .ToList();
          ViewBag.Two = context.Players.Include(p => p.CurrentTeam)
                                        .Where(p => p.CurrentTeam.TeamName == "Penguins" && p.CurrentTeam.Location == "Boston")
                                        .ToList();
          ViewBag.Three = context.Teams.Include(t => t.CurrLeague)
                                      .Where(t => t.CurrLeague.Name == "International Collegiate Baseball Conference")
                                      .ToList();
          ViewBag.Four = context.Teams.Include(t => t.CurrLeague)
                                      .Where(t => t.CurrLeague.Name == "American Conference of Amateur Football")
                                      .ToList();
          ViewBag.Five = context.Teams.ToList();
          ViewBag.Six = context.Teams.Include(t => t.CurrentPlayers)
                                      .Where(t => t.CurrentPlayers
                                      .Any(p => p.FirstName == "Sophia"))
                                      .ToList();
          ViewBag.Seven = context.Players.Include(p => p.CurrentTeam)
                                        .Where(p => p.CurrentTeam.TeamName != "Raptors" && p.LastName == "Flores")
                                        .ToList();
          ViewBag.Eight = context.Players.Include(p => p.CurrentTeam)
                                        .Where(p => p.CurrentTeam.TeamName == "Tiger-Cats" && p.CurrentTeam.Location == "Manitoba")
                                        .ToList();
          ViewBag.Nine = context.Teams.Include(t => t.AllPlayers)
                                      .Where(t => t.AllPlayers.Count >= 12)
                                      .ToList();                                                                            
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
          ViewBag.One = context.Players
                              .Include(p => p.AllTeams)
                              .ThenInclude(pt => pt.TeamOfPlayer)
                              .FirstOrDefault(p => p.FirstName == "Alexander" && p.LastName == "Bailey");
          ViewBag.Two = context.Teams
                              .Include(t => t.AllPlayers)
                              .ThenInclude(pt => pt.PlayerOnTeam)
                              .FirstOrDefault(t => t.Location == "Manitoba" && t.TeamName == "Tiger-Cats");
          ViewBag.Three = context.PlayerTeams
                              .Include(pt => pt.PlayerOnTeam)
                              .ThenInclude(p => p.CurrentTeam)
                              .Include(pt => pt.TeamOfPlayer)
                              .Where(pt => pt.TeamOfPlayer.Location == "Wichita" && pt.TeamOfPlayer.TeamName == "Vikings")
                              .Where(pt => pt.PlayerOnTeam.CurrentTeam.Location != "Wichita" && pt.PlayerOnTeam.CurrentTeam.TeamName != "Vikings")
                              .ToList();
          ViewBag.Four = context.PlayerTeams
                                .Include(pt => pt.PlayerOnTeam)
                                .ThenInclude(p => p.CurrentTeam)
                                .Include(pt => pt.TeamOfPlayer)
                                .Where(pt => pt.PlayerOnTeam.FirstName == "Emily" && pt.PlayerOnTeam.LastName == "Sanchez")
                                .Where(pt => pt.TeamOfPlayer.Location != "Indiana" && pt.TeamOfPlayer.TeamName != "Royals")
                                .ToList();
          ViewBag.Five = context.PlayerTeams
                                .Include(pt => pt.TeamOfPlayer)
                                .ThenInclude( t => t.CurrLeague)
                                .Include(pt => pt.PlayerOnTeam)
                                .Where(pt => pt.PlayerOnTeam.FirstName == "Levi")
                                .Where(pt => pt.TeamOfPlayer.CurrLeague.Name == "Atlantic Federation of Amateur Baseball Players")
                                .ToList();
          ViewBag.Six = context.Players
                                .Include(p => p.AllTeams)
                                .OrderByDescending(p => p.AllTeams.Count)
                                .ToList();



            return View();
        }

    }
}