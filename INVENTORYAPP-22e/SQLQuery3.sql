
SELECT *
FROM AspNetRoles

select * from MenuMasters


INSERT INTO RoleMenus(RoleId,MenuId)
VALUES
('e5aa31be-1384-4836-b2d1-4adfc81e39db',1),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',2),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',3),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',4),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',5),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',6),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',7),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',8),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',9),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',10),
('e5aa31be-1384-4836-b2d1-4adfc81e39db',11);



INSERT INTO RoleMenus(RoleId,MenuId)
VALUES
('3d2474cb-7fdc-43bf-870e-9850625e8368',1),
('3d2474cb-7fdc-43bf-870e-9850625e8368',2),
('3d2474cb-7fdc-43bf-870e-9850625e8368',3),
('3d2474cb-7fdc-43bf-870e-9850625e8368',4),
('3d2474cb-7fdc-43bf-870e-9850625e8368',5),
('3d2474cb-7fdc-43bf-870e-9850625e8368',6),
('3d2474cb-7fdc-43bf-870e-9850625e8368',7),
('3d2474cb-7fdc-43bf-870e-9850625e8368',8),
('3d2474cb-7fdc-43bf-870e-9850625e8368',10);




INSERT INTO RoleMenus(RoleId,MenuId)
VALUES
('c202f2f4-983f-447c-9d2f-0476887545b3',1),
('c202f2f4-983f-447c-9d2f-0476887545b3',4),
('c202f2f4-983f-447c-9d2f-0476887545b3',5),
('c202f2f4-983f-447c-9d2f-0476887545b3',8);


INSERT INTO RoleMenus(RoleId,MenuId)
VALUES
('e06b2370-5399-4a83-af92-5cd1b7d88077',1),
('e06b2370-5399-4a83-af92-5cd1b7d88077',4);



SELECT *
FROM RoleMenus
ORDER BY RoleId, MenuId



SELECT Id, Name
FROM AspNetRoles



INSERT INTO MenuMasters
(
    MenuName,
    ParentMenuId,
    MenuUrl,
    Icon,
    SortOrder,
    IsActive
)
VALUES
(
    'Customer Portal',
    NULL,
    '/customer-portal',
    'customer',
    12,
    1
)

SELECT * FROM MenuMasters


INSERT INTO RoleMenus
(
    RoleId,
    MenuId
)
VALUES
(
    'e06b2370-5399-4a83-af92-5cd1b7d88077',
    32
)




INSERT INTO MenuMasters
(
    MenuId,
    MenuName,
    ParentMenuId,
    MenuUrl,
    Icon,
    SortOrder,
    IsActive
)
VALUES
(
    12,
    'Create Admin',
    10,
    '/admin-registration',
    'users',
    1,
    1
);





INSERT INTO MenuMasters
(
    MenuName,
    ParentMenuId,
    MenuUrl,
    Icon,
    SortOrder,
    IsActive
)
VALUES
(
    'Create Admin',
    10,
    '/admin-registration',
    'users',
    1,
    1
);


SELECT *
FROM MenuMasters
ORDER BY MenuId DESC;