using ConfigurationManagement;
using ConfigurationManagement.DatabaseConnection;
using ConfigurationManagement.Models;
using ConfigurationManagement.Utility;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly MongoDbConnection _connection;

        public ConfigurationController(IConfiguration configuration)
        {
            var ConnectionString = configuration["ConnectionStrings:MongoDbConnString"];
            _connection = new MongoDbConnection(ConnectionString, "SERVICE-A");
            _connection.Open();
        }

        public IActionResult Index()
        {
            
            var records = _connection.GetConfigurationRecords().Select(r => ConfigruationRecordViewModelMapper(r));
            return View(records);
        }

        [HttpPost]
        public IActionResult Update(ConfigurationRecordViewModel recordViewModel)
        {
            _connection.UpdateRecord(ConfigurationRecordMapper(recordViewModel));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _connection.DeleteRecord(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ConfigurationRecordViewModel record)
        {
            _connection.InsertRecord(ConfigurationRecordMapper(record));
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
                Value = ConvertType.ConvertFromType(record.Value),
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
                ConvertType.ConvertToType(viewModel.Value, viewModel.Type),
                viewModel.IsActive
            );
        }

    }
}
