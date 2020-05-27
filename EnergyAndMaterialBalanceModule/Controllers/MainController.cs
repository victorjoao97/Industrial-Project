using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EnergyAndMaterialBalanceModule.Models;
using EnergyAndMaterialBalanceModule.Data;
using Microsoft.EntityFrameworkCore;
using EnergyAndMaterialBalanceModule.Data.Repositories;
using Microsoft.AspNetCore.Http;

namespace EnergyAndMaterialBalanceModule.Controllers
{ 
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly IResourcesRepository _resourceRepository;
        private readonly IBGroupsRepository _bGroupRepository;

        const string SessionSelectedResource = "_Resource";

        public MainController(ILogger<MainController> logger, IResourcesRepository resourceRepository, IBGroupsRepository bGroupsRepository)
        {
            _logger = logger;
            _resourceRepository = resourceRepository;
            _bGroupRepository = bGroupsRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Resources> resources = await _resourceRepository.GetAllResources();
            ViewData["Resources"] = resources;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            IEnumerable<Resources> resources = await _resourceRepository.GetAllResources();
            ViewData["Resources"] = resources;
            HttpContext.Session.SetInt32(SessionSelectedResource, id);
            ViewData["SelectedResource"] = HttpContext.Session.GetInt32(SessionSelectedResource);

            IEnumerable<Bgroups> rootBgroups = await _bGroupRepository.GetRootBgroups(id);
            foreach (var group in rootBgroups)
            {
                LoadSubGroups(group);
            }

            ViewData["BGroups"] = Json(rootBgroups);

            return View();
        }

        private void LoadSubGroups(Bgroups item)
        {
            IEnumerable<Bgroups> childBgroups = item.InverseBgroupIdparentNavigation;

            if (childBgroups != null)
            {
                foreach (var group in childBgroups)
                {
                    Bgroups b = _bGroupRepository.GetById(group.BgroupId);
                    group.InverseBgroupIdparentNavigation = b.InverseBgroupIdparentNavigation;
                    LoadSubGroups(group);
                }

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
