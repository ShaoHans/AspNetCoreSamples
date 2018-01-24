using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptionsApp.Models;
using Microsoft.Extensions.Options;

namespace OptionsApp.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppSettings _appSettings;

        public SettingsController(IOptionsSnapshot<AppSettings> optionsAccessor)
        {
            _appSettings = optionsAccessor.Value;
        }

        public IActionResult Index()
        {
            return View(_appSettings);
        }
    }
}