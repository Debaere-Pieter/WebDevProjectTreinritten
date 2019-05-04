using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Extensions;
using ProjectTreinritten.Service;
using ProjectTreinritten.ViewModel;
using ProjectTreinritten.ViewModels;
using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Controllers
{
    public class TreinController : Controller
    {
        private BoekingService boekingService;
        private HotelService hotelService;
        private RitService ritService;
        private StationService stationService;
        private TrajectService trajectService;
        private TreinTypeService treinTypeService;
        private VakantieDagenService vakantieDagenService;

        public TreinController()
        {
            boekingService = new BoekingService();
            hotelService = new HotelService();
            ritService = new RitService();
            stationService = new StationService();
            trajectService = new TrajectService();
            treinTypeService = new TreinTypeService();
            vakantieDagenService = new VakantieDagenService();
        }

        //todo's vragen voor de prijs en vragen of we moeten checken of ticketten uniek moeten zijn.
        //overstappen hardcoden of computer laten zoeken
        //hotelId nullable maken

        //startpagina met linken naar alle functionaliteiten
        public IActionResult Index()
        {
            return View();
        }

        //pagina met alle informatie over het project: in opdracht van, door wie gemaakt etc.
        public IActionResult Over()
        {
            return View();
        }

        //pagina om een boeking te doen
        public IActionResult Boeken()
        {
            stationService = new StationService();
            ViewBag.StationId =
                new SelectList(stationService.GetAll(),
                 "StationId", "StationNaam");

            hotelService = new HotelService();
            ViewBag.HotelId =
                new SelectList(hotelService.GetAll(),
                 "StationId", "HotelNaam");

            return View();
        }

        [HttpPost]
        public IActionResult Boeken(BoekenVM b)
        {
            return View();
        }

        //pagina die bevestiging van boeking toont
        public IActionResult Bevestiging(BoekingVM b)
        {
            return View(b);
        }

        //pagina om bestelgeschiedenis te bekijken
        public IActionResult Historiek()
        {
            return View();
        }

        //pagina om winkelkar te bekijken
        public IActionResult Winkelkar()
        {
            return RedirectToAction("Index", "ShoppingCart");
        }

        //pagina om in te loggen
        public IActionResult Inloggen()
        {
            return View();
        }

        //pagina voor account aan te maken
        public IActionResult Registreer()
        {
            return View();
        }

        //404 pagina, deze pagina word getoond indien de pagina waarnaar men surft leeg is
        public IActionResult Leeg()
        {
            return View();
        }

        //pagina waar men wachtwoord kan opvragen via email indien men deze vergeten is
        public IActionResult Wachtwoord()
        {
            return View();
        }

        public IActionResult Select(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            Boeking b = boekingService.Get(Convert.ToInt16(id));

            CartVM item = new CartVM
            {
                BoekingNr = b.BoekingId,
                Aantal = 1,
                Prijs = 15,
                DateCreated = DateTime.Now,
                Naam = b.Naam,
                Voornaam = b.Voornaam
            };

            ShoppingCartVM shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            shopping.Cart.Add(item);
            HttpContext.Session.SetObject("ShoppingCart", shopping);


            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}