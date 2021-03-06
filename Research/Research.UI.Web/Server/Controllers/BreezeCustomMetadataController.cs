﻿
namespace Research.UI.Web.Server.Controllers
{
    using Breeze.ContextProvider;
    using Breeze.ContextProvider.EF6;
    using Breeze.WebApi2;
    using Newtonsoft.Json.Linq;
    using Research.UI.Web.Migrations;
    using Research.UI.Web.Server.Components;
    using Research.UI.Web.Server.Model;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Http;

    [BreezeController]
    public class BreezeCustomMetadataController : ApiController
    {
        private readonly EFContextProvider<ResearchDbContext> _contextProvider;
        private static string _customMetaData;
        private readonly ICustomMetaDataBuilder _customMetaDataBuilder;

        public BreezeCustomMetadataController(): this(null, null, null)
        {
        }

        public BreezeCustomMetadataController(EFContextProvider<ResearchDbContext> contextProvider, string customMetaData, ICustomMetaDataBuilder customMetaDataBuilder)
        {
            _contextProvider = contextProvider ??  new EFContextProvider<ResearchDbContext>();

            if (!string.IsNullOrWhiteSpace(customMetaData))
            {
                _customMetaData = customMetaData;
            }

            _customMetaDataBuilder = customMetaDataBuilder ?? new CustomMetaDataBuilder();
        }

        [HttpGet]
        public string CustomMetaData()
        {
            if (string.IsNullOrWhiteSpace(_customMetaData))
            {
                _customMetaData = _customMetaDataBuilder.GetCustomMetaData(new ResearchDbContext());
            }
            return _customMetaData;
        }

        [HttpGet]
        public IQueryable<Declaration> Declaration()
        {
            return _contextProvider.Context.Declarations;
        }

        [HttpGet]
        public IQueryable<Employee> Employee()
        {
            return _contextProvider.Context.Employees;
        }

        [HttpGet]
        public string Metadata()
        {
            string result = _contextProvider.Metadata();
            return result;
        }

        [HttpGet]
        public void ResetCustomMetaData()
        {
            _customMetaData = _customMetaDataBuilder.GetCustomMetaData(_contextProvider.Context);
        }
        
        [HttpGet]
        public void ReSeed()
        {
            var configuration = new Research.UI.Web.Migrations.Configuration();
            configuration.ContextType = typeof(ResearchDbContext);
            var migrator = new DbMigrator(configuration);

            // This will run the migration update script and will run Seed() method.
            migrator.Update();
        }

        [HttpGet]
        public IQueryable<Setting> Setting()
        {
            return _contextProvider.Context.Settings;
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }
    }
}
