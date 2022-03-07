--店铺列表分页存储过程
create View VW_ShopCategoryItem
as
select * from Shops u inner join (select * from CategoryItems where C_Category='S_Category')c on S_Category=CI_ID
go

create procedure P_GetPagedShopByCondition
	@PageSize int,--表示每页显示的记录数
	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
	@RecordCount int output,--表示满足条件的记录的总数
	@S_Name varchar(20),--店铺名称
	@S_ContactName varchar(20),--联系人
	@S_Address varchar(50)--店铺地址
as
	select top (@PageSize) * from VW_ShopCategoryItem 
	where S_ID not in (
						select top (@PageSize*(@CurrentPageIndex-1)) S_ID 
						from VW_ShopCategoryItem
						where S_Name like '%'+@S_Name+'%' and S_ContactName like '%'+@S_ContactName+'%' and S_Address like '%'+@S_Address+'%' 
					  )
	and  S_Name like '%'+@S_Name+'%' and S_ContactName like '%'+@S_ContactName+'%' and S_Address like '%'+@S_Address+'%' 
	--给满足条件的记录的总数来赋值
	select @RecordCount=count(*) from VW_ShopCategoryItem where  S_Name like '%'+@S_Name+'%' and S_ContactName like '%'+@S_ContactName+'%' and S_Address like '%'+@S_Address+'%' 
go
--测试上面的存储过程
declare @r int
exec P_GetPagedShopByCondition 10,1,@r output,'','',''
select @r
select * from Shops u inner join (select * from CategoryItems where C_Category='S_Category')c on S_Category=CI_ID
select * from CategoryItems
go
--会员等级列表分页存储过程
create procedure P_GetPagedCardLevelByCondition
	@PageSize int,--表示每页显示的记录数
	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
	@RecordCount int output,--表示满足条件的记录的总数
	@CL_LevelName varchar(20)--会员等级名称
as
	select top (@PageSize) * from CardLevels 
	where CL_ID not in (
						select top (@PageSize*(@CurrentPageIndex-1)) CL_ID 
						from CardLevels
						where CL_LevelName like '%'+@CL_LevelName+'%'
					  )
	and  CL_LevelName like '%'+@CL_LevelName+'%'
	--给满足条件的记录的总数来赋值
	select @RecordCount=count(*) from CardLevels where  CL_LevelName like '%'+@CL_LevelName+'%' 
go
--测试上面的存储过程
declare @r int
exec P_GetPagedCardLevelByCondition 2,2,@r output,''
select @r
go
create view VW_MemCardCardLevel
as
select m.*,c.CL_LevelName from MemCards m inner join CardLevels c on m.CL_ID=c.CL_ID
go
select * from VW_MemCardCardLevel
go
create procedure P_GetPagedMemCardByCondition
	@PageSize int,--表示每页显示的记录数
	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
	@RecordCount int output,--表示满足条件的记录的总数
	@MC_CardID varchar(50),--会员卡号
	@MC_Name varchar(20),--会员姓名
	@MC_Mobile varchar(50),--电话
	@CL_ID int,--会员等级
	@MC_State int,--状态
	@S_ID int
as
	select top (@PageSize) * from VW_MemCardCardLevel 
	where MC_ID not in (
						select top (@PageSize*(@CurrentPageIndex-1)) MC_ID 
						from VW_MemCardCardLevel
						where MC_CardID like '%'+@MC_CardID+'%' and ((MC_Name is null) or (MC_Name like '%'+@MC_Name+'%')) and ((MC_Mobile is null) or (MC_Mobile like '%'+@MC_Mobile+'%')) and((@CL_ID=0)or(CL_ID=@CL_ID)) and ((@MC_State=0)or(MC_State=@MC_State)) and S_ID=@S_ID
					  )
	and MC_CardID like '%'+@MC_CardID+'%' and ((MC_Name is null) or (MC_Name like '%'+@MC_Name+'%')) and ((MC_Mobile is null) or (MC_Mobile like '%'+@MC_Mobile+'%')) and((@CL_ID=0)or(CL_ID=@CL_ID)) and ((@MC_State=0)or(MC_State=@MC_State)) and S_ID=@S_ID
	--给满足条件的记录的总数来赋值
	select @RecordCount=count(*) from VW_MemCardCardLevel where MC_CardID like '%'+@MC_CardID+'%' and ((MC_Name is null) or (MC_Name like '%'+@MC_Name+'%')) and ((MC_Mobile is null) or (MC_Mobile like '%'+@MC_Mobile+'%')) and((@CL_ID=0)or(CL_ID=@CL_ID)) and ((@MC_State=0)or(MC_State=@MC_State)) and S_ID=@S_ID
