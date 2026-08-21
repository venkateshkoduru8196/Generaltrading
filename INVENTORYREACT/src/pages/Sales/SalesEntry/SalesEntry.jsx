import { useEffect, useState } from "react";

import "./SalesEntry.css";

// import accountService from "../../services/accountService";
// import stockItemService from "../../services/stockItemService";
// import unitService from "../../services/unitService";
// import salesService from "../../services/salesService";

import accountService from "../../../services/accountService";
import stockItemService from "../../../services/stockItemService";
import unitService from "../../../services/unitService";
import salesService from "../../../services/salesService";

import SalesHeader from "../components/SalesHeader/SalesHeader";
import SalesDetailGrid from "../components/SalesDetailGrid/SalesDetailGrid";
// import SalesSummary from "../components/SalesSummary/SalesSummary";
// import SalesButtons from "../components/SalesButtons/SalesButtons";
import SalesFooter from "../components/SalesFooter/SalesFooter";

import EditPasswordModal from "../../../Authentication/components/EditPasswordModal";

import { verifyEditPassword } from "../../../services/authService";

export default function SalesEntry() {

  // ==========================================
  // Header State
  // ==========================================

  const [header, setHeader] = useState({
    docNo: "",

    docDate: new Date().toISOString().split("T")[0],

    partyCode: "",
  });


  // ==========================================
  // Selected Sales Id
  // ==========================================

  const [saleId, setSaleId] = useState(null);

  const [searchDocNo, setSearchDocNo] = useState("");


  // ==========================================
  // Detail State
  // ==========================================

  const [details, setDetails] = useState([
    {
      slNo: 1,

      stockCode: "",

      stockName: "",

      description: "",

      unitCode: "",

      qty: 0,

      rate: 0,

      amount: 0,

      taxableAmount: 0,

      taxRate: 0,

      taxAmount: 0,
    },
  ]);


  // ==========================================
  // Summary State
  // ==========================================

  const [summary, setSummary] = useState({
    totalQty: 0,

    totalAmount: 0,

    totalTax: 0,

    grandTotal: 0,
  });


  // ==========================================
  // Lookup State
  // ==========================================

  const [accounts, setAccounts] = useState([]);

  const [stockItems, setStockItems] = useState([]);

  const [units, setUnits] = useState([]);


  // ==========================================
  // UI State
  // ==========================================

  const [loading, setLoading] = useState(false);

  const [error, setError] = useState("");

  const [showPasswordModal, setShowPasswordModal] = useState(false);

  const [pendingAction, setPendingAction] = useState(null);


  // ==========================================
  // Screen Mode
  // ==========================================

  const [mode, setMode] = useState("idle");

  // idle   -> Initial screen
  // new    -> Creating new invoice
  // saved  -> Saved / View mode
  // edit   -> Editing existing invoice


  // ==========================================
  // Initial Load
  // ==========================================

  useEffect(() => {
    loadMasterData();
  }, []);


  // ==========================================
  // Load Lookup Data
  // ==========================================

  const loadMasterData = async () => {

    try {

      setLoading(true);

      setError("");


      const [
        accountData,
        stockData,
        unitData
      ] = await Promise.all([

        accountService.getLookup(),

        stockItemService.getLookup(),

        unitService.getLookup(),

      ]);


      // ==========================================
      // SALES PARTY FILTER
      //
      // C = Customer
      // S = Supplier
      //
      // G = General
      // B = Bank/Cash
      //
      // Only C and S are displayed in
      // Sales Party dropdown.
      // ==========================================

      const partyAccounts =
        accountData.filter(
          account =>
            account.actype === "C" ||
            account.actype === "S"
        );


      setAccounts(partyAccounts);

      setStockItems(stockData);

      setUnits(unitData);

    }

    catch (err) {

      console.error(
        "Master Data Error :",
        err
      );


      if (err.response) {

        console.log(
          "Status :",
          err.response.status
        );

        console.log(
          "Data :",
          err.response.data
        );

      }


      setError(
        "Unable to load master data."
      );

    }

    finally {

      setLoading(false);

    }

  };


  // ==========================================
  // Calculate Summary
  // ==========================================

  const calculateSummary = (rows) => {

    const summary = rows.reduce(

      (total, row) => {

        const qty =
          Number(row.qty) || 0;

        const amount =
          Number(row.amount) || 0;

        const tax =
          Number(row.taxAmount) || 0;


        total.totalQty += qty;

        total.totalAmount += amount;

        total.totalTax += tax;

        total.grandTotal +=
          amount + tax;


        return total;

      },

      {
        totalQty: 0,

        totalAmount: 0,

        totalTax: 0,

        grandTotal: 0,
      }

    );


    setSummary(summary);

  };


  // ==========================================
  // Detail Change
  // ==========================================

  const handleDetailChange = (updatedRows) => {

    const calculatedRows =
      updatedRows.map((row) => {

        const qty =
          Number(row.qty) || 0;

        const rate =
          Number(row.rate) || 0;

        const taxRate =
          Number(row.taxRate) || 0;


        const amount =
          qty * rate;


        const taxableAmount =
          amount;


        const taxAmount =
          (taxableAmount * taxRate) / 100;


        return {

          ...row,

          qty,

          rate,

          taxRate,

          amount,

          taxableAmount,

          taxAmount

        };

      });


    setDetails(calculatedRows);

    calculateSummary(calculatedRows);

  };


  // ==========================================
  // Add Row
  // ==========================================

  const handleAddRow = () => {

    const updatedRows = [

      ...details,

      {
        slNo: details.length + 1,

        stockCode: "",

        stockName: "",

        description: "",

        unitCode: "",

        qty: 0,

        rate: 0,

        amount: 0,

        taxableAmount: 0,

        taxRate: 0,

        taxAmount: 0,
      },

    ];


    setDetails(updatedRows);

    calculateSummary(updatedRows);

  };


  // ==========================================
  // Delete Row
  // ==========================================

  const handleDeleteRow = (index) => {

    if (details.length === 1)
      return;


    const updatedRows = details

      .filter((_, i) => i !== index)

      .map((row, i) => ({

        ...row,

        slNo: i + 1,

      }));


    setDetails(updatedRows);

    calculateSummary(updatedRows);

  };


  // ==========================================
  // New
  // ==========================================

  const handleNew = () => {

    setHeader({

      docNo: "",

      docDate:
        new Date()
          .toISOString()
          .split("T")[0],

      partyCode: "",

    });


    const defaultRows = [

      {
        slNo: 1,

        stockCode: "",

        stockName: "",

        description: "",

        unitCode: "",

        qty: 0,

        rate: 0,

        amount: 0,

        taxableAmount: 0,

        taxRate: 0,

        taxAmount: 0,
      },

    ];


    setDetails(defaultRows);


    setSummary({

      totalQty: 0,

      totalAmount: 0,

      totalTax: 0,

      grandTotal: 0,

    });


    setError("");

    setSaleId(null);

    setSearchDocNo("");

    setMode("new");

  };


  // ==========================================
  // Save
  // ==========================================

  const handleSave = async () => {

    try {

      // ==========================================
      // Header Validation
      // ==========================================

      if (!header.partyCode) {

        alert(
          "Please select a Party."
        );

        return;

      }


      // ==========================================
      // Detail Validation
      // ==========================================

      if (details.length === 0) {

        alert(
          "Please add at least one item."
        );

        return;

      }


      for (const row of details) {

        if (!row.stockCode) {

          alert(
            "Please select Stock Item."
          );

          return;

        }


        if (!row.unitCode) {

          alert(
            "Please select Unit."
          );

          return;

        }


        if (Number(row.qty) <= 0) {

          alert(
            "Quantity should be greater than zero."
          );

          return;

        }


        if (Number(row.rate) <= 0) {

          alert(
            "Rate should be greater than zero."
          );

          return;

        }

      }


      // ==========================================
      // Backend DTO
      // ==========================================

      const request = {

        docDate:
          header.docDate,

        partyCode:
          header.partyCode,

        details:

          details.map((row) => ({

            slNo:
              row.slNo,

            stockCode:
              row.stockCode,

            description:
              row.description,

            unitCode:
              row.unitCode,

            qty:
              Number(row.qty),

            rate:
              Number(row.rate),

            taxRate:
              Number(row.taxRate),

          })),

      };


      console.log(
        "========== SAVE =========="
      );

      console.log(request);

      console.log(
        "=========================="
      );


      // ==========================================
      // Save API
      // ==========================================

      const response =
        await salesService.create(
          request
        );


      setSaleId(response.id);


      setHeader((prev) => ({

        ...prev,

        docNo:
          response.docNo,

      }));


      alert(
        `Sales Saved Successfully.\nDocument No : ${response.docNo}`
      );


      setMode("saved");

    }

    catch (err) {

      console.error(err);


      alert(

        err?.response?.data?.message ??

        "Unable to save sales."

      );

    }

  };


  // ==========================================
  // Update
  // ==========================================

  const handleUpdate = async () => {

    try {

      // ==========================================
      // Validate Selected Invoice
      // ==========================================

      if (!saleId) {

        alert(
          "Please search an invoice first."
        );

        return;

      }


      // ==========================================
      // Header Validation
      // ==========================================

      if (!header.partyCode) {

        alert(
          "Please select Party."
        );

        return;

      }


      // ==========================================
      // Detail Validation
      // ==========================================

      if (details.length === 0) {

        alert(
          "Please add at least one item."
        );

        return;

      }


      for (const row of details) {

        if (!row.stockCode) {

          alert(
            "Please select Stock."
          );

          return;

        }


        if (!row.unitCode) {

          alert(
            "Please select Unit."
          );

          return;

        }


        if (Number(row.qty) <= 0) {

          alert(
            "Quantity should be greater than zero."
          );

          return;

        }


        if (Number(row.rate) <= 0) {

          alert(
            "Rate should be greater than zero."
          );

          return;

        }

      }


      // ==========================================
      // Backend DTO
      // ==========================================

      const request = {

        docDate:
          header.docDate,

        partyCode:
          header.partyCode,

        details:

          details.map((row) => ({

            slNo:
              row.slNo,

            stockCode:
              row.stockCode,

            description:
              row.description,

            unitCode:
              row.unitCode,

            qty:
              Number(row.qty),

            rate:
              Number(row.rate),

            taxRate:
              Number(row.taxRate),

          })),

      };


      console.log(
        "========== UPDATE =========="
      );

      console.log(
        "Sale Id :",
        saleId
      );

      console.log(
        "Request :",
        request
      );

      console.log(
        "============================"
      );


      await salesService.update(
        saleId,
        request
      );


      alert(
        "Sales Updated Successfully."
      );


      setMode("saved");

    }

    catch (err) {

      console.error(err);


      alert(

        err?.response?.data?.message ??

        "Unable to update sales."

      );

    }

  };


  // ==========================================
  // Delete
  // ==========================================

  const handleDelete = async () => {

    try {

      if (!saleId) {

        alert(
          "Search an invoice first."
        );

        return;

      }


      await salesService.delete(
        saleId
      );


      alert(
        "Sales Deleted Successfully."
      );


      handleNew();

    }

    catch (err) {

      console.error(err);


      alert(
        "Unable to delete sales."
      );

    }

  };


  // ==========================================
  // Open Password Dialog
  // ==========================================

  const openPasswordDialog = (
    action
  ) => {

    setPendingAction(action);

    setShowPasswordModal(true);

  };


  // ==========================================
  // Close Password Dialog
  // ==========================================

  const closePasswordDialog = () => {

    setPendingAction(null);

    setShowPasswordModal(false);

  };


  // ==========================================
  // Verify Password
  // ==========================================

  const handleVerifyPassword = async (
    password
  ) => {

    try {

      await verifyEditPassword(
        password
      );


      setShowPasswordModal(false);


      if (pendingAction === "edit") {

        setMode("edit");

      }

      else if (
        pendingAction === "delete"
      ) {

        await handleDelete();

      }

    }

    catch (err) {

      console.error(err);


      alert(

        err?.response?.data?.message ??

        "Invalid Edit Password."

      );

    }

  };


  // ==========================================
  // Search
  // ==========================================

  const handleSearch = async () => {

    try {

      if (!searchDocNo.trim()) {

        alert(
          "Please enter Invoice Number."
        );

        return;

      }


      setLoading(true);


      const sale =
        await salesService.getByDocNo(
          searchDocNo
        );


      if (!sale) {

        alert(
          "Invoice not found."
        );

        return;

      }


      setSaleId(sale.id);


      setHeader({

        docNo:
          sale.docNo,

        docDate:
          sale.docDate.substring(
            0,
            10
          ),

        partyCode:
          sale.partyCode,

      });


      setSearchDocNo(
        sale.docNo
      );


      const rows =
        sale.details.map(
          (detail) => ({

            slNo:
              detail.slNo,

            stockCode:
              detail.stockCode,

            stockName:
              detail.stockName,

            description:
              detail.description,

            unitCode:
              detail.unitCode,

            qty:
              detail.qty,

            rate:
              detail.rate,

            amount:
              detail.amount,

            taxableAmount:
              detail.taxableAmount,

            taxRate:
              detail.taxRate,

            taxAmount:
              detail.taxAmount,

          })
        );


      setDetails(rows);

      calculateSummary(rows);

      setMode("saved");


      alert(
        "Invoice Loaded Successfully."
      );

    }

    catch (err) {

      console.error(err);


      alert(

        err?.response?.data?.message ??

        "Invoice not found."

      );

    }

    finally {

      setLoading(false);

    }

  };


  // ==========================================
  // Print
  // ==========================================

  const handlePrint = () => {

    console.log("Print");

  };


  // ==========================================
  // Download PDF
  // ==========================================

  const handleDownloadPdf = async () => {

    try {

      if (!saleId) {

        alert(
          "Please search or save an invoice first."
        );

        return;

      }


      setLoading(true);


      const response =
        await salesService.downloadPdf(
          saleId
        );


      const blob =
        new Blob(

          [response.data],

          {
            type:
              "application/pdf",
          }

        );


      const url =
        window.URL.createObjectURL(
          blob
        );


      const link =
        document.createElement(
          "a"
        );


      link.href = url;


      link.download =
        `SalesInvoice_${saleId}.pdf`;


      document.body.appendChild(
        link
      );


      link.click();

      link.remove();


      window.URL.revokeObjectURL(
        url
      );

    }

    catch (err) {

      console.error(
        "PDF Download Error:",
        err
      );


      alert(

        err?.response?.data?.message ??

        "Unable to download PDF."

      );

    }

    finally {

      setLoading(false);

    }

  };


  // ==========================================
  // Download Word
  // ==========================================

  const handleDownloadWord = async () => {

    try {

      if (!saleId) {

        alert(
          "Please search or save an invoice first."
        );

        return;

      }


      setLoading(true);


      const response =
        await salesService.downloadWord(
          saleId
        );


      const blob =
        new Blob(

          [response.data],

          {
            type:
              "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
          }

        );


      const url =
        window.URL.createObjectURL(
          blob
        );


      const link =
        document.createElement(
          "a"
        );


      link.href = url;


      link.download =
        `SalesInvoice_${saleId}.docx`;


      document.body.appendChild(
        link
      );


      link.click();

      link.remove();


      window.URL.revokeObjectURL(
        url
      );

    }

    catch (err) {

      console.error(
        "Word Download Error:",
        err
      );


      alert(

        err?.response?.data?.message ??

        "Unable to download Word document."

      );

    }

    finally {

      setLoading(false);

    }

  };


  // ==========================================
  // Download Excel
  // ==========================================

  const handleDownloadExcel = async () => {

    try {

      if (!saleId) {

        alert(
          "Please search or save an invoice first."
        );

        return;

      }


      setLoading(true);


      const response =
        await salesService.downloadExcel(
          saleId
        );


      const blob =
        new Blob(

          [response.data],

          {
            type:
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          }

        );


      const url =
        window.URL.createObjectURL(
          blob
        );


      const link =
        document.createElement(
          "a"
        );


      link.href = url;


      link.download =
        `SalesInvoice_${saleId}.xlsx`;


      document.body.appendChild(
        link
      );


      link.click();

      link.remove();


      window.URL.revokeObjectURL(
        url
      );

    }

    catch (err) {

      console.error(
        "Excel Download Error:",
        err
      );


      alert(

        err?.response?.data?.message ??

        "Unable to download Excel file."

      );

    }

    finally {

      setLoading(false);

    }

  };


  // ==========================================
  // Read Only
  // ==========================================

  const isReadOnly =
    mode === "idle" ||
    mode === "saved";


  // ==========================================
  // UI
  // ==========================================

  return (

    <div className="sales-entry">


      {error && (

        <div className="error-message">

          {error}

        </div>

      )}


      {/*==========================================*
       * PAGE TITLE
       *==========================================*/}

      <div className="sales-page-title">

        <div className="sales-page-title-icon">

          🧾

        </div>

        <h1>

          Sales Invoice

        </h1>

      </div>


      <SalesHeader

        header={header}

        setHeader={setHeader}

        accounts={accounts}

        readOnly={isReadOnly}

      />


      <SalesDetailGrid

        details={details}

        stockItems={stockItems}

        units={units}

        summary={summary}

        onChange={handleDetailChange}

        onAdd={handleAddRow}

        onDelete={handleDeleteRow}

        readOnly={isReadOnly}

      />


      <SalesFooter

        searchDocNo={searchDocNo}

        setSearchDocNo={setSearchDocNo}

        onSearch={handleSearch}

        mode={mode}

        onNew={handleNew}

        onSave={handleSave}

        onEdit={() =>
          openPasswordDialog("edit")
        }

        onUpdate={handleUpdate}

        onDelete={() =>
          openPasswordDialog("delete")
        }

        onPrint={handlePrint}

        onDownloadPdf={
          handleDownloadPdf
        }

        onDownloadWord={
          handleDownloadWord
        }

        onDownloadExcel={
          handleDownloadExcel
        }

      />


      <EditPasswordModal

        show={showPasswordModal}

        title="Authorization Required"

        onVerify={
          handleVerifyPassword
        }

        onClose={
          closePasswordDialog
        }

      />

    </div>

  );

}