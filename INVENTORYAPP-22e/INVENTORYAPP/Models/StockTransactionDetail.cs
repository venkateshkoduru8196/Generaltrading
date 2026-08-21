using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INVENTORYAPP.Models;

[Table("stocktransdet", Schema = "dbo")]
public class StockTransactionDetail
{
    // ============================================================
    // Primary Key (Artificial / Internal Tracking)
    // ============================================================

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    // ============================================================
    // Document Information
    // ============================================================

    [Column("slno")]
    public int? SlNo { get; set; }

    [Column("gord")]
    [StringLength(1)]
    public string? Gord { get; set; }

    [Column("doctype")]
    [StringLength(6)]
    public string? DocType { get; set; }

    [Column("DOCNO")]
    public int? DocNo { get; set; }

    [Column("mdoctype")]
    [StringLength(3)]
    public string? MDocType { get; set; }

    [Column("sdoctype")]
    [StringLength(4)]
    public string? SDocType { get; set; }

    [Column("docdate")]
    public DateTime? DocDate { get; set; }

    [Column("nfactor")]
    public int? NFactor { get; set; }

    [Column("nfactor2")]
    public int? NFactor2 { get; set; }

    [Column("slno2")]
    public int? SlNo2 { get; set; }

    // ============================================================
    // Item Information
    // ============================================================

    [Column("stkcode")]
    [StringLength(55)]
    public string? StkCode { get; set; }

    [Column("stksubcode")]
    [StringLength(55)]
    public string? StkSubCode { get; set; }

    [Column("stkname")]
    [StringLength(55)]
    public string? StkName { get; set; }

    [Column("stkrefcode")]
    [StringLength(55)]
    public string? StkRefCode { get; set; }

    [Column("itemtype")]
    [StringLength(10)]
    public string? ItemType { get; set; }

    [Column("mtltype")]
    [StringLength(1)]
    public string? MtlType { get; set; }

    [Column("mtltype2")]
    [StringLength(1)]
    public string? MtlType2 { get; set; }

    [Column("karat")]
    [StringLength(2)]
    public string? Karat { get; set; }

    [Column("uom")]
    [StringLength(3)]
    public string? Uom { get; set; }

    [Column("mainuom")]
    [StringLength(4)]
    public string? MainUom { get; set; }

    [Column("subuom")]
    [StringLength(4)]
    public string? SubUom { get; set; }

    [Column("tranuom")]
    [StringLength(4)]
    public string? TranUom { get; set; }

    [Column("lotno")]
    [StringLength(55)]
    public string? LotNo { get; set; }

    [Column("gbatchno")]
    [StringLength(10)]
    public string? GBatchNo { get; set; }

    [Column("orgbatchno")]
    [StringLength(10)]
    public string? OrgBatchNo { get; set; }

    [Column("certno")]
    [StringLength(6)]
    public string? CertNo { get; set; }

    [Column("mbarcode")]
    [StringLength(55)]
    public string? MBarcode { get; set; }

    [Column("mtlcolor")]
    [StringLength(3)]
    public string? MtlColor { get; set; }

    [Column("diashape")]
    [StringLength(3)]
    public string? DiaShape { get; set; }

    [Column("stnshape")]
    [StringLength(3)]
    public string? StnShape { get; set; }

    // ============================================================
    // Party & Job Information
    // ============================================================

    [Column("accode")]
    [StringLength(6)]
    public string? AccCode { get; set; }

    [Column("partycode")]
    [StringLength(6)]
    public string? PartyCode { get; set; }

    [Column("suppcode")]
    [StringLength(6)]
    public string? SuppCode { get; set; }

    [Column("refcode")]
    [StringLength(6)]
    public string? RefCode { get; set; }

    [Column("jobcode")]
    [StringLength(6)]
    public string? JobCode { get; set; }

    [Column("jobslno")]
    public int? JobSlNo { get; set; }

    [Column("jobno")]
    [StringLength(6)]
    public string? JobNo { get; set; }

    [Column("settingcode")]
    [StringLength(6)]
    public string? SettingCode { get; set; }

    [Column("lcccode")] // Legacy or separate location column
    [StringLength(6)]
    public string? LccCode { get; set; }

    [Column("loccode")] // Brand new field missed earlier
    [StringLength(55)]
    public string? LocCode { get; set; }

    [Column("workercode")]
    [StringLength(6)]
    public string? WorkerCode { get; set; }

    [Column("workername")]
    [StringLength(55)]
    public string? WorkerName { get; set; }

    [Column("nextworker")]
    [StringLength(6)]
    public string? NextWorker { get; set; }

    [Column("processcode")]
    [StringLength(10)]
    public string? ProcessCode { get; set; }

    [Column("processname")]
    [StringLength(55)]
    public string? ProcessName { get; set; }

    [Column("nextprocess")]
    [StringLength(3)]
    public string? NextProcess { get; set; }

    [Column("sman")]
    [StringLength(4)]
    public string? SMan { get; set; }

    // ============================================================
    // Quantity & Weight Information
    // ============================================================

