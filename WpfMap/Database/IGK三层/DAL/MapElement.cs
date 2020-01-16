using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using System.Data.SQLite;
namespace Maticsoft.DAL  
{
	 	//MapElement
		public partial class MapElement
	{
   		/// <summary>
		/// 是否存在
		/// </summary>     
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MapElement");
			strSql.Append(" where ");
			                                       strSql.Append(" id = @id  ");
                            			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			return DbHelperSQLite.Exists(strSql.ToString(),parameters);
		}
		
		public bool Exists(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MapElement");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);			
				return DbHelperSQLite.Exists(strSql.ToString());
			}
			else
			{
				return false;
			}
		}
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.MapElement model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MapElement(");			
            strSql.Append("key,value");
			strSql.Append(") values (");
            strSql.Append("@key,@value");            
            strSql.Append(") ");            
            strSql.Append(";select last_insert_rowid()");		
			SQLiteParameter[] parameters = {
			            new SQLiteParameter("@key", DbType.String) ,            
                        new SQLiteParameter("@value", DbType.String)             
              
            };
			            
            parameters[0].Value = model.key;                        
            parameters[1].Value = model.value;                        
			   
			object obj = DbHelperSQLite.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                    
            	return Convert.ToInt32(obj);
                                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.MapElement model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MapElement set ");
			                                                
            strSql.Append(" key = @key , ");                                    
            strSql.Append(" value = @value  ");            			
			strSql.Append(" where id=@id ");
						
SQLiteParameter[] parameters = {
			            new SQLiteParameter("@id", DbType.Int32,8) ,            
                        new SQLiteParameter("@key", DbType.String) ,            
                        new SQLiteParameter("@value", DbType.String)             
              
            };
						            
            parameters[0].Value = model.id;                        
            parameters[1].Value = model.key;                        
            parameters[2].Value = model.value;                        
            int rows=DbHelperSQLite.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据(ID)
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MapElement ");
			strSql.Append(" where id=@id");
						SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;


			int rows=DbHelperSQLite.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		/// <summary>
		/// 删除一条数据(其他条件)
		/// </summary>		
		public bool Delete(string strWhere)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MapElement ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}

			int rows=DbHelperSQLite.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MapElement ");
			strSql.Append(" where ID in ("+idlist + ")  ");
			int rows=DbHelperSQLite.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
				
		
		/// <summary>
		/// 得到一个对象实体(ID)
		/// </summary>
		public Maticsoft.Model.MapElement GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id, key, value  ");			
			strSql.Append("  from MapElement ");
			strSql.Append(" where id=@id");
						SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			
			Maticsoft.Model.MapElement model=new Maticsoft.Model.MapElement();
			DataSet ds=DbHelperSQLite.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
																																				model.key= ds.Tables[0].Rows[0]["key"].ToString();
																																model.value= ds.Tables[0].Rows[0]["value"].ToString();
																										
				return model;
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// 得到一个对象实体(其他条件)
		/// </summary>
		public Maticsoft.Model.MapElement GetModel(string strWhere)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id, key, value  ");			
			strSql.Append("  from MapElement ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			Maticsoft.Model.MapElement model=new Maticsoft.Model.MapElement();
			DataSet ds=DbHelperSQLite.Query(strSql.ToString());
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
																																				model.key= ds.Tables[0].Rows[0]["key"].ToString();
																																model.value= ds.Tables[0].Rows[0]["value"].ToString();
																										
				return model;
			}
			else
			{
				return null;
			}
			
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM MapElement ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQLite.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			//if(Top>0)
			//{
			//	strSql.Append(" top "+Top.ToString());
			//}
			strSql.Append(" * ");
			strSql.Append(" FROM MapElement ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			 if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            if (Top > 0)
            {
                strSql.Append(" limit " + Top);
            }
			return DbHelperSQLite.Query(strSql.ToString());
		}

   
	}
}