go
--测试上面的存储过程
declare @r int
exec P_GetPagedMemCardByCondition 10,1,@r output,'','','',0,0,2
select @r
select * from VW_MemCardCardLevel
go

create procedure P_GetPagedExchangGiftByCondition
	@PageSize int,--表示每页显示的记录数
	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
	@RecordCount int output,--表示满足条件的记录的总数
	@S_ID int
as
	select top (@PageSize) * from ExchangGifts 
	where EG_ID not in (
						select top (@PageSize*(@CurrentPageIndex-1)) EG_ID 
						from ExchangGifts where S_ID=@S_ID
					  )
	and S_ID=@S_ID
	--给满足条件的记录的总数来赋值
	select @RecordCount=count(*) from ExchangGifts where S_ID=@S_ID
go
--测试上面的存储过程
declare @r int
exec P_GetPagedExchangGiftByCondition 10,1,@r output,1
select @r
go
--消费记录分页存储过程
create view ConsumeOrderCategoryItem
as
select cc.*,mc.MC_Name,mc.MC_Mobile,mc.CL_ID from (select * from ConsumeOrders co inner join 
(select * from CategoryItems where C_Category='CO_OrderType') ca on CI_ID=CO_OrderType) cc inner join MemCards mc 
 on cc.MC_ID=mc.MC_ID
go
select * from ConsumeOrderCategoryItem
go
create procedure P_GetPagedConsumeOrderByCondition
	@PageSize int,--表示每页显示的记录数
	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
	@RecordCount int output,--表示满足条件的记录的总数
	@CO_CreateTimea datetime,--消费日期开始
	@CO_CreateTimeb datetime,--消费日期结束
	@MC_CardIDMC_Mobile nvarchar(50),--电话/卡号
	@CO_OrderCode nvarchar(20),--订单号
	@CL_ID int,--会员等级
	@CO_OrderType tinyint,--订单类型
	@CO_DiscountMoney real,--消费金额
	@CO_Recash real,--返现金额
	@CO_DE int,--消费金额条件
	@CO_GavePoint int,--减去积分
	@CO_GE int,--减去积分条件
    @S_ID int, --部门编号
	@MC_ID int --会员编号
