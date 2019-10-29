--------------------------------------------------------------------------------------------
--資料庫檔案(表格)命名方式說明 


--------------------------------------------------------------------------------------------
--表格內容異動記錄檔
--drop table SysTransLog  
--------------------------------------------------------------------------------------------
Create Table SysTransLog(
   Id Bigint Identity(1,1) Not Null,
   TableName Nvarchar(30) not Null,
   InsertData Nvarchar(max) Null,
   DeleteData Nvarchar(max) Null,
   IudType Tinyint not Null,
   WriteTime Datetime Null,
   Constraint PK_SysTransLog Primary Key (Id Asc)
);
Go

Alter Table SysTransLog Add Constraint SysTransLog_Df_WriteTime Default (Getdate()) For WriteTime
Go


/*
--本系統的檔案表格的內容異動,都會用以下的 trigger 方式記錄 log,
Create Trigger Customer_Tr1 On Customer After Insert,Update,Delete Not For Replication As
begin
	Declare @TableName Nvarchar(30);
	Set @TableName='Customer';

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




--------------------------------------------------------------------------------------------
--網頁程式點擊進入動作記錄檔
--drop table SysUserLog;
--------------------------------------------------------------------------------------------
Create Table SysUserLog(
 	Id bigint identity(0,1) not null,
	UserId nvarchar(30) not null,
	Refer nvarchar(255) null,
	Destination nvarchar(255) null,
	QueryString nvarchar(255) null,
	Method nvarchar(30) null,
	IpAddress nvarchar(30) null,
	RequestTime datetime null,
   Constraint PK_SysUserLog Primary Key (Id Asc)
);
Go

Alter Table SysUserLog Add Constraint SysUserLog_Df_RequestTime Default (Getdate()) For RequestTime
Go


/*
程式部分放入如下程式即可自動記錄使用者之點擊網頁的記錄
public class actionlogattribute : system.web.mvc.actionfilterattribute
{
        public override void onresultexecuted(resultexecutedcontext filtercontext)
        {
            base.onresultexecuted(filtercontext);

            if (httpcontext.current.user.identity.isauthenticated)
            {
                using (appdbcontext db = new appdbcontext())
                {
                    aa9log20 log = new aa9log20()
                    {
                        userid = (httpcontext.current.user.identity.isauthenticated ? filtercontext.httpcontext.user.identity.name : "anonymous"),
                        refer = (filtercontext.httpcontext.request.urlreferrer == null ? filtercontext.httpcontext.request.url.absolutepath : filtercontext.httpcontext.request.urlreferrer.absolutepath),
                        //destination = filtercontext.routedata.values["controller"] + "." + filtercontext.routedata.values["action"],
                        destination = filtercontext.httpcontext.request.url.absolutepath,
                        querystring = filtercontext.httpcontext.request.querystring.tostring(),
                        method = filtercontext.httpcontext.request.httpmethod,
                        ipaddress = filtercontext.httpcontext.request.userhostaddress,
                        requesttime = system.datetime.now
                    };
                    db.aa9log20.add(log);
                    db.savechanges();
                }
            }
		}
}

既然使用了mvc，那麼就直接在app_start\filterconfig.cs中設定全站的套用，必經追蹤不是只有一兩個頁面會使用到
filters.add(new actionlogattribute());

*/
