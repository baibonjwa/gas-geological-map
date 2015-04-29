using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Castle.ActiveRecord;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibCommonForm
{
    public partial class TunnelInfoEntering : Form
    {
        private int _formHeight;
        private Tunnel Tunnel { get; set; }

        /// <summary>
        ///     添加
        /// </summary>
        public TunnelInfoEntering()
        {
            InitializeComponent();
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_ADD);
            Text = Const_GM.TUNNEL_INFO_ADD;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        ///     修改
        /// </summary>
        public TunnelInfoEntering(Tunnel tunnel)
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_CHANGE);
            Text = Const_GM.TUNNEL_INFO_CHANGE;
            Tunnel = tunnel;
        }

        private void TunnelInfoEntering_Load(object sender, EventArgs e)
        {
            _formHeight = Height;
            ChangeFormSize(null);

            DataBindUtil.LoadLithology(cboLithology, "煤层");


            var hash = new Hashtable
            {
                {"主运顺槽", 0},
                {"辅运顺槽", 1},
                {"切眼", 2},
                {"回采面其他关联巷道", 3},
                {"掘进巷道", 4},
                {"横川",6 },
                {"其他地点",7},
                {"其他", 5}
            };
            var list = new ArrayList();
            foreach (DictionaryEntry entry in hash)
            {
                list.Add(entry);
            }

            cboTunnelType.DataSource = list;
            cboTunnelType.DisplayMember = "Key";
            cboTunnelType.ValueMember = "Value";


            if (Text == Const_GM.TUNNEL_INFO_ADD)
            {
                selectWorkingFaceControl1.LoadData();
                cboTunnelType.SelectedValue = (int)TunnelTypeEnum.OTHER;
            }
            else
            {
                selectWorkingFaceControl1.LoadData(Tunnel.WorkingFace);
                txtTunnelName.Text = Tunnel.TunnelName;
                cboSupportPattern.Text = Tunnel.TunnelSupportPattern;
                cboLithology.SelectedItem = Tunnel.Lithology;
                txtDesignLength.Text = Tunnel.TunnelDesignLength.ToString(CultureInfo.InvariantCulture);
                cboCoalOrStone.Text = Tunnel.CoalOrStone;
                cboTunnelType.SelectedValue = (int)Tunnel.TunnelType;
            }

        }

        private void AddTunnelInfo()
        {
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            //创建巷道实体

            var workingFace = selectWorkingFaceControl1.SelectedWorkingFace;
            using (new SessionScope())
            {
                workingFace = WorkingFace.Find(workingFace.WorkingFaceId);
                if (workingFace.Tunnels.FirstOrDefault(u => u.TunnelName == txtTunnelName.Text) != null)
                {
                    Alert.alert("该工作面下已有同名巷道！");
                    return;
                }
            }


            var tunnel = new Tunnel
            {
                TunnelName = txtTunnelName.Text,
                TunnelSupportPattern = cboSupportPattern.Text,
                WorkingFace = selectWorkingFaceControl1.SelectedWorkingFace,
                Lithology = (Lithology)cboLithology.SelectedItem,
                TunnelType = (TunnelTypeEnum)cboTunnelType.SelectedValue,
                CoalOrStone = cboCoalOrStone.Text,
                CoalSeams = CoalSeams.FindAll().First(),
                BindingId = IDGenerator.NewBindingID(),
                TunnelWid = 5
            };

            //设计长度
            if (txtDesignLength.Text != "")
            {
                tunnel.TunnelDesignLength = Convert.ToInt32(txtDesignLength.Text);
            }
            if (txtDesignArea.Text != "")
            {
                tunnel.TunnelDesignArea = Convert.ToInt32(txtDesignLength.Text);
            }
            //巷道信息登录

            tunnel.Save();
            Alert.alert("提交成功！");
        }

        private void UpdateTunnelInfo()
        {
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            Tunnel.WorkingFace = selectWorkingFaceControl1.SelectedWorkingFace;
            //巷道名称
            Tunnel.TunnelName = txtTunnelName.Text;
            //支护方式
            Tunnel.TunnelSupportPattern = cboSupportPattern.Text;
            //围岩类型
            Tunnel.Lithology = (Lithology)cboLithology.SelectedItem;
            Tunnel.CoalSeams = CoalSeams.FindAll().First();
            Tunnel.TunnelWid = 5;

            //设计长度
            if (txtDesignLength.Text != "")
            {
                Tunnel.TunnelDesignLength = Convert.ToInt32(txtDesignLength.Text);
            }
            if (txtDesignArea.Text != "")
            {
                Tunnel.TunnelDesignArea = Convert.ToInt32(txtDesignLength.Text);
            }
            //煤巷岩巷
            if (cboCoalOrStone.Text != "")
            {
                Tunnel.CoalOrStone = cboCoalOrStone.Text;
            }

            Tunnel.Save();
            Alert.alert("提交成功！");
        }


        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Text == Const_GM.TUNNEL_INFO_ADD)
            {
                AddTunnelInfo();
            }
            if (Text == Const_GM.TUNNEL_INFO_CHANGE)
            {
                UpdateTunnelInfo();
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool Check()
        {
            if (selectWorkingFaceControl1.SelectedWorkingFace == null)
            {
                Alert.alert("请选择巷道所在工作面信息");
                return false;
            }
            // 判断巷道名称是否入力
            if (String.IsNullOrWhiteSpace(txtTunnelName.Text))
            {
                txtTunnelName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("巷道名称不能为空！");
                txtTunnelName.Focus();
                return false;
            }
            txtTunnelName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            if (String.IsNullOrWhiteSpace(txtDesignLength.Text))
            {
                if (!LibCommon.Check.IsNumeric(txtDesignLength, "设计长度"))
                {
                    return false;
                }
            }

            //验证通过
            return true;
        }

        //改变窗体大小
        private void ChangeFormSize(Object gbx)
        {
            if (gbx == null)
            {
                Height = _formHeight;
            }
            else if (((GroupBox)gbx).Visible)
            {
                Height = _formHeight + ((GroupBox)gbx).Height;
            }
        }

        private void cboLithology_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCoalOrStone.Text = cboLithology.Text == @"煤层" ? "煤巷" : "岩巷";
        }
    }
}