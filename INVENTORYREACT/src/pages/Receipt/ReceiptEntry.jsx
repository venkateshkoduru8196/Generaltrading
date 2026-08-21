import { useEffect, useState } from "react";
import "./ReceiptEntry.css";

import EditPasswordModal from "../../authentication/components/EditPasswordModal";
import {
  getParties,
  getAccounts,
  saveReceipt,
  getNextReceiptNumber,
  searchReceipts,
  getReceiptByDocNo,
  deleteReceipt,
} from "../../services/receiptService";
import { verifyEditPassword } from "../../services/authService";

import ReceiptHeader from "./components/ReceiptHeader/ReceiptHeader";
import ReceiptDetailGrid from "./components/ReceiptDetailGrid/ReceiptDetailGrid";
import ReceiptFooter from "./components/ReceiptFooter/ReceiptFooter";

const createEmptyDetail = () => ({
  slNo: 1,
  accountId: "",
  amount: "",
});

export default function ReceiptEntry() {
  const [header, setHeader] = useState({
    docNo: "",
    docDate: new Date().toISOString().split("T")[0],
    partyId: "",
  });

  const [details, setDetails] = useState([createEmptyDetail()]);
  const [parties, setParties] = useState([]);
  const [accounts, setAccounts] = useState([]);

  const [summary, setSummary] = useState({ totalAmount: 0 });

  const [searchDocNo, setSearchDocNo] = useState("");
  const [mode, setMode] = useState("idle");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const [showPasswordModal, setShowPasswordModal] = useState(false);
  const [pendingAction, setPendingAction] = useState(null);
  const [verifyingPassword, setVerifyingPassword] = useState(false);

  const isReadOnly = mode === "idle" || mode === "saved";

  useEffect(() => {
    loadMasterData();
  }, []);

  const calculateSummary = (rows) => {
    const totalAmount = rows.reduce(
      (total, row) => total + (Number(row.amount) || 0),
      0
    );

    setSummary({ totalAmount });
  };

  const loadMasterData = async () => {
    try {
      setLoading(true);
      setError("");

      const [partyData, accountData] = await Promise.all([
        getParties(),
        getAccounts(),
      ]);

      setParties(partyData ?? []);
      setAccounts(accountData ?? []);
    } catch (err) {
      console.error("Receipt master data error:", err);
      setError("Unable to load Party and Account data.");
    } finally {
      setLoading(false);
    }
  };

  const resetForm = async () => {
    setHeader({
      docNo: "",
      docDate: new Date().toISOString().split("T")[0],
      partyId: "",
    });
    setDetails([createEmptyDetail()]);
    setSummary({ totalAmount: 0 });
    setSearchDocNo("");
    setError("");

    try {
      const nextNo = await getNextReceiptNumber();
      setHeader((prev) => ({ ...prev, docNo: nextNo }));
    } catch (err) {
      console.error("Next receipt number error:", err);
      setError("Unable to generate Receipt Number.");
    }
  };

  const handleNew = async () => {
    setMode("new");
    await resetForm();
  };

  const handleDetailChange = (updatedRows) => {
    setDetails(updatedRows);
    calculateSummary(updatedRows);
  };

  const handleAddRow = () => {
    const updatedRows = [
      ...details,
      {
        ...createEmptyDetail(),
        slNo: details.length + 1,
      },
    ];

    setDetails(updatedRows);
    calculateSummary(updatedRows);
  };

  const handleDeleteRow = (index) => {
    if (details.length === 1) {
      alert("At least one row is required.");
      return;
    }

    const updatedRows = details
      .filter((_, rowIndex) => rowIndex !== index)
      .map((row, rowIndex) => ({
        ...row,
        slNo: rowIndex + 1,
      }));

    setDetails(updatedRows);
    calculateSummary(updatedRows);
  };

  const validateReceipt = () => {
    if (!header.partyId) {
      alert("Please select Party.");
      return false;
    }

    if (details.length === 0) {
      alert("Please add at least one detail row.");
      return false;
    }

    for (const row of details) {
      if (!row.accountId) {
        alert("Please select Account.");
        return false;
      }

      if (Number(row.amount) <= 0) {
        alert("Amount should be greater than zero.");
        return false;
      }
    }

    return true;
  };

  const buildRequest = () => ({
    docNo: Number(header.docNo) || 0,
    docDate: header.docDate,
    partyId: Number(header.partyId),
    details: details.map((row) => ({
      accountId: Number(row.accountId),
      amount: Number(row.amount),
    })),
  });

  const handleSave = async () => {
    if (!validateReceipt()) return;

    try {
      setLoading(true);
      setError("");

      const savedDocNo = await saveReceipt(buildRequest());
      const docNo = savedDocNo?.docNo ?? savedDocNo;

      setHeader((prev) => ({
        ...prev,
        docNo: docNo ?? prev.docNo,
      }));

      alert(`Receipt Saved Successfully.\nDocument No : ${docNo}`);
      setMode("saved");
    } catch (err) {
      console.error("Save Receipt Error:", err);
      alert(err?.response?.data?.message ?? "Unable to save Receipt.");
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = async () => {
    if (!searchDocNo.trim()) {
      alert("Please enter Receipt Number.");
      return;
    }

    try {
      setLoading(true);
      setError("");

      const results = await searchReceipts(searchDocNo.trim());
      const first = results?.[0];

      if (!first) {
        alert("Receipt not found.");
        return;
      }

      await loadReceipt(first.docNo);
    } catch (err) {
      console.error("Search Receipt Error:", err);
      alert(err?.response?.data?.message ?? "Unable to search Receipt.");
    } finally {
      setLoading(false);
    }
  };

  const loadReceipt = async (docNo) => {
    const receipt = await getReceiptByDocNo(docNo);

    if (!receipt) {
      alert("Receipt not found.");
      return;
    }

    const rows = (receipt.details ?? []).map((item, index) => ({
      slNo: index + 1,
      accountId: item.accountId,
      amount: item.amount,
    }));

    const finalRows = rows.length > 0 ? rows : [createEmptyDetail()];

    setHeader({
      docNo: receipt.docNo,
      docDate: receipt.docDate?.substring(0, 10) ?? "",
      partyId: receipt.partyId,
    });

    setDetails(finalRows);
    calculateSummary(finalRows);
    setSearchDocNo(String(receipt.docNo));
    setMode("saved");
  };

  const openPasswordDialog = (action) => {
    setPendingAction(action);
    setShowPasswordModal(true);
  };

  const closePasswordDialog = () => {
    if (verifyingPassword) return;
    setPendingAction(null);
    setShowPasswordModal(false);
  };

  const handleEdit = () => {
    if (mode !== "saved") return;
    openPasswordDialog("edit");
  };

  const handleDelete = () => {
    if (mode !== "saved") return;
    openPasswordDialog("delete");
  };

  const handleUpdate = async () => {
    if (mode !== "edit") return;
    await handleSave();
  };

  const handleVerifyPassword = async (password) => {
    try {
      setVerifyingPassword(true);

      await verifyEditPassword(password);
      setShowPasswordModal(false);

      if (pendingAction === "edit") {
        setPendingAction(null);
        setMode("edit");
        alert("Edit Password verified successfully.");
      } else if (pendingAction === "delete") {
        const docNo = Number(header.docNo);

        if (!docNo) {
          throw new Error("Receipt number is missing.");
        }

        await deleteReceipt(docNo);
        setPendingAction(null);
        alert("Receipt Deleted Successfully.");
        await handleNew();
      }
    } catch (err) {
      console.error("Receipt password/action error:", err);
      alert(err?.response?.data?.message ?? "Invalid Edit Password.");
    } finally {
      setVerifyingPassword(false);
    }
  };

  const handlePrint = () => {
    window.print();
  };

  return (
    <div className="receipt-entry">
      {error && <div className="receipt-error">{error}</div>}

      <ReceiptHeader
        header={header}
        setHeader={setHeader}
        parties={parties}
        readOnly={isReadOnly}
        loading={loading}
      />

      <ReceiptDetailGrid
        details={details}
        accounts={accounts}
        summary={summary}
        onChange={handleDetailChange}
        onAdd={handleAddRow}
        onDelete={handleDeleteRow}
        readOnly={isReadOnly}
      />

      <ReceiptFooter
        searchDocNo={searchDocNo}
        setSearchDocNo={setSearchDocNo}
        onSearch={handleSearch}
        mode={mode}
        onNew={handleNew}
        onSave={handleSave}
        onEdit={handleEdit}
        onUpdate={handleUpdate}
        onDelete={handleDelete}
        onPrint={handlePrint}
        loading={loading}
      />

      <EditPasswordModal
        show={showPasswordModal}
        title="Authorization Required"
        onVerify={handleVerifyPassword}
        onClose={closePasswordDialog}
      />
    </div>
  );
}
