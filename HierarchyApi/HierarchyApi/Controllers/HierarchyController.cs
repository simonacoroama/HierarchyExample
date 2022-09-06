using System.Data;
using System.Data.SqlClient;
using HierarchyApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Attribute = HierarchyApi.Contracts.Attribute;

namespace HierarchyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HierarchyController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<HierarchyController> logger;

        public HierarchyController(
            ILogger<HierarchyController> logger,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        [Microsoft.AspNetCore.Cors.EnableCors("AllowOrigin")]
        [HttpGet(Name = "GetHierarchy")]
        public Hierarchy Get()
        {
            return new Hierarchy
            {
                Children = this.GetItems().ToList(),
                Name = "Rusta"
            };
        }

        private IEnumerable<Country> GetItems()
        {
            var dataTable = new DataTable();

            var connectionString = this.configuration.GetConnectionString("OBPConn");

            using (var conn = new SqlConnection(connectionString))
            {
                var sql =
                    @"SELECT DISTINCT 
	                    country.CountryName, 
	                    ch.Number AS ChainNumber, 
	                    ch.Description AS ChainDescription, 
	                    st.Number AS StoreNr, 
	                    st.Description AS StoreDescr, 	
	                    workstation.Number AS WorkstationNumber,
	                    workstation.Description AS WorkstationDescription
                    FROM [OBP].[config].[BusinessUnit] st 
                    LEFT JOIN [OBP].[config].[BusinessUnit] ch ON ch.Id = st.ParentId AND ch.BusinessUnitType = 1
                    LEFT JOIN [OBP].[config].[Chain] chain ON chain.BusinessUnitId = ch.Id
                    LEFT JOIN [OBP].address.CountryName country ON country.CountryId = chain.CountryId
                    LEFT JOIN OBP.config.Workstation workstation ON workstation.BusinessUnitId = st.Id
                    WHERE st.BusinessUnitType = 2 
	                    AND country.LanguageId = 'EN'
                    ORDER BY country.CountryName, ch.Number, st.number, workstation.Number
                    ";

                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    using (var rd = cmd.ExecuteReader())
                    {
                        dataTable.Load(rd);
                    }
                }
            }

            return dataTable.AsEnumerable()
                .GroupBy(r => r["CountryName"])
                .Select(g => CreateCountry(g.ToArray()));
        }

        private Country CreateCountry(DataRow[] rows)
        {
            var name = rows[0].Field<string>("CountryName");

            var children = rows.GroupBy(r => r["ChainNumber"])
                .Select(r => CreateChain(r.ToArray()));

            return new Country
            {
                Name = name,
                Children = children.ToList(),
                Attributes = new Attribute { Type = ItemType.Country.ToString() }
            };
        }

        private Chain CreateChain(DataRow[] rows)
        {
            var number = rows[0].Field<int>("ChainNumber");
            var name = rows[0].Field<string>("ChainDescription");

            var children = rows.GroupBy(r => r["StoreNr"])
              .Select(r => CreateStore(r.ToArray()));

            return new Chain
            {
                Name = name,
                Attributes = new Attribute
                {
                    Number = number.ToString(),
                    Type = ItemType.Chain.ToString()
                },
                Children = children.ToList()
            };
        }

        private Store CreateStore(DataRow[] rows)
        {
            var number = rows[0].Field<int>("StoreNr");
            var name = rows[0].Field<string>("StoreDescr");

            var children = rows.GroupBy(r => r["WorkstationNumber"])
             .Select(r => CreateWorkstation(r.ToArray()));

            return new Store
            {
                Name = name,
                Attributes = new Attribute
                {
                    Number = number.ToString(),
                    Type = ItemType.Store.ToString()
                },
                Children = children.Where(p => p != null).ToList()
            };
        }

        private Workstation CreateWorkstation(DataRow[] rows)
        {
            var number = rows[0].Field<int?>("WorkstationNumber");
            var name = rows[0].Field<string>("WorkstationDescription");

            if (number == null)
            {
                return null;
            }

            return new Workstation
            {
                Name = name,
                Attributes = new Attribute
                {
                    Number = number.ToString(),
                    Type = ItemType.Workstation.ToString()
                }
            };
        }
    }
}