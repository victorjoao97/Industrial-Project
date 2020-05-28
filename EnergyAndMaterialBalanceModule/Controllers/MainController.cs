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
using EnergyAndMaterialBalanceModule.Models.Form;

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

        [Route("getBGroups/{resourceId}")]
        public async Task<IActionResult> GetBGroups(int resourceId)
        {
            var selectedResource = await _resourceRepository.GetById((short)resourceId);
            _result.SelectedResource = selectedResource;
            IEnumerable<Bgroups> rootBGroups = await _bgroupsRepository.GetRootBGroups(resourceId);
            foreach (var group in rootBGroups)
            {
                await _bgroupsRepository.GetAllChildren(group.BgroupId);
            }
            _result.error = false;
            _result.message = "Success";
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
            _result.error = false;
            _result.message = "Success";
            return new JsonResult(_result);
        }

        [HttpPost]
        [Route("deleteBGroup")]
        public async Task<IActionResult> DeleteBGroup(int bgroupId)
        {
            try
            {
               await _bgroupsRepository.DeleteWithDependent(bgroupId);
            }
            catch (Exception ex)
            {
                _result.error = true;
                _result.message = "Error";
            }
            _result.error = false;
            _result.message = "Success";
            return new JsonResult(_result);
        }

        [HttpPost]
        [Route("createBGroup")]
        public async Task<IActionResult> CreateBgroups(CreateBgroupsFm model)
        {
            Bgroups newBgroups = new Bgroups();
            newBgroups.BgroupName = model.bgroupName;
            newBgroups.ValidDisbalance = model.validDisbalance;
            newBgroups.ResourceId = (short)model.resourceId;

            if (model.bGroupIdParent != null)
            {
                newBgroups.BgroupIdparent = model.bGroupIdParent;
            }

            await _bgroupsRepository.Create(newBgroups);
            _result.error = false;
            _result.message = "Success";
            return new JsonResult(_result);
        }

        [HttpPost]
        [Route("updateBGroup")]
        public async Task<IActionResult> UpdateBgroups(UpdateBgroupsFm model)
        {
            Bgroups bgroups = await _bgroupsRepository.GetById(model.bgroupId);
            bgroups.BgroupName = model.bgroupName;
            bgroups.ValidDisbalance = model.validDisbalance;

            await _bgroupsRepository.Update(bgroups);
            _result.error = false;
            _result.message = "Success";
            _result.SelectedBGroup = bgroups;
            return new JsonResult(_result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBGroupsPoints(int resourceId, int bgroupId)
        {
            var bGroup = await _bGroupsRepository.GetById(bgroupId);
            ViewData["Points"] = bGroup.Points;
            HttpContext.Session.SetInt32(SessionSelectedResource, resourceId);
            ViewData["bGroupId"] = bgroupId;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
