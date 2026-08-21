import { useEffect, useRef, useState } from "react";
import "./EditPasswordModal.css";

export default function EditPasswordModal({
  show,
  title = "Authorization Required",
  message = "Please enter your Edit Password to continue.",
  onVerify,
  onClose,
}) {
  const [password, setPassword] = useState("");

  const inputRef = useRef(null);

  useEffect(() => {
    if (show) {
      setPassword("");

      setTimeout(() => {
        inputRef.current?.focus();
      }, 100);
    }
  }, [show]);

  if (!show) return null;

  const handleVerify = () => {
    if (!password.trim()) {
      alert("Please enter Edit Password.");
      return;
    }

    onVerify(password);

    setPassword("");
  };

  const handleClose = () => {
    setPassword("");

    onClose();
  };

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      handleVerify();
    }

    if (e.key === "Escape") {
      handleClose();
    }
  };

  return (
    <div className="password-overlay">

      <div className="password-modal">

        <div className="password-header">
          <h3>{title}</h3>
        </div>

        <div className="password-body">

          <p>{message}</p>

          <label>Edit Password</label>

          <input
            ref={inputRef}
            type="password"
            value={password}
            onChange={(e) =>
              setPassword(e.target.value)
            }
            onKeyDown={handleKeyDown}
            placeholder="Enter Edit Password"
          />

        </div>

        <div className="password-footer">

          <button
            className="cancel-btn"
            onClick={handleClose}
          >
            Cancel
          </button>

          <button
            className="verify-btn"
            onClick={handleVerify}
          >
            Verify
          </button>

        </div>

      </div>

    </div>
  );
}