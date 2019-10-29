--create table ma9bld10(
--	血型           nvarchar(2)    not null,--(a,b,o,ab)
--	constraint pk_ma9bld10 primary key (血型)
--)
--go
--insert into ma9bld10(血型) values('a');
--insert into ma9bld10(血型) values('b');
--insert into ma9bld10(血型) values('o');
--insert into ma9bld10(血型) values('ab');
--go

--create table ma9str10(
--	星座      nvarchar(32)   not null,
--	constraint pk_ma9str10 primary key (星座)
--)
--go
--insert into ma9bld10(星座) values('水瓶座');
--insert into ma9bld10(星座) values('雙魚座');
--insert into ma9bld10(星座) values('牡羊座');
--insert into ma9bld10(星座) values('金牛座');
--insert into ma9bld10(星座) values('雙子座');
--insert into ma9bld10(星座) values('巨蟹座');
--insert into ma9bld10(星座) values('獅子座');
--insert into ma9bld10(星座) values('處女座');
--insert into ma9bld10(星座) values('天秤座');
--insert into ma9bld10(星座) values('天蠍座');
--insert into ma9bld10(星座) values('射手座');
--insert into ma9bld10(星座) values('魔羯座');


--create table ma9cty10(
--	居住縣市      nvarchar(32)   not null,
--	constraint pk_ma9cty10 primary key (居住縣市)
--)
--go
--insert into ma9cty10(居住縣市) values('台北市');
--insert into ma9cty10(居住縣市) values('新北市');
--insert into ma9cty10(居住縣市) values('桃園市');
--insert into ma9cty10(居住縣市) values('台中市');
--insert into ma9cty10(居住縣市) values('台南市');
--insert into ma9cty10(居住縣市) values('高雄市');
--insert into ma9cty10(居住縣市) values('基隆市');
--insert into ma9cty10(居住縣市) values('宜蘭縣');
--insert into ma9cty10(居住縣市) values('新竹縣市');
--insert into ma9cty10(居住縣市) values('苗栗縣');
--insert into ma9cty10(居住縣市) values('彰化縣');
--insert into ma9cty10(居住縣市) values('雲林縣');
--insert into ma9cty10(居住縣市) values('南投縣');
--insert into ma9cty10(居住縣市) values('嘉義縣市');
--insert into ma9cty10(居住縣市) values('屏東縣');
--insert into ma9cty10(居住縣市) values('花蓮縣');
--insert into ma9cty10(居住縣市) values('台東縣');
--insert into ma9cty10(居住縣市) values('澎湖金馬');
--insert into ma9cty10(居住縣市) values('其他');


create table ma9rgn10(
	宗教           nvarchar(32)   not null,
	constraint pk_ma9rgn10 primary key (宗教)
)
go
insert into ma9rgn10(宗教) values('佛教');
go
insert into ma9rgn10(宗教) values('道教');
go
insert into ma9rgn10(宗教) values('基督教');
go
insert into ma9rgn10(宗教) values('天主教');
go
insert into ma9rgn10(宗教) values('回教');
go
insert into ma9rgn10(宗教) values('印度教');
go
insert into ma9rgn10(宗教) values('其他');
go


create table ma9job10(
	職業類別      nvarchar(32)   not null,
	constraint pk_ma9job10 primary key (職業類別)
)
go
insert into ma9job10(職業類別) values('公務機關');
go
insert into ma9job10(職業類別) values('學校機關');
go
insert into ma9job10(職業類別) values('軍警機關');
go
insert into ma9job10(職業類別) values('國營事業業');
go
insert into ma9job10(職業類別) values('金融金控業');
go
insert into ma9job10(職業類別) values('金融保險業');
go
insert into ma9job10(職業類別) values('電子科技業');
go
insert into ma9job10(職業類別) values('醫療生技業');
go
insert into ma9job10(職業類別) values('傳統製造業');
go
insert into ma9job10(職業類別) values('國際貿易業');
go
insert into ma9job10(職業類別) values('門市服務業');
go
insert into ma9job10(職業類別) values('餐飲業');
go
insert into ma9job10(職業類別) values('交通業');
go
insert into ma9job10(職業類別) values('休閒旅遊業');
go
insert into ma9job10(職業類別) values('醫師律師會計師');
go


create table ma9int10(
	興趣      nvarchar(32)   not null,
	constraint pk_ma9int10 primary key (興趣)
)
go
insert into ma9int10(興趣) values('看書');
go
insert into ma9int10(興趣) values('看電視');
go
insert into ma9int10(興趣) values('唱歌');
go
insert into ma9int10(興趣) values('聽音樂');
go
insert into ma9int10(興趣) values('看電影');
go
insert into ma9int10(興趣) values('爬山健行');
go
insert into ma9int10(興趣) values('郊外踏青');
go
insert into ma9int10(興趣) values('看電影');
go
insert into ma9int10(興趣) values('看電影');
go


create table ma9pnl10(
	個性      nvarchar(32)   not null,
	constraint pk_ma9pnl10 primary key (個性)
)
go
insert into ma9pnl10(個性) values('温和');
go
insert into ma9pnl10(個性) values('獨立');
go
insert into ma9pnl10(個性) values('包容');
go
insert into ma9pnl10(個性) values('樂觀');
go
insert into ma9pnl10(個性) values('沉默');
go

create table ma9skl10(
	專長      nvarchar(32)   not null,
	constraint pk_ma9skl10 primary key (專長)
)
go
insert into ma9skl10(專長) values('電腦');
go
insert into ma9skl10(專長) values('音樂');
go
insert into ma9skl10(專長) values('烹調');
go
insert into ma9skl10(專長) values('水電');
go
insert into ma9skl10(專長) values('健身');
go






