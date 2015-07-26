using System;
using System.Linq;
using System.Threading.Tasks;
using MHTemperature.Service.Domain;
using MHTemperature.Service.Web.Web.Model;
using Residata.Platform.Contract.Web.Attribute;
using Residata.Platform.Web.Controller;

namespace MHTemperature.Service.Web.Web.Controller {
    public class IndexController : ControllerBase {
        private readonly StorageService _storageService;

        public IndexController(StorageService storageService) {
            _storageService = storageService;
        }

        [View]
        public IndexViewModel IndexAction() {
            var viewModel = new IndexViewModel();

            var today = _storageService.GetToday().ToArray();
            var yesterday = _storageService.GetYesterday().ToArray();

            Parallel.Invoke(
                () => viewModel.Swimmer = CreatePoolViewModel(today, yesterday, x => x.Swimmer),
                () => viewModel.NonSwimmer = CreatePoolViewModel(today, yesterday, x => x.NonSwimmer),
                () => viewModel.KidsSlash = CreatePoolViewModel(today, yesterday, x => x.KidSplash)
            );

            return viewModel;
        }

        private PoolViewModel CreatePoolViewModel(MHTemperature.Service.Model.Temperature[] today, MHTemperature.Service.Model.Temperature[] yesterday, Func<MHTemperature.Service.Model.Temperature, float> select) {
            PoolViewModel model = null;

            if (today.Any()) {
                model = new PoolViewModel {
                    Current = select(today.Last()),
                    TodayAverage = today.Average(select),
                    YesterdayAverage = yesterday.Average(select)
                };
            }

            return model;
        }
    }
}