using System;
using System.Collections;
using System.Drawing;
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
        private Tunnel tunnel { set; get; }

        /// <summary>
        ///     添加
        /// </summary>
        public TunnelInfoEntering()
        {
            InitializeComponent();
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
            tunnel = tunnel;
        }

        private void TunnelInfoEntering_Load(object sender, EventArgs e)
        {
            _formHeight = Height;
            ChangeFormSize(null);

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


            if (tunnel == null)
            {
                selectWorkingFaceControl1.LoadData();
                cboTunnelType.SelectedValue = (int)TunnelTypeEnum.OTHER;
            }
            else
            {
                selectWorkingFaceControl1.LoadData(tunnel.workingface);
                txtTunnelName.Text = tunnel.name;
                cboSupportPattern.Text = tunnel.support_pattern;
                cboLithology.SelectedItem = tunnel.lithology;
                txtDesignLength.Text = tunnel.design_length.ToString(CultureInfo.InvariantCulture);
                cboCoalOrStone.Text = tunnel.coal_or_stone;
                cboTunnelType.SelectedValue = (int)tunnel.type;
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
                workingFace = Workingface.Find(workingFace.id);
                if (workingFace.tunnels.FirstOrDefault(u => u.name == txtTunnelName.Text) != null)
                {
                    Alert.AlertMsg("该工作面下已有同名巷道！");
                    return;
                }
            }


            var tunnel = new Tunnel
            {
                name = txtTunnelName.Text,
                support_pattern = cboSupportPattern.Text,
                workingface = selectWorkingFaceControl1.SelectedWorkingFace,
                lithology = cboLithology.SelectedValue.ToString(),
                type = (TunnelTypeEnum)cboTunnelType.SelectedValue,
                coal_or_stone = cboCoalOrStone.Text,
                coal_seam = ConfigHelper.current_seam.name,
                bid = IdGenerator.NewBindingId(),
                width = 5
            };

            //设计长度
            if (txtDesignLength.Text != "")
            {
                tunnel.design_length = Convert.ToInt32(txtDesignLength.Text);
            }
            if (txtDesignArea.Text != "")
            {
                tunnel.design_area = Convert.ToInt32(txtDesignLength.Text);
            }
            //巷道信息登录

            tunnel.Save();
            Alert.AlertMsg("提交成功！");
        }

        private void UpdateTunnelInfo()
        {
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            tunnel.workingface = selectWorkingFaceControl1.SelectedWorkingFace;
            //巷道名称
            tunnel.name = txtTunnelName.Text;
            //支护方式
            tunnel.support_pattern = cboSupportPattern.Text;
            //围岩类型
            tunnel.lithology = cboLithology.SelectedValue.ToString();
            tunnel.coal_seam = ConfigHelper.current_seam.name;
            tunnel.width = 5;

            //设计长度
            if (txtDesignLength.Text != "")
            {
                tunnel.design_length = Convert.ToInt32(txtDesignLength.Text);
            }
            if (txtDesignArea.Text != "")
            {
                tunnel.design_area = Convert.ToInt32(txtDesignLength.Text);
            }
            //煤巷岩巷
            if (cboCoalOrStone.Text != "")
            {
                tunnel.coal_or_stone = cboCoalOrStone.Text;
            }

            tunnel.Save();
            Alert.AlertMsg("提交成功！");
        }


        /// <summary>
        ///     提交
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tunnel == null)
            {
                AddTunnelInfo();
            }
            else
            {
                UpdateTunnelInfo();
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
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
                Alert.AlertMsg("请选择巷道所在工作面信息");
                return false;
            }
            // 判断巷道名称是否入力
            if (String.IsNullOrWhiteSpace(txtTunnelName.Text))
            {
                txtTunnelName.BackColor = Color.Red;
                Alert.AlertMsg("巷道名称不能为空！");
                txtTunnelName.Focus();
                return false;
            }
            txtTunnelName.BackColor = Color.White;
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