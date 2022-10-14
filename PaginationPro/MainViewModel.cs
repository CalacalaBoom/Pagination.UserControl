using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationPro
{
    public class Item
    {
        public string Dmondtov2Name { get; set; }
        public string FractionalDigits { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string Unit { get; set; }
        public string Privilege { get; set; }
        public string TrafficSaving { get; set; }
        public string DeadValue { get; set; }
        public string Memo { get; set; }
        public string Encoding { get; set; }
        public string StringByteOrder { get; set; }
        public string CharCount { get; set; }
        public string Id { get; set; }
        public string IsDeviceChanged { get; set; }
        public string TaskState { get; set; }
    }

    public class MainViewModel:Screen,IShell
    {
        /// <summary>
        /// DataGrid数据源
        /// </summary>
        private ObservableCollection<Item> _Items = new ObservableCollection<Item>();

        //绑定的属性名为ItemsBOM
        public ObservableCollection<Item> dtgdData
        {
            get => _Items;
            set
            {
                if (dtgdData == value) return;
                _Items = value;
                NotifyOfPropertyChange(() => dtgdData);
            }
        }

        public MainViewModel()
        {
            
        }

        private void Add()
        {
            dtgdData.Clear();
            foreach (DataRow row in Cache.PageCache.Rows)
            {
                dtgdData.Add(new Item()
                {
                    Dmondtov2Name = row["Dmondtov2Name"].ToString(),
                    FractionalDigits = row["FractionalDigits"].ToString(),
                    GroupId = row["GroupId"].ToString(),
                    GroupName = row["GroupName"].ToString(),
                    Unit = row["Unit"].ToString(),
                    Privilege = row["Privilege"].ToString(),
                    TrafficSaving = row["TrafficSaving"].ToString(),
                    DeadValue = row["DeadValue"].ToString(),
                    Memo = row["Memo"].ToString(),
                    Encoding = row["Encoding"].ToString(),
                    StringByteOrder = row["StringByteOrder"].ToString(),
                    CharCount = row["CharCount"].ToString(),
                    Id = row["Id"].ToString(),
                    IsDeviceChanged = row["IsDeviceChanged"].ToString(),
                    TaskState = row["TaskState"].ToString()
                });
            }
        }

        public void Loaded()
        {
            Add();
        }
        public void SlcChange()
        {
            Add();
        }
        public void NextClick()
        {
            Add();
        }
        public void PreClick()
        {
            Add();
        }
        public void TurnPress()
        {
            Add();
        }
    }
}
