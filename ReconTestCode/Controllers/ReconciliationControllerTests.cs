using CommodityTradingAPI.Controllers;
using CommodityTradingAPI.Data;
using CommodityTradingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class ReconciliationControllerTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public ReconciliationControllerTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task GetReconciliationReport_ReturnsOkResult()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            context.Reconciliations.Add(new Reconciliation { Id = 1, SupplierName = "Test Supplier" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ReconciliationController(context);

            // Act
            var result = await controller.GetReconciliationReport();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reconciliations = Assert.IsType<List<Reconciliation>>(okResult.Value);
            Assert.Single(reconciliations);
        }
    }

    [Fact]
    public async Task CalculateReconciliation_ReturnsOkResult()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            context.ARAPJDEs.Add(new ARAPJDE
            {
                AcCode = "001",
                Description = "Test",
                SupplierCode = 123,
                SupplierName = "Test Supplier",
                ContractNo = "ABC123",
                DueDate = System.DateTime.Now,
                AmountInCtrm = 1000,
                AmountInJde = 1000
            });

            context.CPMappings.Add(new CPMapping
            {
                AbcodeNumber = 123,
                SalesforceCPName = "Test SF",
                JdeCPName = "Test JDE",
                PdRate = 0.05m
            });

            context.Insurances.Add(new Insurance
            {
                Bizdate = System.DateTime.Now,
                CpMasterId = "123",
                CpName = "Test Supplier",
                LimitUsd = 500,
                PdRate = 0.05m,
                InsuranceRate = 0.1m
            });

            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ReconciliationController(context);

            // Act
            var result = await controller.CalculateReconciliation();

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            var reconciliations = context.Reconciliations.ToList();
            Assert.Single(reconciliations);
            Assert.Equal("Test Supplier", reconciliations.First().SupplierName);
        }
    }
}
