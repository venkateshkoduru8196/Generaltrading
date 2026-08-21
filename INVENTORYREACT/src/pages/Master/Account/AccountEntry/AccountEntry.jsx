import {
    useCallback,
    useEffect,
    useMemo,
    useState
} from "react";

import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import accountService
    from "../../../../services/accountService";

import {
    verifyEditPassword
} from "../../../../services/authService";

import EditPasswordModal
    from "../../../../Authentication/components/EditPasswordModal";

import AccountHeader
    from "../components/AccountHeader/AccountHeader";

import AccountTable
    from "../components/AccountTable/AccountTable";

import AccountPagination
    from "../components/AccountPagination/AccountPagination";

import AccountActionBar
    from "../components/AccountActionBar/AccountActionBar";

import "./AccountEntry.css";


//==========================================================
// INITIAL FORM
//==========================================================

const INITIAL_FORM = {
    id: 0,
    accountCode: "",
    accountName: "",
    actype: "G",
    isActive: true
};


//==========================================================
// MODES
//==========================================================

const MODE = {
    INITIAL: "initial",
    NEW: "new",
    VIEW: "view",
    EDIT: "edit"
};


//==========================================================
// PASSWORD ACTION
//==========================================================

const PASSWORD_ACTION = {
    EDIT: "edit",
    DELETE: "delete"
};


//==========================================================
// ACCOUNT ENTRY
//==========================================================

