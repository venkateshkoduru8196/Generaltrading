import { useEffect, useState } from "react";
import "./PaymentEntry.css";

import EditPasswordModal from "../../authentication/components/EditPasswordModal";
import {
  getParties,
  getAccounts,
  savePayment,
  getNextPaymentNumber,
  searchPayments,
  getPaymentByDocNo,
  deletePayment,
} from "../../services/paymentService";
import { verifyEditPassword } from "../../services/authService";

import PaymentHeader from "./components/PaymentHeader/PaymentHeader";
import PaymentDetailGrid from "./components/PaymentDetailGrid/PaymentDetailGrid";
import PaymentFooter from "./components/PaymentFooter/PaymentFooter";

const createEmptyDetail = () => ({
  slNo: 1,
  accountId: "",
  amount: "",
});

export default function PaymentEntry() {
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
      console.error("Payment master data error:", err);
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
      const nextNo = await getNextPaymentNumber();
      setHeader((prev) => ({ ...prev, docNo: nextNo }));
    } catch (err) {
      console.error("Next payment number error:", err);
      setError("Unable to generate Payment Number.");
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

  const validatePayment = () => {
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
    if (!validatePayment()) return;

    try {
      setLoading(true);
      setError("");

      const savedDocNo = await savePayment(buildRequest());
      const docNo = savedDocNo?.docNo ?? savedDocNo;

      setHeader((prev) => ({
        ...prev,
        docNo: docNo ?? prev.docNo,
      }));

      alert(`Payment Saved Successfully.\nDocument No : ${docNo}`);
      setMode("saved");
    } catch (err) {
      console.error("Save Payment Error:", err);
      alert(err?.response?.data?.message ?? "Unable to save Payment.");
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = async () => {
    if (!searchDocNo.trim()) {
      alert("Please enter Payment Number.");
      return;
    }

    try {
      setLoading(true);
      setError("");

      const results = await searchPayments(searchDocNo.trim());
      const first = results?.[0];

      if (!first) {
        alert("Payment not found.");
        return;
      }

      await loadPayment(first.docNo);
    } catch (err) {
      console.error("Search Payment Error:", err);
      alert(err?.response?.data?.message ?? "Unable to search Payment.");
    } finally {
      setLoading(false);
    }
  };

  const loadPayment = async (docNo) => {
    const payment = await getPaymentByDocNo(docNo);

    if (!payment) {
      alert("Payment not found.");
      return;
    }

    const rows = (payment.details ?? []).map((item, index) => ({
      slNo: index + 1,
      accountId: item.accountId,
      amount: item.amount,
    }));

    const finalRows = rows.length > 0 ? rows : [createEmptyDetail()];

    setHeader({
      docNo: payment.docNo,
      docDate: payment.docDate?.substring(0, 10) ?? "",
      partyId: payment.partyId,
    });

    setDetails(finalRows);
    calculateSummary(finalRows);
    setSearchDocNo(String(payment.docNo));
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
          throw new Error("Payment number is missing.");
        }

        await deletePayment(docNo);
        setPendingAction(null);
        alert("Payment Deleted Successfully.");
        await handleNew();
      }
    } catch (err) {
      console.error("Payment password/action error:", err);
      alert(err?.response?.data?.message ?? "Invalid Edit Password.");
    } finally {
      setVerifyingPassword(false);
    }
  };

  const handlePrint = () => {
    window.print();
  };

  return (
    <div className="payment-entry">
      {error && <div className="payment-error">{error}</div>}

      <PaymentHeader
        header={header}
        setHeader={setHeader}
        parties={parties}
        readOnly={isReadOnly}
        loading={loading}
      />

      <PaymentDetailGrid
        details={details}
        accounts={accounts}
        summary={summary}
        onChange={handleDetailChange}
        onAdd={handleAddRow}
        onDelete={handleDeleteRow}
        readOnly={isReadOnly}
      />

      <PaymentFooter
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