    [Column("pcs")]
    public int? Pcs { get; set; }

    [Column("pieces")]
    public int? Pieces { get; set; }

    [Column("qty")]
    public decimal? Qty { get; set; }

    [Column("grqty")]
    public decimal? GrQty { get; set; }

    [Column("mainuqty")]
    public decimal? MainUQty { get; set; }

    [Column("subuqty")]
    public decimal? SubUQty { get; set; }

    [Column("tranuqty")]
    public decimal? TranUQty { get; set; }

    [Column("transuqty")]
    public decimal? TransUQty { get; set; }

    [Column("netgrqty")]
    public decimal? NetGrQty { get; set; }

    [Column("totalgrqty")]
    public decimal? TotalGrQty { get; set; }

    [Column("totgrqty")]
    public decimal? TotGrQty { get; set; }

    [Column("grweight")]
    public decimal? GrWeight { get; set; }

    [Column("totalwt")]
    public decimal? TotalWt { get; set; }

    [Column("samplewt")]
    public decimal? SampleWt { get; set; }

    [Column("unitweight")]
    public decimal? UnitWeight { get; set; }

    [Column("puqty")]
    public decimal? PuQty { get; set; }

    [Column("newpuqty")]
    public decimal? NewPuQty { get; set; }

    [Column("puloss")]
    public decimal? PuLoss { get; set; }

    [Column("pulossqty")]
    public decimal? PuLossQty { get; set; }

    [Column("lossgrqty")]
    public decimal? LossGrQty { get; set; }

    [Column("plqty")]
    public decimal? PlQty { get; set; }

    [Column("mtlqty")]
    public decimal? MtlQty { get; set; }

    [Column("stnqty")]
    public decimal? StnQty { get; set; }

    [Column("carats")]
    public decimal? Carats { get; set; }

    [Column("stnpieces")]
    public decimal? StnPieces { get; set; }

    [Column("diapieces")]
    public decimal? DiaPieces { get; set; }

    [Column("kbttbcount")]
    public decimal? KbttbCount { get; set; }

    // ============================================================
    // Rate & Cost Information
    // ============================================================

    [Column("price")]
    public decimal? Price { get; set; }

    [Column("unitrate")]
    public decimal? UnitRate { get; set; }

    [Column("totalrate")]
    public decimal? TotalRate { get; set; }

    [Column("cost")]
    public decimal? Cost { get; set; }

    [Column("costprice")]
    public decimal? CostPrice { get; set; }

    [Column("netcost")]
    public decimal? NetCost { get; set; }

    [Column("nettotcost")]
    public decimal? NetTotCost { get; set; }

    [Column("rsprice")]
    public decimal? RsPrice { get; set; }

    [Column("discperc")]
    public decimal? DiscPerc { get; set; }

    [Column("discamt")]
    public decimal? DiscAmt { get; set; }

    [Column("jobperc")]
    public decimal? JobPerc { get; set; }

    [Column("jobamount")]
    public decimal? JobAmount { get; set; }

    [Column("mkgrate")]
    public decimal? MkgRate { get; set; }

    [Column("mkgamt")]
    public decimal? MkgAmt { get; set; }

    [Column("mkgcost")]
    public decimal? MkgCost { get; set; }

    [Column("mtlrate")]
    public decimal? MtlRate { get; set; }

    [Column("mtlamt")]
    public decimal? MtlAmt { get; set; }

    // ============================================================
    // Amount & Currency Information
    // ============================================================

    [Column("amount")]
    public decimal? Amount { get; set; }

    [Column("totalamt")]
    public decimal? TotalAmt { get; set; }

    [Column("totalnetamt")]
    public decimal? TotalNetAmt { get; set; }

    [Column("netamount")]
    public decimal? NetAmount { get; set; }

    [Column("taxableamt")]
    public decimal? TaxableAmt { get; set; }

    [Column("taxamt")]
    public decimal? TaxAmt { get; set; }

    [Column("gtotalamt")]
    public decimal? GTotalAmt { get; set; }

    [Column("lcamount")]
    public decimal? LcAmount { get; set; }

    [Column("fcamount")]
    public decimal? FcAmount { get; set; }

    [Column("amountlc")]
    public decimal? AmountLc { get; set; }

    [Column("mkgamtlc")]
    public decimal? MkgAmtLc { get; set; }

    [Column("mtlamtlc")]
    public decimal? MtlAmtLc { get; set; }

    [Column("totalamtlc")]
    public decimal? TotalAmtLc { get; set; }

    [Column("mkgamtfc")]
    public decimal? MkgAmtFc { get; set; }

    [Column("mtlamtfc")]
    public decimal? MtlAmtFc { get; set; }

    [Column("taxamtfc")]
    public decimal? TaxAmtFc { get; set; }

    [Column("totalamtfc")]
    public decimal? TotalAmtFc { get; set; }

    [Column("plamt")]
    public decimal? PlAmt { get; set; }

    [Column("currcode")]
    [StringLength(3)]
    public string? CurrCode { get; set; }

