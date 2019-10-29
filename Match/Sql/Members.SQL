/*
drop table Liker
go
drop table message
go
drop table MemberPhoto
go
drop table MemberCondition
go
drop table MemberDetail
go
drop table member
go
drop table SysTransLog
go
drop table SysUserLog
go


*/
--------------------------------------------------------------------------------------------
--會員資料檔
--------------------------------------------------------------------------------------------
--Drop table Member
go
create table Member
(
	UserId      int identity(1,1)   not null,
	--FirstName   nvarchar(16)   not null, --姓氐  
	--LastName    nvarchar(16)   not null, --名字
	NickName    nvarchar(30)   not null, --我的暱稱
	Phone       nvarchar(15)   not null, --行動電話
	Email       nvarchar(30)   not null, --電子郵件

	--以下為個人基本條件
	Sex           int          not null,--性別(1=男,2=女)
	BirthYear     int          not null,--出生日期
	Marry         int          not null,--婚姻狀況(1=單身未婚, 2=二春無小孩, 3=二春有小孩, 4=都可以)
	Education     int          not null,--學歷(1=小學,2=中學,3=高中職,4=大學(專),5=碩士,6=博士)

	--以下這些資料會印在聯誼名單的個人資料上
	Heights       int      not null, --身高
	Weights       int      not null,--體重
	Salary        int      not null,--年薪
	Blood         nvarchar(2)    null,--血型(a,b,o,ab)
	Star          nvarchar(30)   null,--星座
	City          nvarchar(30)   null,--居住城市
	School        nvarchar(30)   null,--畢業學校
	Subjects      nvarchar(30)   null,--畢業科系
	JobType       nvarchar(30)   null,--職業類別
	Religion      nvarchar(30)   null,--宗教信仰

	--以下這些資料個人特資料,可在網路查詢
	--Introduction  nvarchar(max) null,--自我介紹
	--LikeCondition nvarchar(max) null,--希望對象

	--找回密碼用
	--PdQuestion   nvarchar(30)   null, --找回密碼問題,由註冊時系統先給 我註冊時設定的密碼
	--PdAnswer     nvarchar(30)   null, --找回密碼答案,由註冊時系統先給 model.通行密碼
	
	--關檔及轉帳查核用
	IsCloseData  bit        not null,--是否關閉檔案,即不做網路配對
	IsClosePhoto bit        not null,--是否關閉相簿
	--BankName     nvarchar(30)   null, --轉帳銀行
	--BankNumber6  nvarchar(30)   null, --帳號後6碼

	--這部份內容由系統自動產生
	MainPhotoUrl  nvarchar(250)   null,--封面相片網址
	PasswordHash  varbinary(2000)   null,
	PasswordSalt  varbinary(2000)   null,
	LoginDate     datetime       null,--上次登入日期
	ActiveDate    datetime       null,--最後活動日期

	--備用欄位 身份驗證查核用
	--PhoneCode     nvarchar(16)    null,--簡訊驗證碼
	--EmailCode     nvarchar(256)   null,--郵件驗證碼
	--IdPhoto11Url  nvarchar(256)   null,--身份證正面
	--IdPhoto12Url  nvarchar(256)   null,--身份證反面
	--JobPhotoUrl   nvarchar(256)   null,--在職證明
	--SchoolUrl     nvarchar(256)   null,--學歷證明

	--以下由系統人員輸入
	UserRole      nvarchar(15)   null,--角色歸屬
	--IsBlackUser   int        not null,--是否列為黑名單
	--UnPayTimes    int        not null,--報名未繳款次數
	--ResetPassword int        not null,--必須重設密碼
	--JobCheck      int        not null,--職業查驗
	--IdCheck       int        not null,--身份證查驗
	--SchoolCheck   int        not null,--學歷查驗

	CreateTime    Datetime       Null,
	UpdateTime    Datetime       Null,
	WriteId       int            Null,
	WriteIp       Nvarchar(30)   Null, 
	constraint pk_Member primary key (UserId) 
);
Go

Create unique index Member_in1 on Member (Email asc);
create unique index Member_in2 on Member (Phone asc);
go

create trigger Member_Tr1 on Member after insert,update,delete not for replication as
begin
	Declare @TableName Nvarchar(30);
	Set @TableName='Member';

	Declare @IudType Tinyint;
	Set @IudType=0;
 
	If Exists(Select 1 From Inserted) And Not Exists(Select 1 From Deleted)
		Set @IudType = 1;    --Insert
	Else If Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @IudType = 2;    --Update
	Else If Not Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @IudType = 3;    --Delete
	
	Declare @InsertData Nvarchar(max);
	Declare @DeleteData Nvarchar(max);

	Set @InsertData=(Select * From Inserted For Json Auto);
	Set @DeleteData=(Select * From Deleted For Json Auto);

	Insert Into SysTransLog(TableName,InsertData,DeleteData,IudType) Values(@TableName,@InsertData,@DeleteData,@IudType);
end
go


--Drop table MemberDetail
go
create table MemberDetail
(
	UserId        int   not null,
	Introduction  nvarchar(max) null,--自我介紹
	LikeCondition nvarchar(max) null,--希望對象

	CreateTime    Datetime       Null,
	UpdateTime    Datetime       Null,
	WriteId       int            Null,
	WriteIp       Nvarchar(30)   Null, 
	constraint pk_MemberDetail primary key (UserId) 
);
Go

Alter table MemberDetail add constraint MemberDetail_Member
      foreign key (UserId)
      references member(UserId)
	  ON UPDATE CASCADE
	  ON DELETE CASCADE