export default function AccountEntry() {

    //======================================================
    // FORM
    //======================================================

    const [form, setForm] =
        useState({ ...INITIAL_FORM });


    //======================================================
    // ACCOUNT LIST
    //======================================================

    const [accounts, setAccounts] =
        useState([]);


    //======================================================
    // SELECTED GRID RECORD
    //
    // IMPORTANT:
    // This is completely separate from form.
    //======================================================

    const [selectedId, setSelectedId] =
        useState(null);


    //======================================================
    // PAGE MODE
    //======================================================

    const [mode, setMode] =
        useState(MODE.INITIAL);


    //======================================================
    // SEARCH
    //======================================================

    const [searchText, setSearchText] =
        useState("");

    const [searchBy, setSearchBy] =
        useState("accountCode");


    //======================================================
    // PAGINATION
    //======================================================

    const [currentPage, setCurrentPage] =
        useState(1);

    const [rowsPerPage, setRowsPerPage] =
        useState(5);


    //======================================================
    // LOADING
    //======================================================

    const [loading, setLoading] =
        useState(false);


    //======================================================
    // PASSWORD MODAL
    //======================================================

    const [showPasswordModal, setShowPasswordModal] =
        useState(false);


    //======================================================
    // PENDING ACTION
    //======================================================

    const [pendingAction, setPendingAction] =
        useState(null);


    //======================================================
    // PENDING ACCOUNT
    //======================================================

    const [pendingAccountId, setPendingAccountId] =
        useState(null);


    //==========================================================
    // NORMALIZE ID
    //
    // Handles:
    //
    // 1028
    // "1028"
    //==========================================================

    const normalizeId = useCallback(
        (value) => {

            if (
                value === null ||
                value === undefined ||
                value === ""
            ) {
                return null;
            }

            const number =
                Number(value);

            return Number.isFinite(number)
                ? number
                : null;

        },
        []
    );


    //==========================================================
    // EXTRACT API DATA
    //
    // Supports:
    //
    // Axios response
    // API envelope
    // Direct object
    //==========================================================

    const extractApiData =
        useCallback(
            (response) => {

                let value = response;


                // Axios response
                if (
                    value &&
                    value.data !== undefined &&
                    (
                        value.status !== undefined ||
                        value.config !== undefined ||
                        value.headers !== undefined
                    )
                ) {

                    value = value.data;

                }


                // API envelope
                if (
                    value &&
                    typeof value === "object" &&
                    value.data !== undefined &&
                    (
                        value.success !== undefined ||
                        value.Success !== undefined ||
                        value.message !== undefined ||
                        value.Message !== undefined
                    )
                ) {

                    return value.data;

                }


                return value;

            },
            []
        );


    //==========================================================
    // API SUCCESS
    //==========================================================

    const isApiSuccess =
        useCallback(
            (response) => {

                let value = response;


                // Axios
                if (
                    value &&
                    value.data !== undefined &&
                    (
                        value.status !== undefined ||
                        value.config !== undefined ||
                        value.headers !== undefined
                    )
                ) {

                    value = value.data;

                }


                if (
                    value?.success !== undefined
                ) {

                    return value.success === true;

                }


                if (
                    value?.Success !== undefined
                ) {

                    return value.Success === true;

                }


                // If API returned data without success flag,
                // consider it successful.
                return true;

            },
            []
        );


    //==========================================================
    // GET ACCOUNT FROM LOCAL LIST
    //==========================================================

    const findAccount =
        useCallback(
            (id) => {

                const normalizedId =
                    normalizeId(id);

                if (
                    normalizedId === null
                ) {

                    return null;

                }


                return (
                    accounts.find(
                        account =>
                            normalizeId(
                                account?.id
                            ) === normalizedId
                    ) || null
                );

            },
            [
                accounts,
                normalizeId
            ]
        );


    //==========================================================
    // SELECTED ACCOUNT
    //==========================================================

    const selectedAccount =
        useMemo(
            () => {

                return findAccount(
                    selectedId
                );

            },
            [
                selectedId,
                findAccount
            ]
        );


    //==========================================================
    // LOAD ACCOUNTS
    //==========================================================

    const loadAccounts =
        useCallback(
            async () => {

                try {

                    setLoading(true);


                    const response =
                        await accountService.getAll();


                    const data =
                        extractApiData(response);


                    setAccounts(
                        Array.isArray(data)
                            ? data
                            : []
                    );

                }
                catch (error) {

                    console.error(
                        "Account Load Error:",
                        error
                    );


                    toast.error(
                        getErrorMessage(
                            error,
                            "Unable to load accounts."
                        )
                    );

                }
                finally {

                    setLoading(false);

                }

            },
            [
                extractApiData
            ]
        );


    //==========================================================
    // INITIAL LOAD
    //==========================================================

    useEffect(
        () => {

            loadAccounts();

        },
        [
            loadAccounts
        ]
    );


    //==========================================================
    // RESET FORM
    //==========================================================

    const resetForm =
        useCallback(
            () => {

                setForm({
                    ...INITIAL_FORM
                });

                setSelectedId(null);

                setPendingAction(null);

                setPendingAccountId(null);

            },
            []
        );


    //==========================================================
    // PUT ACCOUNT INTO FORM
    //==========================================================

    const setAccountToForm =
        useCallback(
            (account) => {

                if (
                    !account
                ) {

                    return;

                }


                const id =
                    normalizeId(
                        account.id
                    );


                if (
                    id === null
                ) {

                    return;

                }


                setForm({

                    id,

                    accountCode:
                        account.accountCode ??
                        "",

                    accountName:
                        account.accountName ??
                        "",

                    actype:
                        account.actype ??
                        "G",

                    isActive:
                        account.isActive ??
                        true

                });

            },
            [
                normalizeId
            ]
        );


    //==========================================================
    // NEW
    //==========================================================

    const handleNew =
        useCallback(
            () => {

                resetForm();

                setMode(
                    MODE.NEW
                );

                setCurrentPage(1);

            },
            [
                resetForm
            ]
        );


    //==========================================================
    // SAVE
    //
    // SAVE
    // ↓
    // SERVER
    // ↓
    // CREATED RECORD
    // ↓
    // FORM
    // ↓
    // GRID
    // ↓
    // VIEW MODE
    //==========================================================

    const handleSave =
        useCallback(
            async () => {

                if (
                    mode !== MODE.NEW
                ) {

                    toast.warning(
                        "Please click New before saving."
                    );

                    return;

                }


                const accountCode =
                    form.accountCode.trim();

                const accountName =
                    form.accountName.trim();


                if (!accountCode) {

                    toast.warning(
                        "Account Code is required."
                    );

                    return;

                }


                if (!accountName) {

                    toast.warning(
                        "Account Name is required."
                    );

                    return;

                }


                try {

                    setLoading(true);


                    const response =
                        await accountService.create({
                            accountCode,
                            accountName,
                            actype:
                                form.actype
                        });


                    const createdAccount =
                        extractApiData(
                            response
                        );


                    if (
                        !createdAccount ||
                        !createdAccount.id
                    ) {

                        throw new Error(
                            "Account was created, but the created account was not returned."
                        );

                    }


                    const createdId =
                        normalizeId(
                            createdAccount.id
                        );


                    //==================================================
                    // Add to grid
                    //==================================================

                    setAccounts(
                        previous => {

                            const exists =
                                previous.some(
                                    item =>
                                        normalizeId(
                                            item.id
                                        ) === createdId
                                );


                            if (exists) {

                                return previous.map(
                                    item =>
                                        normalizeId(
                                            item.id
                                        ) === createdId
                                            ? createdAccount
                                            : item
                                );

                            }


                            return [
                                createdAccount,
                                ...previous
                            ];

                        }
                    );


                    //==================================================
                    // Select created record
                    //==================================================

                    setSelectedId(
                        createdId
                    );


                    //==================================================
                    // Display created record
                    //==================================================

                    setAccountToForm(
                        createdAccount
                    );


                    //==================================================
                    // AUTOMATIC VIEW MODE
                    //==================================================

                    setMode(
                        MODE.VIEW
                    );


                    toast.success(
                        "Account created successfully."
                    );

                }
                catch (error) {

                    console.error(
                        "Account Save Error:",
                        error
                    );


                    toast.error(
                        getErrorMessage(
                            error,
                            "Unable to create account."
                        )
                    );

                }
                finally {

                    setLoading(false);

                }

            },
            [
                mode,
                form.accountCode,
                form.accountName,
                form.actype,
                extractApiData,
                normalizeId,
                setAccountToForm
            ]
        );


    //==========================================================
    // VIEW
    //==========================================================

    const handleView =
        useCallback(
            async () => {

                const id =
                    normalizeId(
                        selectedId
                    );


                if (
                    id === null
                ) {

                    toast.warning(
                        "Please select an account first."
                    );

                    return;

                }


                try {

                    setLoading(true);


                    const response =
                        await accountService.getById(
                            id
                        );


                    const account =
                        extractApiData(
                            response
                        );


                    if (
                        !account?.id
                    ) {

                        toast.error(
                            "Account not found."
                        );

                        return;

                    }


                    const accountId =
                        normalizeId(
                            account.id
                        );


                    setAccounts(
                        previous =>
                            previous.map(
                                item =>
                                    normalizeId(
                                        item.id
                                    ) === accountId
                                        ? account
                                        : item
                            )
                    );


                    setSelectedId(
                        accountId
                    );


                    setAccountToForm(
                        account
                    );


                    setMode(
                        MODE.VIEW
                    );

                }
                catch (error) {

                    console.error(
                        "Account View Error:",
                        error
                    );


                    toast.error(
                        getErrorMessage(
                            error,
                            "Unable to load account."
                        )
                    );

                }
                finally {

                    setLoading(false);

                }

            },
            [
                selectedId,
                normalizeId,
                extractApiData,
                setAccountToForm
            ]
        );


    //==========================================================
    // EDIT
    //==========================================================

    const handleEdit =
        useCallback(
            () => {

                const id =
                    normalizeId(
                        selectedId
                    );


                if (
                    id === null
                ) {

                    toast.warning(
                        "Please select an account to edit."
                    );

                    return;

                }


                if (
                    mode === MODE.NEW
                ) {

                    return;

                }


                if (
                    mode === MODE.EDIT
                ) {

                    toast.info(
                        "Account is already in edit mode."
                    );

                    return;

                }


                setPendingAccountId(
                    id
                );


                setPendingAction(
                    PASSWORD_ACTION.EDIT
                );


                setShowPasswordModal(
                    true
                );

            },
            [
                selectedId,
                mode,
                normalizeId
            ]
        );


    //==========================================================
    // PERFORM EDIT
    //==========================================================

    const performEdit =
        useCallback(
            async (accountId) => {

                const id =
                    normalizeId(
                        accountId
                    );


                if (
                    id === null
                ) {

                    toast.error(
                        "Invalid account selection."
                    );

                    return;

                }


                let account =
                    findAccount(id);


                if (!account) {

                    try {

                        const response =
                            await accountService.getById(
                                id
                            );


                        account =
                            extractApiData(
                                response
                            );

                    }
                    catch (error) {

                        toast.error(
                            getErrorMessage(
                                error,
                                "Unable to load account for editing."
                            )
                        );

                        return;

                    }

                }


                if (
                    !account?.id
                ) {

                    toast.error(
                        "Account not found."
                    );

                    return;

                }


                setSelectedId(
                    id
                );


                setAccountToForm(
                    account
                );


                setMode(
                    MODE.EDIT
                );


                toast.success(
                    "Edit mode enabled."
                );

            },
            [
                normalizeId,
                findAccount,
                extractApiData,
                setAccountToForm
            ]
        );


    //==========================================================
    // UPDATE
    //==========================================================

    const handleUpdate =
        useCallback(
            async () => {

                if (
                    mode !== MODE.EDIT
                ) {

                    toast.warning(
                        "Account is not in edit mode."
                    );

                    return;

                }


                const id =
                    normalizeId(
                        form.id
                    );


                if (
                    id === null
                ) {

                    toast.warning(
                        "Invalid Account Id."
                    );

                    return;

                }


                const accountCode =
                    form.accountCode.trim();

                const accountName =
                    form.accountName.trim();


                if (!accountCode) {

                    toast.warning(
                        "Account Code is required."
                    );

                    return;

                }


                if (!accountName) {

                    toast.warning(
                        "Account Name is required."
                    );

                    return;

                }


                try {

                    setLoading(true);


                    const response =
                        await accountService.update(
                            id,
                            {
                                id,
                                accountCode,
                                accountName,
                                actype:
                                    form.actype,
                                isActive:
                                    form.isActive
                            }
                        );


                    const updatedAccount =
                        extractApiData(
                            response
                        );


                    if (
                        !updatedAccount?.id
                    ) {

                        throw new Error(
                            "Account was updated, but the updated account was not returned."
                        );

                    }


                    const updatedId =
                        normalizeId(
                            updatedAccount.id
                        );


                    setAccounts(
                        previous =>
                            previous.map(
                                item =>
                                    normalizeId(
                                        item.id
                                    ) === updatedId
                                        ? updatedAccount
                                        : item
                            )
                    );


                    setSelectedId(
                        updatedId
                    );


                    setAccountToForm(
                        updatedAccount
                    );


                    setMode(
                        MODE.VIEW
                    );


                    toast.success(
                        "Account updated successfully."
                    );

                }
                catch (error) {

                    console.error(
                        "Account Update Error:",
                        error
                    );


                    toast.error(
                        getErrorMessage(
                            error,
                            "Unable to update account."
                        )
                    );

                }
                finally {

                    setLoading(false);

                }

            },
            [
                mode,
                form,
                normalizeId,
                extractApiData,
                setAccountToForm
            ]
        );


    //==========================================================
    // DELETE
    //==========================================================

    const handleDelete =
        useCallback(
            () => {

                const id =
                    normalizeId(
                        selectedId
                    );


                if (
                    id === null
                ) {

                    toast.warning(
                        "Please select an account to delete."
                    );

                    return;

                }


                if (
                    mode === MODE.NEW
                ) {

                    return;

                }


                if (
                    mode === MODE.EDIT
                ) {

                    toast.warning(
                        "Please finish or cancel the current edit first."
                    );

                    return;

                }


                setPendingAccountId(
                    id
                );


                setPendingAction(
                    PASSWORD_ACTION.DELETE
                );


                setShowPasswordModal(
                    true
                );

            },
            [
                selectedId,
                mode,
                normalizeId
            ]
        );


    //==========================================================
    // PERFORM DELETE
    //==========================================================

    const performDelete =
        useCallback(
            async (accountId) => {

                const id =
                    normalizeId(
                        accountId
                    );


                if (
                    id === null
                ) {

                    toast.error(
                        "Invalid account selection."
                    );

                    return;

                }


                const account =
                    findAccount(id);


                const accountName =
                    account?.accountName ||
                    "this account";


                const confirmed =
                    window.confirm(
                        `Are you sure you want to delete "${accountName}"?`
                    );


                if (!confirmed) {

                    return;

                }


                try {

                    setLoading(true);


                    await accountService.delete(
                        id
                    );


                    setAccounts(
                        previous =>
                            previous.filter(
                                item =>
                                    normalizeId(
                                        item.id
                                    ) !== id
                            )
                    );


                    setForm({
                        ...INITIAL_FORM
                    });


                    setSelectedId(
                        null
                    );


                    setMode(
                        MODE.INITIAL
                    );


                    toast.success(
                        "Account deleted successfully."
                    );

                }
                catch (error) {

                    console.error(
                        "Account Delete Error:",
                        error
                    );


                    toast.error(
                        getErrorMessage(
                            error,
                            "Unable to delete account."
                        )
                    );

                }
                finally {

                    setLoading(false);

                }

            },
            [
                normalizeId,
                findAccount
            ]
        );


    //==========================================================
    // PASSWORD VERIFICATION
    //==========================================================

    const handleVerifyPassword =
        useCallback(
            async (password) => {

                if (
                    !password?.trim()
                ) {

                    toast.warning(
                        "Please enter Edit Password."
                    );

                    return;

                }


                const id =
                    normalizeId(
                        pendingAccountId
                    );


                if (
                    id === null
                ) {

                    toast.error(
                        "No account is selected."
                    );

                    return;

                }


                const action =
                    pendingAction;


                try {

                    setLoading(true);


                    const response =
                        await verifyEditPassword(
                            password
                        );


                    if (
                        !isApiSuccess(
                            response
                        )
                    ) {

                        const data =
                            getResponseData(
                                response
                            );


                        toast.error(
                            data?.message ||
                            data?.Message ||
                            "Invalid Edit Password."
                        );

                        return;

                    }


                    setShowPasswordModal(
                        false
                    );


                    setPendingAction(
                        null
                    );


                    setPendingAccountId(
                        null
                    );


                    if (
                        action ===
                        PASSWORD_ACTION.EDIT
                    ) {

                        await performEdit(
                            id
                        );

                        return;

                    }


                    if (
                        action ===
                        PASSWORD_ACTION.DELETE
                    ) {

                        await performDelete(
                            id
                        );

                        return;

                    }


                    toast.error(
                        "Invalid authorization action."
                    );

                }
                catch (error) {

                    console.error(
                        "Edit Password Error:",
                        error
                    );


                    toast.error(
                        getErrorMessage(
                            error,
                            "Invalid Edit Password."
                        )
                    );

                }
                finally {

                    setLoading(false);

                }

            },
            [
                pendingAccountId,
                pendingAction,
                normalizeId,
                isApiSuccess,
                performEdit,
                performDelete
            ]
        );


    //==========================================================
    // CLOSE PASSWORD MODAL
    //==========================================================

    const handleClosePasswordDialog =
        useCallback(
            () => {

                setShowPasswordModal(
                    false
                );

                setPendingAction(
                    null
                );

                setPendingAccountId(
                    null
                );

            },
            []
        );


    //==========================================================
    // GRID SELECTION
    //==========================================================

    const handleSelectedId =
        useCallback(
            (value) => {

                if (
                    mode === MODE.NEW ||
                    mode === MODE.EDIT
                ) {

                    return;

                }


                let id =
                    value;


                if (
                    value &&
                    typeof value === "object"
                ) {

                    id =
                        value.id ??
                        value.Id;

                }


                const normalizedId =
                    normalizeId(id);


                if (
                    normalizedId === null
                ) {

                    return;

                }


                setSelectedId(
                    normalizedId
                );

            },
            [
                mode,
                normalizeId
            ]
        );


    //==========================================================
    // SEARCH
    //==========================================================

    const filteredAccounts =
        useMemo(
            () => {

                const search =
                    searchText
                        .trim()
                        .toLowerCase();


                if (!search) {

                    return accounts;

                }


                return accounts.filter(
                    item => {

                        let value = "";


                        switch (
                            searchBy
                        ) {

                            case "accountCode":

                                value =
                                    item.accountCode;

                                break;


                            case "accountName":

                                value =
                                    item.accountName;

                                break;


                            //==================================================
                            // Account Type
                            //==================================================

                            case "actype":

                                value =
                                    item.actype === "G"
                                        ? "General"
                                        : item.actype === "B"
                                            ? "Bank/Cash"
                                            : item.actype === "C"
                                                ? "Customer"
                                                : item.actype === "S"
                                                    ? "Supplier"
                                                    : "";

                                break;


                            case "status":

                                value =
                                    item.isActive
                                        ? "active"
                                        : "inactive";

                                break;


                            case "createdBy":

                                value =
                                    item.createdBy;

                                break;


                            case "createdOn":

                                value =
                                    item.createdOn
                                        ? new Date(
                                            item.createdOn
                                        ).toLocaleDateString()
                                        : "";

                                break;


                            default:

                                value =
                                    `${item.accountCode || ""} ${item.accountName || ""}`;

                                break;

                        }


                        return String(
                            value ?? ""
                        )
                            .toLowerCase()
                            .includes(search);

                    }
                );

            },
            [
                accounts,
                searchText,
                searchBy
            ]
        );


    //==========================================================
    // PAGINATION
    //==========================================================

    const totalRecords =
        filteredAccounts.length;


    const totalPages =
        Math.ceil(
            totalRecords /
            rowsPerPage
        );


    const paginatedAccounts =
        useMemo(
            () => {

                const start =
                    (currentPage - 1) *
                    rowsPerPage;


                return filteredAccounts.slice(
                    start,
                    start + rowsPerPage
                );

            },
            [
                filteredAccounts,
                currentPage,
                rowsPerPage
            ]
        );


    //==========================================================
    // PAGE VALIDATION
    //==========================================================

    useEffect(
        () => {

            if (
                totalPages > 0 &&
                currentPage > totalPages
            ) {

                setCurrentPage(
                    totalPages
                );

            }


            if (
                totalPages === 0 &&
                currentPage !== 1
            ) {

                setCurrentPage(
                    1
                );

            }

        },
        [
            currentPage,
            totalPages
        ]
    );


    //==========================================================
    // SEARCH RESET
    //==========================================================

    useEffect(
        () => {

            setCurrentPage(1);

        },
        [
            searchText,
            searchBy
        ]
    );


    //==========================================================
    // ROWS PER PAGE
    //==========================================================

    const handleRowsPerPageChange =
        useCallback(
            (value) => {

                const number =
                    Number(value);


                setRowsPerPage(
                    number > 0
                        ? number
                        : 5
                );


                setCurrentPage(1);

            },
            []
        );


    //==========================================================
    // PRINT
    //==========================================================

    const handlePrint =
        useCallback(
            () => {

                if (
                    !selectedId
                ) {

                    toast.warning(
                        "Please select an account first."
                    );

                    return;

                }


                window.print();

            },
            [
                selectedId
            ]
        );


    //==========================================================
    // KEYBOARD
    //==========================================================

    useEffect(
        () => {

            const handleKeyboard =
                (event) => {

                    if (
                        showPasswordModal
                    ) {

                        return;

                    }


                    const tag =
                        event.target?.tagName;


                    const typing =
                        tag === "INPUT" ||
                        tag === "TEXTAREA" ||
                        tag === "SELECT";


                    if (
                        event.key === "F2" &&
                        !typing
                    ) {

                        event.preventDefault();

                        handleNew();

                        return;

                    }


                    if (
                        event.key === "F3" &&
                        !typing
                    ) {

                        event.preventDefault();

                        handleView();

                        return;

                    }


                    if (
                        event.key === "F4" &&
                        !typing
                    ) {

                        event.preventDefault();

                        handleEdit();

                        return;

                    }


                    if (
                        event.key === "F8" &&
                        !typing
                    ) {

                        event.preventDefault();

                        handleDelete();

                        return;

                    }


                    if (
                        event.ctrlKey &&
                        event.key.toLowerCase() === "s"
                    ) {

                        event.preventDefault();


                        if (
                            mode === MODE.NEW
                        ) {

                            handleSave();

                        }
                        else if (
                            mode === MODE.EDIT
                        ) {

                            handleUpdate();

                        }

                        return;

                    }


                    if (
                        event.ctrlKey &&
                        event.key.toLowerCase() === "p"
                    ) {

                        event.preventDefault();

                        handlePrint();

                        return;

                    }

                };


            window.addEventListener(
                "keydown",
                handleKeyboard
            );


            return () => {

                window.removeEventListener(
                    "keydown",
                    handleKeyboard
                );

            };

        },
        [
            showPasswordModal,
            mode,
            handleNew,
            handleView,
            handleEdit,
            handleDelete,
            handleSave,
            handleUpdate,
            handlePrint
        ]
    );


    //==========================================================
    // RENDER
    //==========================================================

    return (

        <div
            className={
                loading
                    ? "account-entry-page loading"
                    : "account-entry-page"
            }
        >

            {/*==================================================
                PASSWORD MODAL
            ==================================================*/}

            <EditPasswordModal

                show={
                    showPasswordModal
                }

                title={
                    pendingAction ===
                    PASSWORD_ACTION.DELETE
                        ? "Delete Authorization"
                        : "Edit Authorization"
                }

                message={
                    pendingAction ===
                    PASSWORD_ACTION.DELETE
                        ? "Please enter Edit Password to delete this account."
                        : "Please enter Edit Password to edit this account."
                }

                onVerify={
                    handleVerifyPassword
                }

                onClose={
                    handleClosePasswordDialog
                }

            />


            {/*==================================================
                HEADER
            ==================================================*/}

            <AccountHeader

                form={
                    form
                }

                setForm={
                    setForm
                }

                mode={
                    mode
                }

            />


            {/*==================================================
                ACTION BAR
            ==================================================*/}

            <AccountActionBar

                mode={
                    mode
                }

                selectedId={
                    selectedId
                }

                onNew={
                    handleNew
                }

                onSave={
                    handleSave
                }

                onView={
                    handleView
                }

                onEdit={
                    handleEdit
                }

                onUpdate={
                    handleUpdate
                }

                onDelete={
                    handleDelete
                }

                onPrint={
                    handlePrint
                }

            />


            {/*==================================================
                TABLE
            ==================================================*/}

            <AccountTable

                accounts={
                    paginatedAccounts
                }

                selectedId={
                    selectedId
                }

                setSelectedId={
                    handleSelectedId
                }

                searchText={
                    searchText
                }

                setSearchText={
                    setSearchText
                }

                searchBy={
                    searchBy
                }

                setSearchBy={
                    setSearchBy
                }

                mode={
                    mode
                }

            />


            {/*==================================================
                PAGINATION
            ==================================================*/}

            <AccountPagination

                totalRecords={
                    totalRecords
                }

                currentPage={
                    currentPage
                }

                rowsPerPage={
                    rowsPerPage
                }

                totalPages={
                    totalPages
                }

                onPageChange={
                    setCurrentPage
                }

                onRowsPerPageChange={
                    handleRowsPerPageChange
                }

            />


            {/*==================================================
                LOADING
            ==================================================*/}

            {
                loading && (

                    <div className="account-loading-overlay">

                        <div className="account-loading-box">

                            <div className="account-loading-spinner" />

                            <span>
                                Processing...
                            </span>

                        </div>

                    </div>

                )
            }

        </div>

    );

}