as
	select top (@PageSize) * from ConsumeOrderCategoryItem 
	where CO_ID not in (
						select top (@PageSize*(@CurrentPageIndex-1)) CO_ID 
						from ConsumeOrderCategoryItem
						where (MC_CardID like '%'+@MC_CardIDMC_Mobile+'%' or ((MC_Mobile is null)or(MC_Mobile like '%'+@MC_CardIDMC_Mobile+'%' ))) 
						and ((@CO_OrderCode='')or(CO_OrderCode like '%'+@CO_OrderCode +'%')) and ((@CL_ID=0)or(CL_ID=@CL_ID))and
						((@CO_OrderType=0)or(CO_OrderType=@CO_OrderType)) and 
						((@CO_Recash=0)or(((@CO_DE=1) and(CO_Recash>=@CO_Recash)) or ((@CO_DE=0) and(CO_Recash<@CO_Recash)))) and 
						((@CO_DiscountMoney=0)or(((@CO_DE=1) and(CO_DiscountMoney>=@CO_DiscountMoney)) or ((@CO_DE=0) and(CO_DiscountMoney<@CO_DiscountMoney)))) and
						((@CO_GavePoint=0)or(((@CO_GE=1) and(CO_GavePoint>=@CO_GavePoint)) or ((@CO_GE=0) and(CO_GavePoint<@CO_GavePoint))))  and
						((@MC_ID=0)or (MC_ID=@MC_ID))
						and S_ID=@S_ID and (((@CO_CreateTimea='')or(CO_CreateTime >= @CO_CreateTimea)) and ((@CO_CreateTimeb='')or(CO_CreateTime <= @CO_CreateTimeb)))
					  )
	and (MC_CardID like '%'+@MC_CardIDMC_Mobile+'%' or ((MC_Mobile is null)or(MC_Mobile like '%'+@MC_CardIDMC_Mobile+'%' ))) 
						and ((@CO_OrderCode='')or(CO_OrderCode like '%'+@CO_OrderCode +'%')) and ((@CL_ID=0)or(CL_ID=@CL_ID))and
						((@CO_OrderType=0)or(CO_OrderType=@CO_OrderType)) and 
						((@CO_Recash=0)or(((@CO_DE=1) and(CO_Recash>=@CO_Recash)) or ((@CO_DE=0) and(CO_Recash<@CO_Recash)))) and 
						((@CO_DiscountMoney=0)or(((@CO_DE=1) and(CO_DiscountMoney>=@CO_DiscountMoney)) or ((@CO_DE=0) and(CO_DiscountMoney<@CO_DiscountMoney)))) and
						((@CO_GavePoint=0)or(((@CO_GE=1) and(CO_GavePoint>=@CO_GavePoint)) or ((@CO_GE=0) and(CO_GavePoint<@CO_GavePoint))))  and
						((@MC_ID=0)or (MC_ID=@MC_ID))
						and S_ID=@S_ID and (((@CO_CreateTimea='')or(CO_CreateTime >= @CO_CreateTimea)) and ((@CO_CreateTimeb='')or(CO_CreateTime <= @CO_CreateTimeb)))
	--给满足条件的记录的总数来赋值
	select @RecordCount=count(*) from ConsumeOrderCategoryItem where (MC_CardID like '%'+@MC_CardIDMC_Mobile+'%' or ((MC_Mobile is null)or(MC_Mobile like '%'+@MC_CardIDMC_Mobile+'%' ))) 
						and ((@CO_OrderCode='')or(CO_OrderCode like '%'+@CO_OrderCode +'%')) and ((@CL_ID=0)or(CL_ID=@CL_ID))and
						((@CO_OrderType=0)or(CO_OrderType=@CO_OrderType)) and 
						((@CO_Recash=0)or(((@CO_DE=1) and(CO_Recash>=@CO_Recash)) or ((@CO_DE=0) and(CO_Recash<@CO_Recash)))) and 
						((@CO_DiscountMoney=0)or(((@CO_DE=1) and(CO_DiscountMoney>=@CO_DiscountMoney)) or ((@CO_DE=0) and(CO_DiscountMoney<@CO_DiscountMoney)))) and
						((@CO_GavePoint=0)or(((@CO_GE=1) and(CO_GavePoint>=@CO_GavePoint)) or ((@CO_GE=0) and(CO_GavePoint<@CO_GavePoint))))  and
						((@MC_ID=0)or (MC_ID=@MC_ID))
						and S_ID=@S_ID and (((@CO_CreateTimea='')or(CO_CreateTime >= @CO_CreateTimea)) and ((@CO_CreateTimeb='')or(CO_CreateTime <= @CO_CreateTimeb)))
go
--测试上面的存储过程
declare @r int
exec P_GetPagedConsumeOrderByCondition 10,1,@r output,'','','','',0,0,0,0,0,0,0,2,0
select @r
select* from ConsumeOrders
select* from CategoryItems
--  @PageSize int,--表示每页显示的记录数
--	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
--	@RecordCount int output,--表示满足条件的记录的总数
--	@CO_CreateTimea datetime,--消费日期开始
--	@CO_CreateTimeb datetime,--消费日期结束
--	@MC_CardIDMC_Mobile varchar(20),--电话/卡号
--	@CO_OrderCode varchar(20),--订单号
--	@CL_ID int,--会员等级
--	@CO_OrderType int,--订单类型
--	@CO_DiscountMoney real,--消费金额
--	@CO_DE int,--消费金额条件
--	@CO_GavePoint int,--减去积分
--	@CO_GE int,--减去积分条件
--  @S_ID int, --部门编号
--	@MC_ID int --会员编号


select * from ConsumeOrders where S_ID=2

go
--用户列表分页存储过程

