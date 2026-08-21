import {
  getCompaniesApi,
  getCompanyByIdApi,
  getCompanyLookupApi,
  createCompanyApi,
  updateCompanyApi,
  deleteCompanyApi,
} from "../api/companyApi";

/*
===========================================================
COMPANY SERVICE
===========================================================

Business/service layer between React pages and API.

The page should NOT directly call axios.
===========================================================
*/


/*
===========================================================
GET ALL
===========================================================
*/

export const getCompanies = async () => {
  const response =
    await getCompaniesApi();

  return response.data;
};


/*
===========================================================
GET BY ID
===========================================================
*/

export const getCompanyById = async (
  companyId
) => {
  const response =
    await getCompanyByIdApi(
      companyId
    );

  return response.data;
};


/*
===========================================================
LOOKUP
===========================================================
*/

export const getCompanyLookup = async () => {
  const response =
    await getCompanyLookupApi();

  return response.data;
};


/*
===========================================================
CREATE
===========================================================
*/

export const createCompany = async (
  data
) => {
  const response =
    await createCompanyApi(data);

  return response.data;
};


/*
===========================================================
UPDATE
===========================================================
*/

export const updateCompany = async (
  data
) => {
  const response =
    await updateCompanyApi(data);

  return response.data;
};


/*
===========================================================
DELETE
===========================================================
*/

export const deleteCompany = async (
  companyId
) => {
  const response =
    await deleteCompanyApi(
      companyId
    );

  return response.data;
};