using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EnergyAndMaterialBalanceModule.Models;
using EnergyAndMaterialBalanceModule.Data.Repositories;
using Microsoft.AspNetCore.Http;

namespace EnergyAndMaterialBalanceModule.Controllers
{
    [Route("main")]
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly IResourcesRepository _resourceRepository;
        private readonly IBGroupsRepository _bgroupsRepository;
        private readonly IPointsRepository _pointsRepository;

        private ResultDto _result = new ResultDto();

        public MainController(ILogger<MainController> logger, IResourcesRepository resourceRepository, IBGroupsRepository bgroupsRepository, IPointsRepository pointsRepository)
        {
            _logger = logger;
            _resourceRepository = resourceRepository;
            _bgroupsRepository = bgroupsRepository;
            _pointsRepository = pointsRepository;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Resources> resources = await _resourceRepository.GetAllResources();
            ViewData["Resources"] = resources;
            return View();
        }

        [Route("getBGroup/{resourceId}")]
        public async Task<IActionResult> GetBGroups(int resourceId)
        {
            var selectedResource = await _resourceRepository.GetById((short)resourceId);
            _result.SelectedResource = selectedResource;
            IEnumerable<Bgroups> rootBGroups = await _bgroupsRepository.GetRootBGroups(resourceId);
            foreach (var group in rootBGroups)
            {
                await _bgroupsRepository.GetAllChildren(group.BgroupId);
            }
            _result.Bgroups = rootBGroups;
            return new JsonResult(_result);
        }

        [Route("getPoints/{bgroupId}")]
        public async Task<IActionResult> GetPoints(int bgroupId)
        {
            var selectedBGroup = await _bgroupsRepository.GetById(bgroupId);
            var selectedResource = selectedBGroup.Resource;
            _result.SelectedResource = selectedResource;
            _result.SelectedBGroup = selectedBGroup;
            _result.Points = await _pointsRepository.GetAlPonts(selectedBGroup.BgroupId);
            return new JsonResult(_result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