create view VW_UserCategoryItems
as
select * from Users u inner join (select * from CategoryItems where C_Category='U_Role') c on u.U_Role=CI_ID
go
select * from VW_UserCategoryItems
go
create procedure P_GetPagedUserByCondition
	@PageSize int,--表示每页显示的记录数
	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
	@RecordCount int output,--表示满足条件的记录的总数
	@U_LoginName varchar(20),--登录名称
	@U_RealName varchar(20),--真实名称
	@U_Telephone varchar(20),--电话
	@S_ID int
as
	select top (@PageSize) * from VW_UserCategoryItems 
	where U_ID not in (
						select top (@PageSize*(@CurrentPageIndex-1)) U_ID 
						from VW_UserCategoryItems
						where ((U_LoginName is null) or (U_LoginName like '%'+@U_LoginName+'%')) and ((U_RealName is null) or (U_RealName like '%'+@U_RealName+'%')) and ((U_Telephone is null) or (U_Telephone like '%'+@U_Telephone+'%'))and S_ID=@S_ID
					  )
	and ((U_LoginName is null) or (U_LoginName like '%'+@U_LoginName+'%')) and ((U_RealName is null) or (U_RealName like '%'+@U_RealName+'%')) and ((U_Telephone is null) or (U_Telephone like '%'+@U_Telephone+'%'))and S_ID=@S_ID
	--给满足条件的记录的总数来赋值
	select @RecordCount=count(*) from VW_UserCategoryItems where ((U_LoginName is null) or (U_LoginName like '%'+@U_LoginName+'%')) and ((U_RealName is null) or (U_RealName like '%'+@U_RealName+'%')) and ((U_Telephone is null) or (U_Telephone like '%'+@U_Telephone+'%'))and S_ID=@S_ID
go
--测试上面的存储过程
declare @r int
exec P_GetPagedUserByCondition 10,1,@r output,'','','',1
select @r

go
--兑换礼品存储过程
create view VW_ExchangLogMemCard
as
select e.*,m.MC_Mobile from ExchangLogs e inner join MemCards m on m.MC_ID=e.MC_ID
go
select * from VW_ExchangLogMemCard
go
create procedure P_GetPagedExchangLogByCondition
	@PageSize int,--表示每页显示的记录数
	@CurrentPageIndex int,--表示当前是第几页，也就是页的下标
	@RecordCount int output,--表示满足条件的记录的总数
	@StateTime datetime,--消费日期开始
	@EndTime datetime,--消费日期结束
	@MC_CardIDMC_Mobile nvarchar(50),--电话/卡号
	@S_ID int
as
	select top (@PageSize) * from VW_ExchangLogMemCard 
	where EL_ID not in (
						select top (@PageSize*(@CurrentPageIndex-1)) EL_ID 
						from VW_ExchangLogMemCard
						where (MC_CardID like '%'+@MC_CardIDMC_Mobile+'%' or ((MC_Mobile is null)or(MC_Mobile like '%'+@MC_CardIDMC_Mobile+'%' ))) 
						and (((@StateTime='')or(EL_CreateTime >= @StateTime)) and ((@EndTime='')or(EL_CreateTime <= @EndTime))) and S_ID=@S_ID
					  )
	and (MC_CardID like '%'+@MC_CardIDMC_Mobile+'%' or ((MC_Mobile is null)or(MC_Mobile like '%'+@MC_CardIDMC_Mobile+'%' ))) and (((@StateTime='')or(EL_CreateTime >= @StateTime)) and ((@EndTime='')or(EL_CreateTime <= @EndTime)))and S_ID=@S_ID
	--给满足条件的记录的总数来赋值
	select @RecordCount=count(*) from VW_ExchangLogMemCard where (MC_CardID like '%'+@MC_CardIDMC_Mobile+'%' or ((MC_Mobile is null)or(MC_Mobile like '%'+@MC_CardIDMC_Mobile+'%' ))) 
						and (((@StateTime='')or(EL_CreateTime >= @StateTime)) and ((@EndTime='')or(EL_CreateTime <= @EndTime)))and S_ID=@S_ID
go
--测试上面的存储过程
declare @r int
exec P_GetPagedExchangLogByCondition 10,1,@r output,'2010-01-03','2010-01-04','8',2
select @r

select * from ExchangLogs

update ExchangLogs set mc_CardID='800001' where  EL_ID=11



