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
        private readonly IPointsRepository _pointsRepository;
        const string SessionSelectedResource = "_Resource";
        const string SessionSelectedBGroup = "_BGroup";


        public MainController(ILogger<MainController> logger, IResourcesRepository resourceRepository, IBGroupsRepository bGroupsRepository, IPointsRepository pointsRepository)
        {
            _logger = logger;
            _resourceRepository = resourceRepository;
            _bGroupRepository = bGroupsRepository;
            _pointsRepository = pointsRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Resources> resources = await _resourceRepository.GetAllResources();
            ViewData["Resources"] = resources;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int resourceId)
        {
            var resources = await _resourceRepository.GetAllResources();
            ViewData["Resources"] = resources;
            HttpContext.Session.SetInt32(SessionSelectedResource, resourceId);
            ViewData["SelectedResource"] = HttpContext.Session.GetInt32(SessionSelectedResource);

            var rootBgroups = await _bGroupRepository.GetRootBGroups(resourceId);
            
            foreach (var group in rootBgroups)
            {
                LoadSubBgroups(group);
            }

            ViewData["BGroups"] = Json(rootBgroups);

            return View();
        }

        [HttpGet("Main/Index/{resourceId}/{bgroupId}")]
        public async Task<IActionResult> Index(int resourceId, int bGroupId)
        {
            var resources = await _resourceRepository.GetAllResources();
            ViewData["Resources"] = resources;
            ViewData["SelectedResource"] = HttpContext.Session.GetInt32(SessionSelectedResource);

            HttpContext.Session.SetInt32(SessionSelectedBGroup, bGroupId);
            ViewData["SelectedBGroup"] = HttpContext.Session.GetInt32(SessionSelectedBGroup);
            ViewData["SelectedBGroupObj"] = await _bGroupRepository.GetById(bGroupId);

            var points = _pointsRepository.GetAllPoints(bGroupId);
            ViewData["Points"] = points;


            var rootBgroups = await _bGroupRepository.GetRootBGroups(resourceId);

            foreach (var group in rootBgroups)
            {
                LoadSubBgroups(group);
            }

            ViewData["BGroups"] = Json(rootBgroups);

            return View();
        }

        private void LoadSubBgroups(Bgroups item)
        {   
            IEnumerable<Bgroups> subBgroups = item.InverseBgroupIdparentNavigation;

            if (subBgroups != null)
            {
                foreach (var group in subBgroups)
                {
                    Bgroups b = _bGroupRepository.GetBGroupsById(group.BgroupId);
                    group.InverseBgroupIdparentNavigation = b.InverseBgroupIdparentNavigation;
                    LoadSubBgroups(group);
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
