using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaginationPro.Controler
{
    /// <summary>
    /// Pagination.xaml 的交互逻辑
    /// </summary>
    public partial class Pagination : UserControl
    {
        #region 属性
        public string tableName { get; set; }
        public string where { get; set; } = "";
        private string total { get; set; }
        private int totalPage { get; set; }
        public string SlcItemIndex { get; set; } = "0";
        private string Path { get; set; } = "1";
        private string limit = "10";
        #endregion

        #region 跳转事件注册
        public static readonly RoutedEvent TurnPressEvent =
             EventManager.RegisterRoutedEvent("Turn", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pagination));

        public event RoutedEventHandler TurnPress
        {
            add
            {
                AddHandler(TurnPressEvent, value);
            }

            remove
            {
                RemoveHandler(TurnPressEvent, value);
            }
        }
        #endregion
        #region 上一页事件注册
        public static readonly RoutedEvent PreClickEvent =
             EventManager.RegisterRoutedEvent("PreClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pagination));

        public event RoutedEventHandler PreClick
        {
            add
            {
                AddHandler(PreClickEvent, value);
            }

            remove
            {
                RemoveHandler(PreClickEvent, value);
            }
        }
        #endregion
        #region 下一页事件注册
        public static readonly RoutedEvent NextClickEvent =
             EventManager.RegisterRoutedEvent("NextClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pagination));

        public event RoutedEventHandler NextClick
        {
            add
            {
                AddHandler(NextClickEvent, value);
            }

            remove
            {
                RemoveHandler(NextClickEvent, value);
            }
        }
        #endregion
        #region Loaded事件注册
        public static readonly RoutedEvent PageLoadedEvent =
             EventManager.RegisterRoutedEvent("PageLoaded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pagination));

        public event RoutedEventHandler PageLoaded
        {
            add
            {
                AddHandler(PageLoadedEvent, value);
            }

            remove
            {
                RemoveHandler(PageLoadedEvent, value);
            }
        }
        #endregion
        #region 分页条数选择事件注册
        public static readonly RoutedEvent SelectedChangeEvent =
             EventManager.RegisterRoutedEvent("SelectedChange", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pagination));

        public event RoutedEventHandler SelectedChange
        {
            add
            {
                AddHandler(SelectedChangeEvent, value);
            }

            remove
            {
                RemoveHandler(SelectedChangeEvent, value);
            }
        }
        #endregion

        #region 初始化
        public Pagination()
        {
            InitializeComponent();
        }
        private void Turn_Loaded(object sender, RoutedEventArgs e)
        {
            thisPath.Content = Path;
            if(where!="") total=Cache.DB.Ado.GetDataTable("SELECT COUNT(*) FROM " + tableName+" WHERE "+where).Rows[0][0].ToString(); //查询总条数
            else total = Cache.DB.Ado.GetDataTable("EXPLAIN SELECT * FROM " + tableName).Rows[0][9].ToString(); //查询总条数
            totalPage = ZC(Convert.ToInt32(total), Convert.ToInt32(10)); //初始化总页数
            Total.Text = "共" + totalPage.ToString() + "页";
            if (SlcItemIndex != "0" && SlcItemIndex != "1" && SlcItemIndex != "2") return;
            try { slc.SelectedIndex = Convert.ToInt32(SlcItemIndex); } catch (Exception) { return; }
            Cache.PageCache = ExCommand(tableName, "", "10"); //传出数据
            RoutedEventArgs args = new RoutedEventArgs(PageLoadedEvent, this); //外部事件属性路由
            RaiseEvent(args);
        }
        #endregion

        #region 事件
        //下一页
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if(totalPage.ToString()==Path) return; //达到最后一页
            Path = (Convert.ToInt32(Path)+1).ToString(); //当前页数+1
            thisPath.Content = Path;
            limit = GetLimit(Path, slc.SelectedIndex); 
            Total.Text = "共" + totalPage.ToString() + "页";
            Cache.PageCache = ExCommand(tableName, where, limit);
            RoutedEventArgs args = new RoutedEventArgs(NextClickEvent, this);
            RaiseEvent(args);
        }
        //上一页
        private void Pre_Click(object sender, RoutedEventArgs e)
        {
            if (Path=="1"||thisPath.Content.ToString()=="1") return; //达到第一页
            Path = (Convert.ToInt32(Path) - 1).ToString(); //当前页数-1
            thisPath.Content = Path;
            limit = GetLimit(Path, slc.SelectedIndex);
            Total.Text = "共" + totalPage.ToString() + "页";
            Cache.PageCache = ExCommand(tableName, where, limit);
            RoutedEventArgs args = new RoutedEventArgs(PreClickEvent, this);
            RaiseEvent(args);
        }
        //切换每页显示行数
        private void slc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Path = "1";
            thisPath.Content = Path; //初始化
            limit = GetLimit(Path, slc.SelectedIndex);
            Total.Text = "共" + totalPage.ToString() + "页";
            Cache.PageCache = ExCommand(tableName, where, limit);
            RoutedEventArgs args = new RoutedEventArgs(SelectedChangeEvent, this);
            RaiseEvent(args);
        }
        //跳转
        private void Turn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int turnPath = 1;
                try { turnPath = Convert.ToInt32(this.Turn.Text); } catch (Exception) {  return; }
                if (turnPath > totalPage) return;
                Path = turnPath.ToString();
                thisPath.Content = Path;
                limit = GetLimit(Path, slc.SelectedIndex);
                Total.Text = "共" + totalPage.ToString() + "页";
                Cache.PageCache = ExCommand(tableName, where, limit);
                RoutedEventArgs args = new RoutedEventArgs(TurnPressEvent, this);
                RaiseEvent(args);
            }
        }
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int ZC(int x, int y)
        {
            return (x % y) == 0 ? x / y : (x / y) + 1;　//x除以y，有余数结果加1
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">限制条件</param>
        /// <param name="limit">分页条件</param>
        /// <returns></returns>
        private DataTable ExCommand(string tableName, string where, string limit)
        {
            string sql = "";
            if (where != "" && limit != "")
                sql = "select * from " + tableName + " where " + where + " limit " + limit;
            else if (where != "" && limit == "")
                sql = "select * from " + tableName + " where " + where;
            else if (where == "" && limit != "")
                sql = "select * from " + tableName + " limit " + limit;
            else
                sql = "select * from " + tableName;
            return Cache.DB.Ado.GetDataTable(sql);
        }
        /// <summary>
        /// 获取分页条件
        /// </summary>
        /// <param name="Path">当前页</param>
        /// <param name="index">分页条数选择</param>
        /// <returns></returns>
        private string GetLimit(string Path, int index)
        {
            string Limit = "";
            switch (index)
            {
                case 0:
                    totalPage = ZC(Convert.ToInt32(total), 10);
                    Limit = ((Convert.ToInt32(Path) - 1) * 10) + ",10";
                    break;
                case 1:
                    totalPage = ZC(Convert.ToInt32(total), 20);
                    Limit = ((Convert.ToInt32(Path) - 1) * 20) + ",20";
                    break;
                case 2:
                    totalPage = 1;
                    Limit = "";
                    break;
                default:
                    break;
            }
            return Limit;
        }
        #endregion

    }
}
