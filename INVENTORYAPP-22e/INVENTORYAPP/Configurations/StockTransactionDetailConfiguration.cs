using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVENTORYAPP.Configurations;

public class StockTransactionDetailConfiguration : IEntityTypeConfiguration<StockTransactionDetail>
{
    public void Configure(EntityTypeBuilder<StockTransactionDetail> builder)
    {
        // ==========================================
        // Table Mapping
        // ==========================================

        builder.ToTable("stocktransdet", "dbo");

        // ==========================================
        // Primary Key Context
        // ==========================================

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        // ==========================================
        // Document Information
        // ==========================================

        builder.Property(x => x.SlNo)
               .HasColumnName("slno");

        builder.Property(x => x.Gord)
               .HasColumnName("gord")
               .HasMaxLength(1);

        builder.Property(x => x.DocType)
               .HasColumnName("doctype")
               .HasMaxLength(6);

        builder.Property(x => x.DocNo)
               .HasColumnName("DOCNO");

        builder.Property(x => x.MDocType)
               .HasColumnName("mdoctype")
               .HasMaxLength(3);

        builder.Property(x => x.SDocType)
               .HasColumnName("sdoctype")
               .HasMaxLength(4);

        builder.Property(x => x.DocDate)
               .HasColumnName("docdate");

        builder.Property(x => x.NFactor)
               .HasColumnName("nfactor");

        builder.Property(x => x.NFactor2)
               .HasColumnName("nfactor2");

        builder.Property(x => x.SlNo2)
               .HasColumnName("slno2");

        // ==========================================
        // Item Information
        // ==========================================

        builder.Property(x => x.StkCode)
               .HasColumnName("stkcode")
               .HasMaxLength(55);

        builder.Property(x => x.StkSubCode)
               .HasColumnName("stksubcode")
               .HasMaxLength(55);

        builder.Property(x => x.StkName)
               .HasColumnName("stkname")
               .HasMaxLength(55);

        builder.Property(x => x.StkRefCode)
               .HasColumnName("stkrefcode")
               .HasMaxLength(55);

        builder.Property(x => x.ItemType)
               .HasColumnName("itemtype")
               .HasMaxLength(10);

        builder.Property(x => x.MtlType)
               .HasColumnName("mtltype")
               .HasMaxLength(1);

        builder.Property(x => x.MtlType2)
               .HasColumnName("mtltype2")
               .HasMaxLength(1);

        builder.Property(x => x.Karat)
               .HasColumnName("karat")
               .HasMaxLength(2);

        builder.Property(x => x.Uom)
               .HasColumnName("uom")
               .HasMaxLength(3);

        builder.Property(x => x.MainUom)
               .HasColumnName("mainuom")
               .HasMaxLength(4);

        builder.Property(x => x.SubUom)
               .HasColumnName("subuom")
               .HasMaxLength(4);

        builder.Property(x => x.TranUom)
               .HasColumnName("tranuom")
               .HasMaxLength(4);

        builder.Property(x => x.LotNo)
               .HasColumnName("lotno")
               .HasMaxLength(55);

        builder.Property(x => x.GBatchNo)
               .HasColumnName("gbatchno")
               .HasMaxLength(10);

        builder.Property(x => x.OrgBatchNo)
               .HasColumnName("orgbatchno")
               .HasMaxLength(10);

        builder.Property(x => x.CertNo)
               .HasColumnName("certno")
               .HasMaxLength(6);

        builder.Property(x => x.MBarcode)
               .HasColumnName("mbarcode")
               .HasMaxLength(55);

        builder.Property(x => x.MtlColor)
               .HasColumnName("mtlcolor")
               .HasMaxLength(3);

        builder.Property(x => x.DiaShape)
               .HasColumnName("diashape")
               .HasMaxLength(3);

        builder.Property(x => x.StnShape)
               .HasColumnName("stnshape")
               .HasMaxLength(3);

        // ==========================================
        // Party & Manufacturing Information
        // ==========================================

        builder.Property(x => x.AccCode)
               .HasColumnName("accode")
               .HasMaxLength(6);

        builder.Property(x => x.PartyCode)
               .HasColumnName("partycode")
               .HasMaxLength(6);

        builder.Property(x => x.SuppCode)
               .HasColumnName("suppcode")
               .HasMaxLength(6);

        builder.Property(x => x.RefCode)
               .HasColumnName("refcode")
               .HasMaxLength(6);

        builder.Property(x => x.JobCode)
               .HasColumnName("jobcode")
               .HasMaxLength(6);

        builder.Property(x => x.JobSlNo)
               .HasColumnName("jobslno");

        builder.Property(x => x.JobNo)
               .HasColumnName("jobno")
               .HasMaxLength(6);

        builder.Property(x => x.SettingCode)
               .HasColumnName("settingcode")
               .HasMaxLength(6);

        builder.Property(x => x.LccCode)
               .HasColumnName("lcccode")
               .HasMaxLength(6);

        builder.Property(x => x.LocCode)
               .HasColumnName("loccode")
               .HasMaxLength(55);

        builder.Property(x => x.WorkerCode)
               .HasColumnName("workercode")
               .HasMaxLength(6);

        builder.Property(x => x.WorkerName)
               .HasColumnName("workername")
               .HasMaxLength(55);

        builder.Property(x => x.NextWorker)
               .HasColumnName("nextworker")
               .HasMaxLength(6);

        builder.Property(x => x.ProcessCode)
               .HasColumnName("processcode")
               .HasMaxLength(10);

        builder.Property(x => x.ProcessName)
               .HasColumnName("processname")
               .HasMaxLength(55);

        builder.Property(x => x.NextProcess)
               .HasColumnName("nextprocess")
               .HasMaxLength(3);

        builder.Property(x => x.SMan)
               .HasColumnName("sman")
               .HasMaxLength(4);

        // ==========================================
        // Quantity Information
        // ==========================================

        builder.Property(x => x.Pcs)
               .HasColumnName("pcs");

        builder.Property(x => x.Pieces)
               .HasColumnName("pieces");

        builder.Property(x => x.Qty)
               .HasColumnName("qty")
               .HasPrecision(14, 2);

        builder.Property(x => x.GrQty)
               .HasColumnName("grqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.MainUQty)
               .HasColumnName("mainuqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.SubUQty)
               .HasColumnName("subuqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.TranUQty)
               .HasColumnName("tranuqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.TransUQty)
               .HasColumnName("transuqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.NetGrQty)
               .HasColumnName("netgrqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.TotalGrQty)
               .HasColumnName("totalgrqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.TotGrQty)
               .HasColumnName("totgrqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.GrWeight)
               .HasColumnName("grweight")
               .HasPrecision(14, 2);

        builder.Property(x => x.TotalWt)
               .HasColumnName("totalwt")
               .HasPrecision(14, 2);

        builder.Property(x => x.SampleWt)
               .HasColumnName("samplewt")
               .HasPrecision(14, 2);

        builder.Property(x => x.UnitWeight)
               .HasColumnName("unitweight")
               .HasPrecision(14, 2);

        builder.Property(x => x.PuQty)
               .HasColumnName("puqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.NewPuQty)
               .HasColumnName("newpuqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.PuLoss)
               .HasColumnName("puloss")
               .HasPrecision(14, 2);

        builder.Property(x => x.PuLossQty)
               .HasColumnName("pulossqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.LossGrQty)
               .HasColumnName("lossgrqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.PlQty)
               .HasColumnName("plqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.MtlQty)
               .HasColumnName("mtlqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.StnQty)
               .HasColumnName("stnqty")
               .HasPrecision(14, 2);

        builder.Property(x => x.Carats)
               .HasColumnName("carats")
               .HasPrecision(14, 2);

        builder.Property(x => x.StnPieces)
               .HasColumnName("stnpieces")
               .HasPrecision(14, 2);

        builder.Property(x => x.DiaPieces)
               .HasColumnName("diapieces")
               .HasPrecision(14, 2);

        builder.Property(x => x.KbttbCount)
               .HasColumnName("kbttbcount")
               .HasPrecision(14, 2);

        // ==========================================
        // Rate Information
        // ==========================================

        builder.Property(x => x.Price)
               .HasColumnName("price")
               .HasPrecision(14, 2);

        builder.Property(x => x.UnitRate)
               .HasColumnName("unitrate")
               .HasPrecision(14, 2);

        builder.Property(x => x.TotalRate)
               .HasColumnName("totalrate")
               .HasPrecision(16, 4);

        builder.Property(x => x.Cost)
               .HasColumnName("cost")
               .HasPrecision(14, 2);

        builder.Property(x => x.CostPrice)
               .HasColumnName("costprice")
               .HasPrecision(14, 2);

        builder.Property(x => x.NetCost)
               .HasColumnName("netcost")
               .HasPrecision(16, 4);

        builder.Property(x => x.NetTotCost)
               .HasColumnName("nettotcost")
               .HasPrecision(16, 4);

        builder.Property(x => x.RsPrice)
               .HasColumnName("rsprice")
               .HasPrecision(14, 2);

        builder.Property(x => x.DiscPerc)
               .HasColumnName("discperc")
               .HasPrecision(16, 4);

        builder.Property(x => x.DiscAmt)
               .HasColumnName("discamt")
               .HasPrecision(16, 4);

        builder.Property(x => x.JobPerc)
               .HasColumnName("jobperc")
               .HasPrecision(14, 2);

        builder.Property(x => x.JobAmount)
               .HasColumnName("jobamount")
               .HasPrecision(14, 2);

        builder.Property(x => x.MkgRate)
               .HasColumnName("mkgrate")
               .HasPrecision(14, 2);

        builder.Property(x => x.MkgAmt)
               .HasColumnName("mkgamt")
               .HasPrecision(14, 2);

        builder.Property(x => x.MkgCost)
               .HasColumnName("mkgcost")
               .HasPrecision(14, 2);

        builder.Property(x => x.MtlRate)
               .HasColumnName("mtlrate")
               .HasPrecision(14, 2);

        builder.Property(x => x.MtlAmt)
               .HasColumnName("mtlamt")
               .HasPrecision(14, 2);

        builder.Property(x => x.StnRate)
               .HasColumnName("stnrate")
               .HasPrecision(14, 2);

        builder.Property(x => x.StnAmt)
               .HasColumnName("stnamt")
               .HasPrecision(14, 2);

        // ==========================================
        // Amount Information
        // ==========================================

        builder.Property(x => x.Amount)
               .HasColumnName("amount")
               .HasPrecision(14, 2);

        builder.Property(x => x.TotalAmt)
               .HasColumnName("totalamt")
               .HasPrecision(16, 4);

        builder.Property(x => x.TotalNetAmt)
               .HasColumnName("totalnetamt")
               .HasPrecision(16, 4);

        builder.Property(x => x.NetAmount)
               .HasColumnName("netamount")
               .HasPrecision(14, 2);

        builder.Property(x => x.TaxableAmt)
               .HasColumnName("taxableamt")
               .HasPrecision(14, 2);

        builder.Property(x => x.TaxAmt)
               .HasColumnName("taxamt")
               .HasPrecision(14, 2);

        builder.Property(x => x.GTotalAmt)
               .HasColumnName("gtotalamt")
               .HasPrecision(14, 2);

        builder.Property(x => x.LcAmount)
               .HasColumnName("lcamount")
               .HasPrecision(14, 2);

        builder.Property(x => x.FcAmount)
               .HasColumnName("fcamount")
               .HasPrecision(14, 2);

        builder.Property(x => x.AmountLc)
               .HasColumnName("amountlc")
               .HasPrecision(14, 2);

        builder.Property(x => x.MkgAmtLc)
               .HasColumnName("mkgamtlc")
               .HasPrecision(14, 2);

        builder.Property(x => x.MtlAmtLc)
               .HasColumnName("mtlamtlc")
               .HasPrecision(14, 2);

        builder.Property(x => x.TotalAmtLc)
               .HasColumnName("totalamtlc")
               .HasPrecision(14, 2);

        builder.Property(x => x.MkgAmtFc)
               .HasColumnName("mkgamtfc")
               .HasPrecision(14, 2);

        builder.Property(x => x.MtlAmtFc)
               .HasColumnName("mtlamtfc")
               .HasPrecision(14, 2);

        builder.Property(x => x.TaxAmtFc)
               .HasColumnName("taxamtfc")
               .HasPrecision(14, 2);

        builder.Property(x => x.TotalAmtFc)
               .HasColumnName("totalamtfc")
               .HasPrecision(14, 2);

        builder.Property(x => x.StnAmtFc)
               .HasColumnName("stnamtfc")
               .HasPrecision(14, 2);

        builder.Property(x => x.PlAmt)
               .HasColumnName("plamt")
               .HasPrecision(14, 2);

        // ==========================================
        // Purity Information
        // ==========================================

        builder.Property(x => x.Purity)
               .HasColumnName("purity")
               .HasPrecision(14, 2);

        builder.Property(x => x.SPurity)
               .HasColumnName("spurity")
               .HasPrecision(14, 2);

        builder.Property(x => x.APurity)
               .HasColumnName("apurity")
               .HasPrecision(14, 2);

        builder.Property(x => x.TSPurity)
               .HasColumnName("tspurity")
               .HasPrecision(14, 2);

        builder.Property(x => x.DirectGold)
               .HasColumnName("directgold");

        // ==========================================
        // Currency Information
        // ==========================================

        builder.Property(x => x.CurrCode)
               .HasColumnName("currcode")
               .HasMaxLength(3);

        builder.Property(x => x.CurrRate)
               .HasColumnName("currrate")
               .HasPrecision(14, 2);

        builder.Property(x => x.FcCurr)
               .HasColumnName("fccurr")
               .HasMaxLength(3);

        builder.Property(x => x.CurrFc)
               .HasColumnName("currfc")
               .HasMaxLength(3);

        builder.Property(x => x.CurrLc)
               .HasColumnName("currlc")
               .HasMaxLength(3);

        builder.Property(x => x.UsdRate)
               .HasColumnName("usdrate")
               .HasPrecision(14, 2);

        builder.Property(x => x.GozRate)
               .HasColumnName("gozrate")
               .HasPrecision(14, 2);

        // ==========================================
        // Tracking & Reference Information
        // ==========================================

        builder.Property(x => x.TrackNo)
               .HasColumnName("trackno")
               .HasPrecision(14, 2);

        builder.Property(x => x.TrackSlNo)
               .HasColumnName("trackslno")
               .HasPrecision(14, 2);

        builder.Property(x => x.TrackDate)
               .HasColumnName("trackdate");

        builder.Property(x => x.UniqueId)
               .HasColumnName("uniqueid")
               .HasPrecision(14, 2);

        builder.Property(x => x.OrderNo)
               .HasColumnName("orderno")
               .HasPrecision(14, 2);

        builder.Property(x => x.OrderNumber)
               .HasColumnName("ordernumber")
               .HasMaxLength(10);

        builder.Property(x => x.ReportNumber)
               .HasColumnName("reportnumber")
               .HasMaxLength(10);

        builder.Property(x => x.SampleNumber)
               .HasColumnName("samplenumber")
               .HasMaxLength(10);

        builder.Property(x => x.FromTo)
               .HasColumnName("fromto")
               .HasMaxLength(10);

        builder.Property(x => x.GrLossGain)
               .HasColumnName("grlossgain")
               .HasPrecision(14, 2);

        builder.Property(x => x.PuLossGain)
               .HasColumnName("pulossgain")
               .HasPrecision(14, 2);

        // ==========================================
        // Tax Information
        // ==========================================

        builder.Property(x => x.TaxRate)
               .HasColumnName("taxrate")
               .HasPrecision(14, 2);

        builder.Property(x => x.MkgTaxAmt)
               .HasColumnName("mkgtaxamt")
               .HasPrecision(14, 2);

        // ==========================================
        // Narration Information
        // ==========================================

        builder.Property(x => x.LineNarration)
               .HasColumnName("linenarration")
               .HasMaxLength(55);

        builder.Property(x => x.HNarration)
               .HasColumnName("hnarration")
               .HasMaxLength(255);

        builder.Property(x => x.LNarration)
               .HasColumnName("lnarration")
               .HasMaxLength(5000);

        // ==========================================
        // Purchase Reference
        // ==========================================

        builder.Property(x => x.PurDocType)
               .HasColumnName("purdoctype")
               .HasMaxLength(6);

        builder.Property(x => x.PurDocNo)
               .HasColumnName("purdocno")
               .HasMaxLength(8);

        builder.Property(x => x.PurSlNo)
               .HasColumnName("purslno");

        // ==========================================
        // Document References
        // ==========================================

        builder.Property(x => x.DocRefNo)
               .HasColumnName("docrefno")
               .HasMaxLength(55);

        builder.Property(x => x.DocRefDate)
               .HasColumnName("docrefdate")
               .HasMaxLength(55);

        builder.Property(x => x.StfDocNo)
               .HasColumnName("stfdocno")
               .HasMaxLength(10);

        builder.Property(x => x.IssDocNo)
               .HasColumnName("issdocno")
               .HasMaxLength(10);

        // ==========================================
        // Manufacturing Flags
        // ==========================================

        builder.Property(x => x.ProdType)
               .HasColumnName("prodtype")
               .HasMaxLength(55);

        builder.Property(x => x.FixUnFix)
               .HasColumnName("fixunfix")
               .HasMaxLength(55);

        builder.Property(x => x.PartyTrn)
               .HasColumnName("partytrn")
               .HasMaxLength(55);

        builder.Property(x => x.ImpExpType)
               .HasColumnName("impexptype")
               .HasMaxLength(55);

        builder.Property(x => x.MfgDate)
               .HasColumnName("mfgdate");

        // ==========================================
        // Branch & Audit Data
        // ==========================================

        builder.Property(x => x.BrnCode)
               .HasColumnName("brncode")
               .HasMaxLength(4);

        builder.Property(x => x.DivCode)
               .HasColumnName("divcode")
               .HasMaxLength(4);

        builder.Property(x => x.MDrCr)
               .HasColumnName("mdrcr")
               .HasMaxLength(2);

        builder.Property(x => x.DrCr)
               .HasColumnName("drcr")
               .HasMaxLength(2);

        builder.Property(x => x.Timestamp)
               .HasColumnName("timestamp");

        builder.Property(x => x.Ts)
               .HasColumnName("ts");

        builder.Property(x => x.Stimestamp)
               .HasColumnName("stimestamp")
               .HasMaxLength(55);

        builder.Property(x => x.UserCode)
               .HasColumnName("usercode")
               .HasMaxLength(55);

        builder.Property(x => x.IsDeleted)
               .HasColumnName("isdeleted");
    }
}