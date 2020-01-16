using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Maticsoft.Model{
	 	//MapElement
		public class MapElement
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
		private int _id;
        public int id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// key
        /// </summary>		
		private string _key;
        public string key
        {
            get{ return _key; }
            set{ _key = value; }
        }        
		/// <summary>
		/// value
        /// </summary>		
		private string _value;
        public string value
        {
            get{ return _value; }
            set{ _value = value; }
        }        
		   
	}
}