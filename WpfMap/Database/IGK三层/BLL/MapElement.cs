using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Maticsoft.Common;
namespace Maticsoft.BLL {
	 	//MapElement
		public partial class MapElement
	{
   		     
		private readonly Maticsoft.DAL.MapElement dal=new Maticsoft.DAL.MapElement();
		public MapElement()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}
		
		public bool Exists(string strWhere)
		{
			return dal.Exists(strWhere);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.MapElement model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.MapElement model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		
		public bool Delete(string strWhere)
		{
			return dal.Delete(strWhere);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.MapElement GetModel(int id)
		{
			
			return dal.GetModel(id);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		/// <param name="strWhere">筛选条件</param>
        /// <returns></returns>
		public Maticsoft.Model.MapElement GetModel(string strWhere)
		{
			
			return dal.GetModel(strWhere);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.MapElement GetModelByCache(int id)
		{
			
			string CacheKey = "MapElementModel-" + id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.MapElement)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.MapElement> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.MapElement> GetModelList(int Top,string strWhere,string filedOrder="")
		{
			DataSet ds = dal.GetList(Top,strWhere,filedOrder);
			return DataTableToList(ds.Tables[0]);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.MapElement> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.MapElement> modelList = new List<Maticsoft.Model.MapElement>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.MapElement model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.MapElement();					
													if(dt.Rows[n]["id"].ToString()!="")
				{
					model.id=int.Parse(dt.Rows[n]["id"].ToString());
				}
																																				model.key= dt.Rows[n]["key"].ToString();
																																model.value= dt.Rows[n]["value"].ToString();
																						
				
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
#endregion
   
	}
}