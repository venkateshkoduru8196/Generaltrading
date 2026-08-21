select * from applicationuser;

select * from MenuMasters

select * from RoleMenus

delete from RoleMenus;

DELETE FROM tradinguser.MenuMasters;

DBCC CHECKIDENT ('tradinguser.MenuMasters', RESEED, 0);


INSERT INTO tradinguser.MenuMasters
(MenuName, ParentMenuId, MenuUrl, Icon, SortOrder, IsActive)
VALUES
('Dashboard',NULL,'/','dashboard',1,1),
('Master',NULL,NULL,'inventory',2,1),
('Transactions',NULL,NULL,'receipt',3,1),
('Reports',NULL,NULL,'assessment',4,1),
('User Management',NULL,NULL,'people',5,1),
('Settings',NULL,NULL,'settings',6,1);


INSERT INTO tradinguser.MenuMasters
(MenuName,ParentMenuId,MenuUrl,Icon,SortOrder,IsActive)
VALUES
('Item Master',2,'/item-master','inventory_2',1,1),
('Customer Master',2,'/customer-master','person',2,1),
('Account Master',2,'/account-master','account_balance',3,1),
('Stock Master',2,'/stock-master','warehouse',4,1),
('Unit Master',2,'/unit-master','straighten',5,1);



INSERT INTO tradinguser.MenuMasters
(MenuName,ParentMenuId,MenuUrl,Icon,SortOrder,IsActive)
VALUES
('Sales Entry',3,'/sales-entry','shopping_cart',1,1),
('Receipt Entry',3,'/receipt-entry','receipt',2,1);




INSERT INTO tradinguser.MenuMasters
(MenuName,ParentMenuId,MenuUrl,Icon,SortOrder,IsActive)
VALUES
('Business Report',4,'/business-report','bar_chart',1,1);





INSERT INTO tradinguser.MenuMasters
(MenuName,ParentMenuId,MenuUrl,Icon,SortOrder,IsActive)
VALUES
('Create Admin',5,'/admin-registration','admin_panel_settings',1,1),
('Create Employee',5,'/employee-registration','badge',2,1);


INSERT INTO tradinguser.RoleMenus(RoleId,MenuId)
SELECT
'3b852b47-b662-44fd-a993-a3572755cd25',
MenuId
FROM tradinguser.MenuMasters;


UPDATE tradinguser.MenuMasters
SET ParentMenuId = 1003
WHERE MenuName IN
(
'Item Master',
'Customer Master',
'Account Master',
'Stock Master',
'Unit Master'
);




UPDATE tradinguser.MenuMasters
SET ParentMenuId = 1004
WHERE MenuName IN
(
'Sales Entry',
'Receipt Entry'
);


UPDATE tradinguser.MenuMasters
SET ParentMenuId = 1005
WHERE MenuName='Business Report';


UPDATE tradinguser.MenuMasters
SET ParentMenuId = 1006
WHERE MenuName IN
(
'Create Admin',
'Create Employee'
);






