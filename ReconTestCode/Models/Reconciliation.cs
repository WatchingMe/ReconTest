public class Reconciliation
{
    public int Id { get; set; }
    public string AcCode { get; set; }
    public string Description { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; }
    public string ContractNo { get; set; }
    public DateTime DueDate { get; set; }
    public decimal AmountInCtrm { get; set; }
    public decimal AmountInJde { get; set; }
    public decimal PdRate { get; set; }
    public decimal ExpectedLoss { get; set; }
    public string SfAcctTitle { get; set; }
    public bool Insurance { get; set; }
    public decimal InsuranceRate { get; set; }
    public decimal InsuranceLimitUsd { get; set; }
    public decimal NetExposure { get; set; }
}
