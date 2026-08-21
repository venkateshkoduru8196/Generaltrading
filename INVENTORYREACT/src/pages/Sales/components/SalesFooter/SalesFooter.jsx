import "./SalesFooter.css";

import SalesButtons from "../SalesButtons/SalesButtons";

export default function SalesFooter({

  searchDocNo,

  setSearchDocNo,

  onSearch,

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

  return (

    <div className="sales-footer">

      {/*==========================================
          SEARCH
      ==========================================*/}

      <div className="sales-search">

        <label>
          Search Invoice
        </label>

        <div className="search-box">

          <input
            type="text"
            value={searchDocNo}
            onChange={(e) =>
              setSearchDocNo(
                e.target.value
              )
            }
            placeholder="Enter Invoice No"
            disabled={
              mode === "new" ||
              mode === "edit"
            }
          />

          <button
            type="button"
            onClick={onSearch}
            disabled={
              mode === "new" ||
              mode === "edit"
            }
          >
            🔍
          </button>

        </div>

      </div>


      {/*==========================================
          BUTTONS
      ==========================================*/}

      <div className="sales-footer-buttons">

        <SalesButtons

          mode={mode}

          onNew={onNew}

          onSave={onSave}

          onEdit={onEdit}

          onUpdate={onUpdate}

          onDelete={onDelete}

          onPrint={onPrint}

          onDownloadPdf={
            onDownloadPdf
          }

          onDownloadWord={
            onDownloadWord
          }

          onDownloadExcel={
            onDownloadExcel
          }

        />

      </div>

    </div>

  );
}