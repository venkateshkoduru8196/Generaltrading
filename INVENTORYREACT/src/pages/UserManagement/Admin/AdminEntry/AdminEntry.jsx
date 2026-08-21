import { useCallback, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getAdmins,
    updateAdminStatus,
    deleteAdmin
} from "../../../../services/adminService";

import AdminHeader from "./components/AdminHeader";
import AdminSummary from "./components/AdminSummary";
import AdminActionBar from "./components/AdminActionBar";
import AdminSearch from "./components/AdminSearch";
import AdminTable from "./components/AdminTable";
import AdminPagination from "./components/AdminPagination";

import "./AdminEntry.css";


//==========================================================
// ADMIN ENTRY
//==========================================================

export default function AdminEntry() {

    const navigate = useNavigate();


    //======================================================
    // DATA
    //======================================================

    const [admins, setAdmins] = useState([]);


    //======================================================
    // LOADING
    //======================================================

    const [loading, setLoading] = useState(false);


    //======================================================
    // ERROR
    //======================================================

    const [error, setError] = useState("");


    //======================================================
    // FILTERS
    //======================================================

    const [filters, setFilters] = useState({
        search: "",
        isActive: null,
        companyId: null
    });


    //======================================================
    // PAGINATION
    //======================================================

    const [pagination, setPagination] = useState({
        pageNumber: 1,
        pageSize: 20,
        totalRecords: 0,
        totalPages: 0
    });


    //======================================================
    // SUMMARY
    //======================================================

    const [summary, setSummary] = useState({
        active: 0,
        inactive: 0
    });


    //======================================================
    // LOAD ADMINS
    //======================================================

    const loadAdmins = useCallback(async () => {

        setLoading(true);
        setError("");

        try {

            const params = {
                pageNumber: pagination.pageNumber,
                pageSize: pagination.pageSize
            };


            //================================================
            // SEARCH
            //================================================

            if (filters.search.trim()) {

                params.search =
                    filters.search.trim();

            }


            //================================================
            // STATUS
            //================================================

            if (filters.isActive !== null) {

                params.isActive =
                    filters.isActive;

            }


            //================================================
            // COMPANY
            //================================================

            if (filters.companyId !== null) {

                params.companyId =
                    filters.companyId;

            }


            const response =
                await getAdmins(params);


            //================================================
            // SUPPORT COMMON PAGED RESPONSE NAMES
            //================================================

            const items =
                response?.items ??
                response?.users ??
                response?.data ??
                [];


            const totalRecords =
                response?.totalRecords ??
                response?.totalCount ??
                0;


            const totalPages =
                response?.totalPages ??
                (
                    pagination.pageSize > 0
                        ? Math.ceil(
                            totalRecords /
                            pagination.pageSize
                        )
                        : 0
                );


            setAdmins(
                Array.isArray(items)
                    ? items
                    : []
            );


            setPagination(previous => ({
                ...previous,

                totalRecords,
                totalPages
            }));


            //================================================
            // SUMMARY
            //================================================

            const activeCount =
                items.filter(
                    user => user.isActive === true
                ).length;


            const inactiveCount =
                items.filter(
                    user => user.isActive === false
                ).length;


            setSummary({
                active: activeCount,
                inactive: inactiveCount
            });

        }
        catch (err) {

            console.error(
                "Error loading administrators:",
                err
            );


            setAdmins([]);


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Unable to load administrators."
            );

        }
        finally {

            setLoading(false);

        }

    }, [
        filters.search,
        filters.isActive,
        filters.companyId,
        pagination.pageNumber,
        pagination.pageSize
    ]);


    //======================================================
    // LOAD ON PAGE / FILTER CHANGE
    //======================================================

    useEffect(() => {

        loadAdmins();

    }, [loadAdmins]);


    //======================================================
    // CREATE ADMIN
    //======================================================

    const handleCreate = () => {

        navigate(
            "/user-management/admin/create"
        );

    };


    //======================================================
    // EDIT ADMIN
    //======================================================

    const handleEdit = (userId) => {

        navigate(
            `/user-management/admin/edit/${userId}`
        );

    };


    //======================================================
    // SEARCH
    //======================================================

    const handleSearchChange = (value) => {

        setFilters(previous => ({
            ...previous,
            search: value
        }));


        setPagination(previous => ({
            ...previous,
            pageNumber: 1
        }));

    };


    //======================================================
    // STATUS FILTER
    //======================================================

    const handleStatusChange = (value) => {

        let status = null;


        if (value === "true") {

            status = true;

        }
        else if (value === "false") {

            status = false;

        }


        setFilters(previous => ({
            ...previous,
            isActive: status
        }));


        setPagination(previous => ({
            ...previous,
            pageNumber: 1
        }));

    };


    //======================================================
    // COMPANY FILTER
    //======================================================

    const handleCompanyChange = (value) => {

        const companyId =
            value
                ? Number(value)
                : null;


        setFilters(previous => ({
            ...previous,
            companyId
        }));


        setPagination(previous => ({
            ...previous,
            pageNumber: 1
        }));

    };


    //======================================================
    // CLEAR FILTERS
    //======================================================

    const handleClear = () => {

        setFilters({
            search: "",
            isActive: null,
            companyId: null
        });


        setPagination(previous => ({
            ...previous,
            pageNumber: 1
        }));

    };


    //======================================================
    // REFRESH
    //======================================================

    const handleRefresh = () => {

        loadAdmins();

    };


    //======================================================
    // ACTIVATE / DEACTIVATE
    //======================================================

    const handleStatusUpdate = async (
        userId,
        isActive
    ) => {

        const action =
            isActive
                ? "activate"
                : "deactivate";


        const confirmed =
            window.confirm(
                `Are you sure you want to ${action} this administrator?`
            );


        if (!confirmed)
            return;


        setError("");


        try {

            //================================================
            // IMPORTANT
            //
            // Pass userId and isActive separately.
            //
            // DO NOT PASS:
            // updateAdminStatus({
            //     userId,
            //     isActive
            // });
            //
            //================================================

            await updateAdminStatus(
                userId,
                isActive
            );


            await loadAdmins();

        }
        catch (err) {

            console.error(
                "Error updating administrator status:",
                err
            );


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Unable to update administrator status."
            );

        }

    };


    //======================================================
    // DELETE ADMIN
    //======================================================

    const handleDelete = async (userId) => {

        const confirmed =
            window.confirm(
                "Are you sure you want to delete this administrator? The account will be soft deleted."
            );


        if (!confirmed)
            return;


        setError("");


        try {

            await deleteAdmin(userId);


            //================================================
            // IF LAST RECORD ON PAGE WAS DELETED
            // MOVE TO PREVIOUS PAGE
            //================================================

            if (
                admins.length === 1 &&
                pagination.pageNumber > 1
            ) {

                setPagination(previous => ({
                    ...previous,
                    pageNumber:
                        previous.pageNumber - 1
                }));

            }
            else {

                await loadAdmins();

            }

        }
        catch (err) {

            console.error(
                "Error deleting administrator:",
                err
            );


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Unable to delete administrator."
            );

        }

    };


    //======================================================
    // PAGE CHANGE
    //======================================================

    const handlePageChange = (page) => {

        if (
            page < 1 ||
            page > pagination.totalPages ||
            page === pagination.pageNumber
        ) {
            return;
        }


        setPagination(previous => ({
            ...previous,
            pageNumber: page
        }));

    };


    //======================================================
    // PAGE SIZE CHANGE
    //======================================================

    const handlePageSizeChange = (size) => {

        setPagination(previous => ({
            ...previous,
            pageNumber: 1,
            pageSize: size
        }));

    };


    //======================================================
    // RENDER
    //======================================================

    return (

        <div className="admin-entry">


            {/*================================================
                PAGE HEADER
            =================================================*/}

            <AdminHeader
                onCreate={handleCreate}
            />


            {/*================================================
                SUMMARY
            =================================================*/}

            <AdminSummary
                totalRecords={
                    pagination.totalRecords
                }

                activeCount={
                    summary.active
                }

                inactiveCount={
                    summary.inactive
                }
            />


            {/*================================================
                ACTION BAR
            =================================================*/}

            <AdminActionBar
                totalRecords={
                    pagination.totalRecords
                }

                onRefresh={
                    handleRefresh
                }

                loading={
                    loading
                }
            />


            {/*================================================
                SEARCH / FILTER
            =================================================*/}

            <AdminSearch
                filters={filters}

                onSearchChange={
                    handleSearchChange
                }

                onStatusChange={
                    handleStatusChange
                }

                onCompanyChange={
                    handleCompanyChange
                }

                onClear={
                    handleClear
                }

                /*
                 * One company / one database:
                 * false
                 *
                 * Multi-company / one database:
                 * true
                 */

                showCompanyFilter={false}
            />


            {/*================================================
                ERROR
            =================================================*/}

            {error && (

                <div className="admin-page-error">

                    <div className="admin-page-error-message">

                        <span className="admin-error-icon">
                            !
                        </span>

                        <span>
                            {error}
                        </span>

                    </div>


                    <button
                        type="button"
                        onClick={() =>
                            setError("")
                        }
                        aria-label="Close error"
                    >
                        ×
                    </button>

                </div>

            )}


            {/*================================================
                TABLE
            =================================================*/}

            <AdminTable
                admins={admins}

                loading={loading}

                onEdit={
                    handleEdit
                }

                onStatusChange={
                    handleStatusUpdate
                }

                onDelete={
                    handleDelete
                }
            />


            {/*================================================
                PAGINATION
            =================================================*/}

            <AdminPagination
                pageNumber={
                    pagination.pageNumber
                }

                pageSize={
                    pagination.pageSize
                }

                totalRecords={
                    pagination.totalRecords
                }

                totalPages={
                    pagination.totalPages
                }

                onPageChange={
                    handlePageChange
                }

                onPageSizeChange={
                    handlePageSizeChange
                }
            />

        </div>

    );

}