USE master
if exists (select * from sysdatabases where name='BGLB_MerberPoint')
DROP DATABASE BGLB_MerberPoint
CREATE DATABASE BGLB_MerberPoint
go
use BGLB_MerberPoint
-- �����������������(Categories)
-- ������� C_Category varchar(20)
-- ���˵�� C_Illustration varchar(20)
-- �Ƿ��ڽ�������ʾ C_IsShow bit
CREATE TABLE Categories(
C_Category varchar(20) primary key,
C_Illustration varchar(20),
C_IsShow bit
)
-- ����������������(CategoryItems)
-- ������� C_Category varchar(20)
-- ������� CI_ID int
-- ��������� CI_Name varchar(20)
CREATE TABLE CategoryItems(
C_Category varchar(20) primary key,
CI_ID int,
CI_Name varchar(20)
FOREIGN KEY (C_Category) REFERENCES Categories(C_Category)
)



-- ����������(Shops)
--���̱�� S_ID int TRUE �� TRUE
--�������� S_Name varchar(20) �� �� ��
--������� S_Category int �� �� ��
--��ϵ�� S_ContactName varchar(20) �� �� ��	
--��ϵ�绰 S_ContactTel varchar(20) �� �� ��
--��ַ S_Address varchar(50) �� �� ��
--��ע S_Remark varchar(100) �� �� ��
--�Ƿ��ѷ������Ա S_IsHasSetAdmin bit �� �� ��
--����ʱ�� S_CreateTime datetime �� �� ��
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
--�������û�(Users)
--�û���� U_ID int
--���̱�� S_ID int
--�û���¼�� U_LoginName nvarchar(20)
--���� U_Password nvarchar(50)
--��ʵ���� U_RealName nvarchar(20)
--�Ա� U_Sex nvarchar(10)
--�绰 U_Telephone nvarchar(20)
--��ɫ U_Role int
--�Ƿ����ɾ�� U_CanDelete bit
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
--��������Ա�ȼ�(CardLevels)
--�ȼ���� CL_ID int
--�ȼ����� CL_LevelName nvarchar(20) ��
--���������� CL_NeedPoint nvarchar(50) ��
--���ֱ��� CL_Point float ��
--�ۿ۱��� CL_Percent float ��
CREATE TABLE CardLevels(
CL_ID int PRIMARY KEY IDENTITY(1,1),
CL_LevelName nvarchar(20),
CL_NeedPoint nvarchar(50),
CL_Point float,
CL_Percent float 
)


--��������Ա(MemCards)
--��Ա��� MC_ID int
--��Ա�ȼ� CL_ID int
--���̱�� S_ID int
--��Ա���� MC_CardID nvarchar(50)
--��Ƭ���� MC_Password nvarchar(20)
--��Ա���� MC_Name nvarchar(20)
--��Ա�Ա� MC_Sex int
--�ֻ����� MC_Mobile nvarchar(50)
--���� MC_Photo varchar(200)
--��Ա����--�� MC_Birthday_Month int
--��Ա����--�� MC_Birthday_Day int
--��Ա�������� MC_BirthdayType tinyint
--�Ƿ����ÿ�Ƭ����ʱ�� MC_IsPast tinyint
--��Ƭ����ʱ�� MC_PastTime datetime
--��ǰ���� MC_Point int
--��Ƭ���� MC_Money real
--�ۼ����� MC_TotalMoney real
--�ۼ����Ѵ��� MC_TotalCount int
--��Ƭ״̬ MC_State int
--�����Ƿ�����Զ����ɵȼ� MC_IsPointAuto tinyint
--�Ƽ���ID MC_RefererID int
--�Ƽ��˿��� MC_RefererCard nvarchar(50)
--�Ƽ������� MC_RefererName nvarchar(20)
--�Ǽ����� MC_CreateTime datetime
CREATE TABLE MemCards(
MC_ID int PRIMARY KEY IDENTITY(800000,1),
CL_ID int FOREIGN KEY(Cl_ID) REFERENCES CardLevels(CL_ID),
S_ID int FOREIGN KEY(S_ID) REFERENCES Shops(S_ID),
MC_CardID nvarchar(50),-- ��������
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


--��������Ʒ(ExchangGifts)
--��Ʒ��� EG_ID int
--���̱�� S_ID int
--��Ʒ���� EG_GiftCode varchar(255)
--��Ʒ���� EG_GiftName varchar(255)
--��ƷͼƬ EG_Photo varchar(255)
--������� EG_Point int
--������ EG_Number int
--�Ѷһ������� EG_ExchangNum int
--��ע EG_Remark varchar(255)
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


--���������Ѷ���(ConsumeOrders)
--���ѱ�� CO_ID int
--���̱�� S_ID int
--�û���� U_ID int
--������ CO_OrderCode nvarchar(20)
--�������� CO_OrderType tinyint
--��Ա��� MC_ID int
--��Ա���� MC_CardID nvarchar(50)
--��Ʒ��� EG_ID int
--��� CO_TotalMoney money
--ʵ��֧�� CO_DiscountMoney money
--��/������ CO_GavePoint int
--���ַ��� CO_Recash money
--��ע CO_Remark varchar(255)
--����ʱ�� CO_CreateTime datetime
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



--������ת�ʼ�¼(TransferLogs)
--ת�ʱ�� TL_ID int
--���̱�� S_ID int
--�û���� U_ID int
--ת����Ա��� TL_FromMC_ID int
--ת����Ա���� TL_FromMC_CardID nvarchar(50)
--ת���Ա��� TL_ToMC_ID int
--ת���Ա���� TL_ToMC_CardID nvarchar(50)
--ת�ʽ�� TL_TransferMoney money
--ת�ʱ�ע TL_Remark varchar(200)
--ת������ TL_CreateTime datetime
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
--�������һ���¼(ExchangLogs)
--�һ���¼��� EL_ID int
--���̱�� S_ID int
--�û���� U_ID int
--��Ա��� MC_ID int
--��Ա���� MC_CardID nvarchar(50)
--��Ա���� MC_Name nvarchar(20)
--��Ʒ��� EG_ID int
--��Ʒ���� EG_GiftCode nvarchar(50)
--��Ʒ���� EG_GiftName nvarchar(50)
--�һ����� EL_Number int
--���û��� EL_Point int
--�һ�ʱ�� EL_CreateTime datetime
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
--Categories���е����ݲ����޸�
INSERT INTO Categories(C_Category,C_Illustration,C_IsShow) VALUES
('B_State','����״̬','False'),
('C_UseCharacter', 'ʹ������', 'True'),
('C_VehideCategory','��������','True'),
('CredentialCategory','֤������','True'),
('G_Unit','���ﵥλ','True'),
('N_NetCategory','�������','True'),
('SR_Bank','��������','True'),
('SR_UserCategory','�ͻ����','True'),
('U_Role','�û���ɫ','False'),
('W_ArriveCategory','�����ʽ','False'),
('W_TransportCategory', '���䷽ʽ','False'),
('WD_WrapCategory','��װ��ʽ','True'),
('WP_PayCategory','���ʽ','False')
go

INSERT INTO CardLevels(CL_LevelName,CL_NeedPoint,CL_Point,CL_Percent) VALUES
('��ͨ��Ա', 100, 10, 1),
('������Ա', 500, 8, 0.9),
('�𿨻�Ա', 1000,6, 0.8),
('����VIP', 5000, 5, 0.7)
