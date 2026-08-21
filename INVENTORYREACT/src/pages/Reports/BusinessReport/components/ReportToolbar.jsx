import {

    FaEye,
    FaFilePdf,
    FaDownload,
    FaFileExcel,
    FaFileWord

} from "react-icons/fa";

export default function ReportToolbar({

    loadingAction,

    onView,

    onPdf,

    onDownloadPdf,

    onExcel,

    onWord

}) {

    return (

        <div className="toolbar-container">

            {/*======================================*/}
            {/* Preview */}
            {/*======================================*/}

            <button

                className="toolbar-btn btn-view"

                onClick={onView}

                disabled={loadingAction !== ""}

            >

                <FaEye />

                {

                    loadingAction === "preview"

                        ? "Loading..."

                        : "Preview"

                }

            </button>

            {/*======================================*/}
            {/* PDF Preview */}
            {/*======================================*/}

            <button

                className="toolbar-btn btn-pdf"

                onClick={onPdf}

                disabled={loadingAction !== ""}

            >

                <FaFilePdf />

                {

                    loadingAction === "pdfPreview"

                        ? "Generating..."

                        : "PDF Preview"

                }

            </button>

            {/*======================================*/}
            {/* Download PDF */}
            {/*======================================*/}

            <button

                className="toolbar-btn btn-download"

                onClick={onDownloadPdf}

                disabled={loadingAction !== ""}

            >

                <FaDownload />

                {

                    loadingAction === "pdfDownload"

                        ? "Downloading..."

                        : "Download PDF"

                }

            </button>

            {/*======================================*/}
            {/* Excel */}
            {/*======================================*/}

            <button

                className="toolbar-btn btn-excel"

                onClick={onExcel}

                disabled={loadingAction !== ""}

            >

                <FaFileExcel />

                {

                    loadingAction === "excel"

                        ? "Generating..."

                        : "Excel"

                }

            </button>

            {/*======================================*/}
            {/* Word */}
            {/*======================================*/}

            <button

                className="toolbar-btn btn-word"

                onClick={onWord}

                disabled={loadingAction !== ""}

            >

                <FaFileWord />

                {

                    loadingAction === "word"

                        ? "Generating..."

                        : "Word"

                }

            </button>

        </div>

    );

}