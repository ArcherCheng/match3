
--------------------------------------------------------------------------------
--留言版 bulletin
--------------------------------------------------------------------------------
--drop table Message
go
create table Message
(
	Id            int identity(1,1) not null,
	-- PartyId       int not null,
	SenderId      int not null,
	RecipientId   int not null,
	Contents      nvarchar(1000) not null, 
	SendDate      datetime not null, 

	IsRead           bit NOT null, 
	ReadDate         datetime null, 
	SenderDeleted    bit NOT null, 
	RecipientDeleted bit NOT null, 

	CreateTime    Datetime       Null,
	UpdateTime    Datetime       Null,
	WriteId       int            Null,
	WriteIp       Nvarchar(30)   Null, 
	constraint pk_Message primary key (Id)
);
go
create index message_in1 on Message(SenderId,RecipientId) ;
go          
create index message_in2 on Message(RecipientId,SenderId) ;
go          


--alter table message drop constraint message_senderId
alter table Message add constraint Message_Sender
      foreign key (SenderId)
      references Member(UserId)
 	  --ON UPDATE CASCADE
 	  --ON DELETE No Action
go

--alter table message drop constraint message_recipientId
alter table Message add constraint Message_Recipient
      foreign key (RecipientId)
      references Member(UserId)
 	  --ON UPDATE CASCADE
 	  --ON DELETE No Action
go


create trigger Message_Tr1 on Message after insert,update,delete not for replication as
begin
	Declare @TableName Nvarchar(30);
	Set @TableName='Message';

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
