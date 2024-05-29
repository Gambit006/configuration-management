using ConfigurationManagement;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly ConfigurationRecordsUtilities _configurationRecordsUtilities;

        public ConfigurationController(IConfiguration configuration)
        {
            var ConnectionString = configuration["ConnectionStrings:MongoDbConnString"];
            _configurationRecordsUtilities = new ConfigurationRecordsUtilities(ConnectionString, "SERVICE-A");
        }

        public IActionResult Index()
        {
            var records = _configurationRecordsUtilities.GetConfigurationRecords().Select(r => ConfigruationRecordViewModelMapper(r));
            return View(records);
        }

        [HttpPost]
        public IActionResult Update(ConfigurationRecordViewModel recordViewModel)
        {
            _configurationRecordsUtilities.UpdateRecord(ConfigurationRecordMapper(recordViewModel));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _configurationRecordsUtilities.DeleteRecord(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ConfigurationRecordViewModel record)
        {
            _configurationRecordsUtilities.InsertRecord(ConfigurationRecordMapper(record));
            return RedirectToAction("Index");
        }

        public ConfigurationRecordViewModel ConfigruationRecordViewModelMapper(ConfigurationRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            return new ConfigurationRecordViewModel
            {
                Id = record.Id,
                Name = record.Name,
                Type = record.Type,
                Value = ConfigurationRecordsUtilities.ConvertFromType(record.Value),
                IsActive = record.IsActive,
            };
        }

        public ConfigurationRecord ConfigurationRecordMapper(ConfigurationRecordViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new ConfigurationRecord
            (
                viewModel.Id,
                viewModel.Name,
                viewModel.Type,
                ConfigurationRecordsUtilities.ConvertToType(viewModel.Value, viewModel.Type),
                viewModel.IsActive
            );
        }

    }
}
