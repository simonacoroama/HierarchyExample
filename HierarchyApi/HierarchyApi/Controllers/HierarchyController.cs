using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HierarchyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HierarchyController   : ControllerBase
    {
        public int count = 0;
        private IConfiguration Configuration;
        private readonly ILogger<HierarchyController> _logger;

        public HierarchyController(ILogger<HierarchyController> logger,
            IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }

        [Microsoft.AspNetCore.Cors.EnableCors("AllowOrigin")]
        [HttpGet(Name = "GetHierarchy")]
        public Hierarchy Get()
        {
            var dataTable = new DataTable();
            
            var connectionstring = this.Configuration.GetConnectionString("OBPConn");
            //build the sqlconnection and execute the sql command
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                var sql = @"Select distinct st.id StoreId, st.number StoreNr, 
st.Description StoreDescr, ch.Number ChainNumber, 
ch.Description ChainDescription, ch.Id ChainId,
country.CountryName CountryName, country.CountryId CountryId, 
workstation.Number As WorkstationNumber,
workstation.Description As WorkstationDescription 
FROM
[OBP].[config].[BusinessUnit] st left  join[OBP].[config].[BusinessUnit] ch on ch.id = st.ParentId and ch.BusinessUnitType = 1
left join[OBP].[config].[Chain] chain on chain.BusinessUnitId = ch.Id
left join[OBP].address.CountryName country on country.CountryId = chain.CountryId
Left join OBP.config.Workstation workstation on workstation.BusinessUnitId = st.Id
  where st.BusinessUnitType = 2 and
  country.LanguageId = 'EN'
order by ch.number,st.number, country.CountryId, workstation.Number
";
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {    
                        dataTable.Load(rd);
                    }
                }
                
            }
            var items = dataTable.AsEnumerable().GroupBy(r => r["CountryId"])
                .Select(g => CreateCountry(g.ToArray()));

            return
                new Hierarchy
                {
                    Children = items.ToList(),
                    Name = "DRS"
                };
           
        }

        private Chain CreateChain(DataRow[] rows)
        {
            var id = rows[0].Field<Guid>("ChainId");
            var number = rows[0].Field<Int32>("ChainNumber");
            var name = rows[0].Field<string>("ChainDescription");

            var children = rows.GroupBy(r => r["StoreId"])
              .Select(r => CreateStore(r.ToArray()));

            return new Chain { Id = id, Name = name, Number = number, Children = children.ToList() };
        }

        private Country CreateCountry(DataRow[] rows)
        {
            var id = rows[0].Field<string>("CountryId");
            var name = rows[0].Field<String>("CountryName");

            var children = rows.GroupBy(r => r["ChainId"])
              .Select(r => CreateChain(r.ToArray()));

            return new Country { CountryCode = id, Name = name, Children = children.ToList() };
        }

        private Store CreateStore(DataRow[] rows)
        {
            var id = rows[0].Field<Guid>("StoreId");
            var number = rows[0].Field<Int32>("StoreNr");
            var name = rows[0].Field<String>("StoreDescr");
            var children = rows.GroupBy(r => r["WorkstationNumber"])
             .Select(r => CreateWorkstation(r.ToArray()));
            

           // return new Store { Id = id, Name = name, Number = number };
            return new Store { Id = id, Name = name, Number = number, Children = children.Where(p =>p !=null).ToList() };
        }

        private Workstation CreateWorkstation(DataRow[] rows)
        {
            var number = rows[0].Field<Int32?>("WorkstationNumber");
            var name = rows[0].Field<String>("WorkstationDescription");
            if(number == null)
            {
                return null;
            }
            return new Workstation { Number = number, Name = name };
        }
    }
}