--------------------------------------------------------------------------------------------
--會員投票檔
--------------------------------------------------------------------------------------------
--drop table Liker
go
create table Liker
(
	UserId        int               not null,
	LikerId       int               not null,
	AddedDate     datetime          not null,
	IsDelete      bit               not null,
	DeleteDate    datetime          null,

	CreateTime    Datetime       Null,
	UpdateTime    Datetime       Null,
	WriteId       int            Null,
	WriteIp       Nvarchar(30)   Null, 
	constraint Pk_liker primary key (UserId,LikerId) 
);
go


-- alter table Liker drop constraint Liker_MyLiker
go
alter table Liker add constraint Liker_MyLiker
      foreign key (LikerId)
      references Member(UserId)
	  ON UPDATE NO ACTION
	  ON DELETE NO ACTION
go

-- alter table Liker drop constraint liker_LikerMe
go
alter table Liker add constraint Liker_LikerMe
      foreign key (UserId)
      references Member(UserId)
	  ON UPDATE NO ACTION
	  ON DELETE NO ACTION
go


create trigger Liker_Tr1 on Liker after insert,update,delete not for replication as
begin
	declare @tablename nvarchar(30);
	set @tablename='Liker';

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