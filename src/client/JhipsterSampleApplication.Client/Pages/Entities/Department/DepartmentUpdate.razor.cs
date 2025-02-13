using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Department;
using JhipsterSampleApplication.Client.Services.EntityServices.Location;

using JhipsterSampleApplication.Client.Services.EntityServices.Employee;

using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Department
{
    public partial class DepartmentUpdate : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        private IDepartmentService DepartmentService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private ILocationService LocationService { get; set; }

        private IEnumerable<LocationModel> Locations { get; set; } = new List<LocationModel>();

        public DepartmentModel DepartmentModel { get; set; } = new DepartmentModel();

        public IEnumerable<long?> LocationIds { get; set; } = new List<long?>();

        public long? LocationId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Locations = await LocationService.GetAll();
            LocationIds = Locations.Select(location => location.Id).ToList();

            if (Id != 0 && Id != null)
            {
                DepartmentModel = await DepartmentService.Get(Id.ToString());
                LocationId = DepartmentModel.LocationId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            DepartmentModel.LocationId = Locations?.SingleOrDefault(location => location.Id.Equals(LocationId))?.Id;

            if (Id != 0 && Id != null)
            {
                await DepartmentService.Update(DepartmentModel);
            }
            else
            {
                await DepartmentService.Add(DepartmentModel);
            }
            NavigationService.Previous();
        }
    }
}