go

create trigger MemberDetail_Tr1 on MemberDetail after insert,update,delete not for replication as
begin
	Declare @TableName Nvarchar(30);
	Set @TableName='MemberDetail';

	Declare @IudType Tinyint;
	Set @IudType=0;
 
	If Exists(Select 1 From Inserted) And Not Exists(Select 1 From Deleted)
		Set @IudType = 1;    --Insert
	Else If Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @IudType = 2;    --Update
	Else If Not Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @IudType = 3;    --Delete
	
	Declare @InsertData Nvarchar(max);
	Declare @DeleteData Nvarchar(max);

	Set @InsertData=(Select * From Inserted For Json Auto);
	Set @DeleteData=(Select * From Deleted For Json Auto);

	Insert Into SysTransLog(TableName,InsertData,DeleteData,IudType) Values(@TableName,@InsertData,@DeleteData,@IudType);
end
go


-------------------------------------------------------------------------------------
--會員相片檔
-------------------------------------------------------------------------------------
--Drop table MemberPhoto
go
create table MemberPhoto
(
	Id             int identity(1,1) not null ,
	UserId         int not null ,
	IsMain         bit not null  ,
	Descriptions   nvarchar(250) ,
	PhotoUrl       nvarchar(250) ,
	PublicId       nvarchar(250) ,
	AddedDate      datetime,

	--以下每檔資料表都會有這些欄位
	CreateTime    Datetime       Null,
	UpdateTime    Datetime       Null,
	WriteId       int            Null,
	WriteIp       Nvarchar(30)   Null, 
	constraint pk_MemberPhoto primary key (id)
);
Go

Create index MemberPhoto_n1 on MemberPhoto(userId,id) ;
go

Alter table MemberPhoto add constraint Photo_Member
      foreign key (UserId)
      references member(UserId)
	  ON UPDATE CASCADE
	  ON DELETE CASCADE
go 


create trigger tr_MemberPhoto_iud on MemberPhoto after insert,update,delete not for replication as
begin
	Declare @TableName Nvarchar(30);
	Set @TableName='MemberPhoto';

	Declare @IudType Tinyint;
	Set @IudType=0;
 
	If Exists(Select 1 From Inserted) And Not Exists(Select 1 From Deleted)
		Set @IudType = 1;    --Insert
	Else If Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @IudType = 2;    --Update
	Else If Not Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @IudType = 3;    --Delete
	
	Declare @InsertData Nvarchar(max);
	Declare @DeleteData Nvarchar(max);

	Set @InsertData=(Select * From Inserted For Json Auto);
	Set @DeleteData=(Select * From Deleted For Json Auto);

	Insert Into SysTransLog(TableName,InsertData,DeleteData,IudType) Values(@TableName,@InsertData,@DeleteData,@IudType);
end
go

--Drop table MemberCondition
go
create table MemberCondition
(
	UserId      int not null,
	Sex         int not null,
	MatchSex    int not null,

	--以下為個人基本條件
    MarryMin         int      not null,--婚姻狀況(1=單身未婚, 2=二春無小孩, 3=二春有小孩, 4=都可以)
    MarryMax         int      not null,--

	YearMin          int      not null,--最少年次
	YearMax          int      not null,--最大年次

	EducationMin     int      not null,--學歷(1=小學,2=中學,3=高中職,4=大學(專),5=碩士,6=博士)
	EducationMax     int      not null,--

	HeightsMin       int      not null,--身高
	HeightsMax       int      not null,--

	WeightsMin       int      not null,--體重
	WeightsMax       int      not null,--

	SalaryMin        int      not null,--年薪
	
	--以下這些資料會印在聯誼名單的個人資料上
	BloodInclude     nvarchar(15)   null,--血型(a,b,o,ab)
	StarInclude      nvarchar(120)  null,--星座
	CityInclude      nvarchar(120)  null,--居住城市
	JobTypeInclude   nvarchar(120)  null,--職業類別
	ReligionInclude  nvarchar(120)  null,--宗教信仰

	--以下每檔資料表都會有這些欄位
	CreateTime    Datetime       Null,
	UpdateTime    Datetime       Null,
	WriteId       int            Null,
	WriteIp       Nvarchar(30)   Null, 
	constraint pk_MemberCondition primary key (UserId) 
);
Go
Alter table MemberCondition add constraint Condition_Member
      foreign key (UserId)
      references member(UserId)
	  ON UPDATE CASCADE
	  ON DELETE CASCADE
go 


/*
公家機關
學校機關
軍警機關
金融機關
醫療機關
上市櫃公司
外商公司
中小企業
百貨休閒餐飲服務業
門市店面工作室
其他
*/

/*
佛教
道教
佛道教
基督教
天主教
伊斯蘭教
其他教
*/




























--統計會員的聯誼次數
/*

--Drop function UF_MyCounts
go
create function UF_MyCounts(@as_usrid integer)
returns integer
begin
	declare @rc integer 
	select @rc=count(userId) from activity where userId=@as_usrid;

	Return @rc;
end
go
alter table member add activityCounts as dbo.UF_MyCounts(usrid)
go
--select usrid,count(usrid) activityCounts from activity group by usrid
go



--Drop function UF_GetMainPhoto
go
create function UF_GetMainPhoto(@as_usrid integer)
returns nvarchar(256)
begin
	declare @rc nvarchar(256) 
	select top 1  @rc=PhotoUrl from photos where userId=@as_usrid and IsMain=1;

	Return @rc;
end
go
alter table member add PhotoUrl as dbo.UF_GetMainPhoto(usrid)
go

*/