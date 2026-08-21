SELECT Id, Name
FROM AspNetRoles;


INSERT INTO RoleMenus
(
    RoleId,
    MenuId
)
VALUES
(
    'e5aa31be-1384-4836-b2d1-4adfc81e39db',
    33
);


INSERT INTO RoleMenus
(
    RoleId,
    MenuId
)
VALUES
(
    'e5aa31be-1384-4836-b2d1-4adfc81e39db',
    33
);



SELECT
    rm.RoleMenuId,
    r.Name AS RoleName,
    m.MenuName
FROM RoleMenus rm
INNER JOIN AspNetRoles r
ON rm.RoleId = r.Id
INNER JOIN MenuMasters m
ON rm.MenuId = m.MenuId
WHERE m.MenuId = 33;


select * from menumasters;





INSERT INTO RoleMenus
(
    RoleId,
    MenuId
)
VALUES
(
    'e5aa31be-1384-4836-b2d1-4adfc81e39db', -- SuperAdmin RoleId
    34                                      -- Create Employee MenuId
);



SELECT
    rm.RoleMenuId,
    r.Name AS RoleName,
    m.MenuName,
    m.MenuUrl
FROM RoleMenus rm
INNER JOIN AspNetRoles r
    ON rm.RoleId = r.Id
INNER JOIN MenuMasters m
    ON rm.MenuId = m.MenuId
WHERE m.MenuName = 'Create Employee';