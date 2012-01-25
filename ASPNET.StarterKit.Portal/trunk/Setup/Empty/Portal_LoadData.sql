-- ===============================================
-- CONFIG ASP.NET PORTAL STARTER KIT DATABASE
-- Load Sample Data Script
-- 
-- Version:	1.0 - 10/2002
--
-- ===============================================

-- =======================================================
-- INSERT INITIAL DATA INTO ASP.NET PORTAL STARTER KIT DB
-- =======================================================

-- point to proper DB 
use [Portal]
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
SET DATEFORMAT mdy
GO

-- insert default Portal data
INSERT INTO Portal_Roles (PortalID,RoleName) VALUES (0,'Admins')
INSERT INTO Portal_Users (Name, Password, Email) VALUES ('Guest','D0-09-1A-0F-E2-B2-09-34-D8-8B-46-06-84-F5-97-89','guest')
INSERT INTO Portal_UserRoles (UserID,RoleID) VALUES (1,0)

--
-- End load data
-- 


