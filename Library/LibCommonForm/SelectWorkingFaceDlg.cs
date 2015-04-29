using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibEntity;

namespace LibCommonForm
{
    public partial class SelectWorkingFaceDlg : Form
    {

        //public int workFaceId;
        //public string workFaceName;
        //public WorkingfaceTypeEnum workFaceType;
        public WorkingFace SelectedWorkingFace { get; set; }

        public SelectWorkingFaceDlg()
        {
            InitializeComponent();
            selectWorkingFaceControl1.LoadData();
        }

        //public SelectWorkingFaceDlg(params WorkingfaceTypeEnum[] workingfaceTypes)
        //{
        //    InitializeComponent();
        //    //SetFilterOn(workingfaceTypes);
        //    this.selectWorkingFaceControl1.LoadData();
        //}

        //private void SetFilterOn(WorkingfaceTypeEnum workingfaceType)
        //{
        //    this.selectWorkingFaceControl1.SetFilterOn(workingfaceType);
        //}

        //private void SetFilterOn(params WorkingfaceTypeEnum[] types)
        //{
        //    //this.selectWorkingFaceControl1.SetFilterOn(types);
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedWorkingFace = selectWorkingFaceControl1.SelectedWorkingFace;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            // 关闭窗口
            Close();
        }


    }
}
