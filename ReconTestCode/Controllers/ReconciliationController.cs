using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class ReconciliationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReconciliationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetReconciliationReport()
    {
        var reconciliations = await _context.Reconciliations.ToListAsync();
        return Ok(reconciliations);
    }

    [HttpPost]
    public async Task<IActionResult> CalculateReconciliation()
    {
        var arapData = await _context.ARAPJDEs.ToListAsync();
        var cpMappings = await _context.CPMappings.ToListAsync();
        var insuranceData = await _context.Insurances.ToListAsync();

        foreach (var arap in arapData)
        {
            var cpMapping = cpMappings.FirstOrDefault(cp => cp.AbcodeNumber == arap.SupplierCode);
            var insurance = insuranceData.FirstOrDefault(ins => ins.CpName == arap.SupplierName);

            if (cpMapping != null)
            {
                decimal pdRate = cpMapping.PdRate;
                decimal expectedLoss = arap.AmountInCtrm * pdRate;

                bool hasInsurance = insurance != null;
                decimal insuranceRate = hasInsurance ? insurance.InsuranceRate : 0;
                decimal insuranceLimitUsd = hasInsurance ? insurance.LimitUsd : 0;

                decimal netExposure = arap.AmountInCtrm - (arap.AmountInCtrm * insuranceRate);

                var reconciliation = new Reconciliation
                {
                    AcCode = arap.AcCode,
                    Description = arap.Description,
                    SupplierCode = arap.SupplierCode,
                    SupplierName = arap.SupplierName,
                    ContractNo = arap.ContractNo,
                    DueDate = arap.DueDate,
                    AmountInCtrm = arap.AmountInCtrm,
                    AmountInJde = arap.AmountInJde,
                    PdRate = pdRate,
                    ExpectedLoss = expectedLoss,
                    SfAcctTitle = cpMapping.SalesforceCPName,
                    Insurance = hasInsurance,
                    InsuranceRate = insuranceRate,
                    InsuranceLimitUsd = insuranceLimitUsd,
                    NetExposure = netExposure
                };

                _context.Reconciliations.Add(reconciliation);
            }
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
}
