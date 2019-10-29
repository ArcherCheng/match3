--drop table GroupKeyValue
go
create table GroupKeyValue
(
	Id            int identity(1,1) not null,
	KeyGroup      nvarchar(30)      not null, 
	KeySeq        int               not null,
	KeyValue      nvarchar(30)      not null,
	KeyLabel      nvarchar(60)      not null,
	IsChecked     bit               not null,

	--CreateTime    Datetime       Null,
	--UpdateTime    Datetime       Null,
	--WriteId       int            Null,
	--WriteIp       Nvarchar(30)   Null,
	constraint pk_GroupKeyValue primary key (Id) 
);
create unique index GroupKeyValue_in1 on GroupKeyValue (KeyGroup asc,KeyValue asc);
go

/*
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
*/

go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Blood',1,'A','A',0);
GO
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Blood',2,'B','B',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Blood',3,'O','O',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Blood',4,'AB','AB',0);
go

insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',1,'水瓶座','水瓶座(1/20-2/18)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',2,'雙魚座','雙魚座(2/19-3/20)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',3,'牡羊座','牡羊座(3/21-4/19)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',4,'金牛座','金牛座(4/20-5/20)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',5,'雙子座','雙子座(5/21-6/21)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',6,'巨蟹座','巨蟹座(6/22-7/22)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',7,'獅子座','獅子座(7/23-8/22)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',8,'處女座','處女座(8/23-9/22)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',9,'天秤座','天秤座(9/23-10/23)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',10,'天蠍座','天蠍座(10/24-11/22)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',11,'射手座','射手座(11/23-12/21)',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Star',12,'魔羯座','魔羯座(12/22-1/19)',0);
go


insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',1,'基隆市','基隆市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',2,'台北市','台北市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',3,'新北市','新北市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',4,'桃園市','桃園市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',5,'新竹縣市','新竹縣市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',6,'苗栗縣','苗栗縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',7,'台中市','台中市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',8,'彰化縣','彰化縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',9,'南投縣','南投縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',10,'雲林縣','雲林縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',11,'嘉義縣市','嘉義縣市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',12,'台南市','台南市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',13,'高雄市','高雄市',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',14,'屏東縣','屏東縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',15,'宜蘭縣','宜蘭縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',16,'花蓮縣','花蓮縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',17,'台東縣','台東縣',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',18,'澎湖金馬','澎湖金馬',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',19,'中國大陸','中國大陸',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',20,'東南亞','東南亞',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',21,'美洲','美洲',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',22,'歐洲','歐洲',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',23,'澳洲','澳洲',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('City',99,'其他','其他',0);
go



insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Religion',1,'佛教','佛教',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Religion',2,'道教','道教',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Religion',3,'基督教','基督教',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Religion',4,'天主教','天主教',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Religion',5,'伊斯蘭教','伊斯蘭教',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Religion',6,'一貫道','一貫道',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Religion',99,'其他','其他',0);
go


insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',1,'公務機關','公務機關',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',2,'學校機關','學校機關',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',3,'軍警消機關','軍警消機關',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',4,'國營事業機關','國營事業機關',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',5,'上市櫃公司','上市櫃公司',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',6,'外商公司','外商公司',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',7,'一般中小企業','一般中小企業',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',8,'財團或社團法人機關','財團或社團法人機關',0);
go
insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
values('Job',99,'其他','其他',0);
go

--update GroupKeyValue set keyLabel = keyValue where keyLabal is null;

--看書
--看電視
--看電影
--聽音樂
--戶外活動
--電腦上網
--寵物
--個性
--美食


-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'看書',0);
-- go
-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'看電視',0);
-- go
-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'看電影',0);
-- go
-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'聽音樂',0);
-- go
-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'爬山健行',0);
-- go
-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'旅遊踏青',0);
-- go
-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'上網滑手機',0);
-- go
-- insert into GroupKeyValue(KeyGroup,KeySeq,KeyValue,KeyLabel,IsChecked) 
-- values('Activity',1,'玩電玩',0);
-- go



-- create table ma9pnl10(
-- 	個性      nvarchar(32)   not null,
-- 	constraint pk_ma9pnl10 primary key (個性)
-- )
-- go
-- insert into ma9pnl10(個性) values('温和');
-- go
-- insert into ma9pnl10(個性) values('獨立');
-- go
-- insert into ma9pnl10(個性) values('包容');
-- go
-- insert into ma9pnl10(個性) values('樂觀');
-- go
-- insert into ma9pnl10(個性) values('沉默');
-- go

-- create table ma9skl10(
-- 	專長      nvarchar(32)   not null,
-- 	constraint pk_ma9skl10 primary key (專長)
-- )
-- go
-- insert into ma9skl10(專長) values('電腦');
-- go
-- insert into ma9skl10(專長) values('音樂');
-- go
-- insert into ma9skl10(專長) values('烹調');
-- go
-- insert into ma9skl10(專長) values('水電');
-- go
-- insert into ma9skl10(專長) values('健身');
-- go




