
import {
    BrowserRouter,
    Routes,
    Route,
    Navigate,
} from "react-router-dom";


//==========================================================
// PUBLIC PAGES
//==========================================================

import Login
    from "../pages/Login/Login";

import Register
    from "../pages/Register/Register";


//==========================================================
// DASHBOARD
//==========================================================

import Dashboard
    from "../pages/dashboard/Dashboard";


//==========================================================
// ADMIN
//==========================================================

// import AdminRegistration
//     from "../pages/Admin/AdminRegistration";


//==========================================================
// EMPLOYEE
//==========================================================

import EmployeeRegistration
    from "../pages/Employee/EmployeeRegistration";


    //==========================================================
// EMPLOYEE
//==========================================================

import CompanyManagement
    from "../pages/Company/CompanyManagement";


//==========================================================
// REPORTS
//==========================================================

import BusinessReport
    from "../pages/Reports/BusinessReport/BusinessReport";


//==========================================================
// ITEM MASTER
//==========================================================

import ItemMaster
    from "../pages/ItemMaster/ItemMaster";


//==========================================================
// RECEIPT
//==========================================================

import ReceiptEntry
    from "../pages/Receipt/ReceiptEntry";



//==========================================================
// payment entry
//==========================================================




import PaymentEntry from "../pages/Payment/PaymentEntry";    


//==========================================================
// SALES
//==========================================================

import SalesEntry
    from "../pages/Sales/SalesEntry/SalesEntry";


//==========================================================
// STOCK ITEM
//==========================================================

import StockItemEntry
    from "../pages/Master/StockItem/StockItemEntry/StockItemEntry";


//==========================================================
// ACCOUNT
//==========================================================

import AccountEntry
    from "../pages/Master/Account/AccountEntry/AccountEntry";


//==========================================================
// UNIT
// IMPORTANT:
// Unit is directly inside Master.
//==========================================================

import UnitEntry
    from "../pages/Master/Unit/UnitEntry/UnitEntry";



//==========================================================
// ADMIN MANAGEMENT
//==========================================================

import AdminEntry
    from "../pages/UserManagement/Admin/AdminEntry/AdminEntry";

import AdminRegistration
    from "../pages/UserManagement/Admin/AdminRegistration/AdminRegistration";

import AdminEdit
    from "../pages/UserManagement/Admin/AdminEdit/AdminEdit";







//==========================================================
// AUTH SERVICE
//==========================================================

import {
    isAuthenticated,
} from "../services/authService";


//==========================================================
// PROTECTED ROUTE
//==========================================================

function ProtectedRoute({
    children,
}) {

    if (!isAuthenticated()) {

        return (

            <Navigate
                to="/login"
                replace
            />

        );
    }

    return children;
}


//==========================================================
// PUBLIC ROUTE
//==========================================================

function PublicRoute({
    children,
}) {

    if (isAuthenticated()) {

        return (

            <Navigate
                to="/"
                replace
            />

        );
    }

    return children;
}


//==========================================================
// APP ROUTES
//==========================================================

export default function AppRoutes() {

    return (

        <BrowserRouter>

            <Routes>


                {/*==================================================
                    LOGIN
                ==================================================*/}

                <Route
                    path="/login"
                    element={

                        <PublicRoute>

                            <Login />

                        </PublicRoute>

                    }
                />


                {/*==================================================
                    CUSTOMER REGISTRATION
                ==================================================*/}

                <Route
                    path="/register"
                    element={

                        <PublicRoute>

                            <Register />

                        </PublicRoute>

                    }
                />


                {/*==================================================
                    DASHBOARD
                ==================================================*/}

                <Route
                    path="/"
                    element={

                        <ProtectedRoute>

                            <Dashboard />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    ADMIN REGISTRATION
                ==================================================*/}

                <Route
                    path="/admin-registration"
                    element={

                        <ProtectedRoute>

                            <AdminRegistration />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    EMPLOYEE REGISTRATION
                ==================================================*/}

                <Route
                    path="/employee-registration"
                    element={

                        <ProtectedRoute>

                            <EmployeeRegistration />

                        </ProtectedRoute>

                    }
                />




                  {/*==================================================
                    COMPANY REGISTRATION
                ==================================================*/}

                <Route
                    path="/company-registration"
                    element={

                        <ProtectedRoute>

                            <CompanyManagement />

                        </ProtectedRoute>

                    }
                />






                {/*==================================================
                    ITEM MASTER
                ==================================================*/}

                <Route
                    path="/item-master"
                    element={

                        <ProtectedRoute>

                            <ItemMaster />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    BUSINESS REPORT
                ==================================================*/}

                <Route
                    path="/business-report"
                    element={

                        <ProtectedRoute>

                            <BusinessReport />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    SALES ENTRY
                ==================================================*/}

                <Route
                    path="/sales-entry"
                    element={

                        <ProtectedRoute>

                            <SalesEntry />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    STOCK ITEM MASTER
                ==================================================*/}

                <Route
                    path="/stock-master"
                    element={

                        <ProtectedRoute>

                            <StockItemEntry />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    ACCOUNT MASTER
                ==================================================*/}

                <Route
                    path="/account-master"
                    element={

                        <ProtectedRoute>

                            <AccountEntry />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    UNIT MASTER
                ==================================================*/}

                <Route
                    path="/unit-master"
                    element={

                        <ProtectedRoute>

                            <UnitEntry />

                        </ProtectedRoute>

                    }
                />


                {/*==================================================
                    RECEIPT ENTRY
                ==================================================*/}

                <Route
                    path="/receipt-entry"
                    element={

                        <ProtectedRoute>

                            <ReceiptEntry />

                        </ProtectedRoute>

                    }
                />

                  

                   <Route
    path="/payment-entry"
    element={
        <ProtectedRoute>
            <PaymentEntry />
        </ProtectedRoute>
    }
/>







{/*==========================================================
    ADMIN MANAGEMENT
==========================================================*/}

<Route
    path="/user-management/admin"
    element={
        <ProtectedRoute>
            <AdminEntry />
        </ProtectedRoute>
    }
/>


{/*==========================================================
    CREATE ADMIN
==========================================================*/}

<Route
    path="/user-management/admin/create"
    element={
        <ProtectedRoute>
            <AdminRegistration />
        </ProtectedRoute>
    }
/>


{/*==========================================================
    EDIT ADMIN
==========================================================*/}

<Route
    path="/user-management/admin/edit/:userId"
    element={
        <ProtectedRoute>
            <AdminEdit />
        </ProtectedRoute>
    }
/>














                {/*==================================================
                    INVALID URL
                ==================================================*/}

                <Route
                    path="*"
                    element={

                        <Navigate
                            to="/login"
                            replace
                        />

                    }
                />

            </Routes>

        </BrowserRouter>

    );
}




















