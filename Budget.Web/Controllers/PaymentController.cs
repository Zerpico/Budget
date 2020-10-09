﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Web.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}