    [Column("currrate")]
    public decimal? CurrRate { get; set; }

    [Column("fccurr")]
    [StringLength(3)]
    public string? FcCurr { get; set; }

    [Column("currfc")]
    [StringLength(3)]
    public string? CurrFc { get; set; }

    [Column("currlc")]
    [StringLength(3)]
    public string? CurrLc { get; set; }

    [Column("usdrate")]
    public decimal? UsdRate { get; set; }

    [Column("gozrate")]
    public decimal? GozRate { get; set; }

    // ============================================================
    // Purity Information
    // ============================================================

    [Column("purity")]
    public decimal? Purity { get; set; }

    [Column("spurity")]
    public decimal? SPurity { get; set; }

    [Column("apurity")]
    public decimal? APurity { get; set; }

    [Column("tspurity")]
    public decimal? TSPurity { get; set; }

    [Column("directgold")]
    public short? DirectGold { get; set; }

    // ============================================================
    // Tracking & References
    // ============================================================

    [Column("trackno")]
    public decimal? TrackNo { get; set; }

    [Column("trackslno")]
    public decimal? TrackSlNo { get; set; }

    [Column("trackdate")]
    public DateTime? TrackDate { get; set; }

    [Column("uniqueid")]
    public decimal? UniqueId { get; set; }

    [Column("fromto")]
    [StringLength(10)]
    public string? FromTo { get; set; }

    [Column("orderno")]
    public decimal? OrderNo { get; set; }

    [Column("ordernumber")]
    [StringLength(10)]
    public string? OrderNumber { get; set; }

    [Column("reportnumber")]
    [StringLength(10)]
    public string? ReportNumber { get; set; }

    [Column("samplenumber")]
    [StringLength(10)]
    public string? SampleNumber { get; set; }

    [Column("grlossgain")]
    public decimal? GrLossGain { get; set; }

    [Column("pulossgain")]
    public decimal? PuLossGain { get; set; }

    // ============================================================
    // Tax Information
    // ============================================================

    [Column("taxrate")]
    public decimal? TaxRate { get; set; }

    [Column("mkgtaxamt")]
    public decimal? MkgTaxAmt { get; set; }

    // ============================================================
    // Stone Information
    // ============================================================

    [Column("stnrate")]
    public decimal? StnRate { get; set; }

    [Column("stnamt")]
    public decimal? StnAmt { get; set; }

    [Column("stnamtfc")]
    public decimal? StnAmtFc { get; set; }

    // ============================================================
    // Narration
    // ============================================================

    [Column("linenarration")]
    [StringLength(55)]
    public string? LineNarration { get; set; }

    [Column("hnarration")]
    [StringLength(255)]
    public string? HNarration { get; set; }

    [Column("lnarration")]
    [StringLength(5000)]
    public string? LNarration { get; set; }

    // ============================================================
    // Purchase & Document References
    // ============================================================

    [Column("purdoctype")]
    [StringLength(6)]
    public string? PurDocType { get; set; }

    [Column("purdocno")]
    [StringLength(8)]
    public string? PurDocNo { get; set; }

    [Column("purslno")]
    public int? PurSlNo { get; set; }

    [Column("docrefno")]
    [StringLength(55)]
    public string? DocRefNo { get; set; }

    [Column("docrefdate")]
    [StringLength(55)]
    public string? DocRefDate { get; set; }

    [Column("stfdocno")]
    [StringLength(10)]
    public string? StfDocNo { get; set; }

    [Column("issdocno")]
    [StringLength(10)]
    public string? IssDocNo { get; set; }

    // ============================================================
    // Manufacturing & Flags
    // ============================================================

    [Column("mfgdate")]
    public DateTime? MfgDate { get; set; }

    [Column("prodtype")]
    [StringLength(55)]
    public string? ProdType { get; set; }

    [Column("fixunfix")]
    [StringLength(55)]
    public string? FixUnFix { get; set; }

    [Column("partytrn")]
    [StringLength(55)]
    public string? PartyTrn { get; set; }

    [Column("impexptype")]
    [StringLength(55)]
    public string? ImpExpType { get; set; }

    // ============================================================
    // Branch / Debit / Credit
    // ============================================================

    [Column("brncode")]
    [StringLength(4)]
    public string? BrnCode { get; set; }

    [Column("divcode")]
    [StringLength(4)]
    public string? DivCode { get; set; }

    [Column("mdrcr")]
    [StringLength(2)]
    public string? MDrCr { get; set; }

    [Column("drcr")]
    [StringLength(2)]
    public string? DrCr { get; set; }

    // ============================================================
    // Audit / Metadata
    // ============================================================

    [Column("timestamp")]
    public DateTime? Timestamp { get; set; }

    [Column("ts")]
    public DateTime? Ts { get; set; }

    [Column("stimestamp")]
    [StringLength(55)]
    public string? Stimestamp { get; set; }

    [Column("usercode")]
    [StringLength(55)]
    public string? UserCode { get; set; }

    [Column("isdeleted")]
    public short? IsDeleted { get; set; }
}