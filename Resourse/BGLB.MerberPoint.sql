USE master
if exists (select * from sysdatabases where name='BGLB_MerberPoint')
DROP DATABASE BGLB_MerberPoint
CREATE DATABASE BGLB_MerberPoint
go
use BGLB_MerberPoint
-- 表名：基础数据类别(Categories)
-- 类别名字 C_Category varchar(20)
-- 类别说明 C_Illustration varchar(20)
-- 是否在界面上显示 C_IsShow bit
CREATE TABLE Categories(
C_Category varchar(20) primary key,
C_Illustration varchar(20),
C_IsShow bit
)
-- 表名：基础数据项(CategoryItems)
-- 类别名字 C_Category varchar(20)
-- 类别项编号 CI_ID int
-- 类别项名称 CI_Name varchar(20)
CREATE TABLE CategoryItems(
C_Category varchar(20) primary key,
CI_ID int,
CI_Name varchar(20)
FOREIGN KEY (C_Category) REFERENCES Categories(C_Category)
)



-- 表名：店铺(Shops)
--店铺编号 S_ID int TRUE 　 TRUE
--店铺名称 S_Name varchar(20) 　 　 　
--店铺类别 S_Category int 　 　 　
--联系人 S_ContactName varchar(20) 　 　 　	
--联系电话 S_ContactTel varchar(20) 　 　 　
--地址 S_Address varchar(50) 　 　 　
--备注 S_Remark varchar(100) 　 　 　
--是否已分配管理员 S_IsHasSetAdmin bit 　 　 　
--加盟时间 S_CreateTime datetime 　 　 　
CREATE TABLE Shops(
S_ID int PRIMARY KEY IDENTITY(1,1),
S_Name varchar(20),
S_Category int,
S_ContactName varchar(20),
S_ContactTel varchar(20),
S_Address varchar(50) ,
S_Remark varchar(100),
S_IsHasSetAdmin bit,
S_CreateTime datetime,
)
--表名：用户(Users)
--用户编号 U_ID int
--店铺编号 S_ID int
--用户登录名 U_LoginName nvarchar(20)
--密码 U_Password nvarchar(50)
--真实姓名 U_RealName nvarchar(20)
--性别 U_Sex nvarchar(10)
--电话 U_Telephone nvarchar(20)
--角色 U_Role int
--是否可以删除 U_CanDelete bit
CREATE TABLE Users(
U_ID int PRIMARY KEY IDENTITY(1,1),
S_ID int,
U_LoginName nvarchar(20),
U_Password nvarchar(50),
U_RealName nvarchar(20),
U_Sex nvarchar(10),
U_Telephone nvarchar(20),
U_Role int,
U_CanDelete bit
FOREIGN KEY (S_ID) REFERENCES Shops(S_ID)
)
--表名：会员等级(CardLevels)
--等级编号 CL_ID int
--等级名称 CL_LevelName nvarchar(20) 　
--所需最大积分 CL_NeedPoint nvarchar(50) 　
--积分比例 CL_Point float 　
--折扣比例 CL_Percent float 　
CREATE TABLE CardLevels(
CL_ID int PRIMARY KEY IDENTITY(1,1),
CL_LevelName nvarchar(20),
CL_NeedPoint nvarchar(50),
CL_Point float,
CL_Percent float 
)


--表名：会员(MemCards)
--会员编号 MC_ID int
--会员等级 CL_ID int
--店铺编号 S_ID int
--会员卡号 MC_CardID nvarchar(50)
--卡片密码 MC_Password nvarchar(20)
--会员姓名 MC_Name nvarchar(20)
--会员性别 MC_Sex int
--手机号码 MC_Mobile nvarchar(50)
--靓照 MC_Photo varchar(200)
--会员生日--月 MC_Birthday_Month int
--会员生日--日 MC_Birthday_Day int
--会员生日类型 MC_BirthdayType tinyint
--是否设置卡片过期时间 MC_IsPast tinyint
--卡片过期时间 MC_PastTime datetime
--当前积分 MC_Point int
--卡片付费 MC_Money real
--累计消费 MC_TotalMoney real
--累计消费次数 MC_TotalCount int
--卡片状态 MC_State int
--积分是否可以自动换成等级 MC_IsPointAuto tinyint
--推荐人ID MC_RefererID int
--推荐人卡号 MC_RefererCard nvarchar(50)
--推荐人姓名 MC_RefererName nvarchar(20)
--登记日期 MC_CreateTime datetime
CREATE TABLE MemCards(
MC_ID int PRIMARY KEY IDENTITY(800000,1),
CL_ID int FOREIGN KEY(Cl_ID) REFERENCES CardLevels(CL_ID),
S_ID int FOREIGN KEY(S_ID) REFERENCES Shops(S_ID),
MC_CardID nvarchar(50),-- 设置自增
MC_Password nvarchar(20),
MC_Name nvarchar(20),
MC_Sex int,
MC_Mobile nvarchar(50),
MC_Photo varchar(200),
MC_Birthday_Month int,
MC_Birthday_Day int,
MC_BirthdayType tinyint,
MC_IsPast tinyint,
MC_PastTime datetime,
MC_Point int,
MC_Money real,
MC_TotalMoney real,
MC_TotalCount int,
MC_State int,
MC_IsPointAuto tinyint,
MC_RefererID int,
MC_RefererCard nvarchar(50),
MC_RefererName nvarchar(20),
MC_CreateTime datetime
)


