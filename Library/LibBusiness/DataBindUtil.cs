using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LibEntity;

namespace LibBusiness
{
    public class DataBindUtil
    {
        private static void DataBindListControl(ListControl lc,
            ICollection<object> dataSource, string displayMember,
            string valueMember, String selectedText = "")
        {
            if (dataSource.Count <= 0) lc.DataSource = null;
            lc.DataSource = dataSource;
            lc.DisplayMember = displayMember;
            lc.ValueMember = valueMember;
            lc.Text = selectedText;
        }

        private static void DataBindListControl(DataGridView dgv,
            ICollection<object> dataSource)
        {
            if (dataSource.Count <= 0) return;
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = dataSource;
        }

        public static void LoadMineName(ListControl lb, String selectedText
            = "")
        {
            var mines = Mine.FindAll();
            if (mines != null)
                DataBindListControl(lb, mines, "name",
 "id", selectedText);
        }

        public static void LoadMineName(DataGridView dgv, String
            selectedText = "")
        {
            var mines = Mine.FindAll();
            DataBindListControl(dgv, mines);
        }

        public static void LoadHorizontalName(ListControl lb, int mineId,
            String selectedText = "")
        {
            var horizontals = Horizontal.FindAllByProperty("mine.id", mineId);
            if (horizontals != null)
                DataBindListControl(lb, horizontals, "name",
                    "id", selectedText);
        }

        public static void LoadHorizontalName(DataGridView dgv, int mineId)
        {
            var horizontals = Horizontal.FindAllByProperty("mine.id", mineId);
            if (horizontals != null) DataBindListControl(dgv, horizontals);
        }

        public static void LoadMiningAreaName(ListControl lb, int
            horizontalId, string selectedText = "")
        {
            var miningAreas =
                MiningArea.FindAllByProperty("horizontal.id", horizontalId);
            if (miningAreas != null)
                DataBindListControl(lb, miningAreas, "name",
                    "id", selectedText);
        }

        public static void LoadMiningAreaName(DataGridView dgv, int
            horizontalId)
        {
            var miningAreas =
                 MiningArea.FindAllByProperty("horizontal.id", horizontalId);
            if (miningAreas != null) DataBindListControl(dgv, miningAreas);
        }



        public static void LoadWorkingFaceName(ListControl lb, int
            miningAreaId, String selectedText = "")
        {
            var workingFaces =
                Workingface.FindAllByProperty("mining_area.id", miningAreaId);
            if (workingFaces != null)
                DataBindListControl(lb, workingFaces, "name",
                    "id", selectedText);
        }

        public static void LoadTunnelName(ListControl lb, int workingFaceId,
            String selectedText = "")
        {
            var tunnels = Tunnel.FindAllByProperty("workingface.id", workingFaceId);
            if (tunnels != null)
                DataBindListControl(lb, tunnels, "name", "id",
                    selectedText);
        }

        public static DataTable ToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys =
                    list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                foreach (var t in list)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(t, null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}
