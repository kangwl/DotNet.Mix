namespace XK.Model {
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole_Model {
        public UserRole_Model() {
            RoleCode = 2;
        }
        /// <summary>
        /// 主键自增长
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDesc { get; set; }
        /// <summary>
        /// 权限标识
        /// 1：超级管理员
        /// 2：普通管理员
        /// </summary>
        public int RoleCode { get; set; }
    }
}