--表名：礼品(ExchangGifts)
--礼品编号 EG_ID int
--店铺编号 S_ID int
--礼品编码 EG_GiftCode varchar(255)
--礼品名称 EG_GiftName varchar(255)
--礼品图片 EG_Photo varchar(255)
--所需积分 EG_Point int
--总数量 EG_Number int
--已兑换的数量 EG_ExchangNum int
--备注 EG_Remark varchar(255)
CREATE TABLE ExchangGifts(
EG_ID int PRIMARY KEY IDENTITY(1,1),
S_ID int FOREIGN KEY (S_ID) REFERENCES Shops(S_ID),
EG_GiftCode varchar(255),
EG_GiftName varchar(255),
EG_Photo varchar(255),
EG_Point int,
EG_Number int,
G_ExchangNum int,
EG_Remark varchar(255)
)


--表名：消费订单(ConsumeOrders)
--消费编号 CO_ID int
--店铺编号 S_ID int
--用户编号 U_ID int
--订单号 CO_OrderCode nvarchar(20)
--订单类型 CO_OrderType tinyint
--会员编号 MC_ID int
--会员卡号 MC_CardID nvarchar(50)
--礼品编号 EG_ID int
--额度 CO_TotalMoney money
--实际支付 CO_DiscountMoney money
--兑/减积分 CO_GavePoint int
--积分返现 CO_Recash money
--备注 CO_Remark varchar(255)
--消费时间 CO_CreateTime datetime
CREATE TABLE ConsumeOrders(
CO_ID int PRIMARY KEY IDENTITY(1,1),
S_ID int FOREIGN KEY(S_ID) REFERENCES Shops(S_ID),
U_ID int FOREIGN KEY(U_ID) REFERENCES Users(U_ID),
CO_OrderCode nvarchar(20),
CO_OrderType tinyint,
MC_ID int,
MC_CardID nvarchar(50),
EG_ID int,
CO_TotalMoney money,
CO_DiscountMoney money,
CO_GavePoint int,
CO_Recash money,
CO_Remark varchar(255),
CO_CreateTime datetime
)



--表名：转帐记录(TransferLogs)
--转帐编号 TL_ID int
--店铺编号 S_ID int
--用户编号 U_ID int
--转出会员编号 TL_FromMC_ID int
--转出会员卡号 TL_FromMC_CardID nvarchar(50)
--转入会员编号 TL_ToMC_ID int
--转入会员卡号 TL_ToMC_CardID nvarchar(50)
--转帐金额 TL_TransferMoney money
--转帐备注 TL_Remark varchar(200)
--转帐日期 TL_CreateTime datetime
CREATE TABLE TransferLogs(
TL_ID int PRIMARY KEY IDENTITY(1,1),
S_ID int FOREIGN KEY (S_ID) REFERENCES Shops(S_ID),
U_ID int FOREIGN KEY (U_ID) REFERENCES Users(U_ID),
TL_FromMC_ID int,
TL_FromMC_CardID nvarchar(50),
TL_ToMC_ID int,
TL_ToMC_CardID nvarchar(50),
TL_TransferMoney money,
TL_Remark varchar(200),
TL_CreateTime datetime,
)
--表名：兑换记录(ExchangLogs)
--兑换记录编号 EL_ID int
--店铺编号 S_ID int
--用户编号 U_ID int
--会员编号 MC_ID int
--会员卡号 MC_CardID nvarchar(50)
--会员姓名 MC_Name nvarchar(20)
--礼品编号 EG_ID int
--礼品编码 EG_GiftCode nvarchar(50)
--礼品名称 EG_GiftName nvarchar(50)
--兑换数量 EL_Number int
--所用积分 EL_Point int
--兑换时间 EL_CreateTime datetime
CREATE TABLE ExchangLogs(
EL_ID int PRIMARY KEY IDENTITY(1,1),
S_ID int FOREIGN KEY (S_ID) REFERENCES Shops(S_ID),
U_ID int FOREIGN KEY (U_ID) REFERENCES Users(U_ID),
MC_ID int,
MC_CardID nvarchar(50),
MC_Name nvarchar(20),
EG_ID int,
EG_GiftCode nvarchar(50),
EG_GiftName nvarchar(50),
L_Number int,
EL_Point int,
EL_CreateTime datetime
)

go
--Categories表中的数据不能修改
INSERT INTO Categories(C_Category,C_Illustration,C_IsShow) VALUES
('B_State','订单状态','False'),
('C_UseCharacter', '使用性质', 'True'),
('C_VehideCategory','车辆类型','True'),
('CredentialCategory','证件类型','True'),
('G_Unit','货物单位','True'),
('N_NetCategory','网点类别','True'),
('SR_Bank','开户银行','True'),
('SR_UserCategory','客户类别','True'),
('U_Role','用户角色','False'),
('W_ArriveCategory','提件方式','False'),
('W_TransportCategory', '运输方式','False'),
('WD_WrapCategory','包装方式','True'),
('WP_PayCategory','付款方式','False')
go

INSERT INTO CardLevels(CL_LevelName,CL_NeedPoint,CL_Point,CL_Percent) VALUES
('普通会员', 100, 10, 1),
('银卡会员', 500, 8, 0.9),
('金卡会员', 1000,6, 0.8),
('至尊VIP', 5000, 5, 0.7)
