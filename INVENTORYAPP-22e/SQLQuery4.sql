DELETE FROM RoleMenus;

SELECT Id, Name
FROM AspNetRoles;

SELECT Id, Name
FROM AspNetRoles;


INSERT INTO RoleMenus(RoleId, MenuId)
SELECT
'e5aa31be-1384-4836-b2d1-4adfc81e39db',
MenuId
FROM MenuMasters;


SELECT *
FROM RoleMenus
WHERE RoleId =
'e5aa31be-1384-4836-b2d1-4adfc81e39db';

INSERT INTO RoleMenus(RoleId, MenuId)
SELECT
'3d2474cb-7fdc-43bf-870e-9850625e8368',
MenuId
FROM MenuMasters
WHERE MenuId NOT IN (30,31);


INSERT INTO RoleMenus(RoleId, MenuId)
VALUES
('c202f2f4-983f-447c-9d2f-0476887545b3',1),   -- Dashboard
('c202f2f4-983f-447c-9d2f-0476887545b3',3),   -- Purchase
('c202f2f4-983f-447c-9d2f-0476887545b3',4),   -- Sales
('c202f2f4-983f-447c-9d2f-0476887545b3',5),   -- Inventory
('c202f2f4-983f-447c-9d2f-0476887545b3',15),  -- Purchase Entry
('c202f2f4-983f-447c-9d2f-0476887545b3',16),  -- Purchase Return
('c202f2f4-983f-447c-9d2f-0476887545b3',17),  -- Sales Entry
('c202f2f4-983f-447c-9d2f-0476887545b3',18),  -- Sales Return
('c202f2f4-983f-447c-9d2f-0476887545b3',19),  -- Stock Ledger
('c202f2f4-983f-447c-9d2f-0476887545b3',20);  -- Stock Transfer



INSERT INTO RoleMenus(RoleId, MenuId)
VALUES
('e06b2370-5399-4a83-af92-5cd1b7d88077',1),   -- Dashboard
('e06b2370-5399-4a83-af92-5cd1b7d88077',4),   -- Sales
('e06b2370-5399-4a83-af92-5cd1b7d88077',25);  -- Sales Report


SELECT
R.Name,
M.MenuName
FROM RoleMenus RM
INNER JOIN AspNetRoles R
ON RM.RoleId = R.Id
INNER JOIN MenuMasters M
ON RM.MenuId = M.MenuId
ORDER BY R.Name,M.SortOrder;


select * from MenuMasters


SELECT Id, Name
FROM AspNetRoles;


select * from menumasters;

UPDATE MenuMasters
SET MenuUrl = '/business-report'
WHERE MenuId = 25;



SELECT * from gsal;

SELECT * from gsaldet;



SELECT
    Id,
    qty,
    IsActive,
    GSalId
FROM gsaldet
WHERE docno = 'SAL000011'
ORDER BY Id;