// import {
//   BrowserRouter,
//   Routes,
//   Route,
//   Navigate,
// } from "react-router-dom";

// import Login from "../pages/Login/Login";
// import Register from "../pages/Register/Register";
// import Dashboard from "../pages/dashboard/Dashboard";
// import AdminRegistration from "../pages/Admin/AdminRegistration";

// import BusinessReport from "../pages/Reports/BusinessReport/BusinessReport";
// import ItemMaster from "../pages/ItemMaster/ItemMaster";

// import EmployeeRegistration
// from "../pages/Employee/EmployeeRegistration";


// import ReceiptEntry from "../pages/Receipt/ReceiptEntry";



// import {
//   isAuthenticated,
// } from "../services/authService";

// import SalesEntry from "../pages/Sales/SalesEntry/SalesEntry";

// // import StockItemEntry
// // from "../pages/Master/StockItem/StockItemEntry/StockItemEntry";

// import StockItemEntry
//     from "../pages/Master/StockItem/StockItemEntry/StockItemEntry";

// //==========================================================
// // Account
// //==========================================================

// // import AccountEntry
// //     from "../pages/Account/AccountEntry/AccountEntry";

// import AccountEntry
//     from "../pages/Master/Account/AccountEntry/AccountEntry";

// import UnitEntry from "../pages/Master/Unit/UnitEntry/UnitEntry";

// // =========================
// // Protected Route
// // =========================
// function ProtectedRoute({
//   children,
// }) {
//   if (!isAuthenticated()) {
//     return (
//       <Navigate
//         to="/login"
//         replace
//       />
//     );
//   }

//   return children;
// }

// // =========================
// // Public Route
// // =========================
// function PublicRoute({
//   children,
// }) {
//   if (isAuthenticated()) {
//     return (
//       <Navigate
//         to="/"
//         replace
//       />
//     );
//   }

//   return children;
// }

// // =========================
// // App Routes
// // =========================
// export default function AppRoutes() {
//   return (
//     <BrowserRouter>

//       <Routes>

//         {/* Login */}
//         <Route
//           path="/login"
//           element={
//             <PublicRoute>
//               <Login />
//             </PublicRoute>
//           }
//         />

//         {/* Customer Registration */}
//         <Route
//           path="/register"
//           element={
//             <PublicRoute>
//               <Register />
//             </PublicRoute>
//           }
//         />
        


//          {/* Dashboard */}
//         <Route
//           path="/"
//           element={
//             <ProtectedRoute>
//               <Dashboard />
//             </ProtectedRoute>
//           }
//         />

//         {/* Create Admin */}
//         <Route
//           path="/admin-registration"
//           element={
//             <ProtectedRoute>
//               <AdminRegistration />
//             </ProtectedRoute>
//           }
//         />

         
//          <Route
//   path="/employee-registration"
//   element={
//     <ProtectedRoute>
//       <EmployeeRegistration />
//     </ProtectedRoute>
//   }
// />



//  <Route
//           path="/item-master"
//           element={
//             <ProtectedRoute>
//               < ItemMaster/>
//             </ProtectedRoute>
//           }
//         />


//         <Route
//     path="/business-report"
//     element={
//         <ProtectedRoute>
//             <BusinessReport />
//         </ProtectedRoute>

        
//     }
// />



// {/* 
//         <Route
//     path="/sales-entry"
//     element={
//         <ProtectedRoute>
//             <SalesEntry />
//         </ProtectedRoute>




        
//     }
// /> */}


//      <Route
//     path="/sales-entry"
//     element={
//         <ProtectedRoute>
//             <SalesEntry />
//         </ProtectedRoute>




        
//     }
// />



// <Route
//     path="/stock-master"
//     element={
//         <ProtectedRoute>
//             <StockItemEntry />
//         </ProtectedRoute>
//     }
// />


// <Route
//     path="/account-master"
//     element={
//         <ProtectedRoute>
//             <AccountEntry />
//         </ProtectedRoute>
//     }
// />


// <Route
//     path="/unit-master"
//     element={
//         <ProtectedRoute>
//             <UnitEntry />
//         </ProtectedRoute>
//     }
// />






//      <Route
//     path="/receipt-entry"
//     element={
//         <ProtectedRoute>
//             <ReceiptEntry />
//         </ProtectedRoute>




        
//     }
// />





 











//         {/* Invalid URL */}
//         <Route
//           path="*"
//           element={
//             <Navigate
//               to="/login"
//               replace
//             />
//           }
//         />

//       </Routes>

//     </BrowserRouter>
//   );
// }