//==========================================================
// RESPONSE DATA
//==========================================================

function getResponseData(
    response
) {

    let value =
        response;


    // Axios response
    if (
        value &&
        value.data !== undefined &&
        (
            value.status !== undefined ||
            value.config !== undefined ||
            value.headers !== undefined
        )
    ) {

        value =
            value.data;

    }


    return value;

}


//==========================================================
// USER-FRIENDLY ERROR MESSAGE
//==========================================================

function getErrorMessage(
    error,
    fallback
) {

    const data =
        error?.response?.data;


    //==========================================================
    // API OBJECT
    //==========================================================

    if (
        data &&
        typeof data === "object"
    ) {

        if (
            data.message
        ) {

            return data.message;

        }


        if (
            data.Message
        ) {

            return data.Message;

        }


        if (
            data.errors
        ) {

            const messages = [];


            Object.values(
                data.errors
            ).forEach(
                value => {

                    if (
                        Array.isArray(value)
                    ) {

                        messages.push(
                            ...value
                        );

                    }

                }
            );


            if (
                messages.length
            ) {

                return messages.join(
                    "\n"
                );

            }

        }

    }


    //==========================================================
    // STRING RESPONSE
    //==========================================================

    if (
        typeof data === "string"
    ) {

        const text =
            data.trim();


        if (!text) {

            return fallback;

        }


        if (
            text.includes(
                "Account Code already exists"
            )
        ) {

            return "Account Code already exists.";

        }


        if (
            text.includes(
                "Account not found"
            )
        ) {

            return "Account not found.";

        }


        if (
            text.includes(
                "Company is not assigned"
            )
        ) {

            return "Company is not assigned.";

        }


        if (
            text.includes(
                "Invalid Edit Password"
            )
        ) {

            return "Invalid Edit Password.";

        }


        const technicalErrors = [

            "Microsoft.EntityFrameworkCore",

            "Microsoft.Data.SqlClient",

            "SqlException",

            "DbUpdateException",

            "IX_AccountMaster",

            "Cannot insert duplicate key",

            "at INVENTORYAPP."

        ];


        if (
            technicalErrors.some(
                item =>
                    text.includes(item)
            )
        ) {

            return fallback;

        }


        return text;

    }


    //==========================================================
    // AXIOS ERROR
    //==========================================================

    if (
        error?.message &&
        !error.message.includes(
            "Request failed with status code"
        )
    ) {

        return error.message;

    }


    return fallback;

}