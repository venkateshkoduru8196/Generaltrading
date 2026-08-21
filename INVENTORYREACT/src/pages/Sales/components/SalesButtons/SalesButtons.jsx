import {
  useEffect,
  useRef,
  useState
} from "react";

import "./SalesButtons.css";

export default function SalesButtons({

  mode,

  onNew,

  onSave,

  onEdit,

  onUpdate,

  onDelete,

  onPrint,

  onDownloadPdf,

  onDownloadWord,

  onDownloadExcel,

}) {

  const [
    downloadOpen,
    setDownloadOpen
  ] = useState(false);

  const downloadRef =
    useRef(null);


  //==========================================
  // CLOSE DROPDOWN WHEN CLICKING OUTSIDE
  //==========================================

  useEffect(() => {

    const handleOutsideClick =
      (event) => {

        if (
          downloadRef.current &&
          !downloadRef.current.contains(
            event.target
          )
        ) {
          setDownloadOpen(false);
        }

      };

    document.addEventListener(
      "mousedown",
      handleOutsideClick
    );

    return () => {

      document.removeEventListener(
        "mousedown",
        handleOutsideClick
      );

    };

  }, []);


  //==========================================
  // DOWNLOAD DISABLED
  //==========================================

  const downloadDisabled =
    mode !== "saved";


  //==========================================
  // TOGGLE DOWNLOAD
  //==========================================

  const handleDownloadToggle = () => {

    if (downloadDisabled) {
      return;
    }

    setDownloadOpen(
      previous => !previous
    );

  };


  //==========================================
  // PDF
  //==========================================

  const handlePdf = async () => {

    setDownloadOpen(false);

    await onDownloadPdf();

  };


  //==========================================
  // WORD
  //==========================================

  const handleWord = async () => {

    setDownloadOpen(false);

    await onDownloadWord();

  };


  //==========================================
  // EXCEL
  //==========================================

  const handleExcel = async () => {

    setDownloadOpen(false);

    await onDownloadExcel();

  };


  return (

    <div className="sales-buttons">


      {/* NEW */}

      <button
        type="button"
        className="btn btn-new"
        onClick={onNew}
        disabled={
          mode === "new" ||
          mode === "edit"
        }
      >
        New
      </button>


      {/* SAVE */}

      <button
        type="button"
        className="btn btn-save"
        onClick={onSave}
        disabled={
          mode !== "new"
        }
      >
        Save
      </button>


      {/* EDIT */}

      <button
        type="button"
        className="btn btn-edit"
        onClick={onEdit}
        disabled={
          mode !== "saved"
        }
      >
        Edit
      </button>


      {/* UPDATE */}

      <button
        type="button"
        className="btn btn-update"
        onClick={onUpdate}
        disabled={
          mode !== "edit"
        }
      >
        Update
      </button>


      {/* DELETE */}

      <button
        type="button"
        className="btn btn-delete"
        onClick={onDelete}
        disabled={
          mode !== "saved"
        }
      >
        Delete
      </button>


      {/*==========================================
          DOWNLOAD
      ==========================================*/}

      <div
        className="download-dropdown"
        ref={downloadRef}
      >

        <button
          type="button"
          className="btn btn-download"
          onClick={
            handleDownloadToggle
          }
          disabled={
            downloadDisabled
          }
        >

          Download

          <span
            className={
              downloadOpen
                ? "dropdown-arrow open"
                : "dropdown-arrow"
            }
          >
            ▼
          </span>

        </button>


        {downloadOpen && (

          <div className="download-menu">

            <button
              type="button"
              className="download-menu-item"
              onClick={handlePdf}
            >
              <span className="download-icon">
                📄
              </span>

              <span>
                PDF
              </span>
            </button>


            <button
              type="button"
              className="download-menu-item"
              onClick={handleWord}
            >
              <span className="download-icon">
                📝
              </span>

              <span>
                Word
              </span>
            </button>


            <button
              type="button"
              className="download-menu-item"
              onClick={handleExcel}
            >
              <span className="download-icon">
                📊
              </span>

              <span>
                Excel
              </span>
            </button>

          </div>

        )}

      </div>


      {/* PRINT */}

      <button
        type="button"
        className="btn btn-print"
        onClick={onPrint}
        disabled={
          mode !== "saved"
        }
      >
        Print
      </button>

    </div>

  );
}